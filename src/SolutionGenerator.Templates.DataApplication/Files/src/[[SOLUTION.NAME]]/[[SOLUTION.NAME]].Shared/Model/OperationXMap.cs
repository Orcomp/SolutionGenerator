// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationXMap.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace [[SOLUTION.NAME]]
{
	using CsvHelper.Configuration;

	public sealed class OperationXMap : CsvClassMap<OperationX>
	{
		public OperationXMap()
		{
			Map(x => x.Id).Name("Id");
			Map(x => x.Name).Name("Name");
			Map(x => x.StartTime).Name("StartTime");
			Map(x => x.Duration).Name("Duration");
			Map(x => x.Quantity).Name("Quantity");
			Map(x => x.Enabled).Name("Enabled");
		}
	}
}