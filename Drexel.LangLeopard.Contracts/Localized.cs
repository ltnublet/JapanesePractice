using System;

namespace Drexel.LangLeopard.Contracts
{
    /// <summary>
    /// Represents a localized string.
    /// </summary>
    public sealed class Localized
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Localized"/> class.
        /// </summary>
        /// <param name="value">
        /// The localized <see langword="string"/>.
        /// </param>
        /// <param name="language">
        /// The <see cref="Language"/> the <paramref name="value"/> is localized in.
        /// </param>
        public Localized(string value, Language language)
        {
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
            this.Language = language ?? throw new ArgumentNullException(nameof(language));
        }

        /// <summary>
        /// The <see cref="Language"/> this <see cref="Localized"/> is localized in.
        /// </summary>
        public Language Language { get; }

        /// <summary>
        /// The localized <see langword="string"/>.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Implicitly converts a given <see cref="Localized"/> <paramref name="self"/> into a
        /// <see langword="string"/>.
        /// </summary>
        /// <param name="self">
        /// The <see cref="Localized"/> to convert to a <see langword="string"/>.
        /// </param>
        /// <returns>
        /// If <paramref name="self"/> is <see langword="null"/>, <see langword="null"/>. Else,
        /// <see cref="Localized.Value"/>.
        /// </returns>
        public static implicit operator string(Localized self)
        {
            return self?.Value;
        }

        /// <summary>
        /// Determines whether the specified <see langword="object"/> <paramref name="obj"/> is equal to the current
        /// <see cref="Localized"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see langword="object"/> to compare with the current <see cref="Localized"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the specified <see langword="object"/> <paramref name="obj"/> is equal to the
        /// current <see cref="Localized"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is Localized other)
            {
                return this.Language.Equals(other.Language)
                    && this.Value.Equals(other.Value, StringComparison.Ordinal);
            }

            return false;
        }

        /// <summary>
        /// Returns the hash code for this <see cref="Symbol"/>.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer hash code.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return 17
                    + (31 * this.Language.Abbreviation.GetHashCode())
                    + (31 * this.Value.GetHashCode());
            }
        }

        /// <summary>
        /// Returns a <see langword="string"/> that represents the current <see cref="Localized"/>.
        /// </summary>
        /// <returns>
        /// A <see langword="string"/> that represents the current <see cref="Localized"/>.
        /// </returns>
        public override string ToString() => this.Value;
    }
}
