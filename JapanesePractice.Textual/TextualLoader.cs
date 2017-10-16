using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JapanesePractice.Contexts;
using JapanesePractice.Loaders;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using JapanesePractice.Interpretations;

namespace JapanesePractice.Textual
{
    [Export(typeof(ILoader))]
    public class TextualLoader : ILoader
    {
        private static readonly IReadOnlyCollection<string> SupportedTypes = new string[] { "Textual" };

        public string[] TypesSupported => TextualLoader.SupportedTypes.ToArray();

        public Category CreateCategoryFromJson(string categoryJson)
        {
            JObject jCategory = JObject.Parse(categoryJson);
            return new Category(
                jCategory.Value<string>("Name"),
                jCategory.Value<JArray>("Symbols").Select(x => this.CreateSymbolFromJson(x.ToString())));
        }

        public Symbol CreateSymbolFromJson(string symbolJson)
        {
            JObject jSymbol = JObject.Parse(symbolJson);
            return new Symbol(
                jSymbol.Value<string>("Name"),
                new TextualInterpretation(jSymbol.Value<JArray>("Interpretations").Select(x => x.ToString())));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Usage",
            "CA2202:Do not dispose objects multiple times",
            Justification = "Erroneous - we specify that the JsonTextReader should not close the underlying stream upon being disposed.")]
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
