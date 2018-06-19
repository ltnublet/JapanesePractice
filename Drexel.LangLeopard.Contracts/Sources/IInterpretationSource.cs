using System.Collections.Generic;

namespace Drexel.LangLeopard.Contracts.Sources
{
    public interface IInterpretationSource<out T>
    {
        Language LanguageFrom { get; }

        Language LanguageTo { get; }

        bool CanInterpret(ISymbol symbol);

        IReadOnlyList<IInterpretation<T>> GetInterpretations(ISymbol symbol);
    }
}
