﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateLoader.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates
{
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Logging;
    using Catel.Reflection;

    public class TemplateLoader
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly Assembly _assembly;

        public TemplateLoader()
            : this(typeof(TemplateLoader).GetAssemblyEx())
        {
        }

        public TemplateLoader(Assembly assembly)
        {
            Argument.IsNotNull(() => assembly);

            _assembly = assembly;
        }

        public virtual async Task<Stream> LoadTemplateStreamAsync(ITemplateFile templateFile)
        {
            Argument.IsNotNull(() => templateFile);

            Log.Debug("Loading template from '{0}'", templateFile);

            var resource = templateFile as ResourceTemplateFile;
            if (resource is not null)
            {
                var memoryStream = new MemoryStream();

                TemplateFileHelper.ExtractResource(resource.Assembly, resource.ResourceName, memoryStream);

                return memoryStream;
            }

            var embeddedResource = templateFile as EmbeddedResourceTemplateFile;
            if (embeddedResource is not null)
            {
                var memoryStream = new MemoryStream();

                TemplateFileHelper.ExtractEmbeddedResource(embeddedResource.Assembly, embeddedResource.ResourceName, memoryStream);

                return memoryStream;
            }

            var file = templateFile as FileTemplateFile;
            if (file is not null)
            {
                return File.OpenRead(file.FileName);
            }

            throw Log.ErrorAndCreateException<SolutionGeneratorException>($"Template file type '{templateFile.GetType().Name}' is not yet supported");
        }

        public async Task<string> LoadTemplateAsync(ITemplateFile templateFile)
        {
            Argument.IsNotNull(() => templateFile);

            using (var stream = await LoadTemplateStreamAsync(templateFile))
            {
                stream.Position = 0L;

                using (var streamReader = new StreamReader(stream))
                {
                    return await streamReader.ReadToEndAsync();
                }
            }
        }
    }
}