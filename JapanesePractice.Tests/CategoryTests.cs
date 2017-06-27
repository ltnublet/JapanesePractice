using System.Linq;
using JapanesePractice.Interpretations;
using Xunit;

namespace JapanesePractice.Tests
{
    public class CategoryTests
    {
        [Fact]
        public void Merge_ValidSymbols_ShouldSucceed()
        {
            Category categoryOne = new Category(
                "CategoryOne", 
                new[] 
                {
                    new Symbol("SymbolOne", new Textual("CategoryOne"))
                });
            Category categoryTwo = new Category(
                "CategoryTwo",
                new[]
                {
                    new Symbol("SymbolOne", new Textual("CategoryTwo"))
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
