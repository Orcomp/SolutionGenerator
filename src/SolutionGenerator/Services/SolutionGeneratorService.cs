// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SolutionGeneratorService.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Logging;
    using Catel.Reflection;
    using Templates;

    public class SolutionGeneratorService : ISolutionGeneratorService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public async Task GenerateAsync(ITemplateDefinition templateDefinition)
        {
            Argument.IsNotNull(() => templateDefinition);

            var templateLoader = new TemplateLoader(templateDefinition.GetType().Assembly);
            var templateEngine = new TemplateEngine(templateLoader);

            var templates = GetTemplates(templateDefinition);
            var resources = templateDefinition.GetTemplateFiles();

            Log.Debug($"Found '{resources.Count}' resources");

            // TODO: Consider multithreading
            foreach (var resource in resources)
            {
                await ExtractResourceAndReplaceValuesAsync(templateDefinition, templateEngine, templateLoader, templates, resource);
            }
        }

        private async Task ExtractResourceAndReplaceValuesAsync(ITemplateDefinition templateDefinition, TemplateEngine engine, 
            TemplateLoader templateLoader, List<ITemplate> templates, ITemplateFile templateFile)
        {
            var templateContext = templateDefinition.TemplateContext;

            Log.Debug($"Determining final name for '{templateFile}'");

            var targetFileName = templateFile.RelativeName;

            foreach (var template in templates)
            {
                targetFileName = engine.ReplaceValues(targetFileName, template);
            }

            Log.Debug($"Extracting content for '{templateFile}'");

            var content = templateLoader.LoadTemplate(templateFile);

            Log.Debug($"Replacing template values in content for '{templateFile}'");

            foreach (var template in templates)
            {
                content = engine.ReplaceValues(content, template);
            }

            var fullTargetFileName = Path.Combine(templateContext.Solution.Directory, targetFileName);

            var directory = Path.GetDirectoryName(fullTargetFileName);

            Directory.CreateDirectory(directory);
        
            // TODO: Check if binary streams don't break by this code
            using (var fileStream = File.Create(fullTargetFileName))
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    await streamWriter.WriteAsync(content);
                }
            }

            Log.Debug($"Extracted resource '{templateFile}' to '{targetFileName}'");
        }

        private List<ITemplate> GetTemplates(ITemplateDefinition templateDefinition)
        {
            var templateContext = templateDefinition.TemplateContext;

            Log.Debug($"Finding templates on context '{templateContext.GetType().Name}'");

            var templates = new List<ITemplate>();

            var propertiesImplementingTemplate = (from property in templateContext.GetType().GetPropertiesEx()
                                                  where property.PropertyType.ImplementsInterfaceEx<ITemplate>()
                                                  select property);

            foreach (var property in propertiesImplementingTemplate)
            {
                var template = property.GetValue(templateContext) as ITemplate;
                if (template != null)
                {
                    templates.Add(template);
                }
            }

            Log.Debug($"Found '{templates.Count}' templates on context '{templateContext.GetType().Name}'");

            return templates;
        }
    }
}