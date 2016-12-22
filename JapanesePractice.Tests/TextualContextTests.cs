using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SUT = JapanesePractice.Contexts;

namespace JapanesePractice.Tests
{
    public class TextualContextTests
    {
        private const string ResourcePath = @"..\..\..\";
        private const string Skeleton = ResourcePath + "Skeleton.json";

        [Fact]
        public void FromFile_ValidJson_ShouldSucceed()
        {
            SUT.TextualContext context = SUT.TextualContext.FromFile(Skeleton);
        }
    }
}
