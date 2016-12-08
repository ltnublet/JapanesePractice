using System;
using System.Collections.Generic;
using System.Linq;

namespace JapanesePractice
{
    /// <summary>
    /// An arbitrary symbol and its associated interpretations.
    /// </summary>
    public class Symbol<T>
    {
        /// <summary>
        /// Instantiates a <see cref="Symbol{T}"/> using the supplied values.
        /// </summary>
        /// <param name="actual">
        /// The actual value.
        /// </param>
        /// <param name="expected">
        /// The permitted expected values.
        /// </param>
        public Symbol(T actual, IEnumerable<T> expected)
        {
            this.Actual = actual;
            this.Expected = expected;
        }

        /// <summary>
        /// The supplied symbol.
        /// </summary>
        public T Actual { get; private set; }

        /// <summary>
        /// The symbol's allowed interpretations.
        /// </summary>
        public IEnumerable<T> Expected { get; private set; }

        /// <summary>
        /// Compares the supplied values to the expected values.
        /// </summary>
        /// <param name="supplied">
        /// The values which will be compared with the expected values.
        /// </param>
        /// <returns>
        /// Whether or not any of the supplied values satisfied the expected values.
        /// </returns>
        public bool Evaluate(IEnumerable<T> supplied)
        {
            return this.Evaluate(supplied, (expected, actual) => expected.NullComparer(actual));
        }

        /// <summary>
        /// Compares the supplied values to the expected values.
        /// </summary>
        /// <param name="supplied">
        /// The values which will be compared with the expected values.
        /// </param>
        /// <param name="comparator">
        /// The means by which to compare an instance of an expected and actual pair. Expected to return true when two <see cref="T"/>s are equivalent.
        /// </param>
        /// <returns>
        /// True if any of the supplied values satisfied the expected constraints given the supplied comparator.
        /// </returns>
        public bool Evaluate(IEnumerable<T> supplied, Func<T, T, bool> comparator)
        {
            return this.Expected.Any(expected => supplied.Any(actual => comparator(expected, actual)));
        }
    }
}
