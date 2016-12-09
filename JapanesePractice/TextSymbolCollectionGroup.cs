using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapanesePractice
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Properties;

    /// <summary>
    /// A context containing <see cref="SymbolCollection{Category, String}"/>s of <see cref="Symbol{String}"/>s. Exposes functionality for such a usecase.
    /// </summary>
    public class TextSymbolCollectionGroup : SymbolCollectionGroup<Category, string>
    {
        /// <summary>
        /// Instantiates a <see cref="TextSymbolCollectionGroup"/> using the supplied contents.
        /// </summary>
        /// <param name="contents"></param>
        public TextSymbolCollectionGroup(IEnumerable<SymbolCollection<Category, string>> contents) : base(contents)
        {
        }

        /// <summary>
        /// Instantiates a <see cref="TextSymbolCollectionGroup"/> from the supplied <paramref name="path"/>.
        /// </summary>
        /// <param name="path">
        /// A path to a JSON-formatted file containing the expected <see cref="Symbol{TValue}"/>s.
        /// </param>
        /// <returns>
        /// A <see cref="TextSymbolCollectionGroup"/> with contents generated from the supplied <paramref name="path"/>.
        /// </returns>
        public static TextSymbolCollectionGroup FromFile(string path)
        {
            return (TextSymbolCollectionGroup)SymbolCollectionGroup<Category, string>.FromFile(
                path, 
                (Func<StreamReader, TextSymbolCollectionGroup>)(
                    file =>
                    {
                        JObject fileContents = null;
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            fileContents = (JObject)JToken.ReadFrom(reader);
                        }
                        
                        List<SymbolCollection<Category, string>> content =
                            new List<SymbolCollection<Category, string>>();

                        foreach (KeyValuePair<string, JToken> categoryToken in fileContents)
                        {
                            List<Symbol<string>> symbols = new List<Symbol<string>>();

                            foreach (KeyValuePair<string, JToken> symbolToken in (JObject)categoryToken.Value)
                            {
                                symbols.Add(
                                    new Symbol<string>(
                                        symbolToken.Key,
                                        symbolToken.Value.Value<JArray>("AcceptedInterpretations")
                                            .Select(x => x.ToString())));
                            }

                            content.Add(new SymbolCollection<Category, string>(
                                new Category(categoryToken.Key),
                                symbols,
                                (expected, actual) => expected?.Equals(actual) ?? actual == null, 
                                isReadOnly: true));
                        }

                        return new TextSymbolCollectionGroup(content);
                    }));
        }

        /// <summary>
        /// Returns a <see cref="String"/> that represents the current <see cref="TextSymbolCollectionGroup"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="String"/> representing the current <see cref="TextSymbolCollectionGroup"/>.
        /// </returns>
        public override string ToString()
        {
            StringBuilder content = new StringBuilder();

            foreach (SymbolCollection<Category, string> category in this.Contents)
            {
                content.AppendLine($"{category.Key.Name}: ");

                foreach (Symbol<string> symbol in category)
                {
                    content.AppendLine($"{symbol.Actual} - {(symbol.Expected.Any() ? symbol.Expected.Aggregate((x, y) => $"{x}, {y}") : Resources.None)}");
                }
            }

            return content.ToString();
        }
    }
}
