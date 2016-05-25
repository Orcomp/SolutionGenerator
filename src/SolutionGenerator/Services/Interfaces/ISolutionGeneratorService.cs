// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISolutionGeneratorService.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Services
{
    using Models;

    public interface ISolutionGeneratorService
    {
        void DoWork(Solution solution);
    }
}