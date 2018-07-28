using System.Collections.Generic;

namespace Drexel.LangLeopard.Contracts
{
    /// <summary>
    /// Represents a source of <see cref="IInterpretation{T}"/>s.
    /// </summary>
    /// <typeparam name="T">
    /// The generic parameter of the underlying <see cref="IInterpretation{T}"/>s.
    /// </typeparam>
    public interface IInterpretationSource<out T>
    {
        /// <summary>
        /// Gets the <see cref="IInterpretation{T}"/>s contained by this source.
        /// </summary>
        /// <returns>
        /// The <see cref="IInterpretation{T}"/>s contained by this source.
        /// </returns>
        IEnumerable<IInterpretation<T>> GetInterpretations();

        /// <summary>
        /// Gets the <see cref="IInterpretation{T}"/>s contained by this source for the specified <see cref="Symbol"/>
        /// <paramref name="symbol"/>.
        /// </summary>
        /// <param name="symbol">
        /// The <see cref="Symbol"/> to retrieve the <see cref="IInterpretation{T}"/>s for.
        /// </param>
        /// <returns>
        /// The <see cref="IInterpretation{T}"/>s contained by this source for the specified <see cref="Symbol"/>
        /// <paramref name="symbol"/>.
        /// </returns>
        IEnumerable<IInterpretation<T>> GetInterpretations(Symbol symbol);
    }
}
