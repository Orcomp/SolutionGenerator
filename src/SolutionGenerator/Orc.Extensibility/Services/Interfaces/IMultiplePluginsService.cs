// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMultiplePluginsService.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Extensibility
{
    using System.Collections.Generic;

    public interface IMultiplePluginsService : IPluginService
    {
        IEnumerable<IPlugin> ConfigureAndLoadPlugins(params string[] requestedPlugins);
    }
}