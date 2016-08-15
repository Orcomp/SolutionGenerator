// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsViewModel.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates.DataApplication.ViewModels
{
    using Catel;
    using Catel.MVVM;

    public class SettingsViewModel : ViewModelBase
    {
        private readonly DataApplicationTemplateContext _templateContext;

        public SettingsViewModel(DataApplicationTemplateContext templateContext)
        {
            Argument.IsNotNull(() => templateContext);

            _templateContext = templateContext;

            Data = templateContext.Data;
        }

		
		public DataTemplate Data { get; private set; }
    }
}