// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModuleInitializer.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2016 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator
{
	using Catel.IoC;
	using Services;

	/// <summary>
	/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
	/// </summary>
	public static class ModuleInitializer
	{
		/// <summary>
		/// Initializes the module.
		/// </summary>
		public static void Initialize()
		{
			var serviceLocator = IoCConfiguration.DefaultServiceLocator;

			//serviceLocator.RegisterType<IDirectoryCreator, DirectoryCreator>();
			//serviceLocator.RegisterType<ISolutionGeneratorService, SolutionGeneratorService>();
			//serviceLocator.RegisterType<IReferencesService, ReferencesService>();
			//serviceLocator.RegisterType<IProjectTypeConverterService, ProjectTypeConverterService>();

			serviceLocator.RegisterType<ITemplateRenderer, TemplateRenderer>();
			serviceLocator.RegisterType<IGitService, GitService>();

			serviceLocator.RegisterType<ISolutionGeneratorService, SolutionGeneratorService>();
			serviceLocator.RegisterType<ITemplateProvider, TemplateProvider>();
			serviceLocator.RegisterType<IFileSystemService, FileSystemService>();
		}
	}
}