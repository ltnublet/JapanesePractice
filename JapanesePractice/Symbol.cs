﻿using System.Collections.Generic;
using System.Linq;
using JapanesePractice.Interpretations;

namespace JapanesePractice
{
    /// <summary>
    /// Represents a mapping between a <see cref="string"/> name and a set of <see cref="IInterpretation"/> interpretations. <see cref="Symbol"/>s with the same <see cref="Symbol.Name"/> are considered the same, even if their <see cref="Symbol.Interpretations"/> aren't.
    /// </summary>
    public class Symbol
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
            this.Interpretations = interpretations.ToList();
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
        public ICollection<IInterpretation> Interpretations { get; }
    }
}
