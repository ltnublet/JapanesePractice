using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drexel.LangLeopard.Contracts.Tests
{
    [TestClass]
    public class LanguageTests
    {
        [TestMethod]
        public void Langauge_Ctor_Succeeds()
        {
            const string abbreviation = "foo-ba";
            const string englishFormalName = "Foo (Bar)";
            const string englishInformalName = "Foo";
            const string localizedFormalName = "Bazinga";
            const string localizedInformalName = "Baz";

            Language language = new Language(
                abbreviation,
                englishFormalName,
                englishInformalName,
                localizedFormalName,
                localizedInformalName);

            Assert.AreEqual(abbreviation, language.Abbreviation);
            Assert.AreEqual(englishFormalName, language.EnglishFormalName);
            Assert.AreEqual(englishInformalName, language.EnglishInformalName);
            Assert.AreEqual(localizedFormalName, language.LocalizedFormalName);
            Assert.AreEqual(localizedInformalName, language.LocalizedInformalName);
        }
    }
}
