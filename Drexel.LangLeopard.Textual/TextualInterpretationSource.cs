using System;
using System.Collections.Generic;
using Drexel.LangLeopard.Contracts;
using Drexel.LangLeopard.Contracts.Sources;

namespace Drexel.LangLeopard.Textual
{
    public class TextualInterpretationSource : IInterpretationSource<Localized>
    {
        private readonly IReadOnlyDictionary<ISymbol, IReadOnlyList<TextualInterpretation>> backingInterpretations;

        public TextualInterpretationSource(
            Language languageFrom,
            Language languageTo,
            IReadOnlyDictionary<ISymbol, IReadOnlyList<TextualInterpretation>> interpretations)
        {
            this.LanguageFrom = languageFrom ?? throw new ArgumentNullException(nameof(languageFrom));
            this.LanguageTo = languageTo ?? throw new ArgumentNullException(nameof(languageTo));
            this.backingInterpretations = interpretations ?? throw new ArgumentNullException(nameof(interpretations));
        }

        public Language LanguageFrom { get; }

        public Language LanguageTo { get; }

        public bool CanInterpret(ISymbol symbol) => this.backingInterpretations.ContainsKey(symbol);

        public IReadOnlyList<IInterpretation<Localized>> GetInterpretations(ISymbol symbol)
        {
            if (this.backingInterpretations.TryGetValue(
                symbol,
                out IReadOnlyList<TextualInterpretation> interpretations))
            {
                return interpretations;
            }
            else
            {
                return new List<TextualInterpretation>(0);
            }
        }
    }
}
