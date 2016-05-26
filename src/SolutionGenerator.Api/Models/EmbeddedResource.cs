// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmbeddedResource.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator
{
    using System.Reflection;
    using Catel;

    public class EmbeddedResource
    {
        public EmbeddedResource(Assembly assembly, string resourceName, string relativeResourceName)
        {
            Argument.IsNotNull(() => assembly);
            Argument.IsNotNull(() => resourceName);
            Argument.IsNotNull(() => relativeResourceName);

            Assembly = assembly;
            ResourceName = resourceName;
            RelativeResourceName = relativeResourceName;
        }

        public Assembly Assembly { get; private set; }

        public string ResourceName { get; private set; }

        public string RelativeResourceName { get; private set; }

        public override string ToString()
        {
            return RelativeResourceName;
        }
    }
}