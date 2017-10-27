using System;
using JapanesePractice.Contract.Selectors;
using JapanesePractice.Contract.Utility;

namespace JapanesePractice.Contract.ReferenceImplementation
{
    /// <summary>
    /// A implementation of <see cref="ISymbolSelector"/> which will select randomly from the supplied set of <see cref="ICategory"/>s.
    /// </summary>
    public sealed class RandomSymbolSelector : ISymbolSelector
    {
        private IRandomSource source;

        /// <summary>
        /// Instantiates a new <see cref="RandomCategorySelector"/> using the supplied <see cref="IRandomSource"/> <paramref name="source"/> as the source of randomness.
        /// </summary>
        /// <param name="source">
        /// The source of randomness the <see cref="RandomCategorySelector"/> should use.
        /// </param>
        public RandomSymbolSelector(IRandomSource source)
        {
            this.source = source;
        }

        /// <summary>
        /// Randomly selects a <see cref="ISymbol"/> from the supplied <see cref="ICategory"/> <paramref name="category"/>.
        /// </summary>
        /// <param name="category">
        /// The <see cref="ICategory"/> from which to select the <see cref="ISymbol"/>.
        /// </param>
        /// <returns>
        /// A randomly selected <see cref="ISymbol"/> from the <see cref="ICategory"/> <paramref name="category"/>.
        /// </returns>
        public ISymbol SelectFrom(ICategory category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            else if (category.Symbols.Count == 0)
            {
                throw new ArgumentException("Supplied category does not contain any symbols.", nameof(category));
            }

            return category.Symbols[this.source.Next(category.Symbols.Count)];
        }
    }
}
