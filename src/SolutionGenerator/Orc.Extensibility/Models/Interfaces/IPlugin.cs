// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPlugin.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Extensibility
{
    public interface IPlugin
    {
        #region Properties
        object Instance { get; }
        IPluginInfo Info { get; }
        #endregion
    }
}