// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Plugin.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Extensibility
{
    using Catel;

    public class Plugin : IPlugin
    {
        public Plugin(object instance, IPluginInfo info)
        {
            Argument.IsNotNull(() => instance);
            Argument.IsNotNull(() => info);

            Instance = instance;
            Info = info;
        }

        public object Instance { get; private set; }

        public IPluginInfo Info { get; private set; }

        public override string ToString()
        {
            return $"{Info}";
        }
    }
}