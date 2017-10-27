using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JapanesePractice.Contract.Interpretations;
using JapanesePractice.Contract.Selectors;
using JapanesePractice.Contract.Utility;

namespace JapanesePractice.Contract.ReferenceImplementation
{
    /// <summary>
    /// A implementation of <see cref="IInterpretationSelector"/> which will select randomly from a supplied <see cref="InterpretationCollection"/>.
    /// </summary>
    public class RandomInterpretationSelector : IInterpretationSelector
    {
        private IRandomSource source;

        /// <summary>
        /// Instantiates a new <see cref="RandomInterpretationSelector"/> using the supplied <see cref="IRandomSource"/> <paramref name="source"/> as the source of randomness.
        /// </summary>
        /// <param name="source">
        /// The source of randomness the <see cref="RandomInterpretationSelector"/> should use.
        /// </param>
        public RandomInterpretationSelector(IRandomSource source)
        {
            this.source = source;
        }

        /// <summary>
        /// Randomly selects a <see cref="IInterpretation"/> from the supplied <see cref="InterpretationCollection"/> <paramref name="collection"/>.
        /// </summary>
        /// <param name="collection">
        /// The <see cref="InterpretationCollection"/> from which to select the <see cref="IInterpretation"/>.
        /// </param>
        /// <returns>
        /// A randomly selected <see cref="IInterpretation"/> from the <see cref="InterpretationCollection"/> <paramref name="collection"/>.
        /// </returns>
        public IInterpretation SelectFrom(InterpretationCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            else if (collection.Count == 0)
            {
                throw new ArgumentException("Supplied collection does not contain any interpretations.", nameof(collection));
            }

            return collection[this.source.Next(collection.Count)];
        }
    }
}
