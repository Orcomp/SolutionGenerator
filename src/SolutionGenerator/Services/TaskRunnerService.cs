// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskRunnerService.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using Catel;
    using Catel.Logging;
    using Models;
    using Orchestra.Models;
    using Orchestra.Services;
    using Views;

    public class TaskRunnerService : ITaskRunnerService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private string _title = "Solution Generator";

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                TitleChanged.SafeInvoke(this);
            }
        }

        public event EventHandler TitleChanged;

        public bool ShowCustomizeShortcutsButton
        {
            get { return true; }
        }

        public object GetViewDataContext()
        {
            return new Settings();
        }

        public FrameworkElement GetView()
        {
            return new SettingsView();
        }

        public async Task RunAsync(object dataContext)
        {
            var settings = (Settings) dataContext;

            Log.Info("Running action with the following settings:");
            Log.Indent();
            //Log.Info("Working directory => {0}", settings.WorkingDirectory);
            //Log.Info("Output directory => {0}", settings.OutputDirectory);
            //Log.Info("Current time => {0}", settings.CurrentTime);
            //Log.Info("Horizon start => {0}", settings.HorizonStart);
            //Log.Info("Horizon end => {0}", settings.HorizonEnd);
            Log.Unindent();

            Log.Info("Sleeping to show long running action with blocking thread");

            Thread.Sleep(2500);

            Log.Info("Action is complete!");
        }

        public Size GetInitialWindowSize()
        {
            return Size.Empty;
        }

        public AboutInfo GetAboutInfo()
        {
            return new AboutInfo();
        }
    }
}