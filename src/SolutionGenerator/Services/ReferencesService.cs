// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReferencesService.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Services
{
	using System;
	using Catel;
	using Catel.Logging;
	using Models;

	public class ReferencesService : IReferencesService
	{
		#region Constants
		private static readonly ILog Log = LogManager.GetCurrentClassLogger();
		#endregion

		#region IReferencesService Members
		public void AddRequiredReferences(Project project)
		{
			Argument.IsNotNull(() => project);

			var coreReferences = GetCoreReferences();

			ProjectReference additionalReferences = null;

			switch (project.ProjectType)
			{
				case ProjectTypes.Console:
					additionalReferences = GetConsoleReferences();
					break;

				case ProjectTypes.WPF:
					additionalReferences = GetWpfReference();
					break;

				case ProjectTypes.WinForms:
					additionalReferences = GetWinFormsReferences();
					break;

				case ProjectTypes.Test:
					additionalReferences = GetNUnitReferences();
					break;

				case ProjectTypes.Library:
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}

			if (coreReferences != null)
			{
				AddRequiredReferences(project, coreReferences);
			}

			if (additionalReferences != null)
			{
				AddRequiredReferences(project, additionalReferences);
			}
		}

		public void AddRequiredReferences(Project project, ProjectReference projectReference)
		{
			Argument.IsNotNull(() => project);
			Argument.IsNotNull(() => projectReference);

			Log.Info("Adding reference '{0}' to project '{1}'", projectReference, project);

			project.ProjectReferences += projectReference.ProjectReferences;
			project.FileIncludes += projectReference.FileIncludes;
		}

		public ProjectReference GetNUnitReferences()
		{
			var reference = new ProjectReference("NUnit");

			reference.ProjectReferences += @"
    <Reference Include=""nunit.framework, Version=2.6.3.13283, Culture=neutral, PublicKeyToken=96d09a1eb7f44a77, processorArchitecture=MSIL"">
        <HintPath>..\packages\NUnit.2.6.3\lib\nunit.framework.dll</HintPath>
    </Reference>";

			reference.FileIncludes += @"  
    <ItemGroup>
        <None Include=""packages.config"" />
    </ItemGroup>";

			return reference;
		}

		public ProjectReference GetWinFormsReferences()
		{
			var reference = new ProjectReference("WinForms");

			reference.ProjectReferences += @"    
    <Reference Include=""System.Deployment"" />
    <Reference Include=""System.Drawing"" />
    <Reference Include=""System.Windows.Forms"" />";

			reference.FileIncludes += @"
  <ItemGroup>
    <Compile Include=""Form1.cs"">
      <SubType>Form</SubType>
    </Compile>
    <Compile Include=""Form1.Designer.cs"">
      <DependentUpon>Form1.cs</DependentUpon>
    </Compile>
    <Compile Include=""Program.cs"" />
  </ItemGroup>";

			return reference;
		}

		public ProjectReference GetCoreReferences()
		{
			var reference = new ProjectReference("Core");

			reference.ProjectReferences += @"    
    <Reference Include=""System"" />
    <Reference Include=""System.Core"" />
    <Reference Include=""System.Xml.Linq"" />
    <Reference Include=""System.Data.DataSetExtensions"" />
    <Reference Include=""Microsoft.CSharp"" />
    <Reference Include=""System.Data"" />
    <Reference Include=""System.Xml"" />";

			return reference;
		}

		public ProjectReference GetWpfReference()
		{
			var reference = new ProjectReference("WPF");

			reference.ProjectReferences += @"
    <Reference Include=""System.Xaml"">
      <RequiredTargetFramework>4.0</RequiredTargetFramework>
    </Reference>
    <Reference Include=""WindowsBase"" />
    <Reference Include=""PresentationCore"" />
    <Reference Include=""PresentationFramework"" />";

			reference.FileIncludes = @"  
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

			return reference;
		}

		public ProjectReference GetConsoleReferences()
		{
			var reference = new ProjectReference("Console");

			reference.FileIncludes += @"
  <ItemGroup>
    <Compile Include=""Program.cs"" />
  </ItemGroup>";

			return reference;
		}
		#endregion
	}
}