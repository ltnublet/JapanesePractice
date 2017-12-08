using System.Collections.Generic;
using System.IO;
using System.Linq;
using JapanesePractice.Contract.Contexts;
using JapanesePractice.Contract.Interpretations;
using JapanesePractice.Textual;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JapanesePractice.Tests
{
    [TestClass]
    public class TextualContextTests
    {
        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Smoke")]
        public void Condense_ValidCategories_ShouldSucceed()
        {
            IContext context = new TextualLoader().LoadContextFromPath(SharedResources.Skeleton);

            IInterpretation actualInterpretation = context
                .Condense("Category1", "Category4")
                .Single(x => x.Name == "A")
                .Interpretations
                .Condense();

            Assert.IsTrue(
                new ObjectInterpretation(
                    "A",
                    "Ae",
                    "Ayy (lmao)",
                    "Category4Interpretation")
                .Compare(actualInterpretation));
        }
    }
}
