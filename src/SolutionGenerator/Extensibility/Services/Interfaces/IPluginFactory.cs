// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPluginFactory.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Extensibility
{
    public interface IPluginFactory
    {
        #region Methods
        object CreatePlugin(IPluginInfo pluginInfo);
        #endregion
    }
}