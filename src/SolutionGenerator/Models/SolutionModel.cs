﻿namespace SolutionGenerator.Models
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

        public SolutionModel()
        {
            ProjectGuid = Guid.NewGuid().ToString("B");
            SolutionGuid = Guid.NewGuid().ToString("B");
            TestProjectGuid = Guid.NewGuid().ToString("B");
            TargetFramework = "v4.5";
            SolutionReadme = "# the real project name.\r\n\r\n----------------------------------------------";
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
    }
}