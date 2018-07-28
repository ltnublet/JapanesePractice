using System;
using System.Collections.Generic;
using System.Linq;

namespace Drexel.LangLeopard.Contracts
{
    /// <summary>
    /// Represents a language.
    /// </summary>
    public sealed class Language
    {
        private readonly IReadOnlyDictionary<Language, Localized> formalNames;
        private readonly IReadOnlyDictionary<Language, Localized> informalNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class.
        /// </summary>
        /// <param name="abbreviation">
        /// The human-readable abbreviation for this language.
        /// </param>
        /// <param name="formalNames">
        /// A set of mappings from <see cref="Language"/> to <see langword="string"/>. The values are the formal name
        /// of <b>this <see cref="Language"/></b> in the <b>language of the key</b>.
        /// </param>
        /// <param name="informalNames">
        /// A set of mappings from <see cref="Language"/> to <see langword="string"/>. The values are the informal name
        /// of <b>this <see cref="Language"/></b> in the <b>language of the key</b>.
        /// </param>
        public Language(
            string abbreviation,
            IReadOnlyDictionary<Language, string> formalNames,
            IReadOnlyDictionary<Language, string> informalNames)
        {
            this.Abbreviation = abbreviation ?? throw new ArgumentNullException(nameof(abbreviation));
            this.formalNames = formalNames?.ToDictionary(x => x.Key, x => new Localized(x.Value, x.Key))
                ?? throw new ArgumentNullException(nameof(formalNames));
            this.informalNames = informalNames?.ToDictionary(x => x.Key, x => new Localized(x.Value, x.Key))
                ?? throw new ArgumentNullException(nameof(informalNames));
        }

        /// <summary>
        /// The human-readable abbreviation for this <see cref="Language"/>.
        /// </summary>
        public string Abbreviation { get; }

        /// <summary>
        /// Retrieves the formal name of this <see cref="Language"/>.
        /// </summary>
        /// <param name="language">
        /// The <see cref="Language"/> in which the formal name of this <see cref="Language"/> is to be retrieved.
        /// </param>
        /// <returns>
        /// The formal name of this <see cref="Language"/> in the specified <paramref name="language"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Occurs when <paramref name="language"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="KeyNotFoundException">
        /// Occurs when <paramref name="language"/> is not recognized.
        /// </exception>
        public Localized GetFormalNameIn(Language language) =>
            this
                .formalNames
                .TryGetValue(
                    language ?? throw new ArgumentNullException(nameof(language)),
                    out Localized buffer)
                ? buffer
                : throw new KeyNotFoundException();

        /// <summary>
        /// Retrieves the informal name of this <see cref="Language"/>.
        /// </summary>
        /// <param name="language">
        /// The <see cref="Language"/> in which the informal name of this <see cref="Language"/> is to be retrieved.
        /// </param>
        /// <returns>
        /// The informal name of this <see cref="Language"/> in the specified <paramref name="language"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Occurs when <paramref name="language"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="KeyNotFoundException">
        /// Occurs when <paramref name="language"/> is not recognized.
        /// </exception>
        public Localized GetInformalNameIn(Language language) =>
            this
                .informalNames
                .TryGetValue(
                    language ?? throw new ArgumentNullException(nameof(language)),
                    out Localized buffer)
                ? buffer
                : throw new KeyNotFoundException();
    }
}
