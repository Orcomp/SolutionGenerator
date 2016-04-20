// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGitService.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Services
{
	public interface IGitService
	{
		void InitGitRepository(string directoryName);
	}
}