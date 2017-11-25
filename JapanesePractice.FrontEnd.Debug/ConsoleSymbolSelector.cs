using System;
using System.Collections.Generic;
using JapanesePractice.Contract;
using JapanesePractice.Contract.Selectors;
using JapanesePractice.Contract.Utility;

namespace JapanesePractice.FrontEnd.Debug
{
    public class ConsoleSymbolSelector : ISymbolSelector
    {
        private HashSet<ISymbol> disallowedSymbols;

        public ConsoleSymbolSelector()
        {
            this.ExpectedSymbol = null;
            this.disallowedSymbols = new HashSet<ISymbol>();
        }

        public IEnumerable<ISymbol> DisallowedOnNextSelectSymbols => this.disallowedSymbols;

        public ISymbol ExpectedSymbol { get; set; }

        public void AddDisallowedOnNextSelectSymbol(ISymbol disallowed)
        {
            this.disallowedSymbols.Add(disallowed);
        }

        public void RemoveDisallowedOnNextSelectSymbol(ISymbol reallowed)
        {
            this.disallowedSymbols.Remove(reallowed);
        }

        public ISymbol SelectFrom(ICategory category)
        {
            if (this.ExpectedSymbol != null)
            {
                return this.ExpectedSymbol;
            }

            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            if (category.Symbols.Count == 0)
            {
                throw new ArgumentException("Supplied category did not contain any symbols.", nameof(category));
            }
            else if (this.disallowedSymbols.Overlaps(category)
                && category.Symbols.Count < this.disallowedSymbols.Count)
            {
                throw new ArgumentException(
                    "Supplied category did not contain any symbols which were allowed.",
                    nameof(category));
            }

            while (true)
            {
                ISymbol retVal = category.Symbols[ThreadSafeRandom.Singleton.Next(category.Symbols.Count)];
                if (!this.disallowedSymbols.Contains(retVal))
                {
                    this.disallowedSymbols.Clear();
                    return retVal;
                }
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
