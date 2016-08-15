// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataApplicationTemplateContext.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates.DataApplication
{
    using System.Reflection;
    using Catel.Reflection;

    public class DataApplicationTemplateContext : TemplateContextBase
    {
        public DataApplicationTemplateContext()
        {
            var assembly = GetType().GetAssemblyEx();

            Company.Name = "MyCompanyName";

            Solution.Name = "MySolutionName";

			Data = new DataTemplate();
			Data.DataFolder = "";
        }

		public DataTemplate Data { get; protected set; }
    }
}