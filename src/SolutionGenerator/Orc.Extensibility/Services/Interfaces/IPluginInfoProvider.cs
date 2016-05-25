// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPluginInfoProvider.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Extensibility
{
    using System;

    public interface IPluginInfoProvider
    {
        #region Methods
        IPluginInfo GetPluginInfo(Type pluginType);
        #endregion
    }
}