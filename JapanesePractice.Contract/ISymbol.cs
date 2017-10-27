using JapanesePractice.Contract.Interpretations;

namespace JapanesePractice.Contract
{
    /// <summary>
    /// Represents a mapping between a <see cref="string"/> name and a set of <see cref="IInterpretation"/> interpretations. <see cref="ISymbol"/>s with the same <see cref="ISymbol.Name"/> are considered the same, even if their <see cref="ISymbol.Interpretations"/> aren't.
    /// </summary>
    public interface ISymbol
    {
        /// <summary>
        /// The name of the <see cref="ISymbol"/>. <see cref="ISymbol"/>s with the same <see cref="ISymbol.Name"/> are considered the same, even if their <see cref="ISymbol.Interpretations"/> aren't.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The set of <see cref="IInterpretation"/>s this <see cref="ISymbol"/> is mapped to.
        /// </summary>
        InterpretationCollection Interpretations { get; }
    }
}