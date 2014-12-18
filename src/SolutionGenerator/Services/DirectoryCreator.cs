// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectoryCreator.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Services
{
	using System.IO;

	public class DirectoryCreator : IDirectoryCreator
	{
		#region Constructors
		public DirectoryCreator(FileInfo folderConfigFile, DirectoryInfo rootFolder)
		{
			FolderConfigFile = folderConfigFile;
			RootFolder = rootFolder;
		}
		#endregion

		#region IDirectoryCreator Members
		public FileInfo FolderConfigFile { get; set; }
		public DirectoryInfo RootFolder { get; set; }

		public void CreateDirectoryStructure()
		{
			var directoriesToCreate = File.ReadAllLines(FolderConfigFile.FullName);

			foreach (var dir in directoriesToCreate)
			{
				RootFolder.CreateSubdirectory(dir);
			}
		}
		#endregion
	}
}