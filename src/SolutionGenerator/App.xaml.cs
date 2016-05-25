// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator
{
    using System;
    using System.Windows;
    using Catel.ApiCop;
    using Catel.ApiCop.Listeners;
    using Catel.IoC;
    using Catel.Logging;
    using Catel.Reflection;
    using Orchestra.Services;
    using Orchestra.Views;
    using Services;

    public partial class App
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        #region Methods
        protected override void OnStartup(StartupEventArgs e)
        {
#if DEBUG
            LogManager.AddDebugListener();
#endif

            try
            {
                var serviceLocator = ServiceLocator.Default;

                serviceLocator.RegisterType<ITaskRunnerService, TaskRunnerService>();

                var shellService = serviceLocator.ResolveType<IShellService>();
                shellService.CreateAsync<ShellWindow>();

                base.OnStartup(e);
            }
            catch (Exception ex)
            {
                Log.Error(ex);

                var assemblyTitle = GetType().Assembly.Title();

                MessageBox.Show($"A critical error has occurred in '{assemblyTitle}'.\n\nPlease contact support, they will know what to do.",
                    $"Critical error in '{assemblyTitle}' - please contact support", MessageBoxButton.OK, MessageBoxImage.Stop);

                LogManager.FlushAll();

                Environment.Exit(1);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Get advisory report in console
            ApiCopManager.AddListener(new ConsoleApiCopListener());
            ApiCopManager.WriteResults();

            base.OnExit(e);
        }
        #endregion
    }
}