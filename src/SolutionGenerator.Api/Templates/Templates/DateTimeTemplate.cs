// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeTemplate.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates
{
    using System;

    public class DateTimeTemplate : TemplateBase
    {
        public DateTimeTemplate()
        {
            var now = DateTime.Now;

            Year = now.Year.ToString();
            Month = now.Month.ToString();
            Day = now.Day.ToString();
        }

        public string Year { get; set; }

        public string Month { get; set; }

        public string Day { get; set; }
    }
}