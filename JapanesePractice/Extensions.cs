using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapanesePractice
{
    /// <summary>
    /// Class containing extension methods which apply generally.
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Compares two objects of type T.
        /// </summary>
        /// <typeparam name="T">
        /// The type of object which will be compared.
        /// </typeparam>
        /// <param name="expected">
        /// The expected object.
        /// </param>
        /// <param name="actual">
        /// The actual object.
        /// </param>
        /// <returns>
        /// True if the objects are equivalent. Equivalent here means either both null, or <param name="expected" />.Equals(<param name="actual" />) returns true.
        /// </returns>
        public static bool NullComparer<T>(this T expected, T actual)
        {
            return expected.NullComparer(actual, (x, y) => x.Equals(y));
        }

        /// <summary>
        /// Compares two objects of type T.
        /// </summary>
        /// <typeparam name="T">
        /// The type of object which will be compared.
        /// </typeparam>
        /// <param name="expected">
        /// The expected object.
        /// </param>
        /// <param name="actual">
        /// The actual object.
        /// </param>
        /// <param name="comparer">
        /// The means by which to compare the supplied objects.
        /// </param>
        /// <returns>
        /// True if the objects are equivalent. Equivalent here means either both null, or satisfying the comparer.
        /// </returns>
        public static bool NullComparer<T>(this T expected, T actual, Func<T, T, bool> comparer)
        {
            return expected == null ? actual == null : comparer.Invoke(expected, actual);
        }
    }
}
