// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataRecordField.cs" company="WildGums">
//   Copyright (c) 2012 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Tests.Templates.TestModels
{
    using Catel;
    using Catel.Data;

    public class DataRecordField : ModelBase
    {
        public DataRecordField()
        {
            IsIncluded = true;
            TypeName = "string";
        }

        public bool IsIncluded { get; set; }

        public string Source { get; set; }

        public string Target { get; set; }

        public string TypeName { get; set; }

        public bool IsBool
        {
            get { return TypeName.ContainsIgnoreCase("bool"); }
        }

        public bool IsDateTime
        {
            get { return TypeName.ContainsIgnoreCase("DateTime"); }
        }

        public bool IsNullable
        {
            get { return TypeName == "string" || TypeName.Contains("?"); }
        }
    }
}