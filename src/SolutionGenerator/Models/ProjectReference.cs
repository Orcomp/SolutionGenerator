// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectReference.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Models
{
	using System.ComponentModel;
	using Catel;
	using Catel.Data;

	public class ProjectReference : ModelBase
	{
		#region Constructors
		public ProjectReference(string name)
		{
			Argument.IsNotNull(() => name);

			Name = name;
		}
		#endregion

		#region Properties
		public string Name { get; private set; }

		[DefaultValue("")]
		public string ProjectReferences { get; set; }

		[DefaultValue("")]
		public string FileIncludes { get; set; }
		#endregion

		#region Methods
		public override string ToString()
		{
			return Name ?? string.Empty;
		}
		#endregion
	}
}