namespace SolutionGenerator
{
    using System.Collections.Generic;
    using System.IO;
    using Models;

    public class SolutionGeneratorService
    {
        private const string SolutionTemplate = "./Templates/SolutionTemplate.txt";
        private const string SolutionWithTestTemplate = "./Templates/SolutionWithTestTemplate.txt";
        private const string ProjectTemplate = "./Templates/ProjectTemplate.txt";
        private const string GitAttributeTemplate = "./Templates/gitAttributeTemplate.txt";
        private const string GitIgnoreTemplate = "./Templates/gitIgnoreTemplate.txt";
        private const string ReadmeTemplate = "./Templates/ReadmeTemplate.txt";
        private const string ResharperSettingsTemplate = "./Templates/resharperSettingsTemplate.txt";
        private const string StyleCopTemplate = "./Templates/styleCopTemplate.txt";
        private const string LicenseTemplate = "./Templates/licenseTemplate.txt";
        private const string FolderStructureFile = "./folders.txt";

        public void DoWork(SolutionModel model)
        {
            // create folders under root path
            var rootDirectoryInfo = new DirectoryInfo(model.RootPath);
            CreateFolderStructure(rootDirectoryInfo);
            CreateSolutionAssets(rootDirectoryInfo, model);

            // create files under root/src path
            rootDirectoryInfo = new DirectoryInfo(string.Format("{0}/src/", model.RootPath));
            CreateSolutionFile(rootDirectoryInfo, model);
            CreateProjectFile(rootDirectoryInfo, model);
            if (model.IncludeTestProject)
            {
                CreateTestProjectFile(rootDirectoryInfo, model);
            }
            CreateProjectAssets(rootDirectoryInfo, model);

            if (model.InitiliazeGit)
            {
                GitService.InitGitRepository(rootDirectoryInfo);
            }
        }

        private void CreateFolderStructure(DirectoryInfo root)
        {
            if (!root.Exists)
            {
                root.Create();
            }

            var directoryCreator = new DirectoryCreator(new FileInfo(FolderStructureFile), root);
            directoryCreator.CreateDirectoryStructure();
        }

        private FileInfo CreateSolutionFile(DirectoryInfo root, SolutionModel model)
        {
            var templateToRender = model.IncludeTestProject ? SolutionWithTestTemplate : SolutionTemplate;
            var solutionFile = new FileInfo(string.Format("{0}{1}.sln", root.FullName, model.SolutionName));
            
            File.WriteAllText(solutionFile.FullName, TemplateRenderer.Render(templateToRender, model));

            return solutionFile;
        }

        private FileInfo CreateProjectFile(DirectoryInfo root, SolutionModel model)
        {
            string projectRoot = string.Format("{0}/{1}/", root.FullName, model.ProjectName);
            var directoryInfo = new DirectoryInfo(projectRoot);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            var projectModel = new ProjectModel(model.TestProjectGuid)
            {
                ProjectAssemblyName = model.ProjectAssemblyName,
                ProjectName = model.ProjectName,
                ProjectRootNameSpace = model.ProjectRootNameSpace,
                TargetFramework = model.TargetFramework,
                ReleaseOutputPath = string.Format("../../output/Release/{0}", model.ProjectName),
                DebugOutputPath = string.Format("../../output/Debug/{0}", model.ProjectName),
                ProjectType = model.ProjectType
            };

            projectModel.ProjectOutputType = projectModel.ProjectTypeToProjectOutputType(model.ProjectType);
            projectModel.AddCoreReferences();
            
            var projectFile = new FileInfo(projectRoot + projectModel.ProjectName + ".csproj");
            File.WriteAllText(projectFile.FullName, TemplateRenderer.Render(ProjectTemplate, projectModel));
            
            return projectFile;
        }

        private FileInfo CreateTestProjectFile(DirectoryInfo root, SolutionModel model)
        {
            string projectRoot = string.Format("{0}/{1}.Tests/", root.FullName, model.ProjectName);
            var directoryInfo = new DirectoryInfo(projectRoot);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            var projectName = string.Format("{0}.Tests", model.ProjectName);
            var projectModel = new ProjectModel(model.TestProjectGuid)
            {
                ProjectAssemblyName = string.Format("{0}.Tests", model.ProjectAssemblyName),
                ProjectName = projectName,
                ProjectRootNameSpace = string.Format("{0}.Tests", model.ProjectRootNameSpace),
                TargetFramework = model.TargetFramework,
                ReleaseOutputPath = string.Format("../../output/Release/{0}", projectName),
                DebugOutputPath = string.Format("../../output/Debug/{0}", projectName),
                ProjectOutputType = "Library"
            };

            var projectFile = new FileInfo(projectRoot + projectModel.ProjectName + ".csproj");
            File.WriteAllText(projectFile.FullName, TemplateRenderer.Render(ProjectTemplate, projectModel));

            return projectFile;
        }

        private FileInfo[] CreateSolutionAssets(DirectoryInfo root, SolutionModel model)
        {
            var files = new List<FileInfo>();

            FileInfo solutionFile;

            if (model.IncludeGitAttribute)
            {
                solutionFile = new FileInfo(string.Format("{0}/.gitattributes", root.FullName));
                File.WriteAllText(solutionFile.FullName, TemplateRenderer.Render(GitAttributeTemplate, model));
                files.Add(solutionFile);
            }
            if (model.IncludeGitIgnore)
            {
                solutionFile = new FileInfo(string.Format("{0}/.gitignore", root.FullName));
                File.WriteAllText(solutionFile.FullName, TemplateRenderer.Render(GitIgnoreTemplate, model));
                files.Add(solutionFile);
            }
            if (model.IncludeReadme)
            {
                solutionFile = new FileInfo(string.Format("{0}/README.md", root.FullName));
                File.WriteAllText(solutionFile.FullName, TemplateRenderer.RenderAndRenderContent(ReadmeTemplate, model));
                files.Add(solutionFile);
            }
            if (model.IncludeLicense)
            {
                solutionFile = new FileInfo(string.Format("{0}/License.txt", root.FullName));
                File.WriteAllText(solutionFile.FullName, TemplateRenderer.Render(LicenseTemplate, model));
                files.Add(solutionFile);
            }
            return files.ToArray();
        }

        private FileInfo[] CreateProjectAssets(DirectoryInfo root, SolutionModel model)
        {
            var files = new List<FileInfo>();

            FileInfo solutionFile;
            if (model.IncludeResharper)
            {

                solutionFile = new FileInfo(string.Format("{0}/resharper.settings", root.FullName));
                File.WriteAllText(solutionFile.FullName, TemplateRenderer.Render(ResharperSettingsTemplate, model));
                files.Add(solutionFile);
            }
            if (model.IncludeStylecop)
            {
                solutionFile = new FileInfo(string.Format("{0}/Settings.StyleCop", root.FullName));
                File.WriteAllText(solutionFile.FullName, TemplateRenderer.Render(StyleCopTemplate, model));
                files.Add(solutionFile);
            }
            return files.ToArray();
        }
    }
}
