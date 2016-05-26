// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INestedTemplate.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates
{
    using System;

    public interface INestedTemplate
    {
        object Tag { get; set; }

        Func<object, string> Callback { get; set; }
    }
}