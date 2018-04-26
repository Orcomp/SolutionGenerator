// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITemplateExtensions.cs" company="WildGums">
//   Copyright (c) 2012 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Catel;
    using Catel.Reflection;

    public static class ITemplateExtensions
    {
        public static List<string> GetPossibleDataPrefixes(this ITemplate template)
        {
            var possibleDataPrefixes = new List<string>();

            var templateModelType = template.GetType();

            var templateContainer = templateModelType.Name;
            if (templateContainer.EndsWith("Template"))
            {
                templateContainer = templateContainer.Substring(0, templateContainer.Length - "Template".Length);
            }

            if (template is CollectionItemTemplate)
            {
                // Direct scope (scope is collection item)
                possibleDataPrefixes.Add(string.Empty);
            }

            possibleDataPrefixes.Add($"{templateContainer}.");

            var abbreviationAttributes = templateModelType.GetCustomAttributes<AbbreviationAttribute>();
            foreach (var abbreviationAttribute in abbreviationAttributes)
            {
                possibleDataPrefixes.Add($"{abbreviationAttribute.Abbreviation}.");
            }

            return possibleDataPrefixes;
        }

        public static ICollection GetForEachCollection(this IEnumerable<ITemplate> templates, string key)
        {
            ICollection collection = null;

            key = CleanTemplateKey(key);

            foreach (var template in templates)
            {
                var possibleDataPrefixes = GetPossibleDataPrefixes(template);
                var usedPrefix = (from possibleDataPrefix in possibleDataPrefixes
                                  where key.StartsWithIgnoreCase(possibleDataPrefix)
                                  select possibleDataPrefix).FirstOrDefault();
                if (usedPrefix == null && possibleDataPrefixes.Contains(string.Empty))
                {
                    // Assume string.Empty
                    usedPrefix = string.Empty;
                }

                if (usedPrefix != null)
                {
                    if (usedPrefix.Length > 0)
                    {
                        key = key.Replace(usedPrefix, string.Empty);
                    }

                    collection = template.GetCollectionValue(key);
                    if (collection != null)
                    {
                        break;
                    }
                }
            }

            return collection;
        }

        public static bool? ResolveIfExpression(this IEnumerable<ITemplate> templates, string key)
        {
            key = CleanTemplateKey(key);

            foreach (var template in templates)
            {
                var availableKeys = template.GetKeys();

                var possibleDataPrefixes = GetPossibleDataPrefixes(template);
                var usedPrefix = (from possibleDataPrefix in possibleDataPrefixes
                                  where key.StartsWithIgnoreCase(possibleDataPrefix)
                                  select possibleDataPrefix).FirstOrDefault();
                if (usedPrefix != null)
                {
                    if (usedPrefix.Length > 0)
                    {
                        key = key.Replace(usedPrefix, string.Empty);
                    }

                    if (!availableKeys.Any(x => x.EqualsIgnoreCase(key)))
                    {
                        continue;
                    }

                    var ifExpression = template.GetValue(key);

                    var resolvedValue = StringToObjectHelper.ToBool(ifExpression);
                    return resolvedValue;
                }
            }

            return null;
        }

        public static string CleanTemplateKey(this string key)
        {
            key = key.Trim(' ', '[', ']');

            if (key.StartsWithIgnoreCase("ForEach "))
            {
                key = key.Remove(0, "ForEach ".Length);
            }

            if (key.StartsWithIgnoreCase("BeginForEach "))
            {
                key = key.Remove(0, "BeginForEach ".Length);
            }

            if (key.StartsWithIgnoreCase("BeginIf "))
            {
                key = key.Remove(0, "BeginIf ".Length);
            }

            return key;
        }
    }
}