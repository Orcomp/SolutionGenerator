namespace SolutionGenerator
{
    using System;
    using System.Windows;
    using Catel.IoC;
    using Catel.Logging;
    using Catel.Reflection;
    using Orchestra.Services;
    using Orchestra.Views;
    using Services;

    public partial class App
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        protected override async void OnStartup(StartupEventArgs e)
        {
#if DEBUG
            LogManager.AddDebugListener(true);
#endif

            try
            {
                var serviceLocator = ServiceLocator.Default;

                serviceLocator.RegisterType<ITaskRunnerService, TaskRunnerService>();

                var shellService = serviceLocator.ResolveType<IShellService>();
                await shellService.CreateAsync<ShellWindow>();

                base.OnStartup(e);
            }
            catch (Exception ex)
            {
                Log.Error(ex);

                var assemblyTitle = GetType().Assembly.Title();

                MessageBox.Show($"A critical error has occurred in '{assemblyTitle}'.\n\nPlease contact support, they will know what to do.",
                    $"Critical error in '{assemblyTitle}' - please contact support", MessageBoxButton.OK, MessageBoxImage.Stop);

                await LogManager.FlushAllAsync();

                Environment.Exit(1);
            }
        }
    }
}
