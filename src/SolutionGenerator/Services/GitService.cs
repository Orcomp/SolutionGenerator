// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GitService.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Services
{
	using Catel;
	using LibGit2Sharp;

	public class GitService : IGitService
	{
		#region IGitService Members
		public void InitGitRepository(string directoryName)
		{
			Argument.IsNotNullOrWhitespace(() => directoryName);

			Repository.Init(directoryName);
		}
		#endregion
	}
}