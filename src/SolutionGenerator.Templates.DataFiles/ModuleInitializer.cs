// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModuleInitializer.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator
{
	using Catel.IoC;
	using Orc.Csv;

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
			serviceLocator.RegisterType<ICodeGenerationService, CodeGenerationService>();
		}
	}
}