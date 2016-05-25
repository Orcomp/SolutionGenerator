// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PluginInfoProvider.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Extensibility
{
    using System;

    public class PluginInfoProvider : IPluginInfoProvider
    {
        public PluginInfoProvider()
        {
        }

        public IPluginInfo GetPluginInfo(Type pluginType)
        {
            return new PluginInfo(pluginType);
        }
    }
}