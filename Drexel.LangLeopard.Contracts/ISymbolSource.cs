using System.Collections.Generic;

namespace Drexel.LangLeopard.Contracts
{
    /// <summary>
    /// Represents a source of <see cref="Symbol"/>s.
    /// </summary>
    public interface ISymbolSource
    {
        /// <summary>
        /// Gets the <see cref="Symbol"/>s contained by this source.
        /// </summary>
        /// <returns>
        /// The <see cref="Symbol"/>s contained by this source.
        /// </returns>
        IEnumerable<Symbol> GetSymbols();

        /// <summary>
        /// Gets the <see cref="ISymbolSource"/>s contained by this source.
        /// </summary>
        /// <returns>
        /// The <see cref="ISymbolSource"/>s contained by this source.
        /// </returns>
        IEnumerable<ISymbolSource> GetSubSources();
    }
}
