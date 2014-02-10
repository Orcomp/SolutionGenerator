// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2013 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.ViewModels
{
    using System.IO;
    using Catel;
    using Catel.Logging;
    using Catel.MVVM;
    using Catel.MVVM.Services;
    using SolutionGenerator.Models;
    using SolutionGenerator.Services;

    /// <summary>
    /// MainWindow view model.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IMessageService _messageService;
        private readonly ISolutionGeneratorService _solutionGeneratorService;
        private readonly IProcessService _processService;

        #region Constructors
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
        #endregion

        #region Properties
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
        #endregion

        #region Commands
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
            _solutionGeneratorService.DoWork(Solution);

            _messageService.Show(string.Format("Solution {0} created with root path '{1}'", Solution.SolutionName, Solution.RootPath));

            if (StartVisualStudio)
            {
                var fileName = Path.Combine(Solution.RootPath, "src", string.Format("{0}.sln", Solution.SolutionName));

                Log.Info("Opening solution '{0}'", fileName);

                _processService.StartProcess(fileName);
            }

            if (OpenFolderOnCreate)
            {
                Log.Info("Opening folder '{0}'", Solution.RootPath);

                _processService.StartProcess(Solution.RootPath);
            }
        }

        protected override void Initialize()
        {
            Log.Info("Welcome to the Solution Generator");
        }
        #endregion

    }
}