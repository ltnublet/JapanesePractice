using System.Collections.Generic;

namespace JapanesePractice.Contract
{
    /// <summary>
    /// Represents a collection of <see cref="ISymbol"/>s.
    /// </summary>
    public interface ICategory : IEnumerable<ISymbol>
    {
        /// <summary>
        /// The name of the <see cref="ICategory"/>.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The symbols contained by the <see cref="ICategory"/>.
        /// </summary>
        IReadOnlyList<ISymbol> Symbols { get; }
    }
}