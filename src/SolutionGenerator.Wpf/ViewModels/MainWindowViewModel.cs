// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Wpf.ViewModels
{
	using System;
	using System.IO;
	using Catel;
	using Catel.Logging;
	using Catel.MVVM;
	using Catel.MVVM.Services;
	using Catel.Services;
	using Models;
	using SolutionGenerator.Services;

	/// <summary>
	/// MainWindow view model.
	/// </summary>
	public class MainWindowViewModel : ViewModelBase
	{
		private static readonly ILog Log = LogManager.GetCurrentClassLogger();

		private readonly IMessageService _messageService;
		private readonly IProcessService _processService;
		private readonly ISolutionGeneratorService _solutionGeneratorService;

		public MainWindowViewModel(IMessageService messageService, ISolutionGeneratorService solutionGeneratorService, IProcessService processService)
		{
			Argument.IsNotNull(() => messageService);
			Argument.IsNotNull(() => solutionGeneratorService);
			Argument.IsNotNull(() => processService);

			_messageService = messageService;
			_solutionGeneratorService = solutionGeneratorService;
			_processService = processService;

			Generate = new Command(OnGenerateExecute, OnGenerateCanExecute);

			Solution = new Solution();
		}

		/// <summary>
		/// Gets the title of the view model.
		/// </summary>
		/// <value>The title.</value>
		public override string Title
		{
			get { return "Solution Generator"; }
		}

		[Model]
		public Solution Solution { get; private set; }

		public bool StartVisualStudio { get; set; }

		public bool OpenFolderOnCreate { get; set; }

		/// <summary>
		/// Gets the Generate command.
		/// </summary>
		public Command Generate { get; private set; }

		private bool OnGenerateCanExecute()
		{
			return !HasErrors;
		}

		private void OnGenerateExecute()
		{
			try
			{
				_solutionGeneratorService.DoWork(Solution);
			}
			catch (Exception e)
			{
				Log.Error(e.Message);
				return;
			}

			_messageService.ShowAsync(string.Format("Solution {0} created with root path '{1}'", Solution.SolutionName, Solution.RootPath));
			//_messageService.Show($"Solution {Solution.SolutionName} created with root path '{Solution.RootPath}'");

			if (StartVisualStudio)
			{
				var fileName = Path.Combine(Solution.RootPath, "src", $"{Solution.SolutionName}.sln");

				Log.Info("Opening solution '{0}'", fileName);

				_processService.StartProcess(fileName);
			}

			if (OpenFolderOnCreate)
			{
				Log.Info("Opening folder '{0}'", Solution.RootPath);

				_processService.StartProcess(Solution.RootPath);
			}
		}

		//}
		//	Log.Info("Welcome to the Solution Generator");
		//{

		//protected override void InitializeAsync()
	}
}