using System.Linq;
using JapanesePractice.Interpretations;
using Xunit;

namespace JapanesePractice.Tests
{
    public static class CategoryTests
    {
        [Fact]
        public static void Merge_ValidSymbols_ShouldSucceed()
        {
            Category categoryOne = new Category(
                "CategoryOne", 
                new[] 
                {
                    new Symbol("SymbolOne", new TextualInterpretation("CategoryOne"))
                });
            Category categoryTwo = new Category(
                "CategoryTwo",
                new[]
                {
                    new Symbol("SymbolOne", new TextualInterpretation("CategoryTwo"))
                });

            Assert.Equal(
                new[] { "CategoryOne", "CategoryTwo" }, 
                Category
                    .Merge(
                        categoryOne, 
                        categoryTwo)
                    .Select(
                        x => 
                        x.Interpretations.Select(y => y.ToString()))
                    .Single());
        }
    }
}
