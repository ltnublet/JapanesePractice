using System;
using System.Collections.Generic;
using JapanesePractice.Contract;
using JapanesePractice.Contract.Contexts;
using JapanesePractice.Contract.ReferenceImplementation;
using JapanesePractice.Contract.Selectors;
using JapanesePractice.Contract.Interpretations;

namespace JapanesePractice.Core
{
    /// <summary>
    /// Represents a state context from which <see cref="Session"/>s should be produced.
    /// </summary>
    public class SessionBuilder
    {
        private ICategorySelector categorySelector;
        private ISymbolSelector symbolSelector;
        private InterpretationSelectorTable interpretationSelectors;

        /// <summary>
        /// Initializes a new <see cref="SessionBuilder"/>.
        /// </summary>
        public SessionBuilder()
        {
            this.categorySelector = new RandomCategorySelector(ThreadSafeRandom.Singleton);
            this.symbolSelector = new RandomSymbolSelector(ThreadSafeRandom.Singleton);
            this.interpretationSelectors = new InterpretationSelectorTable(new RandomInterpretationSelector(ThreadSafeRandom.Singleton));
        }

        /// <summary>
        /// Creates a new <see cref="Session"/> using the current state of the <see cref="SessionBuilder"/>.
        /// </summary>
        /// <param name="context">
        /// The <see cref="IContext"/> to use for the produced <see cref="Session"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Session"/> using the state of the <see cref="SessionBuilder"/> and the supplied <see cref="IContext"/> <paramref name="context"/>.
        /// </returns>
        public Session BuildSession(IContext context)
        {
            return new Session(
                context,
                this.categorySelector,
                this.symbolSelector,
                this.interpretationSelectors);
        }

        /// <summary>
        /// Fluently modifies this <see cref="SessionBuilder"/> to use the supplied <see cref="ICategorySelector"/> <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">
        /// The <see cref="ICategorySelector"/> this <see cref="SessionBuilder"/> will use.
        /// </param>
        /// <returns>
        /// This <see cref="SessionBuilder"/>, using the specified <see cref="ICategorySelector"/> <paramref name="selector"/>.
        /// </returns>
        public SessionBuilder UsingCategorySelector(ICategorySelector selector)
        {
            this.categorySelector = selector;
            return this;
        }

        /// <summary>
        /// Fluently modifies this <see cref="SessionBuilder"/> to use the supplied <see cref="ISymbolSelector"/> <paramref name="selector"/>.
        /// </summary>
        /// <param name="selector">
        /// The <see cref="ISymbolSelector"/> this <see cref="SessionBuilder"/> will use.
        /// </param>
        /// <returns>
        /// This <see cref="SessionBuilder"/>, using the specified <see cref="ISymbolSelector"/> <paramref name="selector"/>.
        /// </returns>
        public SessionBuilder UsingSymbolSelector(ISymbolSelector selector)
        {
            this.symbolSelector = selector;
            return this;
        }

        /// <summary>
        /// Fluently modifies this <see cref="SessionBuilder"/> to use the supplied <see cref="IInterpretationSelector"/> <paramref name="selector"/> for the given <see cref="Type"/> <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The specific type of <see cref="ISymbol"/> for which the <see cref="IInterpretationSelector"/> <paramref name="selector"/> is to be mapped. This <see cref="Type"/> must implement <see cref="IInterpretation"/>.
        /// </typeparam>
        /// <param name="selector">
        /// The <see cref="IInterpretationSelector"/> to map to the specified <see cref="Type"/> <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// This <see cref="SessionBuilder"/>, with the specified <see cref="IInterpretationSelector"/> <paramref name="selector"/> mapped to the supplied <see cref="Type"/> <typeparamref name="T"/>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter",
            Justification = "Copying the style of the ServiceBuilder in ASP.NET; it's not really necessary, but it looks cool!")]
        public SessionBuilder UsingInterpretationSelector<T>(IInterpretationSelector selector)
            where T : IInterpretation
        {
            Type type = typeof(T);
            if (this.interpretationSelectors.ContainsKey(type))
            {
                throw new InvalidOperationException(
                    "Session builder already contains a selector for the specified type.");
            }

            this.interpretationSelectors[typeof(T)] = selector;
            return this;
        }
    }
}
