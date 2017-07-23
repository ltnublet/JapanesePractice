using System.Collections.Generic;
using System.Linq;

namespace JapanesePractice.Interpretations
{
    /// <summary>
    /// A concrete instance of <see cref="Interpretation{T}"/> with type <see cref="string"/>.
    /// </summary>
    public class Textual : Interpretation<string>
    {
        private const string ToStringResultWhenNull = "{NULL}";
        private const string ToStringConcatDelimiter = ", ";

        /// <summary>
        /// Instantiates a new <see cref="Textual"/> with the specified <paramref name="permittedRepresentations"/>.
        /// </summary>
        /// <param name="permittedRepresentations">
        /// The initial set of interpretations.
        /// </param>
        public Textual(IEnumerable<string> permittedRepresentations)
            : base(permittedRepresentations)
        {
        }

        /// <summary>
        /// Instantiates a new <see cref="Textual"/> with the specified <paramref name="permittedRepresentations"/>.
        /// </summary>
        /// <param name="permittedRepresentations">
        /// The initial set of interpretations.
        /// </param>
        public Textual(params string[] permittedRepresentations)
            : base(permittedRepresentations)
        {
        }

        /// <summary>
        /// Compares the current instance to the supplied <see cref="IInterpretation"/> <paramref name="other"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="IInterpretation"/> to compare the current instance to.
        /// </param>
        /// <returns>
        /// True if equivalent; false otherwise.
        /// </returns>
        public override bool Compare(IInterpretation other)
        {
            return other is Textual value
                && new HashSet<string>(this.PermittedRepresentations).Overlaps(value.PermittedRepresentations);
        }

        /// <summary>
        /// Returns a string that represents the current <see cref="Textual"/>.
        /// </summary>
        /// <returns>
        /// A string that represents the current <see cref="Textual"/>.
        /// </returns>
        public override string ToString()
        {
            int count = this.PermittedRepresentations.Count();

            if (count == 0)
            {
                return Textual.ToStringResultWhenNull;
            }
            else if (count == 1)
            {
                return this.PrimaryRepresentation;
            }
            else
            {
                return string.Join(Textual.ToStringConcatDelimiter, this.PermittedRepresentations);
            }
        }
    }
}
