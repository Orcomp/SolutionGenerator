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

        public SettingsViewModel(IPluginManager pluginManager)
        {
            Argument.IsNotNull(() => pluginManager);

            _pluginManager = pluginManager;
        }

        public List<IPluginInfo> AvailablePlugins { get; private set; }

        public IPluginInfo SelectedPlugin { get; set; }

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
            // TODO: Instantiate and retrieve the view
        }
        #endregion
    }
}