// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PluginExtensions.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Extensibility
{
    using Catel;
    using Catel.IO;

    public static class PluginExtensions
    {
        public static string GetAssemblyName(this IPluginInfo pluginInfo)
        {
            Argument.IsNotNull(() => pluginInfo);

            return Path.GetFileName(pluginInfo.Location);
        }
    }
}