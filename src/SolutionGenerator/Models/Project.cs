// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Project.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Models
{
	using Catel;
	using Catel.Data;

	public class Project : ModelBase
	{
		#region Constructors
		public Project(string projectGuid)
		{
			Argument.IsNotNullOrWhitespace(() => projectGuid);

			ProjectGuid = projectGuid;
			ProjectReferences = string.Empty;
			FileIncludes = string.Empty;
		}
		#endregion

		#region Properties
		public string ProjectName { get; set; }
		public string ProjectAssemblyName { get; set; }
		public string ProjectRootNameSpace { get; set; }
		public string ProjectGuid { get; set; }

		public string DebugOutputPath { get; set; }
		public string ReleaseOutputPath { get; set; }

		public string TargetFramework { get; set; }
		public ProjectOutputTypes ProjectOutputType { get; set; }
		public ProjectTypes ProjectType { get; set; }

		public string ProjectReferences { get; set; }
		public string FileIncludes { get; set; }
		#endregion

		#region Methods
		public override string ToString()
		{
			return ProjectName ?? string.Empty;
		}
		#endregion
	}
}