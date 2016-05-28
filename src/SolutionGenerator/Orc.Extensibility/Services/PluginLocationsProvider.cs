// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PluginLocationsProvider.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Extensibility
{
    using System;
    using System.Collections.Generic;
    using Catel.IO;
    using Catel.Reflection;

    public class PluginLocationsProvider : IPluginLocationsProvider
    {
        public virtual IEnumerable<string> GetPluginDirectories()
        {
            var directories = new List<string>();

            directories.Add(Path.Combine(Path.GetApplicationDataDirectory(), "plugins"));
            directories.Add(Environment.CurrentDirectory);
            directories.Add(AssemblyHelper.GetEntryAssembly().GetDirectory());

            return directories;
        }
    }
}