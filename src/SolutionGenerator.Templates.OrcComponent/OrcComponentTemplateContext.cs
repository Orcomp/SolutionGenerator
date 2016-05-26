// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrcComponentTemplateContext.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates.OrcComponent
{
    using System.Reflection;
    using Catel.Reflection;

    public class OrcComponentTemplateContext : TemplateContextBase
    {
        public OrcComponentTemplateContext()
        {
            var assembly = GetType().GetAssemblyEx();

            Company.Name = assembly.Company();

            Solution.Name = "Orc.";

            NuGet.PackageName = "Orc.";

            GitHub = new GitHubTemplate();
            GitHub.Company = assembly.Company();
            GitHub.RepositoryName = "Orc.";
        }

        public GitHubTemplate GitHub { get; protected set; }
    }
}