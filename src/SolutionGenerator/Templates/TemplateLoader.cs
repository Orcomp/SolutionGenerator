// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateLoader.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates
{
    using System.Reflection;
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

        public string LoadTemplate(string templateResourceName)
        {
            Argument.IsNotNullOrWhitespace(() => templateResourceName);

            Log.Debug("Loading template from '{0}'", templateResourceName);

            var content = SolutionGenerator.ResourceHelper.ExtractEmbeddedResource(_assembly, templateResourceName);
            return content;
        }
    }
}