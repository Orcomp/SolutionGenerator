// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISinglePluginService.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Extensibility
{
    public interface ISinglePluginService : IPluginService
    {
        #region Methods
        IPlugin ConfigureAndLoadPlugin(string expectedPlugin, string defaultPlugin);
        #endregion
    }
}