using System.IO;
using System.Linq;
using JapanesePractice.Contract;
using JapanesePractice.Contract.Contexts;
using JapanesePractice.Contract.Interpretations;
using JapanesePractice.Contract.ReferenceImplementation;
using JapanesePractice.Textual;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace JapanesePractice.Tests
{
    [TestClass]
    public static class TextualLoaderTests
    {
        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Smoke")]
        public static void FromFile_ValidJson_ShouldSucceed()
        {
            IContext context = new TextualLoader().LoadContextFromPath(SharedResources.Skeleton);

            Assert.AreEqual(
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

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Smoke")]
        public static void CreateSymbolFromJson_ValidJson_ShouldSucceed()
        {
            JObject symbolAsJson;
            using (JsonReader reader = new JsonTextReader(new StreamReader(SharedResources.Skeleton_Symbol)))
            {
                symbolAsJson = JObject.Load(reader);
            }

            ISymbol actual = new TextualLoader().CreateSymbolFromJson(symbolAsJson.ToString());
            Symbol expected = new Symbol("A", new ObjectInterpretation("A", "Ae", "Ayy (lmao)"));

            Assert.AreEqual(expected.Name, actual.Name);
            Assert.IsTrue(expected.Interpretations.Compare(actual.Interpretations));
        }
    }
}
