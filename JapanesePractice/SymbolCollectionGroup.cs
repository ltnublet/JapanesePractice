using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JapanesePractice.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JapanesePractice
{
    /// <summary>
    /// A context containing <see cref="SymbolCollection{TKey, TValue}"/>s of <see cref="Symbol{TValue}"/>s.
    /// </summary>
    public class SymbolCollectionGroup<TKey, TValue> : IEnumerable<SymbolCollection<TKey, TValue>>
    {
        /// <summary>
        /// Instantiates a <see cref="SymbolCollectionGroup{TKey, TValue}"/> using the supplied contents.
        /// </summary>
        /// <param name="contents">
        /// An <see cref="IEnumerable{T}"/> of <see cref="SymbolCollection{TKey, TValue}"/>s representing the content of the <see cref="SymbolCollectionGroup{TKey, TValue}"/>.
        /// </param>
        public SymbolCollectionGroup(IEnumerable<SymbolCollection<TKey, TValue>> contents)
        {
            this.Contents = contents;
        }

        /// <summary>
        /// The contents of the <see cref="SymbolCollectionGroup{TKey, TValue}"/>.
        /// </summary>
        public IEnumerable<SymbolCollection<TKey, TValue>> Contents { get; protected set; }

        /// <summary>
        /// Instantiates a <see cref="SymbolCollectionGroup{TKey, TValue}"/> from the supplied <paramref name="path"/> using the supplied <paramref name="parser"/>.
        /// </summary>
        /// <param name="path">
        /// A path to a file containing the expected <see cref="Symbol{TValue}"/>s in a format which the supplied <paramref name="parser"/> expects.
        /// </param>
        /// <param name="parser">
        /// Returns a <see cref="SymbolCollectionGroup{TKey, TValue}"/> given a <see cref="StreamReader"/> which reads the contents of the file at the supplied <paramref name="path"/>.
        /// </param>
        /// <returns>
        /// A <see cref="SymbolCollectionGroup{TKey, TValue}"/> with contents generated from the supplied <paramref name="path"/> using the supplied <paramref name="parser"/>.
        /// </returns>
        public static SymbolCollectionGroup<TKey, TValue> FromFile(
            string path, 
            Func<StreamReader, SymbolCollectionGroup<TKey, TValue>> parser = null)
        {
            if (File.Exists(path.ThrowIfNull(nameof(path))))
            {
                using (StreamReader file = File.OpenText(path))
                {
                    parser.ThrowIfNull(nameof(parser));

                    try
                    {
                        // ReSharper disable once PossibleNullReferenceException - we check for null before entering the try-catch.
                        return parser(file);
                    }
                    catch (Exception e)
                    {
                        throw new AggregateException(Resources.FailedToParseFile, e);
                    }
                }
            }
            else
            {
                throw new FileNotFoundException(Resources.FileNotFound);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<SymbolCollection<TKey, TValue>> GetEnumerator()
        {
            return this.Contents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
