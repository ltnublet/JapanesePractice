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
        /// Compares two <see cref="Object"/>s of type T.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="Object"/> which will be compared.
        /// </typeparam>
        /// <param name="expected">
        /// The expected <see cref="Object"/>.
        /// </param>
        /// <param name="actual">
        /// The actual <see cref="Object"/>.
        /// </param>
        /// <returns>
        /// True if the <see cref="Object"/>s are equivalent. Equivalent here means either both null, or <param name="expected" />.Equals(<param name="actual" />) returns true.
        /// </returns>
        public static bool NullComparer<T>(this T expected, T actual)
        {
            return expected.NullComparer(actual, (x, y) => x.Equals(y));
        }

        /// <summary>
        /// Compares two <see cref="Object"/>s of type T.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="Object"/> which will be compared.
        /// </typeparam>
        /// <param name="expected">
        /// The expected <see cref="Object"/>.
        /// </param>
        /// <param name="actual">
        /// The actual <see cref="Object"/>.
        /// </param>
        /// <param name="comparer">
        /// The means by which to compare the supplied <see cref="Object"/>s.
        /// </param>
        /// <returns>
        /// True if the <see cref="Object"/>s are equivalent. Equivalent here means either both null, or satisfying the comparer.
        /// </returns>
        public static bool NullComparer<T>(this T expected, T actual, Func<T, T, bool> comparer)
        {
            return expected == null ? actual == null : comparer.Invoke(expected, actual);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the supplied <see cref="Object"/> is null.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="Object"/> to compare against null.
        /// </typeparam>
        /// <param name="obj">
        /// The <see cref="Object"/> to compare against null.
        /// </param>
        /// <param name="paramName">
        /// The name of the parameter passed to the ArgumentNullException constructor upon throwing the exception.
        /// </param>
        /// <returns>
        /// <param name="obj" /> when <param name="obj" /> is not null.
        /// </returns>
        public static T ThrowIfNull<T>(this T obj, string paramName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName);
            }

            return obj;
        }
    }
}
