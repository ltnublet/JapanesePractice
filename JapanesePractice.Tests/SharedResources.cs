using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapanesePractice.Tests
{
    public static class SharedResources
    {
        public const string ResourcePath = @"..\..\..\";
        public const string Skeleton = ResourcePath + "Skeleton.json";
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1707:IdentifiersShouldNotContainUnderscores",
            Justification = "Names containing underscores are permitted in test assemblies.")]
        public const string Skeleton_Symbol = ResourcePath + "Skeleton_Symbol.json";
    }
}
