using System;
using JapanesePractice.Contract.Utility;

namespace JapanesePractice.Core
{
    internal class ThreadSafeRandom : IRandomSource
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
