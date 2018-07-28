using System;

namespace Drexel.LangLeopard.Contracts
{
    /// <summary>
    /// Represents a symbol.
    /// </summary>
    public sealed class Symbol
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Symbol"/> class.
        /// </summary>
        /// <param name="key">
        /// The unique key by this this <see cref="Symbol"/> is recognized.
        /// </param>
        public Symbol(Localized key)
        {
            this.Key = key ?? throw new ArgumentNullException(nameof(key));
        }

        /// <summary>
        /// The unique key by which this <see cref="Symbol"/> is recognized. <see cref="Symbol"/>s with the same
        /// key are considered the same, even if their implementations differ.
        /// </summary>
        public Localized Key { get; }

        /// <summary>
        /// Determines whether the specified <see langword="object"/> <paramref name="obj"/> is equal to the current
        /// <see cref="Symbol"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see langword="object"/> to compare with the current <see cref="Symbol"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the specified <see langword="object"/> <paramref name="obj"/> is equal to the
        /// current <see cref="Symbol"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is Symbol other)
            {
                return this.Key.Equals(other.Key);
            }

            return false;
        }

        /// <summary>
        /// Returns the hash code for this <see cref="Symbol"/>.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer hash code.
        /// </returns>
        public override int GetHashCode() => this.Key.GetHashCode();

        /// <summary>
        /// Returns a <see langword="string"/> that represents the current <see cref="Symbol"/>.
        /// </summary>
        /// <returns>
        /// A <see langword="string"/> that represents the current <see cref="Symbol"/>.
        /// </returns>
        public override string ToString() => this.Key;
    }
}
