// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISolutionGeneratorService.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Services
{
    using System.Threading.Tasks;

    public interface ISolutionGeneratorService
    {
        Task GenerateAsync(ITemplateDefinition templateDefinition);
    }
}