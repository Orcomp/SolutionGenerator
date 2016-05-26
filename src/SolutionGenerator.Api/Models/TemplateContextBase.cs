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
            NuGet = new NuGetTemplate();
            DateTime = new DateTimeTemplate();
        }

        public CompanyTemplate Company { get; protected set; }

        public SolutionTemplate Solution { get; protected set; }

        public NuGetTemplate NuGet { get; protected set; }

        public DateTimeTemplate DateTime { get; protected set; }
    }
}