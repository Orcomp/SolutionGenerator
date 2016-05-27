// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateFileHelper.files.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator
{
    using System.Collections.Generic;
    using System.IO;
    using Catel;
    using Catel.Reflection;

    public static partial class TemplateFileHelper
    {
        public static List<ITemplateFile> FindFiles(string baseDirectory)
        {
            Argument.IsNotNull(() => baseDirectory);

            var fullDirectory = Path.GetFullPath(baseDirectory);
            fullDirectory = Catel.IO.Path.AppendTrailingSlash(fullDirectory);

            var files = Directory.GetFiles(fullDirectory, "*", SearchOption.AllDirectories);

            var templateFiles = new List<ITemplateFile>();

            foreach (var file in files)
            {
                templateFiles.Add(new FileTemplateFile(file, file.Substring(fullDirectory.Length)));
            }

            return templateFiles;
        }
    }
}