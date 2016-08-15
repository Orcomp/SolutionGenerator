// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="[[COMPANY.NAME]]">
//   Copyright (c) [[DATETIME.YEAR]] [[COMPANY.NAME]]. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace [[SOLUTION.NAME]]
{
    using Catel.IoC;
    using Orc.Csv;

    static class Program
    {
        static void Main(string[] args)
        {
            var dataFolder = @"[[Data.DataFolder]]";

            var serviceLocator = ServiceLocator.Default;
            var csvReaderService = serviceLocator.ResolveType<ICsvReaderService>();

            var operationXCollection = csvReaderService.ReadCsv<OperationX>($"{dataFolder}\\OperationX.csv", new OperationXMap());
        }
    }
}