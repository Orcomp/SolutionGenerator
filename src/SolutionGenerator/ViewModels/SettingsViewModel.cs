// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsViewModel.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.MVVM;
    using Orc.Extensibility;

    public class SettingsViewModel : ViewModelBase
    {
        private readonly IPluginManager _pluginManager;
        private readonly IMultiplePluginsService _multiplePluginsService;

        public SettingsViewModel(IPluginManager pluginManager, IMultiplePluginsService multiplePluginsService)
        {
            Argument.IsNotNull(() => pluginManager);
            Argument.IsNotNull(() => multiplePluginsService);

            _pluginManager = pluginManager;
            _multiplePluginsService = multiplePluginsService;
        }

        public List<IPluginInfo> AvailablePlugins { get; private set; }

        public IPluginInfo SelectedPlugin { get; set; }

        public ITemplateDefinition ActivePlugin { get; private set; }

        #region Methods
        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            AvailablePlugins = new List<IPluginInfo>(from plugin in _pluginManager.GetPlugins()
                                                     orderby plugin.Name
                                                     select plugin);

            SelectedPlugin = AvailablePlugins.FirstOrDefault();
        }

        private void OnSelectedPluginChanged()
        {
            ITemplateDefinition templateDefinition = null;

            if (SelectedPlugin != null)
            {
                var plugin = _multiplePluginsService.ConfigureAndLoadPlugins(SelectedPlugin.FullTypeName).FirstOrDefault();
                if (plugin != null)
                {
                    templateDefinition = plugin.Instance as ITemplateDefinition;
                }
            }

            ActivePlugin = templateDefinition;
        }
        #endregion
    }
}