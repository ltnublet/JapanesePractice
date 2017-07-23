using System.Linq;
using JapanesePractice.Contexts;
using JapanesePractice.Interpretations;
using Xunit;

namespace JapanesePractice.Tests
{
    public static class TextualContextTests
    {
        private const string ResourcePath = @"..\..\..\";
        private const string Skeleton = ResourcePath + "Skeleton.json";

        [Fact]
        [Trait("Category", "Integration")]
        [Trait("Category", "Smoke")]
        public static void FromFile_ValidJson_ShouldSucceed()
        {
            TextualContext context = TextualContext.FromFile(Skeleton);

            Assert.Equal(
                new string[] 
                {
                    "Category1",
                    "Category2",
                    "Category3",
                    "Category4"
                }, 
                context
                    .Categories
                    .Select(x => x.Name));
        }

        [Fact]
        [Trait("Category", "Integration")]
        [Trait("Category", "Smoke")]
        public static void Condense_ValidCategories_ShouldSucceed()
        {
            TextualContext context = TextualContext.FromFile(Skeleton);

            Assert.True(
                new Textual(
                    "A",
                    "Ae",
                    "Category4Interpretation")
                .CompareAll(
                    context
                        .Condense("Category1", "Category4")
                        .Single(x => x.Name == "A")
                        .Interpretations));
        }
    }
}
