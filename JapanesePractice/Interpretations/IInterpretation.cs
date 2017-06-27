using System.Collections.Generic;

namespace JapanesePractice.Interpretations
{
    /// <summary>
    /// Represents an interpretation of a <see cref="Symbol"/>.
    /// </summary>
    public interface IInterpretation
    {
        /// <summary>
        /// Compares the current instance to the supplied <see cref="IInterpretation"/> <paramref name="other"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="IInterpretation"/> to compare the current instance to.
        /// </param>
        /// <returns>
        /// True if equivalent; false otherwise.
        /// </returns>
        bool Compare(IInterpretation other);

        /// <summary>
        /// Checks that any of the <see cref="IInterpretation"/>s in the supplied <paramref name="list"/> are equivalent to the current instance.
        /// </summary>
        /// <param name="list">
        /// The collection of <see cref="IInterpretation"/>s to compare the current instance to.
        /// </param>
        /// <returns>
        /// True if any of the <see cref="IInterpretation"/>s in <paramref name="list"/> were equivalent; false otherwise.
        /// </returns>
        bool CompareAny(IEnumerable<IInterpretation> list);

        /// <summary>
        /// Checks that all of the <see cref="IInterpretation"/>s in the supplied <paramref name="list"/> are equivalent to the current instance.
        /// </summary>
        /// <param name="list">
        /// The collection of <see cref="IInterpretation"/>s to compare the current instance to.
        /// </param>
        /// <returns>
        /// True if all of the <see cref="IInterpretation"/>s in <paramref name="list"/> were equivalent; false otherwise.
        /// </returns>
        bool CompareAll(IEnumerable<IInterpretation> list);
    }
}
