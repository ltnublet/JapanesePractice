using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapanesePractice.Interpretations
{
    public class Textual : Interpretation<string>
    {
        public Textual(IEnumerable<string> permittedRepresentations) 
            : base(permittedRepresentations)
        {
        }


        public override bool Compare(IInterpretation other)
        {
            Textual value = other as Textual;
            return value != null && this.PermittedRepresentations.SequenceEqual(value.PermittedRepresentations);
        }
    }
}
