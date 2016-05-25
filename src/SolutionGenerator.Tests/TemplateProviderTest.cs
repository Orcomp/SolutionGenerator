// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateProviderTest.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Ionic.Zip;
    using Moq;
    using NUnit.Framework;
    using Services;

    [TestFixture]
    public class TemplateProviderTest
    {
        private readonly ITemplateProvider _target = new TemplateProvider(new FileSystemService());

        private Stream CreateZipStream(string entryName, string entryContent)
        {
            var result = new MemoryStream();
            using (var zip = new ZipFile())
            {
                using (var entryStream = GetStreamFromString(entryContent))
                {
                    zip.AddEntry(entryName, entryStream);
                    zip.Save(result);
                }
            }
            result.Seek(0, SeekOrigin.Begin);
            return result;
        }

        private MemoryStream GetStreamFromString(string value)
        {
            var result = new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
            result.Seek(0, SeekOrigin.Begin);
            return result;
        }

        [Test]
        public void TemplatesProperty_IntegrationWithFileSystemService()
        {
            // TODO: Change with a specific prepared template:

            var result = _target.Templates.Where(ti => ti.FileName.ToLower().Contains("classlibrary45.zip")).ToArray();
            Assert.AreEqual(1, result.Count());

            Assert.IsTrue(result[0].Name.ToLower().Contains("standard"));
            Assert.IsTrue(result[0].Description.ToLower().Contains("standard"));
            Assert.IsTrue(result[0].FileName.ToLower().Contains("classlibrary45.zip"));
            Assert.IsTrue(result[0].IsDefault);
        }

        [Test]
        [TestCase("Name\tTestName\r\nDescription\tTestDescription", "TestName", "TestDescription", Description = "Two simple entry with tabs")]
        [TestCase("Name,TestName\r\nDescription=TestDescription", "TestName", "TestDescription", Description = "Accepts comma and equalitiy sign also as separator")]
        [TestCase("#\n#\r#\n\r#\r\nName\tTestName", "TestName", null, Description = "Comment and various new lines")]
        [TestCase("\r\nDescription\tTestDescription", null, "TestDescription", Description = "Name entry is missing")]
        [TestCase("Name\tTestName\r\n", "TestName", null, Description = "Description entry is missing")]
        [TestCase("Name\tTestName\r\nName\tTestName2\r\nDescription\tTestDescription", "TestName", "TestDescription", Description = "Duplicate entry")]
        public void TestTemplates_HasDescription(string description, string expectedName, string expectedDescription)
        {
            // Arrange
            const string anyZip = "any.zip";
            var fss = new Mock<IFileSystemService>();
            fss.Setup(m => m.Files(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(new List<string> { anyZip });
            fss.Setup(m => m.ReadOnlyStream(It.Is<string>(fn => fn.Contains(anyZip)))).Returns(CreateZipStream(".description", description));
            var target = new TemplateProvider(fss.Object);

            // Act
            var result = target.Templates.ToArray();

            // Assert
            Assert.AreEqual(1, result.Count());
            if (expectedName != null)
            {
                Assert.AreEqual(expectedName, result[0].Name);
            }
            else
            {
                Assert.IsTrue(result[0].Name.Contains($"{anyZip.Replace(".zip", "")} ("));
            }

            if (expectedDescription != null)
            {
                Assert.AreEqual(expectedDescription, result[0].Description);
            }
            else
            {
                Assert.IsTrue(result[0].Description.Contains($"{anyZip.Replace(".zip", "")} ("));
            }

            Assert.IsTrue(result[0].FileName.Contains(anyZip));

            fss.Verify(m => m.Files(Path.GetFullPath(@".\templates"), "*.zip", true), Times.Once);
            fss.Verify(m => m.ReadOnlyStream(It.Is<string>(fn => fn.Contains(anyZip))), Times.Once);
        }

        [Test]
        public void TestTemplates_MultipleTemplates_NoDescription()
        {
            // Arrange
            var fss = new Mock<IFileSystemService>();
            var templateNames = new List<string> { "a.zip", "b.zip", "c.zip" };
            fss.Setup(m => m.Files(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(templateNames);
            foreach (var templateName in templateNames)
            {
                fss.Setup(m => m.ReadOnlyStream(It.Is<string>(fn => fn.Contains(templateName)))).Returns(CreateZipStream(templateName, templateName));
            }

            var target = new TemplateProvider(fss.Object);

            // Act
            var result = target.Templates.ToArray();

            // Assert
            Assert.AreEqual(templateNames.Count, result.Count());
            for (var index = 0; index < templateNames.Count; index++)
            {
                var templateName = templateNames[index];
                Assert.IsTrue(result[index].Name.Contains($"{templateName.Replace(".zip", "")} ("));
                Assert.IsTrue(result[index].Description.Contains($"{templateName.Replace(".zip", "")} ("));
                Assert.IsTrue(result[index].FileName.Contains(templateName));
            }

            fss.Verify(m => m.Files(Path.GetFullPath(@".\templates"), "*.zip", true), Times.Once);
            foreach (var templateName in templateNames)
            {
                fss.Verify(m => m.ReadOnlyStream(It.Is<string>(fn => fn.Contains(templateName))), Times.Once);
            }
        }

        [Test]
        public void TestTemplates_NoTemplates()
        {
            // Arrange
            var fss = new Mock<IFileSystemService>();
            fss.Setup(m => m.Files(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(new List<string>());
            var target = new TemplateProvider(fss.Object);

            // Act
            var result = target.Templates;

            // Assert
            Assert.AreEqual(0, result.Count());

            fss.Verify(m => m.Files(Path.GetFullPath(@".\templates"), "*.zip", true), Times.Once);
        }
    }
}