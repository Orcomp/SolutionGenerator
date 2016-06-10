// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModuleInitializer.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2016 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator
{
    using System.Data.Entity.Infrastructure.Pluralization;
    using Catel.IoC;
    using Extensibility;
    using Orc.Csv;
    using Services;
    using Orchestra.Services;
    using Orc.Extensibility;

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

            // Extensibility => to be moved - START

            serviceLocator.RegisterType<IPluginCleanupService, PluginCleanupService>();
            serviceLocator.RegisterType<IPluginLocationsProvider, PluginLocationsProvider>();
            serviceLocator.RegisterType<IPluginManager, PluginManager>();
            serviceLocator.RegisterType<IPluginFactory, PluginFactory>();
            serviceLocator.RegisterType<IPluginInfoProvider, PluginInfoProvider>();

            serviceLocator.RegisterType<ISinglePluginService, SinglePluginService>();
            serviceLocator.RegisterType<IMultiplePluginsService, MultiplePluginsService>();

            // Extensibility => to be moved - END

            // App specific plugin stuff
            serviceLocator.RegisterType<IPluginFinder, PluginFinder>();

            //serviceLocator.RegisterType<IDirectoryCreator, DirectoryCreator>();
            //serviceLocator.RegisterType<ISolutionGeneratorService, SolutionGeneratorService>();
            //serviceLocator.RegisterType<IReferencesService, ReferencesService>();
            //serviceLocator.RegisterType<IProjectTypeConverterService, ProjectTypeConverterService>();

            serviceLocator.RegisterType<IGitService, GitService>();
            serviceLocator.RegisterType<ISolutionGeneratorService, SolutionGeneratorService>();
            serviceLocator.RegisterType<IFileSystemService, FileSystemService>();
            serviceLocator.RegisterType<IProjectFileService, ProjectFileService>();

            serviceLocator.RegisterType<ICodeGenerationService, CodeGenerationService>();
            serviceLocator.RegisterType<IEntityPluralService, EfEntityPluralService>();
            serviceLocator.RegisterType<IPluralizationService, EnglishPluralizationService>();

            serviceLocator.RegisterType<ITaskRunnerService, TaskRunnerService>();
        }
    }
}