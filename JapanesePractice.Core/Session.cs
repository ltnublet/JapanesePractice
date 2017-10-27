using System;
using System.Collections.Generic;
using JapanesePractice.Contract.Contexts;
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

        private List<CallbackStatePair> beforeDisposalCallbacks;
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

            this.beforeDisposalCallbacks = new List<CallbackStatePair>();
            this.isDisposed = false;
        }

        /// <summary>
        /// Registers a callback which will be triggered after <see cref="Session.Dispose()"/> is called, but before disposal begins.
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
        public IDisposable RegisterDisposedCallback(Action<object> callback, object state)
        {
            CallbackStatePair pair = new CallbackStatePair(this.beforeDisposalCallbacks, callback, state);
            this.beforeDisposalCallbacks.Add(pair);

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Performance",
            "CA1811:AvoidUncalledPrivateCode",
            Justification = "Debugging")]
        internal void Debug()
        {
            // TODO: Delete me.

            this.context.ToString();
            this.symbolSelector.ToString();
            this.categorySelector.ToString();
            this.interpretationSelectors.ToString();
            throw new NotImplementedException("This method only exists to make code analysis stop complaining while I work");
        }

        /// <summary>
        /// Extension point for classes which inherit from <see cref="Session"/>. Occurs after any callbacks, but before <see cref="Session.Dispose()"/> begins disposing.
        /// </summary>
        /// <param name="disposing">
        /// Indicates whether or not this method is being called from <see cref="Session.Dispose()"/>.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // This method intentionally left blank.
        }

        private sealed class CallbackStatePair : IDisposable
        {
            private List<CallbackStatePair> collection;
            private bool isDisposed;

            public CallbackStatePair(List<CallbackStatePair> collection, Action<object> callback, object state)
            {
                this.collection = collection;
                this.Callback = callback;
                this.State = state;

                this.isDisposed = false;
            }

            public Action<object> Callback { get; private set; }
            public object State { get; private set; }

            public void Dispose()
            {
                if (!this.isDisposed)
                {
                    this.isDisposed = true;
                    int index = this.collection.IndexOf(this);
                    if (index > -1)
                    {
                        this.collection.RemoveAt(index);
                    }
                }
            }
        }
    }
}
