using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using JapanesePractice.Contexts;
using JapanesePractice.Interpretations;
using JapanesePractice.Textual;
using Xunit;

namespace JapanesePractice.Tests
{
    public static class TextualLoaderTests
    {
        [Fact]
        [Trait("Category", "Integration")]
        [Trait("Category", "Smoke")]
        public static void FromFile_ValidJson_ShouldSucceed()
        {
            IContext context = new TextualLoader().LoadContextFromPath(SharedResources.Skeleton);

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
        public static void CreateSymbolFromJson_ValidJson_ShouldSucceed()
        {
            JsonObject symbolAsJson;
            using (TextReader reader = new StreamReader(SharedResources.Skeleton_Symbol))
            {
                symbolAsJson = (JsonObject)JsonObject.Load(reader);
            }

            Symbol actual = new TextualLoader().CreateSymbolFromJson(symbolAsJson.ToString());
            Symbol expected = new Symbol("A", new ObjectInterpretation("A", "Ae", "Ayy (lmao)"));

            Assert.Equal(expected.Name, actual.Name);
            Assert.True(expected.Interpretations.Compare(actual.Interpretations));
        }
    }
}
