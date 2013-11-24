namespace SolutionGenerator.Models
{
    using Base;

    public class ProjectModel : ModelBase
    {
        public string ProjectName { get; set; }
        public string ProjectAssemblyName { get; set; }
        public string ProjectRootNameSpace { get; set; }
        public string ProjectGuid { get; set; }

        public string DebugOutputPath { get; set; }
        public string ReleaseOutputPath { get; set; }

        public string TargetFramework { get; set; }
        public string ProjectOutputType { get; set; }
        public string ProjectType { get; set; }

        public string ProjectReferences { get; set; }

        public ProjectModel(string projectGuid)
        {
            ProjectGuid = projectGuid;
            ProjectReferences = string.Empty;
        }

        public string ProjectTypeToProjectOutputType(string projectType)
        {
            switch (projectType)
            {
                case "Library":
                    return "Library";
                case "WPF":
                    return "WinExe";
                case "WinForms":
                    return "WinExe";
                case "Console":
                    return "Exe";
            }

            return string.Empty;
        }

        public void AddNUnitReference()
        {
            ProjectReferences += @"
    <Reference Include=""nunit.framework, Version=2.6.3.13283, Culture=neutral, PublicKeyToken=96d09a1eb7f44a77, processorArchitecture=MSIL"">
        <HintPath>..\packages\NUnit.2.6.3\lib\nunit.framework.dll</HintPath>
    </Reference>";
        }

        public void AddWinformsReferences()
        {
            ProjectReferences += @"    
    <Reference Include=""System.Deployment"" />
    <Reference Include=""System.Drawing"" />
    <Reference Include=""System.Windows.Forms"" />";
        }

        public void AddCoreReferences()
        {
            ProjectReferences += @"    
    <Reference Include=""System"" />
    <Reference Include=""System.Core"" />
    <Reference Include=""System.Xml.Linq"" />
    <Reference Include=""System.Data.DataSetExtensions"" />
    <Reference Include=""Microsoft.CSharp"" />
    <Reference Include=""System.Data"" />
    <Reference Include=""System.Xml"" />";
            AddNeededReferences();
        }

        public void AddWpfReference()
        {
            ProjectReferences += @"
    <Reference Include=""System.Xaml"">
      <RequiredTargetFramework>4.0</RequiredTargetFramework>
    </Reference>
    <Reference Include=""WindowsBase"" />
    <Reference Include=""PresentationCore"" />
    <Reference Include=""PresentationFramework"" />";
        }

        public void AddNeededReferences()
        {
            switch (ProjectType)
            {
                case "WPF":
                    AddWpfReference();
                    break;
                case "WinForms":
                    AddWinformsReferences();
                    break;
                case "Test":
                    AddNUnitReference();
                    break;
            }
        }
    }
}