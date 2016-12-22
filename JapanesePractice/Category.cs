using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JapanesePractice.Interpretations;

namespace JapanesePractice
{
    public class Category : IEnumerable<Symbol>
    {
        public Category(string name, IEnumerable<Symbol> symbols)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (symbols == null || !symbols.Any())
            {
                throw new ArgumentException($"{nameof(symbols)} must be non-null and contain at least one Symbol.", nameof(symbols));
            }

            this.Name = name;
            this.Symbols = symbols;
        }

        public string Name { get; protected set; }

        public IEnumerable<Symbol> Symbols { get; protected set; }

        public static IEnumerable<Symbol> Merge(IEnumerable<Category> categories)
        {
            Dictionary<string, List<IInterpretation>> values = 
                new Dictionary<string, List<IInterpretation>>();
            
            foreach (Category category in categories)
            {
                foreach (Symbol symbol in category.Symbols)
                {
                    if (values.ContainsKey(symbol.Name))
                    {
                        values[symbol.Name].AddRange(symbol.Interpretations);
                    }
                    else
                    {
                        values.Add(symbol.Name, symbol.Interpretations);
                    }
                }
            }

            return values.AsEnumerable().Select(x => new Symbol(x.Key, x.Value));
        }

        public IEnumerator<Symbol> GetEnumerator()
        {
            return Symbols.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
