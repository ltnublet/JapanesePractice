using System.Collections.Generic;

namespace Drexel.LangLeopard.Contracts.Sources
{
    public interface ISymbolSource
    {
        Language Language { get; }

        IEnumerable<ISymbol> Symbols { get; }
    }
}
