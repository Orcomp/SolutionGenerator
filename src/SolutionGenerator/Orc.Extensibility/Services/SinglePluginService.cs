// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SinglePluginService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Extensibility
{
    using System;
    using System.Linq;
    using Catel;
    using Catel.Logging;

    public class SinglePluginService : ISinglePluginService
    {
        #region Fields
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IPluginFactory _pluginFactory;
        private readonly IPluginManager _pluginManager;
        #endregion

        #region Constructors
        public SinglePluginService(IPluginManager pluginManager, IPluginFactory pluginFactory)
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
        public IPlugin ConfigureAndLoadPlugin(string expectedPlugin, string defaultPlugin)
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

            IPluginInfo pluginToLoad = null;

            foreach (var plugin in plugins)
            {
                Log.Info("  * {0} ({1})", plugin, plugin.Location);

                if (string.Equals(plugin.FullTypeName, expectedPlugin))
                {
                    pluginToLoad = plugin;
                }
            }

            var fallbackPlugin = (from plugin in plugins
                                  where string.Equals(plugin.FullTypeName, defaultPlugin)
                                  select plugin).FirstOrDefault();

            if (pluginToLoad == null)
            {
                const string message = "Plugin could not be found, using default plugin";

                Log.Warning(message);

                PluginLoadingFailed.SafeInvoke(this, () => new PluginEventArgs(expectedPlugin, "Failed to load plugin", message));

                pluginToLoad = fallbackPlugin;
            }

            if (pluginToLoad == null)
            {
                return null;
            }

            object pluginInstance;

            try
            {
                Log.Info("Instantiating plugin '{0}'", pluginToLoad.FullTypeName);

                pluginInstance = _pluginFactory.CreatePlugin(pluginToLoad);
            }
            catch (Exception ex)
            {
                var message = $"Plugin '{pluginToLoad.Name}' could not be loaded, falling back to default plugin";

                Log.Warning(ex, message);

                PluginLoadingFailed.SafeInvoke(this, () => new PluginEventArgs(pluginToLoad.Name, "Failed to load plugin", message));

                pluginToLoad = fallbackPlugin;

                Log.Info("Instantiating plugin '{0}'", pluginToLoad.FullTypeName);

                pluginInstance = _pluginFactory.CreatePlugin(pluginToLoad);
            }

            PluginLoaded.SafeInvoke(this, () => new PluginEventArgs(pluginToLoad, "Loaded plugin", $"Plugin {pluginToLoad.Name} has been loaded and activated"));

            return new Plugin(pluginInstance, pluginToLoad);
        }
        #endregion
    }
}