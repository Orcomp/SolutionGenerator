// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectFileServiceTest.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Tests
{
    using System.IO;
    using NUnit.Framework;
    using Services;

    [TestFixture]
    public class ProjectFileServiceTest
    {
        private readonly ITemplateProvider _target = new TemplateProvider(new FileSystemService());

        [Test]
        public void TestAddItem_Simple()
        {
            var fileName = GetTestFileName("test1.projitems");
            var target = new ProjectFileService();
            target.Open(fileName);
            target.AddItem("Compile", "operationx.cs", "New.cs");
            target.Save(".result");

            AssertFilesAreEqual($"{fileName}.additem_new", $"{fileName}.result");
        }

        [Test]
        public void TestAddItem_Deep()
        {
            var fileName = GetTestFileName("test2.projitems");
            var target = new ProjectFileService();
            target.Open(fileName);
            target.AddItem("None", "operationx.csv", "New.csv");
            target.Save(".result");

            AssertFilesAreEqual($"{fileName}.additem_new", $"{fileName}.result");
        }

        [Test]
        public void TestRemoveItem_Simple()
        {
            var fileName = GetTestFileName("test1.projitems");
            var target = new ProjectFileService();
            target.Open(fileName);
            target.RemoveItem("Compile", "operationx.cs");
            target.Save(".result");

            AssertFilesAreEqual($"{fileName}.removeitem", $"{fileName}.result");
        }

        [Test]
        public void TestRemoveItem_Deep()
        {
            var fileName = GetTestFileName("test2.projitems");
            var target = new ProjectFileService();
            target.Open(fileName);
            target.RemoveItem("None", "operationx.csv");
            target.Save(".result");

            AssertFilesAreEqual($"{fileName}.removeitem", $"{fileName}.result");
        }

        private void AssertFilesAreEqual(string expectedFileName, string actualFileName)
        {
            var expectedText = File.ReadAllText(expectedFileName);
            var actualText = File.ReadAllText(actualFileName);
            Assert.AreEqual(expectedText, actualText);
        }

        private string GetTestFileName(string fileName)
        {
            return Path.GetFullPath($".\\TestFiles\\{fileName}");
        }
    }
}