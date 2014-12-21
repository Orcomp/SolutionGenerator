// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateInfo.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Models
{
	using System;
	using Catel.Data;

	[Serializable]
	public class TemplateInfo : ModelBase
	{
		#region Properties
		public bool IsDefault { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string FileName { get; set; }
		#endregion
	}
}