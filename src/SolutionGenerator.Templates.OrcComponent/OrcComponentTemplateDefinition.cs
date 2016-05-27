// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrcComponentTemplate.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates.OrcComponent
{
    using System.Collections.Generic;
    using System.IO;
    using System.Windows;
    using Catel.Reflection;
    using Views;

    public class OrcComponentTemplateDefinition : TemplateDefinitionBase<OrcComponentTemplateContext>
    {
        public override List<ITemplateFile> GetTemplateFiles()
        {
            var assemblyDirectory = GetType().Assembly.GetDirectory();
            var filesDirectory = Path.Combine(assemblyDirectory, "Files");

            var files = TemplateFileHelper.FindFiles(filesDirectory);
            return files;
        }

        public override FrameworkElement GetView()
        {
            return new SettingsView();
        }
    }
}