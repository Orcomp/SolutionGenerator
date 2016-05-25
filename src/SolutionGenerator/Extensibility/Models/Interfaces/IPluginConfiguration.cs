// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPluginConfiguration.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Extensibility
{
    public interface IPluginConfiguration
    {
        #region Properties
        string PackagesDirectory { get; set; }

        string FeedUrl { get; set; }
        string FeedName { get; set; }
        #endregion
    }
}