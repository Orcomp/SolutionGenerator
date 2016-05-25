// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPluginServiceExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Extensibility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Catel;
    using Catel.Logging;
    using Catel.Reflection;

    public static class IPluginManagerExtensions
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public static List<IPluginInfo> FindPluginImplementations<TPlugin>(this IPluginManager pluginManager)
        {
            return FindPluginImplementations(pluginManager, typeof(TPlugin));
        }

        public static List<IPluginInfo> FindPluginImplementations(this IPluginManager pluginManager, Type interfaceType)
        {
            Argument.IsNotNull(() => interfaceType);

            var interfaceTypeFullName = interfaceType.FullName;
            var plugins = new List<IPluginInfo>();

            Log.Debug("Searching for plugins that implement '{0}'", interfaceType.FullName);

            foreach (var plugin in pluginManager.GetPlugins())
            {
                // Note: look at reflection-only interfaces
                var isValidPlugin = (from iface in plugin.ReflectionOnlyType.GetInterfacesEx()
                                     where string.Equals(interfaceTypeFullName, iface.FullName)
                                     select iface).Any();

                if (isValidPlugin)
                {
                    plugins.Add(plugin);
                }
            }

            return plugins;
        }
    }
}