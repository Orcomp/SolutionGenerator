// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IncludeOptionsViewModel.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Wpf.ViewModels
{
	using Catel;
	using Catel.MVVM;
	using Models;

	public class IncludeOptionsViewModel : ViewModelBase
	{
		public IncludeOptionsViewModel(Solution solution)
		{
			Argument.IsNotNull(() => solution);

			Solution = solution;
		}

		[Model]
		[Catel.Fody.Expose("IncludeGitAttribute")]
		[Catel.Fody.Expose("IncludeGitIgnore")]
		[Catel.Fody.Expose("IncludeReadme")]
		[Catel.Fody.Expose("IncludeReSharper")]
		[Catel.Fody.Expose("IncludeStylecop")]
		[Catel.Fody.Expose("IncludeTestProject")]
		public Solution Solution { get; private set; }
	}
}