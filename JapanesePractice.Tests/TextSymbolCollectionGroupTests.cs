using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SUT = JapanesePractice;

namespace JapanesePractice.Tests
{
    /// <summary>
    /// Contains tests relating to the <see cref="SUT.TextSymbolCollectionGroup"/> class.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", 
        Justification = "Test names should clearly indicate purpose, rendering XML documentation superfluous.")]
    public class TextSymbolCollectionGroupTests
    {
        private const string ResourcePath = @"..\..\..\";
        private const string ValidTargetPath = ResourcePath + "Skeleton.json";

        [Fact]
        public void FromFile_ValidTarget_ShouldSucceed()
        {
            string expected = @"Category1: 
A - A, Ae
B - B, Bee
C - C, Cee
Category2: 
D - D, Dee
E - E, Ee
F - F, Ef
Category3: 
G - G, Gee
H - H, Aych
I - I, Aye, Eye
Category4: 
A - Category4Interpretation
D - Category4Interpretation
G - Category4Interpretation
";

            SUT.TextSymbolCollectionGroup actual = TextSymbolCollectionGroup.FromFile(ValidTargetPath);

            Assert.Equal(expected, actual.ToString());
        }
    }
}
