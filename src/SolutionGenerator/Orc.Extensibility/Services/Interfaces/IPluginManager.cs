// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPluginManager.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Extensibility
{
    using System.Collections.Generic;

    public interface IPluginManager
    {
        #region Methods
        IEnumerable<IPluginInfo> GetPlugins(bool forceRefresh = false);
        #endregion
    }
}