// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateRenderer.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Services
{
    using System.IO;
    using System.Text.RegularExpressions;
    using Catel.Logging;

    public class TemplateRenderer : ITemplateRenderer
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        //private const string TemplateRegex = @"[^{]({[^{].*?})";

        private readonly Regex TemplateRegex = new Regex(@"(?<=\{)[^}]*(?=\})", RegexOptions.Compiled | RegexOptions.Multiline);

        public string RenderFile<T>(string templateContent, T model)
        {
            var text = File.ReadAllText(templateContent);

            return RenderContent(text, model);
        }

        public string RenderContent<T>(string templateContent, T model)
        {
            MatchEvaluator matchEvaluator = match =>
            {
                var propertyName = match.Value;
                object propertyValue = propertyName;

                var property = model.GetType().GetProperty(propertyName);
                if (property == null)
                {
                    Log.Warning("Cannot replace token '{0}' because the property is not found on the model", propertyName);
                }
                else
                {
                    propertyValue = property.GetValue(model);
                }

                return propertyValue.ToString();
            };

            var resolvedTemplate = TemplateRegex.Replace(templateContent, matchEvaluator);
            var replacedContent = resolvedTemplate.Replace("{", string.Empty).Replace("}", string.Empty);
            return replacedContent;
        }
    }
}