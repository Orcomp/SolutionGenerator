// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateEngine.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Catel;
    using Catel.Logging;

    public class TemplateEngine
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private const string TemplateKeyStart = "[[";
        private const string TemplateKeyEnd = "]]";
        private const string TemplateBeginForeach = "BeginForeach ";
        private const string TemplateEndForeach = "EndForeach";
        private static readonly string TemplateBeginForeachKey = $"{TemplateKeyStart}{TemplateBeginForeach}";
        private static readonly string TemplateEndForeachKey = $"{TemplateKeyStart}{TemplateEndForeach}{TemplateKeyEnd}";

        private const string TemplateBeginIf = "BeginIf ";
        private const string TemplateEndIf = "EndIf";

        private static readonly string TemplateBeginIfKey = $"{TemplateKeyStart}{TemplateBeginIf}";
        private static readonly string TemplateEndIfKey = $"{TemplateKeyStart}{TemplateEndIf}{TemplateKeyEnd}";

        private static readonly List<string> KnownReservedPrefixes = new List<string>(new[] 
        {
            TemplateBeginForeach, TemplateEndForeach,
            TemplateBeginIf , TemplateEndIf
        });

        private const char ModifierSeparator = '|';

        private readonly TemplateLoader _templateLoader;

        public TemplateEngine(TemplateLoader templateLoader)
        {
            Argument.IsNotNull(() => templateLoader);

            _templateLoader = templateLoader;
        }

        public string ReplaceValues(string templateContent, ITemplate templateModel)
        {
            Argument.IsNotNullOrWhitespace(() => templateContent);
            Argument.IsNotNull(() => templateModel);

            var possibleDataPrefixes = templateModel.GetPossibleDataPrefixes();

            var availableKeys = templateModel.GetKeys();

            var allPossiblePrefixes = new List<string>();
            allPossiblePrefixes.Add(TemplateBeginForeachKey);
            allPossiblePrefixes.Add(TemplateBeginIfKey);
            allPossiblePrefixes.AddRange(possibleDataPrefixes.Select(x => $"[[{x}"));

            foreach (var possiblePrefix in allPossiblePrefixes)
            {
                var keyPrefix = possiblePrefix;

                var index = templateContent.IndexOfIgnoreCase(keyPrefix);

                // TODO: Optimize by using regular expressions

                while (index >= 0)
                {
                    // Search for the whole key
                    var keyStart = index + keyPrefix.Length;
                    var keyEnd = templateContent.IndexOfAny(new[] { '|', ']' }, keyStart);
                    if (keyEnd < 0)
                    {
                        throw Log.ErrorAndCreateException<SolutionGeneratorException>($"Can't find end of key prefix '{keyPrefix}' at position '{index}'");
                    }

                    var key = templateContent.Substring(keyStart, keyEnd - keyStart).Trim();

                    Log.Debug($"Found template key '{keyPrefix}{key}' at position '{index}'");

                    // Retrieve modifiers that are located after the key
                    var endPos = templateContent.IndexOf(TemplateKeyEnd, index);
                    if (endPos < 0)
                    {
                        throw Log.ErrorAndCreateException<SolutionGeneratorException>($"Can't find end of key '{key}' at position '{index}'");
                    }

                    endPos += TemplateKeyEnd.Length;

                    if (keyPrefix.EqualsIgnoreCase(TemplateBeginForeachKey))
                    {
                        var collection = (new[] { templateModel }).GetForEachCollection(key);
                        if (collection == null)
                        {
                            // Foreach is not for this template, ignore
                            index = FindNextKeyIndex(templateContent, keyPrefix, index);
                            continue;
                        }

                        // Search for the end
                        var foreachEnd = FindEndForForEach(templateContent, endPos);
                        if (foreachEnd < 0)
                        {
                            throw Log.ErrorAndCreateException<SolutionGeneratorException>($"Can't find end of foreach key '{key}' at position '{index}'");
                        }

                        var foreachTemplate = templateContent.Substring(endPos, foreachEnd - endPos);

                        // Strip the template content
                        foreachEnd += TemplateEndForeachKey.Length;
                        var partToReplaceLength = foreachEnd - index;
                        templateContent = templateContent.Remove(index, partToReplaceLength);

                        var collectionArray = collection.Cast<object>().ToArray();
                        for (var i = collectionArray.Length - 1; i >= 0; i--)
                        {
                            var collectionItem = collectionArray[i];

                            var itemContent = GenerateForeachContent(collectionItem, foreachTemplate);
                            if (itemContent != null)
                            {
                                templateContent = templateContent.Insert(index, itemContent);
                            }
                        }

                        index = index - 1;
                    }
                    else if (keyPrefix.EqualsIgnoreCase(TemplateBeginIfKey))
                    {
                        var ifExpressionValue = (new[] { templateModel }).ResolveIfExpression(key);
                        if (!ifExpressionValue.HasValue)
                        {
                            // Foreach is not for this template, ignore
                            index = FindNextKeyIndex(templateContent, keyPrefix, index);
                            continue;
                        }

                        var ifEnd = FindEndForIf(templateContent, endPos);
                        if (ifEnd < 0)
                        {
                            throw Log.ErrorAndCreateException<SolutionGeneratorException>($"Can't find end of if key '{key}' at position '{index}'");
                        }

                        var value = string.Empty;

                        if (ifExpressionValue ?? false)
                        {
                            var ifTemplate = templateContent.Substring(endPos, ifEnd - endPos);
                            value = ReplaceValues(ifTemplate, templateModel);
                        }

                        // Strip the template content
                        ifEnd += TemplateEndIfKey.Length;
                        var partToReplaceLength = ifEnd - index;
                        templateContent = templateContent.Remove(index, partToReplaceLength);

                        if (!string.IsNullOrEmpty(value))
                        {
                            templateContent = templateContent.Insert(index, value);
                        }

                        index = index - 1;
                    }
                    // TODO: Add other special cases here if necessary
                    else
                    {
                        // Regular replacement, but make sure to ignore known keywords
                        var canHandle = !KnownReservedPrefixes.Any(x => key.StartsWithIgnoreCase(x));
                        if (!allPossiblePrefixes.Any(x => x.EqualsIgnoreCase(keyPrefix)) ||
                            !availableKeys.Any(x => x.EqualsIgnoreCase(key)))
                        {
                            // Not a value for this template
                            canHandle = false;
                        }

                        if (!canHandle)
                        {
                            index = FindNextKeyIndex(templateContent, keyPrefix, index);
                            continue;
                        }

                        var value = templateModel.GetValue(key);
                        var modifiersString = templateContent.Substring(keyEnd, endPos - keyEnd).Replace(".", string.Empty).Replace(TemplateKeyEnd, string.Empty);

                        var partToReplaceStart = index;
                        var partToReplaceLength = endPos - index;

                        var contentToReplaceWith = ApplyModifiers(value, modifiersString);

                        // Replace template content by value
                        templateContent = templateContent.Remove(partToReplaceStart, partToReplaceLength);
                        templateContent = templateContent.Insert(partToReplaceStart, contentToReplaceWith);

                        index = partToReplaceStart - 1;
                    }

                    Log.Debug($"Replaced template key '{keyPrefix}{key}' at position '{index}'");

                    index = FindNextKeyIndex(templateContent, keyPrefix, index);
                }
            }

            return templateContent;
        }

        private int FindEndForForEach(string templateContent, int startIndex)
        {
            return FindEndForKeyword(templateContent, startIndex, TemplateBeginForeachKey, TemplateEndForeachKey);
        }

        private int FindEndForIf(string templateContent, int startIndex)
        {
            return FindEndForKeyword(templateContent, startIndex, TemplateBeginIfKey, TemplateEndIfKey);
        }

        private int FindEndForKeyword(string templateContent, int startIndex, string keywordStart, string keywordEnd)
        {
            // Note: this is not a very optimized way of searching for strings, but
            // didn't want to spend too much time on migrating to regex

            var counter = 0;

            for (var i = startIndex; i < templateContent.Length; i++)
            {
                // Get a temporary subset of data
                var data = templateContent.Substring(i, Math.Min(25, templateContent.Length - i - 1));

                if (data.StartsWithIgnoreCase(keywordStart))
                {
                    counter++;
                }
                else if (data.StartsWithIgnoreCase(keywordEnd))
                {
                    if (counter == 0)
                    {
                        return i;
                    }

                    counter--;
                }
            }

            return -1;
        }

        private int FindNextKeyIndex(string content, string keyPrefix, int currentIndex)
        {
            return content.IndexOf(keyPrefix, currentIndex + 1, StringComparison.OrdinalIgnoreCase);
        }

        private string GenerateForeachContent(object scope, string foreachTemplate)
        {
            var template = new CollectionItemTemplate(scope);

            var value = ReplaceValues(foreachTemplate, template);
            return value;
        }

        private string ApplyModifiers(string value, string modifiersString)
        {
            var modifiers = modifiersString.Split(new[] { ModifierSeparator }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var modifier in modifiers)
            {
                value = ApplyModifier(value, modifier);
            }

            return value;
        }

        protected virtual string ApplyModifier(string value, string modifier)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            modifier = modifier.Trim(ModifierSeparator);

            if (modifier.EqualsIgnoreCase(Modifiers.Lowercase))
            {
                value = value.ToLowerInvariant();
            }
            else if (modifier.EqualsIgnoreCase(Modifiers.Uppercase))
            {
                value = value.ToUpperInvariant();
            }
            else if (modifier.EqualsIgnoreCase(Modifiers.Camelcase))
            {
                var character = value[0];
                value = value.Remove(0, 1);
                value = value.Insert(0, char.ToLower(character).ToString());
            }
            else
            {
                throw Log.ErrorAndCreateException<SolutionGeneratorException>($"Modifier '{modifier}' is not supported");
            }

            return value;
        }
    }
}