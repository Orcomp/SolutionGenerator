// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskRunnerService.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using Catel;
    using Catel.Logging;
    using Catel.MVVM;
    using Orchestra.Models;
    using Orchestra.Services;
    using ViewModels;
    using Views;

    public class TaskRunnerService : ITaskRunnerService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly ISolutionGeneratorService _solutionGeneratorService;
        private readonly IViewModelFactory _viewModelFactory;

        private string _title = "Solution Generator";

        public TaskRunnerService(ISolutionGeneratorService solutionGeneratorService, IViewModelFactory viewModelFactory)
        {
            Argument.IsNotNull(() => solutionGeneratorService);
            Argument.IsNotNull(() => viewModelFactory);

            _solutionGeneratorService = solutionGeneratorService;
            _viewModelFactory = viewModelFactory;
        }

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
            return _viewModelFactory.CreateViewModel<SettingsViewModel>(null, null);
        }

        public FrameworkElement GetView()
        {
            return new SettingsView();
        }

        public async Task RunAsync(object dataContext)
        {
            var settings = (SettingsViewModel)dataContext;
            var templateDefinition = settings.ActivePlugin;
            if (templateDefinition == null)
            {
                Log.Error("No template has been selected");
                return;
            }

            var directory = templateDefinition.TemplateContext.Solution.Directory;
            if (string.IsNullOrWhiteSpace(directory))
            {
                Log.Error("No solution directory available");
                return;
            }

            if (Directory.Exists(directory))
            {
                if (Directory.GetFiles(directory, "*", SearchOption.AllDirectories).Any())
                {
                    // TODO: Turn this into a question later
                    Log.Error($"Solution directory '{directory}' must be empty");
                    return;
                }
            }

            Log.Debug($"Ensuring that directory '{directory}' exists");

            Directory.CreateDirectory(directory);

            Log.Info("Validating template data");

            var validationContext = templateDefinition.Validate();

            foreach (var warning in validationContext.GetWarnings())
            {
                Log.Warning(warning.Message);
            }

            foreach (var error in validationContext.GetErrors())
            {
                Log.Error(error.Message);
            }

            if (validationContext.HasErrors)
            {
                Log.Error("1 or more validation errors occurred, cannot generate the solution");
                return;
            }

            Log.Info("Generating the solution");
            Log.Indent();

            try
            {
                templateDefinition.PreGenerate();

                await _solutionGeneratorService.GenerateAsync(templateDefinition);

                templateDefinition.PostGenerate();

                Log.Unindent();

                Log.Info("Generated the solution");
            }
            catch (Exception ex)
            {
                Log.Unindent();

                Log.Error(ex, "Failed to generate the solution");
            }
        }

        public Size GetInitialWindowSize()
        {
            return new Size(800, 800);
        }

        public AboutInfo GetAboutInfo()
        {
            return new AboutInfo();
        }
    }
}