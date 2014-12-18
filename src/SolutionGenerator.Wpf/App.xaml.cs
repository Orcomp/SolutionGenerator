// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Wpf
{
	using System;
	using System.Windows;
	using Catel.IoC;
	using Catel.Logging;
	using Catel.Windows;
	using Models;
	using Services;
	using Services.Interfaces;

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		#region Methods
		/// <summary>
		/// Raises the <see cref="E:System.Windows.Application.Startup" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
		protected override void OnStartup(StartupEventArgs e)
		{
#if DEBUG
			LogManager.AddDebugListener();
#endif

			StyleHelper.CreateStyleForwardersForDefaultStyles();

			var serviceLocator = ServiceLocator.Default;

			serviceLocator.RegisterInstance<IConsoleService>(new ConsoleService());

			// Force load assembly
			Console.WriteLine(typeof (Solution));

			base.OnStartup(e);
		}
		#endregion
	}
}