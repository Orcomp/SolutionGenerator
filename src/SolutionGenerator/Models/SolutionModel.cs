namespace SolutionGenerator.Models
{
    using System;
    using Base;

    public class SolutionModel : ModelBase
    {
        private string _solutionGuid;
        private string _projectGuid;
        private string _targetFramework;
        private string _projectAssemblyName;
        private string _projectRootNameSpace;
        private string _rootPath;
        private string _solutionName;
        private string _projectName;
        private string _testProjectGuid;
        private string _solutionReadme;
        private bool _includeReadme;
        private string _projectType;

        public SolutionModel()
        {
            ProjectGuid = Guid.NewGuid().ToString("B");
            SolutionGuid = Guid.NewGuid().ToString("B");
            TestProjectGuid = Guid.NewGuid().ToString("B");
            TargetFramework = "v4.5";
            SolutionReadme = "{ProjectName}.\r\n----------------------------------------------";

            IncludeTestProject    = true;
            IncludeGitIgnore      = true;
            IncludeGitAttribute   = true;
            IncludeResharper      = true;
            IncludeStylecop       = true;
            IncludeLicense        = true;
            IncludeReadme = true;

        }

        public bool InitiliazeGit { get; set; }
        public bool IncludeTestProject { get; set; }
        public bool IncludeGitIgnore { get; set; }
        public bool IncludeGitAttribute { get; set; }
        public bool IncludeResharper { get; set; }
        public bool IncludeStylecop { get; set; }
        public bool IncludeLicense { get; set; }
        public bool IncludeReadme
        {
            get { return _includeReadme; }
            set
            {
                if (value == _includeReadme)
                {
                    return;
                }
                _includeReadme = value;
                OnPropertyChanged();
            }
        }

        public string SolutionReadme
        {
            get
            {
                return _solutionReadme;
            }
            set
            {
                if (value == _solutionReadme)
                {
                    return;
                }
                _solutionReadme = value;
                OnPropertyChanged();
            }
        }

        public string TestProjectGuid
        {
            get
            {
                return _testProjectGuid;
            }
            set
            {
                if (value == _testProjectGuid)
                {
                    return;
                }
                _testProjectGuid = value;
                OnPropertyChanged();
            }
        }

        public string SolutionGuid
        {
            get
            {
                return _solutionGuid;
            }
            set
            {
                if (value == _solutionGuid)
                {
                    return;
                }
                _solutionGuid = value;
                OnPropertyChanged();
            }
        }

        public string ProjectGuid
        {
            get
            {
                return _projectGuid;
            }
            set
            {
                if (value == _projectGuid)
                {
                    return;
                }
                _projectGuid = value;
                OnPropertyChanged();
            }
        }

        public string ProjectName
        {
            get
            {
                return _projectName;
            }
            set
            {
                if (value == _projectName)
                {
                    return;
                }
                _projectName = value;
                OnPropertyChanged();
            }
        }

        public string SolutionName
        {
            get
            {
                return _solutionName;
            }
            set
            {
                if (value == _solutionName)
                {
                    return;
                }
                _solutionName = value;
                OnPropertyChanged();
            }
        }

        public string RootPath
        {
            get
            {
                return _rootPath;
            }
            set
            {
                if (value == _rootPath)
                {
                    return;
                }
                _rootPath = value;
                OnPropertyChanged();
            }
        }

        public string ProjectRootNameSpace
        {
            get
            {
                return _projectRootNameSpace;
            }
            set
            {
                if (value == _projectRootNameSpace)
                {
                    return;
                }
                _projectRootNameSpace = value;
                OnPropertyChanged();
            }
        }

        public string ProjectAssemblyName
        {
            get
            {
                return _projectAssemblyName;
            }
            set
            {
                if (value == _projectAssemblyName)
                {
                    return;
                }
                _projectAssemblyName = value;
                OnPropertyChanged();
            }
        }

        public string TargetFramework
        {
            get
            {
                return _targetFramework;
            }
            set
            {
                if (value == _targetFramework)
                {
                    return;
                }
                _targetFramework = value;
                OnPropertyChanged();
            }
        }

        public string ProjectType
        {
            get { return _projectType; }
            set
            {
                if (value == _projectType)
                {
                    return;
                }
                _projectType = value;
                OnPropertyChanged();
            }
        }
    }
}