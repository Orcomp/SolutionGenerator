namespace SolutionGenerator
{
    using System.IO;
    using System.Text.RegularExpressions;

    public class TemplateRenderer
    {
        public static string Render<T>(string templateContent, T model)
        {
            string text = File.ReadAllText(templateContent);

            return Regex.Replace(text, @"\{.*?\}", match =>
                {
                    string replace = match.Value.Replace("{", string.Empty).Replace("}", string.Empty);
                    return model.GetType().GetProperty(replace).GetValue(model).ToString();
                });
        }

        
        public static string RenderAndRenderContent<T>(string templateContent, T model)
        {
            string text = File.ReadAllText(templateContent);

            MatchEvaluator matchEvaluator = match =>
                {
                    string replace = match.Value.Replace("{", string.Empty).Replace("}", string.Empty);
                    return model.GetType().GetProperty(replace).GetValue(model).ToString();
                };

            string resolvedTemplate = Regex.Replace(text, @"\{.*?\}", matchEvaluator);
            return Regex.Replace(resolvedTemplate, @"\{.*?\}", matchEvaluator);
        }
    }
}