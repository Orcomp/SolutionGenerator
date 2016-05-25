// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPluginFinder.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Extensibility
{
    using System.Collections.Generic;

    public interface IPluginFinder
    {
        #region Methods
        IEnumerable<IPluginInfo> FindPlugins();
        #endregion
    }
}