namespace SolutionGenerator
{
    using System;
    using System.Linq;
    using System.Text;
    using Catel;

    public static partial class StringExtensions
    {
        public static string ToCamelCase(this string input)
        {
            Argument.IsNotNullOrWhitespace(() => input);

            // Remove all non alphanumeric characters. Leave white spaces.
            var cleanedUpSourceString = new StringBuilder();

            foreach (var c in input)
            {
                if (char.IsLetterOrDigit(c))
                {
                    cleanedUpSourceString.Append(c);
                }
                else
                {
                    cleanedUpSourceString.Append(" ");
                }
            }

            var words = cleanedUpSourceString.ToString().Split(new [] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();

            foreach (var word in words)
            {
                if (string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(word))
                {
                    continue;
                }

                var firstLetter = word.Substring(0, 1).ToUpper();
                var rest = word.Substring(1, word.Length - 1);
                sb.Append(firstLetter + rest.ToLower());
            }

            return sb.ToString();
        }
    }
}
