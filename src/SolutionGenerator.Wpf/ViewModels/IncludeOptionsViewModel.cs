// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IncludeOptionsViewModel.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Wpf.ViewModels
{
	using Catel;
	using Catel.MVVM;
	using Models;

	public class IncludeOptionsViewModel : ViewModelBase
	{
		#region Constructors
		public IncludeOptionsViewModel(Solution solution)
		{
			Argument.IsNotNull(() => solution);

			Solution = solution;
		}
		#endregion

		#region Properties
		[Model]
		[Catel.Fody.Expose("IncludeGitAttribute")]
		[Catel.Fody.Expose("IncludeGitIgnore")]
		[Catel.Fody.Expose("IncludeReadme")]
		[Catel.Fody.Expose("IncludeReSharper")]
		[Catel.Fody.Expose("IncludeStylecop")]
		[Catel.Fody.Expose("IncludeTestProject")]
		public Solution Solution { get; private set; }
		#endregion
	}
}