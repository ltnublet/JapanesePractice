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
    [SuppressMessage("ReSharper", "StyleCop.SA1600", Justification = "Test names should clearly indicate purpose, rendering XML documentation superfluous.")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Test names should be human-readable and clearly separate the SUT, the parameters, and the expected outcome.")]
    public class TextSymbolCollectionGroupTests
    {
        private const string ResourcePath = @"..\..\..\";
        private const string ValidTargetPath = ResourcePath + "Skeleton.json";

        [Fact]
        public void FromFile_ValidTarget_ShouldSucceed()
        {
            SUT.TextSymbolCollectionGroup actual = TextSymbolCollectionGroup.FromFile(ValidTargetPath);
        }
    }
}
