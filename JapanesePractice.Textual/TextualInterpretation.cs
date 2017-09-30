using System.Collections.Generic;
using System.Linq;
using JapanesePractice.Interpretations;

namespace JapanesePractice.Textual
{
    /// <summary>
    /// A concrete instance of <see cref="Interpretation{T}"/> with type <see cref="string"/>.
    /// </summary>
    public class TextualInterpretation : Interpretation<string>
    {
        private const string ToStringResultWhenNull = "{NULL}";
        private const string ToStringConcatDelimiter = ", ";

        /// <summary>
        /// Instantiates a new <see cref="TextualInterpretation"/> with the specified <paramref name="permittedRepresentations"/>.
        /// </summary>
        /// <param name="permittedRepresentations">
        /// The initial set of interpretations.
        /// </param>
        public TextualInterpretation(IEnumerable<string> permittedRepresentations)
            : base(permittedRepresentations)
        {
        }

        /// <summary>
        /// Instantiates a new <see cref="TextualInterpretation"/> with the specified <paramref name="permittedRepresentations"/>.
        /// </summary>
        /// <param name="permittedRepresentations">
        /// The initial set of interpretations.
        /// </param>
        public TextualInterpretation(params string[] permittedRepresentations)
            : base(permittedRepresentations)
        {
        }

        /// <summary>
        /// Returns a string that represents the current <see cref="TextualInterpretation"/>.
        /// </summary>
        /// <returns>
        /// A string that represents the current <see cref="TextualInterpretation"/>.
        /// </returns>
        public override string ToString()
        {
            int count = this.PermittedRepresentations.Count();

            if (count == 0)
            {
                return TextualInterpretation.ToStringResultWhenNull;
            }
            else if (count == 1)
            {
                return this.PrimaryRepresentation;
            }
            else
            {
                return string.Join(TextualInterpretation.ToStringConcatDelimiter, this.PermittedRepresentations);
            }
        }
    }
}
