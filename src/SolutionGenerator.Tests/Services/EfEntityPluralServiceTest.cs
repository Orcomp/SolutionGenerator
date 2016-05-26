// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectFileServiceTest.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Tests
{
    using System.IO;
    using Catel.IoC;
    using NUnit.Framework;
    using Orc.Csv;
    using Services;

    [TestFixture]
    public class EfEntityPluralServiceTest
    {
        private readonly ITemplateProvider _target = new TemplateProvider(new FileSystemService());

        [TestCase("Parts", "Part")]
        [TestCase("Entities", "Entity")]
        [TestCase("Feet", "Foot")]
        [TestCase("Men", "Man")]
        [TestCase("Women", "Woman")]
        [TestCase("People", "Person")]
        public void ToSingularTest(string input, string expected)
        {
            var serviceLocator = ServiceLocator.Default;
            var target = serviceLocator.ResolveType<IEntityPluralService>();

            // Singularize
            Assert.AreEqual(expected, target.ToSingular(input));
            // Check if does not harm an already singular form:
            Assert.AreEqual(expected, target.ToSingular(input));
        }

        [TestCase("Part", "Parts")]
        [TestCase("Entity", "Entities")]
        [TestCase("Foot", "Feet")]
        [TestCase("Man", "Men")]
        [TestCase("Woman", "Women")]
        [TestCase("Person", "People")]
        public void ToPluralTest(string input, string expected)
        {
            var serviceLocator = ServiceLocator.Default;
            var target = serviceLocator.ResolveType<IEntityPluralService>();

            // Pluralize
            Assert.AreEqual(expected, target.ToPlural(input));
            // Check if does not harm an already plural form:
            Assert.AreEqual(expected, target.ToPlural(input));
        }

    }
}