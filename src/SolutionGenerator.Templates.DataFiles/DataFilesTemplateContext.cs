// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataFilesTemplateContext.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates.DataFiles
{

    public class DataFilesTemplateContext : TemplateContextBase
    {
        public DataFilesTemplateContext()
        {
            Company.Name = "MyCompanyName";

            Solution.Name = "MyNamespace";

			Data = new DataTemplate();
			Data.DataFolder = "";
        }

		public DataTemplate Data { get; protected set; }
    }
}