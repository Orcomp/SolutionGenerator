// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileSystemService.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Services
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	public class FileSystemService : IFileSystemService
	{
		public IEnumerable<string> Files(string root, string pattern = "*.*", bool recurse = true)
		{
			var files = Directory.GetFiles(root, pattern, recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
			return files;
		}

		public IEnumerable<string> Files(string root, IEnumerable<string> patterns, bool recurse = true)
		{
			return patterns.SelectMany(pattern => Files(root, pattern, recurse));
		}

		public Stream ReadOnlyStream(string fileName)
		{
			return new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		public void Rename(string @from, string to, string root, IEnumerable<string> patterns, bool folders, bool recurse = true)
		{
			if (folders)
			{
				bool found;
				do
				{
					found = false;
					foreach (var folderName in Folders(root, recurse).ToArray().OrderBy(fn => fn.Length))
					{
						var fullSourceFolderName = Path.GetFullPath(folderName);
						var fullDestinationFolderName = ReplaceLastOccurrence(fullSourceFolderName, @from, to);
						if (fullDestinationFolderName == fullSourceFolderName)
						{
							continue;
						}
						Directory.Move(fullSourceFolderName, fullDestinationFolderName);
						found = true;
						// Restart:
						break;
					}
				} while (found);
			}

			foreach (var fileName in Files(root, patterns, recurse).ToArray())
			{
				var fullSourceFileName = Path.GetFullPath(fileName);
				var fullDestinationFileName = fullSourceFileName.Replace(@from, to);
				if (fullDestinationFileName == fullSourceFileName)
				{
					continue;
				}
				File.Move(fullSourceFileName, fullDestinationFileName);
			}

			// TODO: This will possibly work with common folderstructure of a solution, 
			// may not work in case if parent folder renamed over an also renamebly subfolder
		}

		public void Replace(string @from, string to, string root, IEnumerable<string> patterns, bool recurse = true)
		{
			foreach (var file in Files(root, patterns, recurse))
			{
				var fullFileName = Path.GetFullPath(file);
				var text = File.ReadAllText(fullFileName);
				text = text.Replace(from, to);
				text = text.Replace(from.ToLower(), to.ToLower());
				text = text.Replace(from.ToUpper(), to.ToUpper());

				File.WriteAllText(fullFileName, text);
			}
		}

		public void CreateFolder(string parent, string name)
		{
			Directory.CreateDirectory(Path.GetFullPath(NormalizePath(string.Format(@"{0}\{1}", parent.Trim('\\'), name ?? string.Empty))));
		}

		public string NormalizePath(string path)
		{
			return Path.GetFullPath(new Uri(path).LocalPath)
				.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
		}

		private IEnumerable<string> Folders(string root, bool recurse = true)
		{
			return Directory.GetDirectories(root, "*", recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
		}

		private string ReplaceLastOccurrence(string source, string @from, string to)
		{
			var index = source.LastIndexOf(@from, StringComparison.Ordinal);
			if (index == -1)
			{
				return source;
			}
			var result = source.Remove(index, @from.Length).Insert(index, to);
			return result;
		}
	}
}