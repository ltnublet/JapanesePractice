using System.ComponentModel.Composition;
using JapanesePractice.Contract.Loaders;

namespace JapanesePractice.FrontEnd.Debug
{
    internal class LoaderContainer
    {
        [ImportMany(typeof(ILoader))]
        public ILoader[] Loaders = new ILoader[0];
    }
}
