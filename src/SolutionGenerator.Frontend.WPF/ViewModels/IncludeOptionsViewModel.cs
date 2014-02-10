// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IncludeOptionsViewModel.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2013 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.ViewModels
{
    using Catel;
    using Catel.MVVM;
    using SolutionGenerator.Models;

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