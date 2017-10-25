using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JapanesePractice.Contract;
using JapanesePractice.Contract.Contexts;
using JapanesePractice.Contract.Loaders;

namespace JapanesePractice.Textual
{
    /// <summary>
    /// A concrete <see cref="IContext"/> which can produce <see cref="TextualContext"/> or <see cref="TextualInterpretation"/>s from a data source.
    /// </summary>
    [Export(typeof(ILoader))]
    public class TextualLoader : ILoader
    {
        private static readonly IReadOnlyCollection<string> SupportedTypes = new string[] { "Textual" };

        /// <summary>
        /// Represents the set of types <see cref="TextualLoader"/> implementation supports.
        /// </summary>
        public string[] TypesSupported => TextualLoader.SupportedTypes.ToArray();

        /// <summary>
        /// Creates a <see cref="Category"/> from the supplied JSON-formatted <see cref="string"/> <paramref name="categoryJson"/>.
        /// </summary>
        /// <param name="categoryJson">
        /// The JSON-formatted data from which to create the <see cref="Category"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Category"/> created from the supplied <see cref="string"/> <paramref name="categoryJson"/>.
        /// </returns>
        public Category CreateCategoryFromJson(string categoryJson)
        {
            JObject jCategory = JObject.Parse(categoryJson);
            return new Category(
                jCategory.Value<string>("Name"),
                jCategory.Value<JArray>("Symbols").Select(x => this.CreateSymbolFromJson(x.ToString())));
        }

        /// <summary>
        /// Creates a <see cref="Symbol"/> from the supplied JSON-formatted <see cref="string"/> <paramref name="symbolJson"/>.
        /// </summary>
        /// <param name="symbolJson">
        /// The JSON-formatted data from which to create the <see cref="Symbol"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Symbol"/> created from the supplied <see cref="string"/> <paramref name="symbolJson"/>.
        /// </returns>
        public Symbol CreateSymbolFromJson(string symbolJson)
        {
            JObject jSymbol = JObject.Parse(symbolJson);
            return new Symbol(
                jSymbol.Value<string>("Name"),
                new TextualInterpretation(jSymbol.Value<JArray>("Interpretations").Select(x => x.ToString())));
        }

        /// <summary>
        /// Creates a <see cref="TextualContext"/> from the file specified by <paramref name="path"/>.
        /// </summary>
        /// <param name="path">
        /// The path the <see cref="TextualLoader"/> should create the <see cref="TextualContext"/> from.
        /// </param>
        /// <returns>
        /// An <see cref="TextualContext"/> created by the <see cref="TextualLoader"/>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Usage",
            "CA2202:Do not dispose objects multiple times",
            Justification = "Invalid warning. We set the JsonTextReader's CloseInput property to False.")]
        public IContext LoadContextFromPath(string path)
        {
            JObject fileContents = null;
            using (StreamReader fileReader = new StreamReader(path))
            {
                using (JsonTextReader jsonReader = new JsonTextReader(fileReader))
                {
                    jsonReader.CloseInput = false;
                    fileContents = (JObject)JToken.ReadFrom(jsonReader);
                }
            }

            return new TextualContext(fileContents
                .Value<JArray>("Categories")
                .Select(x => this.CreateCategoryFromJson(x.ToString())));
        }
    }
}
