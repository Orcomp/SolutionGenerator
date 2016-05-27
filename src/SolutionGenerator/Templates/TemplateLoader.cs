// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateLoader.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates
{
    using System.IO;
    using System.Reflection;
    using Catel;
    using Catel.Logging;
    using Catel.Reflection;
    using Models;

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

        public string LoadTemplate(ITemplateFile templateFile)
        {
            Argument.IsNotNull(() => templateFile);

            Log.Debug("Loading template from '{0}'", templateFile);

            var resource = templateFile as ResourceTemplateFile;
            if (resource != null)
            {
                var content = SolutionGenerator.ResourceHelper.ExtractResource(_assembly, resource.ResourceName);
                return content;
            }

            var embeddedResource = templateFile as EmbeddedResourceTemplateFile;
            if (embeddedResource != null)
            {
                var content = SolutionGenerator.ResourceHelper.ExtractEmbeddedResource(_assembly, embeddedResource.ResourceName);
                return content;
            }

            var file = templateFile as FileTemplateFile;
            if (file != null)
            {
                var content = File.ReadAllText(file.FileName);
                return content;
            }

            throw Log.ErrorAndCreateException<SolutionGeneratorException>($"Template file type '{templateFile.GetType().Name}' is not yet supported");
        }
    }
}