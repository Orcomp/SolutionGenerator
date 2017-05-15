// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateEngineFacts.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Tests.Templates
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using ApprovalTests;
    using NUnit.Framework;
    using SolutionGenerator.Templates;
    using TestModels;

    [TestFixture]
    public class TemplateEngineFacts
    {
        private readonly TemplateLoader _templateLoader = new TemplateLoader();

        [Test]
        public async Task NestedIfStatementsAsync()
        {
            var filesContext = new TemporaryFilesContext("NestedIfStatements");

            var templates = CreateTemplates();

            var templateContent = await LoadEmbeddedResourceTemplateAsync("NestedIfStatements.txt");

            var engine = CreateTemplateEngine();

            var result = engine.ReplaceValues(templateContent, templates);

            var outputFile = filesContext.GetFile("NestedIfStatements.txt", true);
            File.WriteAllText(outputFile, result);

            Approvals.VerifyFile(outputFile);
        }

        [Test]
        public async Task NestedForEachStatementsAsync()
        {
            var filesContext = new TemporaryFilesContext("NestedForEachStatements");

            var dataRecordTemplate = new TestModels.DataTemplate();

            var record1 = CreateDataRecord("Record_1", 10);
            record1.Fields[4].IsIncluded = false;
            record1.Fields[6].IsIncluded = false;
            record1.Fields[8].IsIncluded = false;
            dataRecordTemplate.Records.Add(record1);

            var record2 = CreateDataRecord("Record_2", 4);
            dataRecordTemplate.Records.Add(record2);

            var templates = CreateTemplates(dataRecordTemplate);

            var templateContent = await LoadEmbeddedResourceTemplateAsync("NestedForEachStatements.txt");

            var engine = CreateTemplateEngine();

            var result = engine.ReplaceValues(templateContent, templates);

            var outputFile = filesContext.GetFile("NestedForEachStatements.txt", true);
            File.WriteAllText(outputFile, result);

            Approvals.VerifyFile(outputFile);
        }

        [Test]
        public async Task NestedForEachWithIfStatementsAsync()
        {
            var filesContext = new TemporaryFilesContext("NestedForEachWithIfStatements");

            var dataRecordTemplate = new TestModels.DataTemplate();

            var record1 = CreateDataRecord("Record_1", 10);
            record1.Fields[3].TypeName = "DateTime";
            record1.Fields[4].IsIncluded = false;
            record1.Fields[5].TypeName = "DateTime?";
            record1.Fields[6].IsIncluded = false;
            record1.Fields[7].TypeName = "bool";
            record1.Fields[8].IsIncluded = false;
            record1.Fields[9].TypeName = "bool?";
            dataRecordTemplate.Records.Add(record1);

            var record2 = CreateDataRecord("Record_2", 4);
            dataRecordTemplate.Records.Add(record2);

            var templates = CreateTemplates(dataRecordTemplate);

            var templateContent = await LoadEmbeddedResourceTemplateAsync("NestedForEachWithIfStatements.txt");

            var engine = CreateTemplateEngine();

            var result = engine.ReplaceValues(templateContent, templates);

            var outputFile = filesContext.GetFile("NestedForEachWithIfStatements.txt", true);
            File.WriteAllText(outputFile, result);

            Approvals.VerifyFile(outputFile);
        }

        private DataRecord CreateDataRecord(string name, int numberOfFields)
        {
            var record = new DataRecord
            {
                RecordName = name
            };

            for (var i = 0; i < numberOfFields; i++)
            {
                record.Fields.Add(new DataRecordField
                {
                    Source = $"Source {i + 1}",
                    Target = $"Target {i + 1}",
                    IsIncluded = true
                });
            }

            return record;
        }

        private List<ITemplate> CreateTemplates(params ITemplate[] templates)
        {
            var allTemplates = new List<ITemplate>();

            allTemplates.Add(new DateTimeTemplate());
            allTemplates.Add(new CompanyTemplate
            {
                Name = "My company"
            });
            allTemplates.Add(new TestSolutionTemplate());

            allTemplates.AddRange(templates);

            return allTemplates;
        }

        private TemplateEngine CreateTemplateEngine()
        {
            var templateLoader = new TemplateLoader();

            var engine = new TemplateEngine(templateLoader);
            return engine;
        }

        private Task<string> LoadEmbeddedResourceTemplateAsync(string resourceName)
        {
            return _templateLoader.LoadTemplateAsync(new EmbeddedResourceTemplateFile(GetType().Assembly, $"SolutionGenerator.Tests.Templates.TestFiles.{resourceName}", resourceName));
        }
    }
}