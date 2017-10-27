using JapanesePractice.Contract.Interpretations;

namespace JapanesePractice.Contract.Selectors
{
    /// <summary>
    /// Represents a class which is applies some set of constraints to select <see cref="IInterpretation"/>s from an <see cref="InterpretationCollection"/>.
    /// </summary>
    public interface IInterpretationSelector
    {
        /// <summary>
        /// Selects an <see cref="IInterpretation"/> from the supplied <see cref="InterpretationCollection"/> <paramref name="collection"/>.
        /// </summary>
        /// <param name="collection">
        /// The <see cref="InterpretationCollection"/> from which to select the <see cref="IInterpretation"/>.
        /// </param>
        /// <returns>
        /// An <see cref="IInterpretation"/> from the <see cref="InterpretationCollection"/> <paramref name="collection"/>.
        /// </returns>
        IInterpretation SelectFrom(InterpretationCollection collection);
    }
}
