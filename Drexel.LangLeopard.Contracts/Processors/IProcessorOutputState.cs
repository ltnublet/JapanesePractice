using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drexel.LangLeopard.Contracts.Processors
{
    public interface IProcessorOutputState
    {
        bool Continue { get; }

        int TotalCorrect { get; }

        int TotalProcessed { get; }
    }
}
