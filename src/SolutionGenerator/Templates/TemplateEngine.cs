// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateEngine.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates
{
    using System;
    using System.Collections.Generic;
    using Catel;
    using Catel.Logging;
    using Catel.Reflection;

    public class TemplateEngine
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private const string TemplateKeyEnd = "]]";
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

            Log.Debug("Filling template with template data");

            var templateModelType = templateModel.GetType();

            var templateContainer = templateModelType.Name;
            if (templateContainer.EndsWith("Template"))
            {
                templateContainer = templateContainer.Substring(0, templateContainer.Length - "Template".Length);
            }

            var keyPrefix = $"[[{templateContainer}.";

            var index = templateContent.IndexOfIgnoreCase(keyPrefix);

            // TODO: Optimize by using regular expressions

            while (index >= 0)
            {
                // Search for the whole key
                var keyStart = index + keyPrefix.Length;
                var keyEnd = templateContent.IndexOfAny(new [] { '|', ']' }, keyStart);
                if (keyEnd < 0)
                {
                    throw Log.ErrorAndCreateException<SolutionGeneratorException>($"Can't find end of key prefix '{keyPrefix}' at position '{index}'");
                }

                var key = templateContent.Substring(keyStart, keyEnd - keyStart);

                Log.Debug($"Found template key '{templateContainer}.{key}' at position '{index}'");

                var value = templateModel.GetValue(key);

                // Retrieve modifiers that are located after the key
                var endPos = templateContent.IndexOf(TemplateKeyEnd, index);
                if (endPos < 0)
                {
                    throw Log.ErrorAndCreateException<SolutionGeneratorException>($"Can't find end of key '{key}' at position '{index}'");
                }

                endPos += TemplateKeyEnd.Length;

                var modifiersString = templateContent.Substring(keyEnd, endPos - keyEnd).Replace(".", string.Empty).Replace(TemplateKeyEnd, string.Empty);
                var modifiers = modifiersString.Split(new[] { ModifierSeparator }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var modifier in modifiers)
                {
                    value = ApplyModifier(value, modifier);
                }

                // Remove the key from the content
                var length = endPos - index;
                templateContent = templateContent.Remove(index, length);

                // Insert new value
                templateContent = templateContent.Insert(index, value);

                Log.Debug($"Replaced template key '{templateContainer}.{key}' at position '{index}'");

                index = templateContent.IndexOfIgnoreCase(keyPrefix);
            }

            return templateContent;
        }

        protected virtual string ApplyModifier(string value, string modifier)
        {
            modifier = modifier.Trim(ModifierSeparator);

            if (modifier.EqualsIgnoreCase(Modifiers.Lowercase))
            {
                value = value.ToLowerInvariant();
            }
            else if (modifier.EqualsIgnoreCase(Modifiers.Uppercase))
            {
                value = value.ToUpperInvariant();
            }
            else
            {
                throw Log.ErrorAndCreateException<SolutionGeneratorException>($"Modifier '{modifier}' is not supported");
            }

            return value;
        }
    }
}