using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drexel.LangLeopard.Contracts.Processors;

namespace Drexel.LangLeopard.FrontEnd.Debug.Textual
{
    public class DebugTextualProcessorOutputState : IProcessorOutputState
    {
        public bool Continue { get; set; }

        public int TotalCorrect { get; set; }

        public int TotalProcessed { get; set; }
    }
}
