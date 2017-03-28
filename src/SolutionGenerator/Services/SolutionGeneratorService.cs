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
    using Orc.FileSystem;
    using Templates;

    public class SolutionGeneratorService : ISolutionGeneratorService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IDirectoryService _directoryService;
        private readonly IFileService _fileService;
        
        public SolutionGeneratorService(IDirectoryService directoryService, IFileService fileService)
        {
            Argument.IsNotNull(() => directoryService);
            Argument.IsNotNull(() => fileService);

            _directoryService = directoryService;
            _fileService = fileService;
        }

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
				var isBinary = false;

				if (resource.RelativeName.EndsWithIgnoreCase(".exe") || resource.RelativeName.EndsWithIgnoreCase(".dll"))
				{
					isBinary = true;
				}

				await ExtractResourceAndReplaceValuesAsync(templateDefinition, templateEngine, templateLoader, templates, resource, isBinary);
			}
		}

        private async Task ExtractResourceAndReplaceValuesAsync(ITemplateDefinition templateDefinition, TemplateEngine engine,
            TemplateLoader templateLoader, List<ITemplate> templates, ITemplateFile templateFile, bool isBinary)
        {
            var targetFileName = templateFile.RelativeName;

            if (targetFileName.ContainsIgnoreCase("[[ForEach "))
            {
                var keyStart = targetFileName.IndexOfIgnoreCase("[[ForEach ");
                var keyCoreStart = keyStart + "[[ForEach ".Length;
                var keyCoreEnd = targetFileName.IndexOf("]]", keyStart);
                var keyEnd = keyCoreEnd + "]]".Length;
                var keyLength = keyEnd - keyStart;
                var keyCoreLength = keyCoreEnd - keyCoreStart;
                var key = templateFile.RelativeName.Substring(keyCoreStart, keyCoreLength);

                foreach (var item in templates.GetForEachCollection(key))
                {
                    var subTemplates = new List<ITemplate>(templates);

                    // Note: important, this one must be last because it allows replacement without a prefix
                    subTemplates.Add(new CollectionItemTemplate(item));

                    var subTargetFileName = targetFileName.Remove(keyStart, keyLength).Insert(keyStart, item.ToString());

                    await ExtractResourceToTargetFileAsync(templateDefinition, engine, templateLoader, 
                        subTemplates, templateFile, isBinary, subTargetFileName);
                }
            }
            else
            {
                await ExtractResourceToTargetFileAsync(templateDefinition, engine, templateLoader,
                    templates, templateFile, isBinary, targetFileName);
            }
        }

        private async Task ExtractResourceToTargetFileAsync(ITemplateDefinition templateDefinition, TemplateEngine engine,
            TemplateLoader templateLoader, List<ITemplate> templates, ITemplateFile templateFile, bool isBinary, string targetFileName)
        {
            var templateContext = templateDefinition.TemplateContext;

            foreach (var template in templates)
            {
                targetFileName = engine.ReplaceValues(targetFileName, template);
            }

            var fullTargetFileName = Path.Combine(templateContext.Solution.Directory, targetFileName);

            var directory = Path.GetDirectoryName(fullTargetFileName);

            _directoryService.Create(directory);

            using (var sourceStream = await templateLoader.LoadTemplateStreamAsync(templateFile))
            {
                using (var targetStream = _fileService.Create(fullTargetFileName))
                {
                    if (!isBinary)
                    {
                        Log.Debug($"Extracting content for '{templateFile}'");

                        using (var streamReader = new StreamReader(sourceStream))
                        {
                            var content = await streamReader.ReadToEndAsync();

                            Log.Debug($"Replacing template values in content for '{templateFile}'");

                            foreach (var template in templates)
                            {
                                content = engine.ReplaceValues(content, template);
                            }

                            using (var streamWriter = new StreamWriter(targetStream))
                            {
                                await streamWriter.WriteAsync(content);
                            }
                        }
                    }
                    else
                    {
                        Log.Debug($"Copying binary content for '{templateFile}'");

                        await sourceStream.CopyToAsync(targetStream);
                    }
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