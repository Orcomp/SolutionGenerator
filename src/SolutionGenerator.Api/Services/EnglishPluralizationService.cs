namespace SolutionGenerator.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Catel;

    public class EnglishPluralizationService : IPluralizationService
    {
        private readonly Dictionary<string, string> _singleToPlural = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, string> _pluralToSingle = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public EnglishPluralizationService()
        {
            AddSpecialCase("Foot", "Feet");
            AddSpecialCase("Man", "Men");
            AddSpecialCase("Person", "People");
            AddSpecialCase("Woman", "Women");
        }

        protected void AddSpecialCase(string single, string plural)
        {
            _singleToPlural[single] = plural;
            _pluralToSingle[plural] = single;
        }

        public bool IsPlural(string text)
        {
            return !IsSingular(text);
        }

        public bool IsSingular(string text)
        {
            if (_singleToPlural.ContainsKey(text))
            {
                return true;
            }

            if (_pluralToSingle.ContainsKey(text))
            {
                return false;
            }

            if (text.EndsWithAnyIgnoreCase("ies", "s"))
            {
                return false;
            }

            return true;
        }

        public string ToPlural(string text)
        {
            if (IsPlural(text))
            {
                return text;
            }

            if (_singleToPlural.TryGetValue(text, out var plural))
            {
                return plural;
            }

            if (ReplaceSpecialEndings(text, new List<KeyValuePair<string, string>>(new[] {
                new KeyValuePair<string, string>("y", "ies"),
                new KeyValuePair<string, string>("an", "en") }),
                out plural))
            {
                return plural;
            }

            return $"{text}s";
        }

        public string ToSingular(string text)
        {
            if (IsSingular(text))
            {
                return text;
            }

            if (_pluralToSingle.TryGetValue(text, out var singular))
            {
                return singular;
            }

            if (ReplaceSpecialEndings(text, new List<KeyValuePair<string, string>>(new[] {
                new KeyValuePair<string, string>("en", "an"),
                new KeyValuePair<string, string>("ies", "y"),
                new KeyValuePair<string, string>("s", string.Empty) }),
                out singular))
            {
                return singular;
            }

            return text;
        }

        protected bool ReplaceSpecialEndings(string text, List<KeyValuePair<string, string>> endings, out string finalText)
        {
            finalText = text;

            foreach (var ending in endings)
            {
                if (text.EndsWithIgnoreCase(ending.Key))
                {
                    finalText = $"{text.Substring(0, text.Length - ending.Key.Length)}{ending.Value}";
                    return true;
                }
            }

            return false;
        }
    }
}
