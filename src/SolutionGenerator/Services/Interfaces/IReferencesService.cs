// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReferencesService.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Services
{
	using Models;

	public interface IReferencesService
	{
		#region Methods
		void AddRequiredReferences(Project project);
		void AddRequiredReferences(Project project, ProjectReference projectReference);
		ProjectReference GetNUnitReferences();
		ProjectReference GetWinFormsReferences();
		ProjectReference GetCoreReferences();
		ProjectReference GetWpfReference();
		ProjectReference GetConsoleReferences();
		#endregion
	}
}