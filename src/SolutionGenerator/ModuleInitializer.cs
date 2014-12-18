using Catel.IoC;
using SolutionGenerator.Services;

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

        serviceLocator.RegisterType<IDirectoryCreator, DirectoryCreator>();
        serviceLocator.RegisterType<IGitService, GitService>();
        serviceLocator.RegisterType<ISolutionGeneratorService, SolutionGeneratorService>();
        serviceLocator.RegisterType<ITemplateRenderer, TemplateRenderer>();
        serviceLocator.RegisterType<IReferencesService, ReferencesService>();
        serviceLocator.RegisterType<IProjectTypeConverterService, ProjectTypeConverterService>();
    }
}