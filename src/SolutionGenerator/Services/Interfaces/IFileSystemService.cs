// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFileSystemService.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Services
{
	using System.Collections.Generic;
	using System.IO;

	public interface IFileSystemService
	{
		IEnumerable<string> Files(string root, string pattern = "*.*", bool recurse = true);
		IEnumerable<string> Files(string root, IEnumerable<string> patterns, bool recurse = true);
		IEnumerable<string> Folders(string root, string pattern = "*.*", bool recurse = true);
		IEnumerable<string> Folders(string root, IEnumerable<string> patterns, bool recurse = true);
		void DeleteFolder(string folderName);

		Stream ReadOnlyStream(string fileName);
		void Rename(string from, string to, string root, IEnumerable<string> patterns, bool folders = true, bool recurse = true);
		void Replace(string from, string to, string root, IEnumerable<string> patterns, bool recurse = true);
		void CreateFolder(string parent, string name = null);

		string NormalizePath(string path);
		void DeleteFile(string fileName);
		void Copy(string source, string target);

		string[] ReadAllLines(string fileName);
		void WriteAllLines(string fileName, IEnumerable<string> contents);
		void Move(string source, string target);
	}
}