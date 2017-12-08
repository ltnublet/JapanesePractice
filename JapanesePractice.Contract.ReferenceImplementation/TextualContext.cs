using System;
using System.Collections.Generic;
using System.Linq;
using JapanesePractice.Contract.Contexts;

namespace JapanesePractice.Contract.ReferenceImplementation
{
    /// <summary>
    /// Represents a set of <see cref="Category"/>s which contains <see cref="JapanesePractice.Contract.Interpretations.IInterpretation"/>s.
    /// </summary>
    public class Context : IContext
    {
        /// <summary>
        /// Instantiates a new <see cref="Context"/> with the initial set of <see cref="ICategory"/>s <paramref name="categories"/>.
        /// </summary>
        /// <param name="categories">
        /// The initial set of <see cref="ICategory"/>s this <see cref="Context"/> contains.
        /// </param>
        public Context(IEnumerable<ICategory> categories)
        {
            this.Categories = categories.ToList();
        }

        /// <summary>
        /// The <see cref="ICategory"/>s this <see cref="IContext"/> contains.
        /// </summary>
        public ICollection<ICategory> Categories { get; }

        /// <summary>
        /// Returns the merged <see cref="ISymbol"/>s of the <see cref="Context.Categories"/> where <see cref="ICategory.Name"/> was contained in <paramref name="categories"/>.
        /// </summary>
        /// <param name="categories">
        /// The names of the <see cref="ICategory"/>s contained within this <see cref="Context.Categories"/> to merge the <see cref="ISymbol"/>s of.
        /// </param>
        /// <returns>
        /// A collection of merged <see cref="ISymbol"/>s.
        /// </returns>
        public IEnumerable<ISymbol> Condense(params string[] categories)
        {
            if (categories == null)
            {
                throw new ArgumentNullException(nameof(categories));
            }

            return Category.Merge(this.Categories.Where(category => categories.Contains(category.Name)));
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current object.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Join(", ", this.Categories.Select(x => x.Name));
        }
    }
}
