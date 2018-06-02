namespace SolutionGenerator.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class StringExtensionsFacts
    {
        [TestCase("sadfsdfasfsaf", "Sadfsdfasfsaf")]
        [TestCase("SOME_THING", "SomeThing")]
        public void TheToCamelCaseMethod(string input, string expectedOutput)
        {
            var actualOutput = input.ToCamelCase();

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}
