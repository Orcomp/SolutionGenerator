// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NestedTemplate.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates
{
    using System;

    public class NestedTemplate : INestedTemplate
    {
        public NestedTemplate(object tag, Func<object, string> callback)
        {
            Tag = tag;
            Callback = callback;
        }

        public object Tag { get; set; }

        public Func<object, string> Callback { get; set; }
    }
}