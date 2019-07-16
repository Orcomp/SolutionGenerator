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

            // Extensibility => to be moved - START


            serviceLocator.RegisterType<IPluralizationService, EnglishPluralizationService>();

        }
    }
}
