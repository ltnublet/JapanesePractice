using System;
using System.Collections.Generic;
using System.Linq;

namespace JapanesePractice.Contract.Interpretations
{
    /// <summary>
    /// Represents an <see cref="IInterpretation"/> (generally of a <see cref="Symbol"/>) in the form of a set of type <typeparamref name="T"/>s which are considered equivalent.
    /// </summary>
    /// <typeparam name="T">The inner type of the collection of interpretations which are considered equivalent.</typeparam>
    public abstract class Interpretation<T> : IInterpretation
    {
        /// <summary>
        /// Instantiates a new <see cref="Interpretation{T}"/> with the specified <paramref name="permittedRepresentations"/>.
        /// </summary>
        /// <param name="permittedRepresentations">
        /// The initial set of interpretations.
        /// </param>
        protected Interpretation(params T[] permittedRepresentations)
            : this(permittedRepresentations.AsEnumerable())
        {
        }

        /// <summary>
        /// Instantiates a new <see cref="Interpretation{T}"/> with the specified <paramref name="permittedRepresentations"/>.
        /// </summary>
        /// <param name="permittedRepresentations">
        /// The initial set of interpretations.
        /// </param>
        protected Interpretation(IEnumerable<T> permittedRepresentations)
        {
            if (permittedRepresentations == null)
            {
                throw new ArgumentNullException(nameof(permittedRepresentations));
            }
            else if (!permittedRepresentations.Any())
            {
                throw new ArgumentException(
                    FormattableString.Invariant($"{nameof(permittedRepresentations)} cannot be empty."));
            }

            this.PermittedRepresentations = permittedRepresentations;
        }

        /// <summary>
        /// Retrieves the primary representation for this <see cref="Interpretation{T}"/>. The primary representation is the first value in <see cref="Interpretation{T}.PermittedRepresentations"/>.
        /// </summary>
        public virtual T PrimaryRepresentation
        {
            get
            {
                return this.PermittedRepresentations.First();
            }

            protected set
            {
                if (value == null)
                {
                    throw new InvalidOperationException("Primary representation cannot be null.");
                }
                else
                {
                    this.PermittedRepresentations = new T[] { value }.Concat(this.PermittedRepresentations);
                }
            }
        }

        /// <summary>
        /// The set of permitted representations for this <see cref="Interpretation{T}"/>. This is the set of interpretations which are considered equivalent.
        /// </summary>
        public virtual IEnumerable<T> PermittedRepresentations { get; protected set; }

        /// <summary>
        /// Compares the current instance to the supplied <see cref="IInterpretation"/> <paramref name="other"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="IInterpretation"/> to compare the current instance to.
        /// </param>
        /// <returns>
        /// True if equivalent; false otherwise.
        /// </returns>
        public virtual bool Compare(IInterpretation other)
        {
            if (other == null)
            {
                return false;
            }

            return new HashSet<object>(other.GetPermittedInterpretations())
                .SetEquals(this.GetPermittedInterpretations());
        }

        /// <summary>
        /// Checks that all of the <see cref="IInterpretation"/>s in the supplied <paramref name="list"/> are equivalent to the current instance.
        /// </summary>
        /// <param name="list">
        /// The collection of <see cref="IInterpretation"/>s to compare the current instance to.
        /// </param>
        /// <returns>
        /// True if any of the <see cref="IInterpretation"/>s in <paramref name="list"/> were equivalent; false otherwise.
        /// </returns>
        public virtual bool CompareAll(IEnumerable<IInterpretation> list)
        {
            return list.All(item => this.Compare(item));
        }

        /// <summary>
        /// Checks that any of the <see cref="IInterpretation"/>s in the supplied <paramref name="list"/> are equivalent to the current instance.
        /// </summary>
        /// <param name="list">
        /// The collection of <see cref="IInterpretation"/>s to compare the current instance to.
        /// </param>
        /// <returns>
        /// True if all of the <see cref="IInterpretation"/>s in <paramref name="list"/> were equivalent; false otherwise.
        /// </returns>
        public virtual bool CompareAny(IEnumerable<IInterpretation> list)
        {
            return list.Any(item => this.Compare(item));
        }

        /// <summary>
        /// Returns a collection of all the permitted representations of the current instance.
        /// </summary>
        /// <returns>
        /// A collection of all the permitted representations of the current instance.
        /// </returns>
        public IEnumerable<object> GetPermittedInterpretations()
        {
            return this.PermittedRepresentations.Cast<object>();
        }
    }
}
