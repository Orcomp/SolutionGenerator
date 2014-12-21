// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITemplateProvider.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Services
{
	using System.Collections.Generic;
	using Models;

	public interface ITemplateProvider
	{
		#region Properties
		IEnumerable<TemplateInfo> Templates { get; }
		#endregion
	}
}