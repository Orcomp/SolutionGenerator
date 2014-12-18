// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProjectTypeConverterService.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Services
{
	public interface IProjectTypeConverterService
	{
		#region Methods
		ProjectOutputTypes Convert(ProjectTypes projectType);
		#endregion
	}
}