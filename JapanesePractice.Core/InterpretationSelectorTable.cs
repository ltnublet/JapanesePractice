using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JapanesePractice.Contract.Selectors;

namespace JapanesePractice.Core
{
    /// <summary>
    /// Represents a set of mappings from <see cref="Type"/> to <see cref="IInterpretationSelector"/>, with a fallback <see cref="IInterpretationSelector"/> if the set does not contain a given <see cref="Type"/>.
    /// </summary>
    public class InterpretationSelectorTable
    {
        private Dictionary<Type, IInterpretationSelector> selectors;
        private IInterpretationSelector fallback;

        /// <summary>
        /// Instantiates a new <see cref="InterpretationSelectorTable"/>, using the specified <see cref="IInterpretationSelector"/> <paramref name="fallbackSelector"/> as the fallback selector.
        /// </summary>
        /// <param name="fallbackSelector">
        /// The <see cref="IInterpretationSelector"/> to use when a <see cref="IInterpretationSelector"/> is requested, but no <see cref="IInterpretationSelector"/> was mapped to that <see cref="Type"/>.
        /// </param>
        public InterpretationSelectorTable(IInterpretationSelector fallbackSelector)
        {
            this.selectors = new Dictionary<Type, IInterpretationSelector>();
            this.fallback = fallbackSelector;
        }

        /// <summary>
        /// Gets or sets the <see cref="IInterpretationSelector"/> mapped to the specified <see cref="Type"/> <paramref name="type"/>.
        /// </summary>
        /// <param name="type">
        /// The <see cref="Type"/> to get or set the value of.
        /// </param>
        /// <returns>
        /// The <see cref="IInterpretationSelector"/> mapped to the supplied <see cref="Type"/> <paramref name="type"/>, or the fallback selector if no mapping was present.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1043:UseIntegralOrStringArgumentForIndexers",
            Justification = "Desired behaviour is that the InterpretationSelectorTable acts like a Dictionary<Type, IInterpretationSelector> with a fallback.")]
        public IInterpretationSelector this[Type type]
        {
            get
            {
                if (this.selectors.TryGetValue(type, out IInterpretationSelector result))
                {
                    return result;
                }
                else
                {
                    return this.fallback;
                }
            }
            set
            {
                this.selectors[type] = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>
        /// Determines whether the <see cref="InterpretationSelectorTable"/> has an explicit mapping for the supplied <see cref="Type"/> <paramref name="type"/>.
        /// </summary>
        /// <param name="type">
        /// The <see cref="Type"/> to check for an explicit mapping.
        /// </param>
        /// <returns>
        /// True if an explicit mapping was set; false otherwise.
        /// </returns>
        public bool ContainsKey(Type type) => this.selectors.ContainsKey(type);
    }
}
