// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateContextBase.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator
{
    using Templates;

    public abstract class TemplateContextBase : ITemplateContext
    {
        protected TemplateContextBase()
        {
            Company = new CompanyTemplate();
            Solution = new SolutionTemplate();
            DateTime = new DateTimeTemplate();
            Guid = new GuidTemplate();
        }

        public CompanyTemplate Company { get; protected set; }

        public SolutionTemplate Solution { get; protected set; }

        public DateTimeTemplate DateTime { get; protected set; }

        public GuidTemplate Guid { get; protected set; }
    }
}