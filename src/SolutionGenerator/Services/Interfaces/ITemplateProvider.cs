// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITemplateProvider.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Services
{
	using System.Collections.Generic;
	using Models;

	public interface ITemplateProvider
	{
		IEnumerable<TemplateInfo> Templates { get; }
	}
}