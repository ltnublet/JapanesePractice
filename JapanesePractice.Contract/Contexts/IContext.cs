using System.Collections.Generic;

namespace JapanesePractice.Contract.Contexts
{
    /// <summary>
    /// Represents a set of <see cref="Category"/>s.
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// The <see cref="Category"/>s this <see cref="IContext"/> contains.
        /// </summary>
        ICollection<Category> Categories { get; }

        /// <summary>
        /// Returns the merged <see cref="Symbol"/>s of the <see cref="IContext.Categories"/> where <see cref="Category.Name"/> was contained in <paramref name="categories"/>.
        /// </summary>
        /// <param name="categories">
        /// The names of the <see cref="Category"/>s contained within this <see cref="IContext.Categories"/> to merge the <see cref="Symbol"/>s of.
        /// </param>
        /// <returns>
        /// A collection of merged <see cref="Symbol"/>s.
        /// </returns>
        IEnumerable<Symbol> Condense(params string[] categories);
    }
}
