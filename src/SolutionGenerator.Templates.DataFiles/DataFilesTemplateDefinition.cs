// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataFilesTemplate.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates.DataFiles
{
	using System;
	using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
	using System.Linq;
	using System.Windows;
    using Catel;
	using Catel.IoC;
	using Catel.Logging;
	using Catel.Reflection;
	using Orc.Csv;
	using Services;
	using Views;

    public class DataFilesTemplateDefinition : TemplateDefinitionBase<DataFilesTemplateContext>
    {
		private static readonly ILog Log = LogManager.GetCurrentClassLogger();
		private DataFilesTemplateContext _templateContext = null;


	    public override List<ITemplateFile> GetTemplateFiles()
        {
            var assemblyDirectory = GetType().Assembly.GetDirectory();
            var filesDirectory = Path.Combine(assemblyDirectory, "Files");

            var files = TemplateFileHelper.FindFiles(filesDirectory);
            return files;
        }

        public override FrameworkElement GetView()
        {
            return new SettingsView();
        }

	    public override void PostGenerate()
	    {
			ProcessDataFolder(TemplateContext.Solution.Directory);
			base.PostGenerate();
	    }


		private void ProcessDataFolder(string root)
		{
			_templateContext = (DataFilesTemplateContext) TemplateContext;

			var fileSystemService = ServiceLocator.Default.ResolveType<IFileSystemService>();
			var codeGenerationService = ServiceLocator.Default.ResolveType<ICodeGenerationService>();
			var projectFileService = ServiceLocator.Default.ResolveType<IProjectFileService>();
			var entityPluralService = ServiceLocator.Default.ResolveType<IEntityPluralService>();

			CreateModelFiles(root, fileSystemService, codeGenerationService, projectFileService);
			CreateProgramClassFile(root, fileSystemService, entityPluralService);
		}

		private void CreateModelFiles(string root, IFileSystemService fileSystemService, ICodeGenerationService codeGenerationService, IProjectFileService projectFileService)
		{
			var modelFolder = fileSystemService.Folders(root, "*.*").FirstOrDefault(f => f.ToLower().Contains("\\model"));
			if (modelFolder == null)
			{
				return;
			}

			var files = fileSystemService.Files(_templateContext.Data.DataFolder, "*.csv").ToArray();
			foreach (var file in files)
			{
				try
				{
					codeGenerationService.CreateCSharpFiles(Path.GetFullPath(file), TemplateContext.Solution.Name, modelFolder);
				}
				catch (Exception exception)
				{
					Log.Error($"Can not generate Model and Map files for data file: '{Path.GetFileName(file)}'", exception);
				}
			}

			var referenceName = "operationx";
			var newFiles = fileSystemService.Files(modelFolder, "*.cs").ToArray();

			var toBeRemove = new List<string>();
			foreach (var file in newFiles)
			{
				try
				{
					if (file.ToLower().Contains(referenceName))
					{
						toBeRemove.Add(file);
						continue;
					}
                    if (file.ToLower().EndsWith("map.cs"))
					{
						fileSystemService.Move(file, file.Replace(@"\Model\", @"\Model\Import\"));
					}
				}
				catch (Exception e)
				{
					Log.Error($"Can not process file: '{Path.GetFileName(file)}'", e);
				}

			}
            var placeHolderFiles = fileSystemService.Files(modelFolder, ".placeholder.txt").ToArray();
            toBeRemove.AddRange(placeHolderFiles);

            foreach (var file in toBeRemove)
			{
				try
				{
					fileSystemService.DeleteFile(file);
				}
				catch (Exception e)
				{
					Log.Error($"Can not delete file: '{file}'", e);
				}
			}
		}

		private void CreateProgramClassFile(string root, IFileSystemService fileSystemService, IEntityPluralService entityPluralService)
		{
			var files = fileSystemService.Files(_templateContext.Data.DataFolder, "*.csv").ToArray();

			var programFileName = Path.Combine(root, "Program.cs");
			var inputLines = fileSystemService.ReadAllLines(programFileName);
			var outputLines = new List<string>();

			var prologueLines = GetProgramPrologueLines(inputLines);
			outputLines.AddRange(prologueLines);

			foreach (var file in files)
			{
				try
				{
					var className = entityPluralService.ToSingular(Path.GetFileNameWithoutExtension(file).ToCamelCase());
					var programLines = GetProgramLines(inputLines);
					ReplaceInLines(programLines, "OperationX.csv", Path.GetFileName(file));
					ReplaceInLines(programLines, "OperationX", className);
					ReplaceInLines(programLines, "operationX", Char.ToLowerInvariant(className[0]) + className.Substring(1));
					outputLines.AddRange(programLines);
				}
				catch (Exception e)
				{
					Log.Error("Can not add program line for model class {className}", e);
				}
			}
            outputLines.Add($"{new string(' ', 8)}}}");
            outputLines.Add($"{new string(' ', 4)}}}");
			outputLines.Add("}");

			fileSystemService.WriteAllLines(programFileName, outputLines);
		}

		private void ReplaceInLines(IList<string> testLines, string oldValue, string y)
		{
			for (int index = 0; index < testLines.Count; index++)
			{
				testLines[index] = testLines[index].Replace(oldValue, y);
			}
		}

		private IList<string> GetProgramPrologueLines(IEnumerable<string> sourceLines)
		{
			return sourceLines
				.TakeWhile(line => !line.Contains("var operationXCollection"))
				.Select(line => (string)line.Clone()).ToList();
		}

		private IList<string> GetProgramLines(string[] sourceLines)
		{
			var result = new List<string>();
			var isBodyLine = false;
			foreach (var line in sourceLines)
			{
				if (line.Contains("var operationXCollection"))
				{
					isBodyLine = true;
				}
				if (isBodyLine)
				{
					result.Add((string)line.Clone());
				}
				if (line.Contains("}"))
				{
					break;
				}
			}
			return result;
		}
	}
}