using System.Collections.Generic;

namespace JapanesePractice.Contract.Contexts
{
    /// <summary>
    /// Represents a set of <see cref="ICategory"/>s.
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// The <see cref="ICategory"/>s this <see cref="IContext"/> contains.
        /// </summary>
        ICollection<ICategory> Categories { get; }

        /// <summary>
        /// Returns the merged <see cref="ISymbol"/>s of the <see cref="IContext.Categories"/> where <see cref="ICategory.Name"/> was contained in <paramref name="categories"/>.
        /// </summary>
        /// <param name="categories">
        /// The names of the <see cref="ICategory"/>s contained within this <see cref="IContext.Categories"/> to merge the <see cref="ISymbol"/>s of.
        /// </param>
        /// <returns>
        /// A collection of merged <see cref="ISymbol"/>s.
        /// </returns>
        IEnumerable<ISymbol> Condense(params string[] categories);
    }
}
