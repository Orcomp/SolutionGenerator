// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GitService.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Services
{
	using Catel;
	using LibGit2Sharp;

	public class GitService : IGitService
	{
		public void InitGitRepository(string directoryName)
		{
			Argument.IsNotNullOrWhitespace(() => directoryName);
			Repository.Init(directoryName);
		}
	}
}