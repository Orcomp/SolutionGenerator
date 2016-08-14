// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GitHubTemplate.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates
{
    public class GitHubTemplate : TemplateBase
    {
        public GitHubTemplate()
        {
            
        }

        public string Company { get; set; }

        public string RepositoryName { get; set; }
    }
}