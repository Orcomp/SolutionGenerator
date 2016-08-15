// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvImportTests.cs" company="[[COMPANY.NAME]]">
//   Copyright (c) [[DATETIME.YEAR]] [[COMPANY.NAME]]. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace [[SOLUTION.NAME]].Tests
{
    using System.IO;
    using Catel.IoC;
    using NUnit.Framework;
    using Orc.Csv;

    [TestFixture]
    public class CsvImportTests
    {
        private readonly string _testFilePath = Path.GetFullPath("TestFiles");

        [Test]
        public void ImportOperationX()
        {
            var serviceLocator = ServiceLocator.Default;
            var csvReaderService = serviceLocator.ResolveType<ICsvReaderService>();
            var result = csvReaderService.ReadCsv<OperationX>($"{_testFilePath}\\OperationX.csv", new OperationXMap());

            Assert.IsNotNull(result);
            // Change to the expected line count 
            // Assert.AreEqual(10000, result.Count());
        }
    }
}