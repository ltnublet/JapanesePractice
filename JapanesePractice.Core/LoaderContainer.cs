using System.ComponentModel.Composition;
using JapanesePractice.Contract.Loaders;

namespace JapanesePractice.Core
{
    public class LoaderContainer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1051:DoNotDeclareVisibleInstanceFields",
            Justification = "Temporarily exposed until Core has fully supplanted all functionality in `FrontEnd.Debug`.")]
        [ImportMany(typeof(ILoader))]
        public ILoader[] Loaders = new ILoader[0];
    }
}
