// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataTemplate.cs" company="WildGums">
//   Copyright (c) 2012 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Tests.Templates.TestModels
{
    using System.Collections.ObjectModel;
    using SolutionGenerator.Templates;

    [Abbreviation("D")]
    [Abbreviation("DATA")]
    public class DataTemplate : TemplateBase
    {
        public DataTemplate()
        {
            Records = new ObservableCollection<DataRecord>();
        }

        public ObservableCollection<DataRecord> Records { get; private set; }
    }
}