// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


#region using...
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;

#endregion

[assembly: InternalsVisibleTo("SolutionGenerator.Wpf.Tests")]

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

[assembly: AssemblyTitle("SolutionGenerator.Wpf")]
[assembly: AssemblyDescription("SolutionGenerator.Wpf")]
[assembly: AssemblyProduct("SolutionGenerator.Wpf")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.

[assembly: ThemeInfo(
	ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
	//(used if a resource is not found in the page, 
	// or application resource dictionaries)
	ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
	//(used if a resource is not found in the page, 
	// app, or any theme specific resource dictionaries)
	)]
[assembly: ComVisible(false)]

// For version information see the centralized SolutionAssemblyInfo.cs
// Do not comment out the following lines:
// [assembly: AssemblyVersion("x.x.x.x")]
// [assembly: AssemblyFileVersion("x.x.x.x")]
// [assembly: AssemblyInformationalVersion("x.x.x.x")]