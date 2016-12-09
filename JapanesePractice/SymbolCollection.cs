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
    /// A context within which supplied values are compared against known <see cref="Symbol{TValue}"/>s.
    /// </summary>
    public class SymbolCollection<TKey, TValue> : ICollection<Symbol<TValue>>
    {
        /// <summary>
        /// Instantiates a <see cref="SymbolCollection{TKey, TValue}"/> using the supplied parameters.
        /// </summary>
        /// <param name="key">
        /// The key by which the <see cref="SymbolCollection{TKey, TValue}"/> is identified.
        /// </param>
        /// <param name="symbols">
        /// The <see cref="Symbol{TValue}"/>s the collection wraps.
        /// </param>
        /// <param name="comparer">
        /// The means by which to compare an instance of an expected and actual pair. Expected to return true when two <see cref="TValue"/>s are equivalent.
        /// </param>
        /// <param name="isReadOnly">
        /// Controls whether the <see cref="SymbolCollection{TKey, TValue}"/> is read-only.
        /// </param>
        public SymbolCollection(TKey key, IEnumerable<Symbol<TValue>> symbols, Func<TValue, TValue, bool> comparer, bool isReadOnly = false)
        {
            this.Symbols = symbols.ThrowIfNull(nameof(symbols)).ToList();
            this.LocalComparer = comparer.ThrowIfNull(nameof(comparer));
            this.IsReadOnly = isReadOnly;
            this.Key = key;
        }

        /// <summary>
        /// The <see cref="Symbol{T}"/>s which the runner is operating upon.
        /// </summary>
        public List<Symbol<TValue>> Symbols { get; private set; }

        /// <summary>
        /// Returns the number of elements in the <see cref="SymbolCollection{TKey, TValue}"/>.
        /// </summary>
        public int Count => this.Symbols.Count();

        /// <summary>
        /// Gets a value indicating whether the <see cref="SymbolCollection{TKey, TValue}"/> is read-only.
        /// </summary>
        public bool IsReadOnly { get; private set; }

        /// <summary>
        /// The key by which the <see cref="SymbolCollection{TKey, TValue}"/> is identified.
        /// </summary>
        public TKey Key { get; private set; }
        
        /// <summary>
        /// The comparator function to apply when checking for equivalence between actual and expected values.
        /// </summary>
        protected Func<TValue, TValue, bool> LocalComparer { get; set; }

        /// <summary>
        /// Adds an item to the <see cref="SymbolCollection{TKey, TValue}"/>.
        /// </summary>
        /// <param name="item"></param>
        public void Add(Symbol<TValue> item)
        {
            if (!this.IsReadOnly)
            {
                this.Symbols.Add(item);
            }
        }

        /// <summary>
        /// Removes all items from the <see cref="SymbolCollection{TKey, TValue}"/>.
        /// </summary>
        public void Clear()
        {
            if (!this.IsReadOnly)
            {
                this.Symbols.Clear();
            }
        }

        /// <summary>
        /// Determines whether the <see cref="SymbolCollection{TKey, TValue}"/> contains a specific value.
        /// </summary>
        /// <param name="item">
        /// The object to locate in the <see cref="SymbolCollection{TKey, TValue}"/>.
        /// </param>
        /// <returns>
        /// True if item is found in the <see cref="SymbolCollection{TKey, TValue}"/>; otherwise, false.
        /// </returns>
        public bool Contains(Symbol<TValue> item)
        {
            return this.Symbols.Any(expected => item.Expected.Any(actual => actual.NullComparer<TValue>(expected.Actual)));
        }

        /// <summary>
        /// Copies the elements of the <see cref="SymbolCollection{TKey, TValue}"/> to an <see cref="Array"/>, starting at a particular <see cref="Array"/> index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="SymbolCollection{TKey, TValue}"/>. The <see cref="Array"/> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">
        /// The zero-based index in <param name="array" /> at which copying begins.
        /// </param>
        public void CopyTo(Symbol<TValue>[] array, int arrayIndex)
        {
            this.Symbols.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Symbol<TValue>> GetEnumerator()
        {
            return this.Symbols.GetEnumerator();
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="SymbolCollection{TKey, TValue}"/>.
        /// </summary>
        /// <param name="item">
        /// The object to remove from the <see cref="SymbolCollection{TKey, TValue}"/>.
        /// </param>
        /// <returns>
        /// True if item was successfully removed from the <see cref="SymbolCollection{TKey, TValue}"/>; otherwise, false. This method also returns false if item is not found in the original <see cref="SymbolCollection{TKey, TValue}"/>.
        /// </returns>
        public bool Remove(Symbol<TValue> item)
        {
            return !this.IsReadOnly && this.Symbols.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
