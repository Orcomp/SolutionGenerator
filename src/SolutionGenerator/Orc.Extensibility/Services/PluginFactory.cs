// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PluginFactory.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Extensibility
{
    using System.Reflection;
    using Catel;
    using Catel.IoC;
    using Catel.Logging;
    using Catel.Reflection;
    using MethodTimer;

    public class PluginFactory : IPluginFactory
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly ITypeFactory _typeFactory;

        public PluginFactory(ITypeFactory typeFactory)
        {
            Argument.IsNotNull(() => typeFactory);

            _typeFactory = typeFactory;
        }

        [Time]
        public object CreatePlugin(IPluginInfo pluginInfo)
        {
            Argument.IsNotNull(() => pluginInfo);

            Log.Debug("Creating plugin '{0}'", pluginInfo);

            var assembly = Assembly.LoadFrom(pluginInfo.Location);
            var type = assembly.GetType(pluginInfo.FullTypeName);

            var plugin = _typeFactory.CreateInstance(type);

            // Workaround for loading assemblies
            TypeCache.InitializeTypes(type.GetAssemblyEx(), true);

            return plugin;
        }
    }
}