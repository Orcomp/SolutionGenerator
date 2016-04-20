// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateProvider.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Services
{
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text.RegularExpressions;
	using Ionic.Zip;
	using Models;

	public class TemplateProvider : ITemplateProvider
	{
		private const string DescriptionTxt = ".description";

		private readonly IFileSystemService _fileSystemService;

		public TemplateProvider(IFileSystemService fileSystemService)
		{
			_fileSystemService = fileSystemService;
		}

		public IEnumerable<TemplateInfo> Templates
		{
			get
			{
				var templateFileNames = _fileSystemService.Files(Path.GetFullPath(@".\templates"), "*.zip");
				return templateFileNames.Select(GetTemplateInfo).Where(ti => ti != null);
			}
		}

		private TemplateInfo GetTemplateInfo(string fileName)
		{
			try
			{
				var name = Path.GetFileNameWithoutExtension(fileName);
				fileName = Path.GetFullPath(fileName);
				using (var zipFile = ZipFile.Read(_fileSystemService.ReadOnlyStream(fileName)))
				{
					var descriptionEntry = zipFile.FirstOrDefault(entry => entry.FileName.EndsWith(DescriptionTxt));
					if (descriptionEntry == null)
					{
						return new TemplateInfo
						{
							Name = string.Format("{0} (no friendly name provided)", name),
							Description = string.Format("{0} (no description provided)", name),
							FileName = fileName
						};
					}
					using (var stream = new MemoryStream())
					{
						descriptionEntry.Extract(stream);
						stream.Seek(0, SeekOrigin.Begin);
						return GetTemplateInfo(stream, fileName);
					}
				}
			}
			catch
			{
				return null;
			}
		}

		private TemplateInfo GetTemplateInfo(Stream stream, string fileName)
		{
			string text;
			using (var reader = new StreamReader(stream))
			{
				text = reader.ReadToEnd();
			}
			return GetTemplateInfo(text, fileName);
		}

		private TemplateInfo GetTemplateInfo(string text, string fileName)
		{
			var properties = new Dictionary<string, string>();
			foreach (var line in Regex.Split(text, "\r\n|\r|\n"))
			{
				if (string.IsNullOrEmpty(line))
				{
					continue;
				}
				if (line.StartsWith("#"))
				{
					continue;
				}
				var kvpText = line.Split('\t');
				if (kvpText.Length != 2)
				{
					kvpText = line.Split(',');
				}
				if (kvpText.Length != 2)
				{
					kvpText = line.Split('=');
				}
				if (kvpText.Length != 2)
				{
					continue;
				}
				if (string.IsNullOrEmpty(kvpText[0]))
				{
					continue;
				}
				var key = kvpText[0].Trim().ToLower();
				if (!properties.ContainsKey(key))
				{
					properties.Add(key, kvpText[1].Trim());
				}
			}
			fileName = Path.GetFullPath(fileName);
			var defaultName = Path.GetFileNameWithoutExtension(fileName);
			string name;
			string description;
			string isDefaultString;
			bool isDefault;
			properties.TryGetValue("name", out name);
			properties.TryGetValue("description", out description);
			properties.TryGetValue("isdefault", out isDefaultString);
			bool.TryParse(isDefaultString, out isDefault);

			return new TemplateInfo
			{
				Name = name ?? string.Format("{0} (no friendly name provided)", defaultName),
				Description = (description ?? string.Format("{0} (no description provided)", defaultName)).Replace('@', '\n'),
				FileName = fileName,
				IsDefault = isDefault
			};
		}
	}
}