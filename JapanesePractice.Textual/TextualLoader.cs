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
using SysJson = System.Json;

namespace JapanesePractice.Textual
{
    [Export(typeof(ILoader))]
    public class TextualLoader : ILoader
    {
        private const string SecretTypeKey = "Textual";

        public bool AcceptsType(string type)
        {
            return string.Equals(type, TextualLoader.SecretTypeKey, StringComparison.OrdinalIgnoreCase);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            MessageId = "0",
            Justification = "Callers are expected to supply only valid data.")]
        public Category CreateCategoryFromJson(SysJson.JsonObject categoryAsJson)
        {
            return new Category(
                categoryAsJson["Name"],
                (categoryAsJson["Symbols"] as SysJson.JsonArray)
                    .Select(x => this.CreateSymbolFromJson(x as SysJson.JsonObject)));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "TODO: Add method to ILoader interface.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            MessageId = "0",
            Justification = "Callers are expected to supply only valid data.")]
        public Symbol CreateSymbolFromJson(SysJson.JsonObject symbolAsJson)
        {
            return new Symbol(
                symbolAsJson["Name"],
                new TextualInterpretation(
                    ((SysJson.JsonArray)symbolAsJson["Interpretations"])
                        .Select(x => (string)(((SysJson.JsonValue)x)))));
        }

        public IContext FromFile(TextReader source)
        {
            JObject fileContents = null;
            using (JsonTextReader reader = new JsonTextReader(source))
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
                                        new TextualInterpretation(
                                            symbol.Value<JArray>("Interpretations")
                                                .Select(interpretation => interpretation.Value<string>()))
                                    }))));
            }

            return new TextualContext(categories);
        }
    }
}
