using System;
using System.Collections.Generic;
using Drexel.LangLeopard.Contracts;
using Drexel.LangLeopard.Contracts.Processors;
using Drexel.LangLeopard.Contracts.Sources;

namespace Drexel.LangLeopard.FrontEnd.Debug.Textual
{
    public class DebugTextualProcessorFactory : IProcessorFactory<Localized>
    {
        public IProcessor<Localized> GetInstance(
            ISymbolSource symbolSource,
            IInterpretationSource<Localized> interpretationSource)
        {
            return new DebugTextualProcessor(symbolSource, interpretationSource);
        }
    }
}
