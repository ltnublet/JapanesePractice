using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drexel.Configurables.External;
using Drexel.LangLeopard.Contracts;
using Drexel.LangLeopard.Contracts.Loaders;
using Drexel.LangLeopard.Contracts.Sources;

namespace Drexel.LangLeopard.Textual
{
    public class TextualFileLoader : ILoader<Localized>
    {
        public TextualFileLoader(FilePath path)
        {
            // TODO: read from file
            this.SymbolSources = new List<TextualSymbolSource>();
            this.InterpretationSources = new List<TextualInterpretationSource>();
        }

        public IReadOnlyCollection<ISymbolSource> SymbolSources { get; }

        public IReadOnlyCollection<IInterpretationSource<Localized>> InterpretationSources { get; }
    }
}
