// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPluginCleanupService.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Extensibility
{
    public interface IPluginCleanupService
    {
        #region Methods
        bool IsCleanupRequired(string directory);
        void Cleanup(string directory);
        #endregion
    }
}