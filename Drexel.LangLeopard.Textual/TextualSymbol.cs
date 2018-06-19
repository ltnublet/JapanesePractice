using System;
using Drexel.LangLeopard.Contracts;

namespace Drexel.LangLeopard.Textual
{
    public class TextualSymbol : ISymbol
    {
        public TextualSymbol(Localized key)
        {
            this.Key = key ?? throw new ArgumentNullException(nameof(key));
        }

        public Localized Key { get; }
    }
}
