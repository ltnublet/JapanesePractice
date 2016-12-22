using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JapanesePractice.Interpretations;

namespace JapanesePractice
{
    public class Symbol
    {
        public Symbol(string name, List<IInterpretation> interpretations)
        {
            this.Name = name;
            this.Interpretations = interpretations;
        }

        public string Name { get; protected set; }
        
        public List<IInterpretation> Interpretations { get; set; }
    }
}
