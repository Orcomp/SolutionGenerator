// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISolutionGeneratorService.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Services
{
	using Models;

	public interface ISolutionGeneratorService
	{
		#region Methods
		void DoWork(Solution solution);
		#endregion
	}
}