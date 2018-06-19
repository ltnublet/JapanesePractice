using System;

namespace Drexel.LangLeopard.Contracts
{
    public sealed class Localized
    {
        public Localized(string value, Language language)
        {
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
            this.Language = language ?? throw new ArgumentNullException(nameof(language));
        }

        public Language Language { get; }

        public string Value { get; }

        public static implicit operator string(Localized self)
        {
            return self.Value;
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
