using System;
using JapanesePractice.Contract;
using JapanesePractice.Contract.Selectors;
using JapanesePractice.Contract.Utility;

namespace JapanesePractice.FrontEnd.Debug
{
    public class ConsoleSymbolSelector : ISymbolSelector
    {
        public ConsoleSymbolSelector()
        {
            this.ExpectedSymbol = null;
        }

        public ISymbol ExpectedSymbol { get; set; }

        public ISymbol SelectFrom(ICategory category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            else if (category.Symbols.Count == 0)
            {
                throw new ArgumentException("Supplied category did not contain any symbols.", nameof(category));
            }

            if (this.ExpectedSymbol == null)
            {
                return category.Symbols[ThreadSafeRandom.Singleton.Next(category.Symbols.Count)];
            }
            else
            {
                return this.ExpectedSymbol;
            }
        }

        private class ThreadSafeRandom : IRandomSource
        {
            public static readonly ThreadSafeRandom Singleton = new ThreadSafeRandom();

            private Random innerRandom;
            private object randomLock;

            public ThreadSafeRandom()
            {
                this.innerRandom = new Random();
                this.randomLock = new object();
            }

            public int Next(int maxValue)
            {
                lock (this.randomLock)
                {
                    return this.innerRandom.Next(maxValue);
                }
            }
        }
    }
}
