// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPluginService.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Extensibility
{
    using System;

    public interface IPluginService
    {
        event EventHandler<PluginEventArgs> PluginLoadingFailed;

        event EventHandler<PluginEventArgs> PluginLoaded;
    }
}