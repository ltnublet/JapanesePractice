using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JapanesePractice.Interpretations;

namespace JapanesePractice.Contexts
{
    public class TextualContext : IContext
    {
        public TextualContext(IEnumerable<Category> categories)
        {
            this.Categories = categories.ToList();
        }

        public List<Category> Categories { get; protected set; }
        
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

        public IEnumerable<Symbol> Condense(params string[] categories)
        {
            Dictionary<string, Symbol> result = new Dictionary<string, Symbol>();

            foreach (Category category in this.Categories.Where(x => categories.Contains(x.Name)))
            {
                foreach (Symbol symbol in category)
                {
                    if (result.ContainsKey(symbol.Name))
                    {
                        result[symbol.Name].Interpretations.AddRange(symbol.Interpretations);
                    }
                }
            }

            return result.Select(x => x.Value);
        }
    }
}
