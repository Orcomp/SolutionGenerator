namespace SolutionGenerator.Models
{
    using Base;

    public class ProjectModel : ModelBase
    {
        public ProjectModel(string projectGuid)
        {
            ProjectGuid = projectGuid;
            ProjectReferences = string.Empty;
            FileIncludes = string.Empty;
        }

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
        public string FileIncludes { get; set; }

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

            FileIncludes += @"  
    <ItemGroup>
        <None Include=""packages.config"" />
    </ItemGroup>";
        }

        public void AddWinformsReferences()
        {
            ProjectReferences += @"    
    <Reference Include=""System.Deployment"" />
    <Reference Include=""System.Drawing"" />
    <Reference Include=""System.Windows.Forms"" />";

            FileIncludes += @"
  <ItemGroup>
    <Compile Include=""Form1.cs"">
      <SubType>Form</SubType>
    </Compile>
    <Compile Include=""Form1.Designer.cs"">
      <DependentUpon>Form1.cs</DependentUpon>
    </Compile>
    <Compile Include=""Program.cs"" />
  </ItemGroup>";
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

            FileIncludes = @"  
  <ItemGroup>
    <ApplicationDefinition Include=""App.xaml"">
      <Generator>MSBuild:Compile</Generator>
      <SubType>Designer</SubType>
    </ApplicationDefinition>
    <Page Include=""MainWindow.xaml"">
      <Generator>MSBuild:Compile</Generator>
      <SubType>Designer</SubType>
    </Page>
    <Compile Include=""App.xaml.cs"">
      <DependentUpon>App.xaml</DependentUpon>
      <SubType>Code</SubType>
    </Compile>
    <Compile Include=""MainWindow.xaml.cs"">
      <DependentUpon>MainWindow.xaml</DependentUpon>
      <SubType>Code</SubType>
    </Compile>
  </ItemGroup>";
        }

        public void AddNeededReferences()
        {
            switch (ProjectType)
            {
                case "Console":
                    AddConsoleReferences();
                    break;
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

        private void AddConsoleReferences()
        {
            FileIncludes += @"
  <ItemGroup>
    <Compile Include=""Program.cs"" />
  </ItemGroup>";
        }
    }
}