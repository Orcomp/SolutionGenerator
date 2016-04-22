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
	using System.Xml.Linq;
	using Catel.IoC;
	using Catel.Logging;
	using Catel.Reflection;
	using Ionic.Zip;
	using Models;
	using Orc.Csv;

	public class SolutionGeneratorService : ISolutionGeneratorService
	{
		private static readonly ILog Log = LogManager.GetCurrentClassLogger();

		private readonly IFileSystemService _fileSystemService;
		private readonly IGitService _gitService;
		private readonly ITemplateProvider _templateProvider;
		private readonly ITemplateRenderer _templateRenderer;
		private readonly IProjectFileService _projectFileService;

		public SolutionGeneratorService(ITemplateProvider templateProvider, IFileSystemService fileSystemService, IProjectFileService projectFileService, ITemplateRenderer templateRenderer, IGitService gitService)
		{
			_templateProvider = templateProvider;
			_fileSystemService = fileSystemService;
			_templateRenderer = templateRenderer;
			_gitService = gitService;
			_projectFileService = projectFileService;
		}

		public void DoWork(Solution solution)
		{
			var root = CreateRootFolder(solution);
			Extract(solution, root);
			DeleteIgnorableFolders(solution, root);
			ProcessDataFolder(solution, root);
			ProcessSolution(solution, root);
			ProcessFiles(solution, root);
			CreateLicence(solution, root);
			CreateReadme(solution, root);
			ApplyGit(solution, root);
		}

		private void DeleteIgnorableFolders(Solution solution, string root)
		{
			var ignorableFolderPatterns = new string[] {"/bin/", "\\bin\\", "/.vs/", "\\.vs\\" };
			var folders = _fileSystemService.Folders(root, "*.*").ToArray();
			foreach (var folder in folders)
			{
				if (ignorableFolderPatterns.Any(ifp => folder.ToLower().Contains(ifp)))
				{
					try
					{
						_fileSystemService.DeleteFolder(folder);
					}
					catch
					{
					}
				}
			}
		}

		public void ProcessDataFolder(Solution solution, string root)
		{
			if (!solution.HasDataFolder || string.IsNullOrEmpty(solution.DataFolder))
			{
				return;
			}
			// TODO: Create pluggable action here by loading calling an assembly from the template
			CreateModelFiles(solution, root);
			CreateDataFiles(solution, root);
			CreateTestClassFiles(solution, root);
		}

		private void CreateModelFiles(Solution solution, string root)
		{
			var modelFolder = _fileSystemService.Folders(root, "*.*").FirstOrDefault(f => f.ToLower().Contains("\\model"));
			if (modelFolder == null)
			{
				return;
			}

			var nameSpace = GetNameSpace(solution, root);
			CodeGeneration.CreateCSharpFilesForAllCsvFiles(solution.DataFolder, nameSpace, modelFolder);
			var projectFileName = modelFolder.ToLower().Replace("model", $"{nameSpace}.Shared.projitems");

			var referenceName = "operationx";

			// Add generated files:
			_projectFileService.Open(projectFileName);
			var newFiles = _fileSystemService.Files(modelFolder, "*.cs").ToArray();

			foreach (var file in newFiles)
			{
				if (file.ToLower().Contains(referenceName))
				{
					_fileSystemService.DeleteFile(file);
					_projectFileService.RemoveItem("Compile", Path.GetFileName(file).ToLower());
					continue;
				}
				_projectFileService.AddItem("Compile", $"{referenceName}.cs", Path.GetFileName(file));
			}
			
			_projectFileService.Save();
		}

		private void CreateDataFiles(Solution solution, string root)
		{
			var testFilesFolder = _fileSystemService.Folders(root, "*.*").FirstOrDefault(f => f.ToLower().Contains("\\testfiles"));
			if (testFilesFolder == null)
			{
				return;
			}

			var nameSpace = GetNameSpace(solution, root);
			var projectFileName = testFilesFolder.ToLower().Replace("testfiles", $"{nameSpace}.Tests.Shared.projitems");

			// Copy and Add generated files:
			_projectFileService.Open(projectFileName);
			var files = _fileSystemService.Files(solution.DataFolder, "*.csv").ToArray();

			var referenceName = "operationx.csv";
			foreach (var file in files)
			{
				var targetFileName = Path.Combine(testFilesFolder, Path.GetFileName(file));
				_fileSystemService.Copy(file, targetFileName);
				_projectFileService.AddItem("None", referenceName, Path.GetFileName(file));
			}

			_projectFileService.RemoveItem("None", referenceName);
			_projectFileService.Save();

			_fileSystemService.DeleteFile(Path.Combine(testFilesFolder, referenceName));
		}

		private void CreateTestClassFiles(Solution solution, string root)
		{
			var testFolder = _fileSystemService.Folders(root, "*.*")
				.Where(f => f.ToLower().Contains("tests.shared"))
				.OrderBy(f => f.Length)
				.FirstOrDefault();

			if (testFolder == null)
			{
				return;
			}

			var nameSpace = GetNameSpace(solution, root);
			var projectFileName = testFolder.ToLower().Replace("shared", $"Shared\\{nameSpace}.Tests.Shared.projitems");

			// Copy and Add generated files:
			_projectFileService.Open(projectFileName);
			var files = _fileSystemService.Files(solution.DataFolder, "*.csv").ToArray();

			var referenceName = "operationxtests.cs";
			var uniSourceFileName = Path.Combine(testFolder, referenceName);
			foreach (var file in files)
			{
				var className = Path.GetFileNameWithoutExtension(file).ToCamelCase();
				var targetFileName = Path.Combine(testFolder, className+"Tests.cs");
				_fileSystemService.Copy(uniSourceFileName, targetFileName);
				_fileSystemService.Replace("OperationX",className, testFolder, new [] {Path.GetFileName(targetFileName)}, false);
				_projectFileService.AddItem("Compile", referenceName, Path.GetFileName(targetFileName));
			}

			_projectFileService.RemoveItem("Compile", referenceName);
			_projectFileService.Save();
			_fileSystemService.DeleteFile(Path.Combine(testFolder, referenceName));
		}

		private string GetNameSpace(Solution solution, string root)
		{
			var projectNames = _fileSystemService.Files(root, "*.csproj").ToArray();
			if (projectNames.Length == 0)
			{
				throw new ApplicationException($"Template {solution.TemplateInfo.Name} does not contain projects");
			}

			return InferBaseProjectName(projectNames);
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
				var licenseTemplateFileName = Path.Combine(assemblyDirectory, "Templates.Fixed", "Licenses", $"{solution.LicenseName}.txt");
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
				throw new ApplicationException($"Template {solution.TemplateInfo.Name} does not contain projects");
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
					"*.projitems",
					"*.shproj",
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
				throw new ApplicationException($"Can not create folder: '{root}'", e);
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
				throw new ApplicationException($"Can not create subfolders or files.: '{root}'", e);
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
			throw new ApplicationException($@"Can not infer base project name from the available project names: \n{string.Join("\n", projectNameArray)}");
		}

		private string InferSolutionName(string root)
		{
			var result = _fileSystemService.Files(root, "*.sln").FirstOrDefault();
			return Path.GetFileNameWithoutExtension(result);
		}
	}
}