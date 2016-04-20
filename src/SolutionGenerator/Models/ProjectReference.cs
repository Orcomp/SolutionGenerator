// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectReference.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Models
{
	using System.ComponentModel;
	using Catel;
	using Catel.Data;

	public class ProjectReference : ModelBase
	{
		public ProjectReference(string name)
		{
			Argument.IsNotNull(() => name);

			Name = name;
		}

		public string Name { get; }

		[DefaultValue("")]
		public string ProjectReferences { get; set; }

		[DefaultValue("")]
		public string FileIncludes { get; set; }

		public override string ToString()
		{
			return Name ?? string.Empty;
		}
	}
}