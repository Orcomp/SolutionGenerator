// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPluginLocationsProvider.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Extensibility
{
    using System.Collections.Generic;

    public interface IPluginLocationsProvider
    {
        #region Methods
        IEnumerable<string> GetPluginDirectories();
        #endregion
    }
}