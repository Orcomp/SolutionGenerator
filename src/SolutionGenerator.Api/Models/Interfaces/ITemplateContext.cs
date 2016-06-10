// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITemplateContext.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator
{
    using Templates;

    public interface ITemplateContext
    {
        SolutionTemplate Solution { get; }
        DateTimeTemplate DateTime { get; }
        GuidTemplate Guid { get; }
    }
}