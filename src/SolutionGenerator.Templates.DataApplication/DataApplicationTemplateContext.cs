// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataApplicationTemplateContext.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates.DataApplication
{
    using System.Reflection;
    using Catel.Reflection;

    public class DataApplicationTemplateContext : TemplateContextBase
    {
        public DataApplicationTemplateContext()
        {
            var assembly = GetType().GetAssemblyEx();

            Company.Name = assembly.Company();

            Solution.Name = "Orc.";

			Data = new DataTemplate();
			Data.DataFolder = "";

			GitHub = new GitHubTemplate();
            GitHub.Company = assembly.Company();
            GitHub.RepositoryName = "Orc.";
        }

		public DataTemplate Data { get; protected set; }

		public GitHubTemplate GitHub { get; protected set; }
    }
}