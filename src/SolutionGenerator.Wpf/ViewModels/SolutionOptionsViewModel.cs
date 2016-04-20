// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SolutionOptionsViewModel.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Wpf.ViewModels
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.IO;
	using System.Linq;
	using Catel;
	using Catel.Logging;
	using Catel.MVVM;
	using Catel.MVVM.Services;
	using Models;
	using SolutionGenerator.Services;

	public class SolutionOptionsViewModel : ViewModelBase
	{
		private static readonly ILog Log = LogManager.GetCurrentClassLogger();

		private readonly ISelectDirectoryService _selectDirectoryService;
		private readonly ITemplateProvider _templateProvider;

		public SolutionOptionsViewModel(Solution solution, ISelectDirectoryService selectDirectoryService, ITemplateProvider templateProvider)
		{
			Argument.IsNotNull(() => solution);
			Argument.IsNotNull(() => selectDirectoryService);

			Solution = solution;
			_selectDirectoryService = selectDirectoryService;
			_selectDirectoryService.ShowNewFolderButton = true;
			_templateProvider = templateProvider;

			AvailableLicenseNames = Solution.AvailableLicenses;
			AvailableProjectTypes = new ObservableCollection<ProjectTypes>(Enum<ProjectTypes>.GetValues());
			var templateInfos = _templateProvider.Templates.ToArray();
			if (templateInfos.Count() != 0)
			{
				var @default = templateInfos.FirstOrDefault(ti => ti.IsDefault);
				if (@default == null)
				{
					@default = templateInfos.FirstOrDefault();
				}
				Solution.TemplateInfo = @default;
			}
			AvailableTemplateInfos = new ObservableCollection<TemplateInfo>(templateInfos);

			// TODO: Read from registry instead in service
			AvailableTargetFrameworks = new ObservableCollection<string>(new[] {"v2.0", "v3.0", "v3.5", "v4.0", "v4.5"});

			SelectSolutionDirectory = new Command(OnSelectSolutionDirectoryExecute);
		}

		[Model]
		[Catel.Fody.Expose("ProjectType")]
		[Catel.Fody.Expose("TemplateInfo")]
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
		public ObservableCollection<TemplateInfo> AvailableTemplateInfos { get; private set; }

		public ObservableCollection<string> AvailableTargetFrameworks { get; private set; }

		public List<string> AvailableLicenseNames { get; private set; }

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

		private void OnRootPathChanged()
		{
			if (string.IsNullOrWhiteSpace(RootPath))
			{
				return;
			}

			string solutionName = null;
			if (Directory.Exists(RootPath))
			{
				var directoryInfo = new DirectoryInfo(RootPath);
				solutionName = Path.GetFileName(directoryInfo.Name);
			}

			if (string.IsNullOrEmpty(solutionName))
			{
				return;
			}

			if (string.IsNullOrWhiteSpace(SolutionName)
			    || RootPath.Contains(string.Format(@"\{0}\", SolutionName)))
			{
				SolutionName = solutionName;
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