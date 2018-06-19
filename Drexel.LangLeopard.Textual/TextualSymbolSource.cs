using System;
using System.Collections.Generic;
using Drexel.LangLeopard.Contracts;
using Drexel.LangLeopard.Contracts.Sources;

namespace Drexel.LangLeopard.Textual
{
    public class TextualSymbolSource : ISymbolSource
    {
        public TextualSymbolSource(Language language, IEnumerable<ISymbol> symbols)
        {
            this.Language = language ?? throw new ArgumentNullException(nameof(language));
            this.Symbols = symbols ?? throw new ArgumentNullException(nameof(symbols));
        }

        public Language Language { get; }

        public IEnumerable<ISymbol> Symbols { get; }
    }
}
