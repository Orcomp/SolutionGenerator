// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AbbreviationAttribute.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class AbbreviationAttribute : Attribute
    {
        public AbbreviationAttribute(string abbreviation)
        {
            Abbreviation = abbreviation;
        }

        public string Abbreviation { get; set; }
    }
}