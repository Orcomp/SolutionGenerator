// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestTemplate.cs" company="WildGums">
//   Copyright (c) 2012 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Tests.Templates.TestModels
{
    using SolutionGenerator.Templates;

    [Abbreviation("SOLUTION")]
    public class TestSolutionTemplate : SolutionTemplate
    {
        public TestSolutionTemplate()
        {
            Directory = "c:\\temp\\";
            Name = "SomeSolution";
            IsAdvanced = true;
            IsSuperAdvanced = true;
        }

        public bool IsAdvanced { get; set; }
        public bool IsSuperAdvanced { get; set; }
    }
}