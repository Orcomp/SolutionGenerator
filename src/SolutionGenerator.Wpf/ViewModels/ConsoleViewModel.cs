// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleViewModel.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Wpf.ViewModels
{
	using Catel;
	using Catel.Logging;
	using Catel.MVVM;
	using Services.Interfaces;
	using Environment = System.Environment;

	public class ConsoleViewModel : ViewModelBase
	{
		#region Fields
		private readonly IConsoleService _consoleService;
		#endregion

		#region Constructors
		public ConsoleViewModel(IConsoleService consoleService)
		{
			Argument.IsNotNull(() => consoleService);

			Output = string.Empty;

			_consoleService = consoleService;
			_consoleService.LogMessage += OnConsoleServiceLogMessage;
		}
		#endregion

		#region Properties
		public string Output { get; private set; }
		#endregion

		#region Methods
		private void OnConsoleServiceLogMessage(object sender, LogMessageEventArgs e)
		{
			Output += e.Message + Environment.NewLine;
		}
		#endregion
	}
}