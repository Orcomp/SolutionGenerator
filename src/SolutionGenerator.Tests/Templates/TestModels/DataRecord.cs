// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataRecord.cs" company="WildGums">
//   Copyright (c) 2012 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Tests.Templates.TestModels
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Catel.Data;

    public class DataRecord : ModelBase
    {
        public DataRecord()
        {
            Fields = new List<DataRecordField>();
        }

        public string FileName { get; set; }

        public string RelativeFileName
        {
            get { return Path.GetFileName(FileName); }
        }

        public string RecordName { get; set; }

        public List<DataRecordField> Fields { get; private set; }

        public List<DataRecordField> IncludedFields
        {
            get { return Fields.Where(x => x.IsIncluded).ToList(); }
        }

        public override string ToString()
        {
            return RecordName;
        }
    }
}