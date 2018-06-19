using System;
using Drexel.LangLeopard.Contracts;

namespace Drexel.LangLeopard.Textual
{
    public class TextualInterpretation : IInterpretation<Localized>
    {
        public TextualInterpretation(Localized key, Localized value)
        {
            this.Key = key ?? throw new ArgumentNullException(nameof(key));
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public Localized Key { get; }

        public Localized Value { get; }
    }
}
