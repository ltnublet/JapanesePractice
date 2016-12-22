using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapanesePractice.Interpretations
{
    public interface IInterpretation
    {
        bool Compare(IInterpretation other);
        bool CompareAny(IEnumerable<IInterpretation> list);
        bool CompareAll(IEnumerable<IInterpretation> list);
    }
}
