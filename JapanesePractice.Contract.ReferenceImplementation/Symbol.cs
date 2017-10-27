using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JapanesePractice.Contract.Interpretations;

namespace JapanesePractice.Contract.ReferenceImplementation
{
    /// <summary>
    /// Represents a mapping between a <see cref="string"/> name and a set of <see cref="IInterpretation"/> interpretations. <see cref="Symbol"/>s with the same <see cref="Symbol.Name"/> are considered the same, even if their <see cref="Symbol.Interpretations"/> aren't.
    /// </summary>
    public class Symbol : ISymbol
    {
        /// <summary>
        /// Instantiates a new <see cref="Symbol"/> using the supplied <paramref name="name"/> and initial set of <paramref name="interpretations"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the <see cref="Symbol"/>.
        /// </param>
        /// <param name="interpretations">
        /// The initial set of <see cref="IInterpretation"/>s this <see cref="Symbol"/> has.
        /// </param>
        public Symbol(string name, IEnumerable<IInterpretation> interpretations)
        {
            this.Name = name;
            this.Interpretations = new InterpretationCollection(interpretations);
        }

        /// <summary>
        /// Instantiates a new <see cref="Symbol"/> using the supplied <paramref name="name"/> and initial set of <paramref name="interpretations"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the <see cref="Symbol"/>.
        /// </param>
        /// <param name="interpretations">
        /// The initial set of <see cref="IInterpretation"/>s this <see cref="Symbol"/> has.
        /// </param>
        public Symbol(string name, params IInterpretation[] interpretations)
            : this(name, interpretations.AsEnumerable())
        {
        }

        /// <summary>
        /// The name of the <see cref="Symbol"/>. <see cref="Symbol"/>s with the same <see cref="Symbol.Name"/> are considered the same, even if their <see cref="Symbol.Interpretations"/> aren't.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The set of <see cref="IInterpretation"/>s this <see cref="Symbol"/> is mapped to.
        /// </summary>
        public InterpretationCollection Interpretations { get; }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current object.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "({0}): {1}",
                this.Name,
                string.Join("; ", this.Interpretations.Select(x => x.ToString())));
        }
    }
}
