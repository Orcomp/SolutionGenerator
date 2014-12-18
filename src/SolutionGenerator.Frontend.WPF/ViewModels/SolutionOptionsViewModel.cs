// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SolutionOptionsViewModel.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2013 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using Catel;
    using Catel.Logging;
    using Catel.MVVM;
    
    using Catel.Services;
    using SolutionGenerator.Models;

    public class SolutionOptionsViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly ISelectDirectoryService _selectDirectoryService;

        #region Constructors
        public SolutionOptionsViewModel(Solution solution, ISelectDirectoryService selectDirectoryService)
        {
            Argument.IsNotNull(() => solution);
            Argument.IsNotNull(() => selectDirectoryService);

            Solution = solution;
            _selectDirectoryService = selectDirectoryService;
            _selectDirectoryService.ShowNewFolderButton = true;

            AvailableLicenseNames = Solution.AvailableLicenses;
            AvailableProjectTypes = new ObservableCollection<ProjectTypes>(Enum<ProjectTypes>.GetValues());

            // TODO: Read from registry instead in service
            AvailableTargetFrameworks = new ObservableCollection<string>(new[] { "v2.0", "v3.0", "v3.5", "v4.0", "v4.5" });

            SelectSolutionDirectory = new Command(OnSelectSolutionDirectoryExecute);
        }
        #endregion

        #region Properties
        [Model]
        [Catel.Fody.Expose("ProjectType")]
        [Catel.Fody.Expose("TargetFramework")]
        [Catel.Fody.Expose("LicenseName")]
        [Catel.Fody.Expose("Readme", "SolutionReadme")]
        public Solution Solution { get; private set; }

        [ViewModelToModel("Solution")]
        public string RootPath { get; set; }

        [ViewModelToModel("Solution")]
        public string SolutionName { get; set; }

        [ViewModelToModel("Solution")]
        public string ProjectName { get; set; }

        [ViewModelToModel("Solution")]
        public string ProjectRootNameSpace { get; set; }

        [ViewModelToModel("Solution")]
        public string ProjectAssemblyName { get; set; }

        public ObservableCollection<ProjectTypes> AvailableProjectTypes { get; private set; }

        public ObservableCollection<string> AvailableTargetFrameworks { get; private set; }

        public List<string> AvailableLicenseNames { get; private set; }
        #endregion

        #region Commands
        /// <summary>
        /// Gets the SelectSolutionDirectory command.
        /// </summary>
        public Command SelectSolutionDirectory { get; private set; }

        /// <summary>
        /// Method to invoke when the SelectSolutionDirectory command is executed.
        /// </summary>
        private void OnSelectSolutionDirectoryExecute()
        {
            if (_selectDirectoryService.DetermineDirectory())
            {
                RootPath = _selectDirectoryService.DirectoryName;

                Log.Info("Changed solution directory to '{0}'", RootPath);
            }
        }
        #endregion

        private void OnRootPathChanged()
        {
            if (!string.IsNullOrWhiteSpace(RootPath))
            {
                if (!Directory.Exists(RootPath))
                {
                    return;
                }

                var directoryInfo = new DirectoryInfo(RootPath);
                string solutionName = directoryInfo.Name;

                if (string.IsNullOrWhiteSpace(SolutionName))
                {
                    SolutionName = solutionName;
                }
            }
        }

        private void OnSolutionNameChanged()
        {
            var solutionName = SolutionName;

            ProjectName = solutionName;
        }

        private void OnProjectNameChanged()
        {
            var projectName = ProjectName;

            ProjectRootNameSpace = projectName;
            ProjectAssemblyName = projectName;   
        }
    }
}