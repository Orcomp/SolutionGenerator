// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PluginFinder.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Extensibility
{
    using System;
    using System.Linq;
    using Catel;
    using Catel.Reflection;
    using Orc.Extensibility;

    public class PluginFinder : PluginFinderBase
    {
        private static readonly string ExpectedInterfaceName = typeof(ITemplateDefinition).FullName;

        public PluginFinder(IPluginLocationsProvider pluginLocationsProvider, IPluginInfoProvider pluginInfoProvider,
            IPluginCleanupService pluginCleanupService)
            : base(pluginLocationsProvider, pluginInfoProvider, pluginCleanupService)
        {
        }

        protected override bool ShouldIgnoreAssembly(string assemblyPath)
        {
            if (base.ShouldIgnoreAssembly(assemblyPath))
            {
                return true;
            }

            // This app
            if (assemblyPath.ContainsIgnoreCase("SolutionGenerator.exe"))
            {
                return true;
            }

            // Ef stuff
            if (assemblyPath.ContainsIgnoreCase("EntityFramework.dll") ||
                assemblyPath.ContainsIgnoreCase("EntityFramework.SqlServer.dll"))
            {
                return true;
            }

            // Git stuff
            if (assemblyPath.ContainsIgnoreCase("libgit2sharp") ||
                assemblyPath.ContainsIgnoreCase("git-") && assemblyPath.EndsWithIgnoreCase(".dll"))
            {
                return true;
            }

            // Random stuff
            if (assemblyPath.ContainsIgnoreCase("Ionic.zip") ||
                assemblyPath.ContainsIgnoreCase("CsvHelper"))
            {
                return true;
            }

            return false;
        }

        protected override bool IsPlugin(Type type)
        {
            // Note: we are in reflection only context here, we must check against the name, not the type
            if ((from iface in type.GetInterfacesEx()
                 where string.Equals(iface.FullName, ExpectedInterfaceName)
                 select iface).Any())
            {
                return true;
            }

            return false;
        }
    }
}