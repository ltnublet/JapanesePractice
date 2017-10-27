using System;
using System.Collections.Generic;
using System.Linq;
using JapanesePractice.Contract.Contexts;
using JapanesePractice.Contract.Selectors;
using JapanesePractice.Contract.Utility;

namespace JapanesePractice.Contract.ReferenceImplementation
{
    /// <summary>
    /// A implementation of <see cref="ICategorySelector"/> which will select randomly from the supplied set of <see cref="ICategory"/>s.
    /// </summary>
    public sealed class RandomCategorySelector : ICategorySelector
    {
        private IRandomSource source;

        /// <summary>
        /// Instantiates a new <see cref="RandomCategorySelector"/> using the supplied <see cref="IRandomSource"/> <paramref name="source"/> as the source of randomness.
        /// </summary>
        /// <param name="source">
        /// The source of randomness the <see cref="RandomCategorySelector"/> should use.
        /// </param>
        public RandomCategorySelector(IRandomSource source)
        {
            this.source = source;
        }

        /// <summary>
        /// Randomly selects a <see cref="ICategory"/> from the supplied <see cref="IContext"/> <paramref name="context"/>.
        /// </summary>
        /// <param name="context">
        /// The <see cref="IContext"/> from which to select the <see cref="ICategory"/>.
        /// </param>
        /// <returns>
        /// A randomly selected <see cref="ICategory"/> from the <see cref="IContext"/> <paramref name="context"/>.
        /// </returns>
        public ICategory SelectFrom(IContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            List<ICategory> categories = context.Categories.ToList();

            if (categories.Count == 0)
            {
                throw new ArgumentException("Supplied context does not contain any categories.", nameof(context));
            }

            return categories[this.source.Next(categories.Count)];
        }
    }
}
