// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateRenderer.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2013 Orcomp development team. All rights reserved.
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

        #region Constants
        //private const string TemplateRegex = @"[^{]({[^{].*?})";
        private readonly Regex TemplateRegex = new Regex(@"(?<=\{)[^}]*(?=\})", RegexOptions.Compiled | RegexOptions.Multiline);
        #endregion

        #region ITemplateRenderer Members
        public string RenderFile<T>(string templateContent, T model)
        {
            string text = File.ReadAllText(templateContent);

            return RenderContent(text, model);
        }

        public string RenderContent<T>(string templateContent, T model)
        {
            MatchEvaluator matchEvaluator = match =>
            {
                string propertyName = match.Value;
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

            string resolvedTemplate = TemplateRegex.Replace(templateContent, matchEvaluator);
            var replacedContent = resolvedTemplate.Replace("{", string.Empty).Replace("}", string.Empty);
            return replacedContent;
        }
        #endregion
    }
}