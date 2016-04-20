// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SolutionGeneratorService.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Services
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using Catel.Logging;
	using Catel.Reflection;
	using Ionic.Zip;
	using Models;

	public class SolutionGeneratorService : ISolutionGeneratorService
	{
		private static readonly ILog Log = LogManager.GetCurrentClassLogger();

		private readonly IFileSystemService _fileSystemService;
		private readonly IGitService _gitService;
		private readonly ITemplateProvider _templateProvider;
		private readonly ITemplateRenderer _templateRenderer;

		public SolutionGeneratorService(ITemplateProvider templateProvider, IFileSystemService fileSystemService, ITemplateRenderer templateRenderer, IGitService gitService)
		{
			_templateProvider = templateProvider;
			_fileSystemService = fileSystemService;
			_templateRenderer = templateRenderer;
			_gitService = gitService;
		}

		public void DoWork(Solution solution)
		{
			var root = CreateRootFolder(solution);
			Extract(solution, root);
			ProcessSolution(solution, root);
			ProcessFiles(solution, root);

			CreateLicence(solution, root);
			CreateReadme(solution, root);
			ApplyGit(solution, root);
		}

		private void ApplyGit(Solution solution, string root)
		{
			if (solution.InitializeGit)
			{
				_gitService.InitGitRepository(root);
			}
		}

		private void CreateReadme(Solution solution, string root)
		{
			if (solution.IncludeReadme)
			{
				var solutionFile = new FileInfo(Path.Combine(root, "README.md"));
				File.WriteAllText(solutionFile.FullName, _templateRenderer.RenderContent(solution.SolutionReadme, solution));
			}
		}

		private void CreateLicence(Solution solution, string root)
		{
			if (solution.IncludeLicense)
			{
				var assemblyDirectory = GetType().Assembly.GetDirectory();
				var licenseTemplateFileName = Path.Combine(assemblyDirectory, "Templates.Fixed", "Licenses", string.Format("{0}.txt", solution.LicenseName));
				var licenseContent = File.ReadAllText(licenseTemplateFileName);
				var solutionFile = new FileInfo(Path.Combine(root, "License.txt"));
				File.WriteAllText(solutionFile.FullName, licenseContent);
			}
		}

		private void ProcessFiles(Solution solution, string root)
		{
			var projectNames = _fileSystemService.Files(root, "*.csproj").ToArray();
			if (projectNames.Length == 0)
			{
				throw new ApplicationException(string.Format("Template {0} does not contain projects", solution.TemplateInfo.Name));
			}

			var baseProjectName = InferBaseProjectName(projectNames);

			try
			{
				_fileSystemService.Rename(baseProjectName, solution.ProjectName, root, new[] {"*.*"});
			}
			catch (Exception e)
			{
				Log.Error(e.Message);
				throw new ApplicationException(string.Format("Can not create files"), e);
			}

			try
			{
				_fileSystemService.Replace(baseProjectName, solution.ProjectName, root, new[]
				{
					"*.sln",
					"*.csproj",
					"*.user",
					"*.user",
					"*.pubxml",
					"*.edmx",
					"*.tt",
					"*.config",
					"*.settings",
					"*.resx",
					"*.cs",
					"*.cshtml",
					"*.xml",
					"*.xaml",
					"*.wxs",
					"*.wxi",
					"*.rtf"
				});
			}
			catch (Exception e)
			{
				Log.Error(e.Message);
				throw new ApplicationException(string.Format("Can not create files"), e);
			}
		}

		private void ProcessSolution(Solution solution, string root)
		{
			try
			{
				var solutionName = InferSolutionName(root);
				_fileSystemService.Rename(solutionName, solution.SolutionName, root, new[] {"*.sln*", "*.suo"}, false);
			}
			catch (Exception e)
			{
				Log.Error(e.Message);
				throw new ApplicationException(string.Format("Can not create solution file"), e);
			}
		}

		private string CreateRootFolder(Solution solution)
		{
			var root = string.Empty;
			try
			{
				root = _fileSystemService.NormalizePath(solution.RootPath);
				_fileSystemService.CreateFolder(solution.RootPath);
			}
			catch (Exception e)
			{
				Log.Error(e.Message);
				throw new ApplicationException(string.Format("Can not create folder: '{0}'", root), e);
			}
			return root;
		}

		private static void Extract(Solution solution, string root)
		{
			try
			{
				using (var zipFile = ZipFile.Read(solution.TemplateInfo.FileName))
				{
					foreach (var zipEntry in zipFile)
					{
						if (zipEntry.FileName.ToLower().Contains(".description"))
						{
							continue;
						}
						zipEntry.Extract(root, ExtractExistingFileAction.Throw);
					}
				}
			}
			catch (Exception e)
			{
				Log.Error(e.Message);
				throw new ApplicationException(string.Format("Can not create subfolders or files.: '{0}'", root), e);
			}
		}

		private string InferBaseProjectName(IEnumerable<string> projectNames)
		{
			var projectNameArray = projectNames
				.Select(Path.GetFileNameWithoutExtension)
				.OrderBy(pn => pn.Length)
				.ToArray();

			var shortest = projectNameArray.First();
			const int minimumLength = 3;
			for (var i = shortest.Length; i >= minimumLength; i--)
			{
				var lookFor = shortest.Substring(0, i);
				if (projectNameArray.All(pn => pn.StartsWith(lookFor)))
				{
					return lookFor.Trim().Trim('.').Trim();
				}
			}
			throw new ApplicationException(string.Format(@"Can not infer base project name from the available project names: \n{0}", string.Join("\n", projectNameArray)));
		}

		private string InferSolutionName(string root)
		{
			var result = _fileSystemService.Files(root, "*.sln").FirstOrDefault();
			return Path.GetFileNameWithoutExtension(result);
		}
	}
}