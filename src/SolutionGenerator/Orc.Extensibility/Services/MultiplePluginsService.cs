// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiplePluginsService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Extensibility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Catel;
    using Catel.Logging;
    using MethodTimer;

    public class MultiplePluginsService : IMultiplePluginsService
    {
        #region Fields
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IPluginFactory _pluginFactory;
        private readonly IPluginManager _pluginManager;
        #endregion

        #region Constructors
        public MultiplePluginsService(IPluginManager pluginManager, IPluginFactory pluginFactory)
        {
            Argument.IsNotNull(() => pluginManager);
            Argument.IsNotNull(() => pluginFactory);

            _pluginManager = pluginManager;
            _pluginFactory = pluginFactory;
        }
        #endregion

        #region Events
        public event EventHandler<PluginEventArgs> PluginLoadingFailed;

        public event EventHandler<PluginEventArgs> PluginLoaded;
        #endregion

        #region Methods
        /// <summary>
        /// Configures the and load plugins.
        /// </summary>
        /// <param name="requestedPlugins">The requested plugins.</param>
        /// <returns>IEnumerable&lt;IPlugin&gt;.</returns>
        [Time]
        public IEnumerable<IPlugin> ConfigureAndLoadPlugins(params string[] requestedPlugins)
        {
            //var pluginConfiguration = new PluginConfiguration
            //{
            //    FeedUrl = feedUrlTemplate
            //};

            //if (!_pluginConfigurationService.Configure(pluginConfiguration))
            //{
            //    return null;
            //}

            var plugins = _pluginManager.GetPlugins();

            Log.Info("Found '{0}' plugins", plugins.Count());

            var pluginsToLoad = new List<IPluginInfo>();

            foreach (var plugin in plugins)
            {
                Log.Info("  * {0} ({1})", plugin, plugin.Location);

                if (requestedPlugins.Length == 0 || requestedPlugins.Contains(plugin.FullTypeName))
                {
                    pluginsToLoad.Add(plugin);
                }
            }

            var pluginInstances = new List<Plugin>();

            foreach (var pluginToLoad in pluginsToLoad)
            {
                try
                {
                    Log.Info("Instantiating plugin '{0}'", pluginToLoad.FullTypeName);

                    var pluginInstance = _pluginFactory.CreatePlugin(pluginToLoad);
                    var plugin = new Plugin(pluginInstance, pluginToLoad);
                    pluginInstances.Add(plugin);

                    PluginLoaded.SafeInvoke(this, () => new PluginEventArgs(pluginToLoad, "Loaded plugin", $"Plugin {pluginToLoad.Name} has been loaded and activated"));
                }
                catch (Exception ex)
                {
                    var message = $"Plugin '{pluginToLoad.Name}' could not be loaded";

                    Log.Warning(ex, message);

                    PluginLoadingFailed.SafeInvoke(this, () => new PluginEventArgs(pluginToLoad, "Failed to load plugin", message));
                }
            }

            return pluginInstances;
        }
        #endregion
    }
}