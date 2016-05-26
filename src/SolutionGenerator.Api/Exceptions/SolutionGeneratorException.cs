// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SolutionGeneratorException.cs" company="WildGums">
//   Copyright (c) 2008 - 2014 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator
{
    using System;

    public class SolutionGeneratorException : Exception
    {
        public SolutionGeneratorException(string message)
            : base(message)
        {
        }

        public SolutionGeneratorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}