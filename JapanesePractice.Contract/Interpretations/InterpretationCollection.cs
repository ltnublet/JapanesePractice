using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JapanesePractice.Contract.Interpretations
{
    /// <summary>
    /// Represents a collection of <see cref="IInterpretation"/>s.
    /// </summary>
    public class InterpretationCollection : IReadOnlyList<IInterpretation>
    {
        private List<IInterpretation> interpretations = new List<IInterpretation>();

        /// <summary>
        /// Instantiates a new <see cref="InterpretationCollection"/> using the specified <paramref name="interpretations"/> as the initial set of permitted interpretations.
        /// </summary>
        /// <param name="interpretations">
        /// The initial set of interpretations.
        /// </param>
        public InterpretationCollection(IEnumerable<IInterpretation> interpretations)
        {
            this.interpretations.AddRange(interpretations);
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="InterpretationCollection"/>.
        /// </summary>
        public int Count => this.interpretations.Count;

        /// <summary>
        /// Returns the <see cref="IInterpretation"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">
        /// The index of the <see cref="IInterpretation"/> in the <see cref="InterpretationCollection"/> to return.
        /// </param>
        /// <returns>
        /// The <see cref="IInterpretation"/> at the specified <paramref name="index"/>.
        /// </returns>
        public IInterpretation this[int index] => this.interpretations[index];

        /// <summary>
        /// Compares the current instance to the supplied <see cref="InterpretationCollection"/> <paramref name="other"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="InterpretationCollection"/> to compare the current instance to.
        /// </param>
        /// <param name="comparer">
        /// The means by which to compare any given two <see cref="IInterpretation"/> instances for equivalency. When null, the sequences returned by <see cref="IInterpretation.GetPermittedInterpretations"/> will be checked for exact equivalence.
        /// </param>
        /// <returns>
        /// True if equivalent; false otherwise.
        /// </returns>
        public bool Compare(
            InterpretationCollection other,
            Func<IInterpretation, IInterpretation, bool> comparer = null)
        {
            if (other == null)
            {
                return false;
            }

            return this.SequenceEqual(other, new InterpretationComparer(
                comparer ?? ((x, y) => x.GetPermittedInterpretations().SequenceEqual(y.GetPermittedInterpretations()))));
        }

        /// <summary>
        /// Returns a merged <see cref="IInterpretation"/> representing all <see cref="IInterpretation"/>s contained in the <see cref="InterpretationCollection"/>.
        /// </summary>
        /// <returns>
        /// A single <see cref="IInterpretation"/> representing all <see cref="IInterpretation"/>s in the <see cref="InterpretationCollection"/>.
        /// </returns>
        public IInterpretation Condense()
        {
            return new ObjectInterpretation(this.SelectMany(x => x.GetPermittedInterpretations()));
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="InterpretationCollection"/>.
        /// </summary>
        /// <returns>
        /// An enumerator that iterates through the <see cref="InterpretationCollection"/>.
        /// </returns>
        public IEnumerator<IInterpretation> GetEnumerator()
        {
            return this.interpretations.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="InterpretationCollection"/>.
        /// </summary>
        /// <returns>
        /// An enumerator that iterates through the <see cref="InterpretationCollection"/>.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class InterpretationComparer : IEqualityComparer<IInterpretation>
        {
            private Func<IInterpretation, IInterpretation, bool> comparer;

            public InterpretationComparer(Func<IInterpretation, IInterpretation, bool> comparer)
            {
                this.comparer = comparer;
            }

            public bool Equals(IInterpretation x, IInterpretation y)
            {
                bool xIsNull = x == null;
                bool yIsNull = x == null;

                if (xIsNull ^ yIsNull)
                {
                    return false;
                }
                else if (xIsNull && yIsNull)
                {
                    return true;
                }

                return this.comparer(x, y);
            }

            public int GetHashCode(IInterpretation obj)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(nameof(obj));
                }

                return obj.GetHashCode();
            }
        }
    }
}
