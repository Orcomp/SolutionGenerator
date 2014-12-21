// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFileSystemService.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Services
{
	using System.Collections.Generic;
	using System.IO;

	public interface IFileSystemService
	{
		#region Methods
		IEnumerable<string> Files(string root, string pattern = "*.*", bool recurse = true);
		IEnumerable<string> Files(string root, IEnumerable<string> patterns, bool recurse = true);
		Stream ReadOnlyStream(string fileName);
		void Rename(string from, string to, string root, IEnumerable<string> patterns, bool folders = true, bool recurse = true);
		void Replace(string from, string to, string root, IEnumerable<string> patterns, bool recurse = true);
		void CreateFolder(string parent, string name = null);

		string NormalizePath(string path);
		#endregion
	}
}