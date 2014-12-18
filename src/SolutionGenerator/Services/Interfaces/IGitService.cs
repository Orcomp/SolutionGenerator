// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGitService.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Services
{
	public interface IGitService
	{
		#region Methods
		void InitGitRepository(string directoryName);
		#endregion
	}
}