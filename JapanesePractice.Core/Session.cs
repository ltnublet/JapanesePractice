using System;
using System.Collections.Generic;
using JapanesePractice.Contract;
using JapanesePractice.Contract.Contexts;
using JapanesePractice.Contract.Interpretations;
using JapanesePractice.Contract.Selectors;

namespace JapanesePractice.Core
{
    /// <summary>
    /// Represents a session instance.
    /// </summary>
    public class Session : IDisposable
    {
        private IContext context;
        private ICategorySelector categorySelector;
        private ISymbolSelector symbolSelector;
        private InterpretationSelectorTable interpretationSelectors;

        private SynchronizedCollection<CallbackStatePair> beforeDisposalCallbacks;
        private bool isDisposed;

        /// <summary>
        /// Initializes a new <see cref="Session"/> using the supplied parameters.
        /// </summary>
        /// <param name="context">
        /// The <see cref="IContext"/> to use for this <see cref="Session"/>.
        /// </param>
        /// <param name="categorySelector">
        /// The <see cref="ICategorySelector"/> to use for this <see cref="Session"/>.
        /// </param>
        /// <param name="symbolSelector">
        /// The <see cref="ISymbolSelector"/> to use for this <see cref="Session"/>.
        /// </param>
        /// <param name="interpretationSelectors">
        /// The <see cref="InterpretationSelectorTable"/> to use for this <see cref="Session"/>.
        /// </param>
        public Session(
            IContext context,
            ICategorySelector categorySelector,
            ISymbolSelector symbolSelector,
            InterpretationSelectorTable interpretationSelectors)
        {
            this.context = context;
            this.symbolSelector = symbolSelector;
            this.categorySelector = categorySelector;
            this.interpretationSelectors = interpretationSelectors;

            this.beforeDisposalCallbacks = new SynchronizedCollection<CallbackStatePair>();
            this.isDisposed = false;
        }

        /// <summary>
        /// Registers a callback which will be triggered after <see cref="Dispose()"/> is called, but before disposal begins.
        /// </summary>
        /// <param name="callback">
        /// The callback to register. When invoked, <paramref name="callback"/> will be provided the supplied <paramref name="state"/>.
        /// </param>
        /// <param name="state">
        /// Any state which <paramref name="callback"/> depends on.
        /// </param>
        /// <returns>
        /// An <see cref="IDisposable"/> which, if disposed, unregisters the <paramref name="callback"/>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1030:UseEventsWhereAppropriate",
            Justification = "Callback is more than delegate.")]
        public IDisposable AddOnDisposalCallback(Action<object> callback, object state)
        {
            CallbackStatePair pair = new CallbackStatePair(callback, state);
            this.beforeDisposalCallbacks.Add(pair);
            pair.AddOnDisposalCallback(this.RemoveCallback);

            return pair;
        }

        /// <summary>
        /// Registers the <see cref="Session"/> for disposal, triggering any registered callbacks before releasing any managed or unmanaged resources belonging to the <see cref="Session"/> instance.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1063:ImplementIDisposableCorrectly",
            Justification = "Spurious.")]
        public void Dispose()
        {
            if (!this.isDisposed)
            {
                GC.SuppressFinalize(this);
                this.isDisposed = true;
                foreach (CallbackStatePair pair in this.beforeDisposalCallbacks)
                {
                    pair.Callback.Invoke(pair.State);
                }

                this.Dispose(true);
            }
        }

        /// <summary>
        /// Selects an <see cref="ICategory"/> from the <see cref="Session"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="ICategory"/> contained by the <see cref="Session"/>.
        /// </returns>
        public ICategory SelectCategory()
        {
            return this.categorySelector.SelectFrom(this.context);
        }

        /// <summary>
        /// Selects an <see cref="IInterpretation"/> from the <see cref="ISymbol"/> <paramref name="symbol"/>.
        /// </summary>
        /// <param name="symbol">
        /// The <see cref="ISymbol"/> from which to select the <see cref="IInterpretation"/>.
        /// </param>
        /// <returns>
        /// An <see cref="IInterpretation"/> contained within the supplied <see cref="ISymbol"/> <paramref name="symbol"/>'s <see cref="ISymbol.Interpretations"/>.
        /// </returns>
        public IInterpretation SelectInterpretation(ISymbol symbol)
        {
            if (symbol == null)
            {
                throw new ArgumentNullException(nameof(symbol));
            }

            return this.interpretationSelectors[symbol.GetType()].SelectFrom(symbol.Interpretations);
        }

        /// <summary>
        /// Selects an <see cref="ISymbol"/> from the <see cref="ICategory"/> <paramref name="category"/>.
        /// </summary>
        /// <param name="category">
        /// The <see cref="ICategory"/> from which to select the <see cref="ISymbol"/>.
        /// </param>
        /// <returns>
        /// An <see cref="ISymbol"/> selected from the <see cref="ICategory"/> <paramref name="category"/>.
        /// </returns>
        public ISymbol SelectSymbol(ICategory category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            return this.symbolSelector.SelectFrom(category);
        }

        /// <summary>
        /// Extension point for classes which inherit from <see cref="Session"/>. Occurs after any callbacks, but before <see cref="Session.Dispose()"/> begins disposing.
        /// </summary>
        /// <param name="disposing">
        /// Indicates whether or not this method is being called from <see cref="Session.Dispose()"/>.
        /// </param>
        protected virtual void Dispose(bool disposing = false)
        {
            // This method intentionally left blank.
        }

        private void RemoveCallback(CallbackStatePair toRemove)
        {
            this.beforeDisposalCallbacks.Remove(toRemove);
        }

        /// <summary>
        /// Represents a pairing of a callback, and some state associated with the callback.
        /// </summary>
        protected sealed class CallbackStatePair : IDisposable
        {
            private bool isDisposed;
            private List<Action<CallbackStatePair>> onDisposals;

            /// <summary>
            /// Creates a new instance of a <see cref="CallbackStatePair"/> using the supplied arguments.
            /// </summary>
            /// <param name="callback">
            /// The operation associated with this callback.
            /// </param>
            /// <param name="state">
            /// Any state associated with this callback; it is expected that the <paramref name="callback"/> understands how to interpret it.
            /// </param>
            public CallbackStatePair(Action<object> callback, object state = null)
            {
                this.onDisposals = new List<Action<CallbackStatePair>>();

                this.Callback = callback;
                this.State = state;

                this.isDisposed = false;
            }

            /// <summary>
            /// The operation associated with this callback.
            /// </summary>
            public Action<object> Callback { get; private set; }

            /// <summary>
            /// Any state associated with this callback.
            /// </summary>
            public object State { get; private set; }

            /// <summary>
            /// Adds the supplied callback <paramref name="action"/> to the set of callbacks which will be invoked upon disposal of the <see cref="CallbackStatePair"/>.
            /// </summary>
            /// <param name="action">
            /// The callback to invoke upon disposal of this <see cref="CallbackStatePair"/>.
            /// </param>
            [System.Diagnostics.CodeAnalysis.SuppressMessage(
                "Microsoft.Design",
                "CA1030:UseEventsWhereAppropriate",
                Justification = "Callback is more than delegate.")]
            public void AddOnDisposalCallback(Action<CallbackStatePair> action)
            {
                if (action == null)
                {
                    throw new ArgumentNullException(nameof(action));
                }

                this.onDisposals.Add(action);
            }

            /// <summary>
            /// Disposes of this <see cref="CallbackStatePair"/>.
            /// </summary>
            public void Dispose()
            {
                if (!this.isDisposed)
                {
                    this.isDisposed = true;
                    foreach (Action<CallbackStatePair> onDispose in this.onDisposals)
                    {
                        onDispose.Invoke(this);
                    }
                }
            }
        }
    }
}
