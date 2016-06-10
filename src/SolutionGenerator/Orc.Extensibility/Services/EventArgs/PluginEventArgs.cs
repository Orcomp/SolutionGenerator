// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PluginEventArgs.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Extensibility
{
    using System;

    public class PluginEventArgs : EventArgs
    {
        public PluginEventArgs(IPluginInfo pluginInfo, string messageTitle, string messageDetails)
            : this(pluginInfo.Name, messageTitle, messageDetails)
        {
            PluginInfo = pluginInfo;
        }

        public PluginEventArgs(string pluginName, string messageTitle, string messageDetails)
        {
            PluginName = pluginName;
            MessageTitle = messageTitle;
            MessageDetails = messageDetails;
        }

        public string PluginName { get; private set; }

        public IPluginInfo PluginInfo { get; private set; }

        public string MessageTitle { get; private set; }

        public string MessageDetails { get; set; }
    }
}