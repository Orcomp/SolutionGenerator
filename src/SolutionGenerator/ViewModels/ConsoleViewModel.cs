// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleViewModel.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.ViewModels
{
    using Catel;
    using Catel.Logging;
    using Catel.MVVM;
    using SolutionGenerator.Services;
    using Environment = System.Environment;

    public class ConsoleViewModel : ViewModelBase
    {
        private readonly IConsoleService _consoleService;

        public ConsoleViewModel(IConsoleService consoleService)
        {
            Argument.IsNotNull(() => consoleService);

            Output = string.Empty;

            _consoleService = consoleService;
            _consoleService.LogMessage += OnConsoleServiceLogMessage;
        }

        public string Output { get; private set; }

        private void OnConsoleServiceLogMessage(object sender, LogMessageEventArgs e)
        {
            Output += e.Message + Environment.NewLine;
        }
    }
}