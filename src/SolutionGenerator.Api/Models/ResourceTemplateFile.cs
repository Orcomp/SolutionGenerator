// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceTemplateFile.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator
{
    using System.Reflection;
    using Catel;

    public class ResourceTemplateFile : ITemplateFile
    {
        public ResourceTemplateFile(Assembly assembly, string resourceName, string relativeName)
        {
            Argument.IsNotNull(() => assembly);
            Argument.IsNotNull(() => resourceName);
            Argument.IsNotNull(() => relativeName);

            Assembly = assembly;
            ResourceName = resourceName;
            RelativeName = relativeName;
        }

        public Assembly Assembly { get; private set; }

        public string ResourceName { get; private set; }

        public string RelativeName { get; private set; }

        public override string ToString()
        {
            return RelativeName;
        }
    }
}