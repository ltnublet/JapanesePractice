﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JapanesePractice.Contract.Interpretations;

namespace JapanesePractice.Contract.ReferenceImplementation
{
    /// <summary>
    /// Represents a collection of <see cref="ISymbol"/>s.
    /// </summary>
    public class Category : ICategory
    {
        /// <summary>
        /// Instantiates a new <see cref="Category"/> instance with the specified <paramref name="name"/> and initial set of <paramref name="symbols"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the category.
        /// </param>
        /// <param name="symbols">
        /// The initial set of <see cref="ISymbol"/>s contained within this <see cref="Category"/>.
        /// </param>
        public Category(string name, IEnumerable<ISymbol> symbols)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (symbols == null || !symbols.Any())
            {
                throw new ArgumentException(
                    FormattableString.Invariant($"{nameof(symbols)} must be non-null and contain at least one Symbol."),
                    nameof(symbols));
            }

            this.Name = name;
            this.Symbols = symbols.ToList();
        }

        /// <summary>
        /// The name of the <see cref="Category"/>.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The symbols contained by the <see cref="Category"/>.
        /// </summary>
        public IReadOnlyList<ISymbol> Symbols { get; protected set; }

        /// <summary>
        /// Merges the supplied <paramref name="categories"/> such that the resulting set is distinct by <see cref="ISymbol.Name"/>, and the <see cref="ISymbol"/> instance's <see cref="ISymbol.Interpretations"/> contains all <see cref="IInterpretation"/>s from the original set of <see cref="ICategory"/>s.
        /// </summary>
        /// <param name="categories">
        /// A collection of <see cref="ICategory"/>s to merge.
        /// </param>
        /// <returns>
        /// A collection of merged <see cref="ISymbol"/>s.
        /// </returns>
        public static IEnumerable<ISymbol> Merge(params ICategory[] categories)
        {
            return Category.Merge(categories.AsEnumerable());
        }

        /// <summary>
        /// Merges the supplied <paramref name="categories"/> such that the resulting set is distinct by <see cref="ISymbol.Name"/>, and the <see cref="ISymbol"/> instance's <see cref="ISymbol.Interpretations"/> contains all <see cref="IInterpretation"/>s from the original set of <see cref="ICategory"/>s.
        /// </summary>
        /// <param name="categories">
        /// A collection of <see cref="ICategory"/>s to merge.
        /// </param>
        /// <returns>
        /// A collection of merged <see cref="ISymbol"/>s.
        /// </returns>
        public static IEnumerable<ISymbol> Merge(IEnumerable<ICategory> categories)
        {
            return categories
                .SelectMany(category => category)
                .GroupBy(symbol => symbol.Name)
                .Select(
                    group =>
                    new Symbol(
                        group.Key,
                        group.SelectMany(sym => sym.Interpretations)));
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that iterates through the collection.
        /// </returns>
        public IEnumerator<ISymbol> GetEnumerator()
        {
            return this.Symbols.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that iterates through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current object.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "({0}): {1}",
                this.Name,
                string.Join(", ", this.Symbols.Select(x => x.Name)));
        }
    }
}
