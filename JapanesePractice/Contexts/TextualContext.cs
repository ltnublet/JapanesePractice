using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JapanesePractice.Interpretations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JapanesePractice.Contexts
{
    /// <summary>
    /// Represents a set of <see cref="Category"/>s which contain <see cref="Textual"/> <see cref="IInterpretation"/>s.
    /// </summary>
    public class TextualContext : IContext
    {
        /// <summary>
        /// Instantiates a new <see cref="TextualContext"/> with the initial set of <see cref="Category"/>s <paramref name="categories"/>.
        /// </summary>
        /// <param name="categories">
        /// The initial set of <see cref="Category"/>s this <see cref="TextualContext"/> contains.
        /// </param>
        public TextualContext(IEnumerable<Category> categories)
        {
            this.Categories = categories.ToList();
        }

        /// <summary>
        /// The <see cref="Category"/>s this <see cref="IContext"/> contains.
        /// </summary>
        public ICollection<Category> Categories { get; }

        /// <summary>
        /// Loads a <see cref="TextualContext"/> from the file specified by <paramref name="path"/>.
        /// </summary>
        /// <param name="path">
        /// The complete file path to read from.
        /// </param>
        /// <returns>
        /// A <see cref="TextualContext"/> whose contents have been loaded from the specified file.
        /// </returns>
        public static TextualContext FromFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            using (TextReader file = new StreamReader(path))
            {
                JObject fileContents = null;
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    reader.CloseInput = false;
                    fileContents = (JObject)JToken.ReadFrom(reader);
                }

                List<Category> categories = new List<Category>();

                foreach (JObject jCategory in fileContents.Value<JArray>("Categories"))
                {
                    categories.Add(
                        new Category(
                            jCategory.Value<string>("Name"),
                            jCategory.Value<JArray>("Symbols")
                                .Select(symbol =>
                                    new Symbol(
                                        symbol.Value<string>("Name"),
                                        new List<IInterpretation>
                                        {
                                            // TODO: Use the category Type to instantiate correct Intepretation type.
                                            new Textual(
                                                symbol.Value<JArray>("Interpretations")
                                                    .Select(interpretation => interpretation.Value<string>()))
                                        }))));
                }

                return new TextualContext(categories);
            }
        }

        /// <summary>
        /// Returns the merged <see cref="Symbol"/>s of the <see cref="TextualContext.Categories"/> where <see cref="Category.Name"/> was contained in <paramref name="categories"/>.
        /// </summary>
        /// <param name="categories">
        /// The names of the <see cref="Category"/>s contained within this <see cref="TextualContext.Categories"/> to merge the <see cref="Symbol"/>s of.
        /// </param>
        /// <returns>
        /// A collection of merged <see cref="Symbol"/>s.
        /// </returns>
        public IEnumerable<Symbol> Condense(params string[] categories)
        {
            if (categories == null)
            {
                throw new ArgumentNullException(nameof(categories));
            }

            return Category.Merge(this.Categories.Where(category => categories.Contains(category.Name)));

            ////Dictionary<string, Symbol> result = new Dictionary<string, Symbol>();

            ////foreach (Category category in this.Categories.Where(x => categories.Contains(x.Name)))
            ////{
            ////    foreach (Symbol symbol in category)
            ////    {
            ////        if (result.ContainsKey(symbol.Name))
            ////        {
            ////            result[symbol.Name].Interpretations.AddRange(symbol.Interpretations);
            ////        }
            ////    }
            ////}

            ////return result.Select(x => x.Value);
        }
    }
}
