using System.Collections.Generic;
using System.Linq;
using JapanesePractice.Contract.Interpretations;
using JapanesePractice.Contract.ReferenceImplementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JapanesePractice.Tests
{
    [TestClass]
    public static class CategoryTests
    {
        [TestMethod]
        public static void Merge_ValidSymbols_ShouldSucceed()
        {
            const string symbolName = "Symbol";
            const string categoryOneValue = "CategoryOne";
            const string categoryTwoValue = "CategoryTwo";


            Category categoryOne = new Category(
                "CategoryOne_NameShouldntAffectTest", 
                new[] 
                {
                    new Symbol(symbolName, new ObjectInterpretation(categoryOneValue))
                });
            Category categoryTwo = new Category(
                "CategoryTwo_NameShouldntAffectTest",
                new[]
                {
                    new Symbol(symbolName, new ObjectInterpretation(categoryTwoValue))
                });

            string[] expected = new[] { categoryOneValue, categoryTwoValue };
            IEnumerable<object> actual = Category
                .Merge(
                    categoryOne,
                    categoryTwo)
                .Select(
                    x =>
                    x.Interpretations.SelectMany(y => y.GetPermittedInterpretations()))
                .Single();

            Assert.AreEqual(expected, actual);
        }
    }
}
