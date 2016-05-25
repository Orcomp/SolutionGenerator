// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPluginInfo.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Extensibility
{
    using System;

    public interface IPluginInfo
    {
        #region Properties
        string Name { get; set; }
        string Description { get; set; }
        string Version { get; set; }
        string Company { get; set; }
        string Customer { get; set; }

        string Location { get; }
        string FullTypeName { get; }
        Type ReflectionOnlyType { get; }
        #endregion
    }
}