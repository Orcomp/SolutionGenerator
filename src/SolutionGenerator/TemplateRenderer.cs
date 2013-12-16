namespace SolutionGenerator
{
    using System.IO;
    using System.Text.RegularExpressions;

    public class TemplateRenderer
    {
        private static string _templateRegex = @"[^{]({[^{].*?})";

        public static string Render<T>(string templateContent, T model)
        {
            string text = File.ReadAllText(templateContent);

            return Regex.Replace(text, _templateRegex, match =>
                {
                    var indexOfFirst = match.Value.IndexOf('{');
                    var prefix = indexOfFirst > 0 ? match.Value.Substring(0, 1) : string.Empty;
                    var value = match.Value.Substring(indexOfFirst);
                    string replace = value.Replace("{", string.Empty).Replace("}", string.Empty);
                    return prefix + model.GetType().GetProperty(replace).GetValue(model).ToString();
                }).Replace("{{", "{").Replace("}}", "}");
        }
        
        public static string RenderAndRenderContent<T>(string templateContent, T model)
        {
            string text = File.ReadAllText(templateContent);

            MatchEvaluator matchEvaluator = match =>
            {
                var indexOfFirst = match.Value.IndexOf('{');
                var prefix = indexOfFirst > 0 ? match.Value.Substring(0, 1) : string.Empty;
                var value = match.Value.Substring(indexOfFirst);
                string replace = value.Replace("{", string.Empty).Replace("}", string.Empty);
                return prefix + model.GetType().GetProperty(replace).GetValue(model).ToString();
            };

            string resolvedTemplate = Regex.Replace(text, _templateRegex, matchEvaluator);
            return Regex.Replace(resolvedTemplate, _templateRegex, matchEvaluator).Replace("{{", "{").Replace("}}", "}");
        }
    }
}