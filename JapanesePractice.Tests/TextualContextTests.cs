using System.Collections.Generic;
using System.IO;
using System.Linq;
using JapanesePractice.Contract.Contexts;
using JapanesePractice.Contract.Interpretations;
using JapanesePractice.Textual;
using Xunit;

namespace JapanesePractice.Tests
{
    public static class TextualContextTests
    {
        [Fact]
        [Trait("Category", "Integration")]
        [Trait("Category", "Smoke")]
        public static void Condense_ValidCategories_ShouldSucceed()
        {
            IContext context = new TextualLoader().LoadContextFromPath(SharedResources.Skeleton);

            IInterpretation actualInterpretation = context
                .Condense("Category1", "Category4")
                .Single(x => x.Name == "A")
                .Interpretations
                .Condense();

            Assert.True(
                new ObjectInterpretation(
                    "A",
                    "Ae",
                    "Ayy (lmao)",
                    "Category4Interpretation")
                .Compare(actualInterpretation));
        }
    }
}
