// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDirectoryCreator.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Services
{
	using System.IO;

	public interface IDirectoryCreator
	{
		#region Properties
		FileInfo FolderConfigFile { get; set; }
		DirectoryInfo RootFolder { get; set; }
		#endregion

		#region Methods
		void CreateDirectoryStructure();
		#endregion
	}
}