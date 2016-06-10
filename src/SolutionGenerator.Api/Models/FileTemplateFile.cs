// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileResource.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator
{
    using Catel;

    public class FileTemplateFile : ITemplateFile
    {
        public FileTemplateFile(string fileName, string relativeName)
        {
            Argument.IsNotNull(() => fileName);
            Argument.IsNotNull(() => relativeName);

            FileName = fileName;
            RelativeName = relativeName;
        }

        public string FileName { get; private set; }

        public string RelativeName { get; private set; }

        public override string ToString()
        {
            return RelativeName;
        }
    }
}