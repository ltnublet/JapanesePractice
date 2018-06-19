using System;
using System.Diagnostics;

namespace Drexel.LangLeopard.Contracts
{
    [DebuggerDisplay("{Abbreviation} - {EnglishFormalName} ({LocalizedFormalName})")]
    public sealed class Language
    {
        public Language(
            string abbreviation,
            string englishFormalName,
            string englishInformalName,
            string localizedFormalName,
            string localizedInformalName)
        {
            this.Abbreviation =
                abbreviation ?? throw new ArgumentNullException(nameof(abbreviation));
            this.EnglishFormalName =
                englishFormalName ?? throw new ArgumentNullException(nameof(englishFormalName));
            this.EnglishInformalName =
                englishInformalName ?? throw new ArgumentNullException(nameof(englishInformalName));
            this.LocalizedFormalName =
                localizedFormalName ?? throw new ArgumentNullException(nameof(localizedFormalName));
            this.LocalizedInformalName =
                localizedInformalName ?? throw new ArgumentNullException(nameof(localizedInformalName));
        }

        public string Abbreviation { get; }

        public string EnglishFormalName { get; }

        public string EnglishInformalName { get; }

        public string LocalizedFormalName { get; }

        public string LocalizedInformalName { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Language other))
            {
                return false;
            }

            return this.Abbreviation == other.Abbreviation;
        }

        public override int GetHashCode()
        {
            return this.Abbreviation.GetHashCode();
        }
    }
}
