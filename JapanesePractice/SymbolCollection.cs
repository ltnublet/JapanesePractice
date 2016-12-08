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
    /// A context within which supplied values are compared against known <see cref="Symbol{T}"/>s.
    /// </summary>
    public class SymbolCollection<T> : ICollection<Symbol<T>>
    {
        /// <summary>
        /// Instantiates a <see cref="SymbolCollection{T}"/> using the supplied parameters.
        /// </summary>
        /// <param name="symbols">
        /// The <see cref="Symbol{T}"/>s the collection wraps.
        /// </param>
        /// <param name="comparer">
        /// The means by which to compare an instance of an expected and actual pair. Expected to return true when two <see cref="T"/>s are equivalent.
        /// </param>
        /// <param name="isReadOnly"></param>
        public SymbolCollection(IEnumerable<Symbol<T>> symbols, Func<T, T, bool> comparer, bool isReadOnly = false)
        {
            this.Symbols = symbols.ThrowIfNull(nameof(symbols)).ToList();
            this.LocalComparer = comparer.ThrowIfNull(nameof(comparer));
            this.IsReadOnly = isReadOnly;

            this.Categories = this.Symbols
        }

        /// <summary>
        /// The <see cref="Symbol{T}"/>s which the runner is operating upon.
        /// </summary>
        public List<Symbol<T>> Symbols { get; private set; }

        /// <summary>
        /// An <see cref="IEnumerable{Category}"/> containing all categories in the <see cref="SymbolCollection{T}"/>.
        /// </summary>
        public IEnumerable<Category> Categories { get; private set; }

        /// <summary>
        /// Returns the number of elements in the <see cref="SymbolCollection{T}"/>.
        /// </summary>
        public int Count => this.Symbols.Count();

        /// <summary>
        /// Gets a value indicating whether the <see cref="SymbolCollection{T}"/> is read-only.
        /// </summary>
        public bool IsReadOnly { get; private set; }
        
        /// <summary>
        /// The comparator function to apply when checking for equivalence between actual and expected values.
        /// </summary>
        protected Func<T, T, bool> LocalComparer { get; set; }

        /// <summary>
        /// Instantiates a <see cref="SymbolCollection{T}"/> from the supplied path using the supplied parser.
        /// </summary>
        /// <param name="path">
        /// A path to a JSON-formatted file containing the expected <see cref="Symbol{T}"/>s.
        /// </param>
        /// <returns>
        /// A <see cref="SymbolCollection{T}"/> representing the supplied file.
        /// </returns>
        public static SymbolCollection<T> FromFile(string path)
        {
            if (File.Exists(path.ThrowIfNull(nameof(path))))
            {
                JObject contents = null;

                using (StreamReader file = File.OpenText(path))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    contents = (JObject)JToken.ReadFrom(reader);
                }

                foreach (KeyValuePair<string, JToken> symbol in contents)
                {
                    symbol.Value
                }
            }
            else
            {
                throw new FileNotFoundException(Resources.FileNotFound);
            }

            return null;
        }

        /// <summary>
        /// Adds an item to the <see cref="SymbolCollection{T}"/>.
        /// </summary>
        /// <param name="item"></param>
        public void Add(Symbol<T> item)
        {
            if (!this.IsReadOnly)
            {
                this.Symbols.Add(item);
            }
        }

        /// <summary>
        /// Removes all items from the <see cref="SymbolCollection{T}"/>.
        /// </summary>
        public void Clear()
        {
            if (!this.IsReadOnly)
            {
                this.Symbols.Clear();
            }
        }

        /// <summary>
        /// Determines whether the <see cref="SymbolCollection{T}"/> contains a specific value.
        /// </summary>
        /// <param name="item">
        /// The object to locate in the <see cref="SymbolCollection{T}"/>.
        /// </param>
        /// <returns>
        /// True if item is found in the <see cref="SymbolCollection{T}"/>; otherwise, false.
        /// </returns>
        public bool Contains(Symbol<T> item)
        {
            return this.Symbols.Any(expected => item.Expected.Any(actual => actual.NullComparer<T>(expected.Actual)));
        }

        /// <summary>
        /// Copies the elements of the <see cref="SymbolCollection{T}"/> to an <see cref="Array"/>, starting at a particular <see cref="Array"/> index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="SymbolCollection{T}"/>. The <see cref="Array"/> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">
        /// The zero-based index in <param name="array" /> at which copying begins.
        /// </param>
        public void CopyTo(Symbol<T>[] array, int arrayIndex)
        {
            this.Symbols.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Symbol<T>> GetEnumerator()
        {
            return this.Symbols.GetEnumerator();
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="SymbolCollection{T}"/>.
        /// </summary>
        /// <param name="item">
        /// The object to remove from the <see cref="SymbolCollection{T}"/>.
        /// </param>
        /// <returns>
        /// True if item was successfully removed from the <see cref="SymbolCollection{T}"/>; otherwise, false. This method also returns false if item is not found in the original <see cref="SymbolCollection{T}"/>.
        /// </returns>
        public bool Remove(Symbol<T> item)
        {
            return !this.IsReadOnly && this.Symbols.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
