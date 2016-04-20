// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateInfo.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Models
{
	using System;
	using Catel.Data;

	[Serializable]
	public class TemplateInfo : ModelBase
	{
		public bool IsDefault { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string FileName { get; set; }
	}
}