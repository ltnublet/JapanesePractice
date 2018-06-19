using System.Collections.Generic;
using Drexel.LangLeopard.Contracts.Sources;

namespace Drexel.LangLeopard.Contracts.Loaders
{
    public interface ILoader<out T>
    {
        IReadOnlyCollection<ISymbolSource> SymbolSources { get; }

        IReadOnlyCollection<IInterpretationSource<T>> InterpretationSources { get; }
    }
}
