using System;
using System.Collections.Generic;
using System.Linq;
using JapanesePractice.Contract;
using JapanesePractice.Contract.Contexts;

namespace JapanesePractice.Textual
{
    /// <summary>
    /// Represents a set of <see cref="Category"/>s which contain <see cref="TextualInterpretation"/> <see cref="IInterpretation"/>s.
    /// </summary>
    public class TextualContext : IContext
    {
        /// <summary>
        /// Instantiates a new <see cref="TextualContext"/> with the initial set of <see cref="Category"/>s <paramref name="categories"/>.
        /// </summary>
        /// <param name="categories">
        /// The initial set of <see cref="Category"/>s this <see cref="TextualContext"/> contains.
        /// </param>
        public TextualContext(IEnumerable<Category> categories)
        {
            this.Categories = categories.ToList();
        }

        /// <summary>
        /// The <see cref="Category"/>s this <see cref="IContext"/> contains.
        /// </summary>
        public ICollection<Category> Categories { get; }

        /// <summary>
        /// Returns the merged <see cref="Symbol"/>s of the <see cref="TextualContext.Categories"/> where <see cref="Category.Name"/> was contained in <paramref name="categories"/>.
        /// </summary>
        /// <param name="categories">
        /// The names of the <see cref="Category"/>s contained within this <see cref="TextualContext.Categories"/> to merge the <see cref="Symbol"/>s of.
        /// </param>
        /// <returns>
        /// A collection of merged <see cref="Symbol"/>s.
        /// </returns>
        public IEnumerable<Symbol> Condense(params string[] categories)
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
