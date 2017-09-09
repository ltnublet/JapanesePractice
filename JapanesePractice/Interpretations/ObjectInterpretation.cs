using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapanesePractice.Interpretations
{
    /// <summary>
    /// A concrete instance of <see cref="Interpretation{T}"/> with type <see cref="object"/>. Usage of this type should generally be avoided, unless the type of the contained objects is not known until runtime.
    /// </summary>
    internal class ObjectInterpretation : Interpretation<object>
    {
        /// <summary>
        /// Instantiates a new <see cref="ObjectInterpretation"/> with the specified <paramref name="permittedRepresentations"/>.
        /// </summary>
        /// <param name="permittedRepresentations">
        /// The initial set of interpretations.
        /// </param>
        public ObjectInterpretation(IEnumerable<object> permittedRepresentations)
            : base((IEnumerable<object>)permittedRepresentations)
        {
        }

        /// <summary>
        /// Instantiates a new <see cref="ObjectInterpretation"/> with the specified <paramref name="permittedRepresentations"/>.
        /// </summary>
        /// <param name="permittedRepresentations">
        /// The initial set of interpretations.
        /// </param>
        public ObjectInterpretation(params object[] permittedRepresentations)
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
            if (other == null)
            {
                return false;
            }

            return new HashSet<object>(other.GetPermittedInterpretations())
                .SetEquals(this.GetPermittedInterpretations());
        }
    }
}
