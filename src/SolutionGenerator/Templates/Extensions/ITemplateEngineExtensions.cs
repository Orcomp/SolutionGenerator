// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITemplateEngineExtensions.cs" company="WildGums">
//   Copyright (c) 2012 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ITemplateEngineExtensions
    {
        public static string ReplaceValues(this TemplateEngine templateEngine, string templateContent, params ITemplate[] templates)
        {
            return ReplaceValues(templateEngine, templateContent, templates.AsEnumerable());
        }

        public static string ReplaceValues(this TemplateEngine templateEngine, string templateContent, IEnumerable<ITemplate> templates)
        {
            var content = templateContent;

            foreach (var template in templates)
            {
                content = templateEngine.ReplaceValues(content, template);
            }

            return content;
        }
    }
}