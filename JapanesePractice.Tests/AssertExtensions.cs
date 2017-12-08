using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JapanesePractice.Tests
{
    public static class AssertExtensions
    {
        public static void SequenceEqual<T>(this Assert assert, IEnumerable<T> expected, IEnumerable<T> actual)
        {
            bool expectedNull = expected == null;
            bool actualNull = actual == null;

            if (expectedNull && !actualNull)
            {
                throw new AssertFailedException("Expected null, actual was not null.");
            }
            else if (!expectedNull && actualNull)
            {
                throw new AssertFailedException("Expected not null, actual was null.");
            }
            else
            {
                IEnumerator<T> expectedEnumerator = expected.GetEnumerator();
                IEnumerator<T> actualEnumerator = actual.GetEnumerator();
                for (int index = 0; expectedEnumerator.MoveNext(); index++)
                {
                    if (!actualEnumerator.MoveNext())
                    {
                        throw new AssertFailedException($"Actual was shorter than expected (actual length: {index})");
                    }

                    if (!expectedEnumerator.Current.Equals(actualEnumerator.Current))
                    {
                        throw new AssertFailedException(
                            string.Format(
                                "Actual differed from expected at index `{0}`: expected `{1}`, actual `{2}`.",
                                index,
                                expectedEnumerator.Current,
                                actualEnumerator.Current));
                    }
                }
            }
        }
    }
}
