// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SolutionGeneratorService.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2013 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Services
{
    using System.Collections.Generic;
    using System.IO;
    using Catel;
    using Catel.Logging;
    using Catel.Reflection;
    using SolutionGenerator.Models;

    public class SolutionGeneratorService : ISolutionGeneratorService
    {
        #region Constants
        private const string SolutionTemplate = "./Templates/SolutionTemplate.txt";
        private const string SolutionWithTestTemplate = "./Templates/SolutionWithTestTemplate.txt";
        private const string ProjectTemplate = "./Templates/ProjectTemplate.txt";
        private const string WpfProjectTemplate = "./Templates/WPF/ProjectTemplate.txt";
        private const string GitAttributeTemplate = "./Templates/gitAttributeTemplate.txt";
        private const string GitIgnoreTemplate = "./Templates/gitIgnoreTemplate.txt";
        private const string ReadmeTemplate = "./Templates/ReadmeTemplate.txt";
        private const string ResharperSettingsTemplate = "./Templates/resharperSettingsTemplate.txt";
        private const string StyleCopTemplate = "./Templates/styleCopTemplate.txt";
        private const string LicenseTemplate = "./Templates/licenseTemplate.txt";
        private const string PackagesTemplate = "./Templates/packagesConfigTemplate.txt";
        private const string ConsoleProgramClass = "./Templates/consoleProgramClass.txt";

        private const string AppXaml = "./Templates/WPF/appXaml.txt";
        private const string AppXamlCs = "./Templates/WPF/appXamlCs.txt";
        private const string MainWindowXaml = "./Templates/WPF/mainWindowXaml.txt";
        private const string MainWindowXamlCs = "./Templates/WPF/mainWindowXamlCs.txt";

        private const string ProgramCs = "./Templates/Winform/programCs.txt";
        private const string Form1DesignerCs = "./Templates/Winform/form1DesignerCs.txt";
        private const string Form1Cs = "./Templates/Winform/form1Cs.txt";

        private const string FolderStructureFile = "./folders.txt";
        #endregion

        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        #region Fields
        private readonly IGitService _gitService;
        private readonly IProjectTypeConverterService _projectTypeConverterService;
        private readonly IReferencesService _referencesService;
        private readonly ITemplateRenderer _templateRenderer;
        #endregion

        #region Constructors
        public SolutionGeneratorService(IGitService gitService, ITemplateRenderer templateRenderer,
            IProjectTypeConverterService projectTypeConverterService, IReferencesService referencesService)
        {
            Argument.IsNotNull(() => gitService);
            Argument.IsNotNull(() => templateRenderer);
            Argument.IsNotNull(() => projectTypeConverterService);
            Argument.IsNotNull(() => referencesService);

            _gitService = gitService;
            _templateRenderer = templateRenderer;
            _projectTypeConverterService = projectTypeConverterService;
            _referencesService = referencesService;
        }
        #endregion

        #region ISolutionGeneratorService Members
        public void DoWork(Solution solution)
        {
            Argument.IsNotNull(() => solution);

            Log.Info("Generating solution '{0}'", solution);

            // create folders under root path
            var rootDirectoryInfo = new DirectoryInfo(solution.RootPath);
            CreateFolderStructure(rootDirectoryInfo);
            CreateSolutionAssets(rootDirectoryInfo, solution);

            // create files under root/src path
            var sourceDirectoryInfo = new DirectoryInfo(string.Format("{0}/src/", solution.RootPath));
            CreateSolutionFile(sourceDirectoryInfo, solution);
            CreateProjectFile(sourceDirectoryInfo, solution);
            if (solution.IncludeTestProject)
            {
                CreateTestProjectFile(sourceDirectoryInfo, solution);
            }

            CreateProjectAssets(sourceDirectoryInfo, solution);

            if (solution.InitializeGit)
            {
                _gitService.InitGitRepository(rootDirectoryInfo.FullName);
            }
        }
        #endregion

        #region Methods
        private void CreateFolderStructure(DirectoryInfo root)
        {
            Log.Info("Creating folder structure");

            if (!root.Exists)
            {
                root.Create();
            }

            var directoryCreator = new DirectoryCreator(new FileInfo(FolderStructureFile), root);
            directoryCreator.CreateDirectoryStructure();
        }

        private FileInfo CreateSolutionFile(DirectoryInfo root, Solution model)
        {
            Log.Info("Creating solution file");

            var templateToRender = model.IncludeTestProject ? SolutionWithTestTemplate : SolutionTemplate;
            var solutionFile = new FileInfo(string.Format("{0}{1}.sln", root.FullName, model.SolutionName));

            File.WriteAllText(solutionFile.FullName, _templateRenderer.RenderFile(templateToRender, model));

            return solutionFile;
        }

        private FileInfo CreateProjectFile(DirectoryInfo root, Solution solution)
        {
            Log.Info("Creating project file");

            string projectRoot = string.Format("{0}/{1}/", root.FullName, solution.ProjectName);
            var directoryInfo = new DirectoryInfo(projectRoot);
            var projectTemplate = ProjectTemplate;

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            var project = new Project(solution.TestProjectGuid)
            {
                ProjectAssemblyName = solution.ProjectAssemblyName,
                ProjectName = solution.ProjectName,
                ProjectRootNameSpace = solution.ProjectRootNameSpace,
                TargetFramework = solution.TargetFramework,
                ReleaseOutputPath = string.Format("../../output/Release/{0}", solution.ProjectName),
                DebugOutputPath = string.Format("../../output/Debug/{0}", solution.ProjectName),
                ProjectType = solution.ProjectType
            };

            project.ProjectOutputType = _projectTypeConverterService.Convert(solution.ProjectType);
            _referencesService.AddRequiredReferences(project);

            if (string.Equals(project.ProjectOutputType, "Exe"))
            {
                File.WriteAllText(projectRoot + "Program.cs", _templateRenderer.RenderFile(ConsoleProgramClass, project));
            }
            else if (string.Equals(solution.ProjectType, "WPF"))
            {
                projectTemplate = WpfProjectTemplate;
                File.WriteAllText(projectRoot + "App.xaml", _templateRenderer.RenderFile(AppXaml, project));
                File.WriteAllText(projectRoot + "App.xaml.cs", _templateRenderer.RenderFile(AppXamlCs, project));
                File.WriteAllText(projectRoot + "MainWindow.xaml", _templateRenderer.RenderFile(MainWindowXaml, project));
                File.WriteAllText(projectRoot + "MainWindow.xaml.cs", _templateRenderer.RenderFile(MainWindowXamlCs, project));
            }
            else if (string.Equals(solution.ProjectType, "WinForms"))
            {
                File.WriteAllText(projectRoot + "Form1.cs", _templateRenderer.RenderFile(Form1Cs, project));
                File.WriteAllText(projectRoot + "Form1.Designer.cs", _templateRenderer.RenderFile(Form1DesignerCs, project));
                File.WriteAllText(projectRoot + "Program.cs", _templateRenderer.RenderFile(ProgramCs, project));
            }

            var projectFile = new FileInfo(projectRoot + project.ProjectName + ".csproj");
            File.WriteAllText(projectFile.FullName, _templateRenderer.RenderFile(ProjectTemplate, project));

            return projectFile;
        }

        private FileInfo CreateTestProjectFile(DirectoryInfo root, Solution solution)
        {
            string projectRoot = string.Format("{0}/{1}.Tests/", root.FullName, solution.ProjectName);
            var directoryInfo = new DirectoryInfo(projectRoot);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            var projectName = string.Format("{0}.Tests", solution.ProjectName);
            var project = new Project(solution.TestProjectGuid)
            {
                ProjectAssemblyName = string.Format("{0}.Tests", solution.ProjectAssemblyName),
                ProjectName = projectName,
                ProjectRootNameSpace = string.Format("{0}.Tests", solution.ProjectRootNameSpace),
                TargetFramework = solution.TargetFramework,
                ReleaseOutputPath = string.Format("../../output/Release/{0}", projectName),
                DebugOutputPath = string.Format("../../output/Debug/{0}", projectName),
                ProjectOutputType = ProjectOutputTypes.Library
            };

            if (string.Equals(solution.TargetFramework, "v4.5"))
            {
                project.ProjectType = ProjectTypes.Test;
                var packagesFile = new FileInfo(projectRoot + "packages.config");
                File.WriteAllText(packagesFile.FullName, _templateRenderer.RenderFile(PackagesTemplate, project));
            }

            _referencesService.AddRequiredReferences(project);

            var projectFile = new FileInfo(projectRoot + project.ProjectName + ".csproj");
            File.WriteAllText(projectFile.FullName, _templateRenderer.RenderFile(ProjectTemplate, project));

            return projectFile;
        }

        private FileInfo[] CreateSolutionAssets(DirectoryInfo root, Solution solution)
        {
            var files = new List<FileInfo>();

            if (solution.IncludeGitAttribute)
            {
                var solutionFile = new FileInfo(Path.Combine(root.FullName, ".gitattributes"));
                File.WriteAllText(solutionFile.FullName, _templateRenderer.RenderFile(GitAttributeTemplate, solution));
                files.Add(solutionFile);
            }

            if (solution.IncludeGitIgnore)
            {
                var solutionFile = new FileInfo(Path.Combine(root.FullName, ".gitignore"));
                File.WriteAllText(solutionFile.FullName, _templateRenderer.RenderFile(GitIgnoreTemplate, solution));
                files.Add(solutionFile);
            }

            if (solution.IncludeReadme)
            {
                var solutionFile = new FileInfo(Path.Combine(root.FullName, "README.md"));
                File.WriteAllText(solutionFile.FullName, _templateRenderer.RenderContent(solution.SolutionReadme, solution));
                files.Add(solutionFile);
            }

            if (solution.IncludeLicense)
            {
                var assemblyDirectory = GetType().Assembly.GetDirectory();
                string licenseTemplateFileName = Path.Combine(assemblyDirectory, "Templates", "Licenses", string.Format("{0}.txt", solution.LicenseName));
                string licenseContent = File.ReadAllText(licenseTemplateFileName);

                var solutionFile = new FileInfo(Path.Combine(root.FullName, "License.txt"));
                File.WriteAllText(solutionFile.FullName, licenseContent);
                files.Add(solutionFile);
            }

            return files.ToArray();
        }

        private FileInfo[] CreateProjectAssets(DirectoryInfo root, Solution model)
        {
            Log.Info("Creating project assets");

            var files = new List<FileInfo>();

            FileInfo solutionFile;
            if (model.IncludeReSharper)
            {
                solutionFile = new FileInfo(string.Format("{0}/resharper.settings", root.FullName));
                File.WriteAllText(solutionFile.FullName, _templateRenderer.RenderFile(ResharperSettingsTemplate, model));
                files.Add(solutionFile);
            }

            if (model.IncludeStylecop)
            {
                solutionFile = new FileInfo(string.Format("{0}/Settings.StyleCop", root.FullName));
                File.WriteAllText(solutionFile.FullName, _templateRenderer.RenderFile(StyleCopTemplate, model));
                files.Add(solutionFile);
            }

            return files.ToArray();
        }
        #endregion
    }
}