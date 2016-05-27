// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZippedFileTemplate.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator
{
    using Catel;

    public class ZippedFileTemplate : ITemplateFile
    {
        public ZippedFileTemplate(string zipFile, string fileName, string relativeName)
        {
            Argument.IsNotNull(() => zipFile);
            Argument.IsNotNull(() => fileName);
            Argument.IsNotNull(() => relativeName);

            ZipFile = zipFile;
            FileName = fileName;
            RelativeName = relativeName;
        }

        public string ZipFile { get; private set; }

        public string FileName { get; private set; }

        public string RelativeName { get; private set; }
    }
}