using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drexel.LangLeopard.Contracts;
using Drexel.LangLeopard.Contracts.Processors;
using Drexel.LangLeopard.Contracts.Sources;

namespace Drexel.LangLeopard.FrontEnd.Debug.Textual
{
    public class DebugTextualProcessor : IProcessor<Localized>
    {
        private readonly Random random;
        private readonly Dictionary<ISymbol, IReadOnlyList<IInterpretation<Localized>>> symbolInterpretationMappings;
        private readonly DebugTextualProcessorOutputState state;

        public DebugTextualProcessor(ISymbolSource symbols, IInterpretationSource<Localized> interpretations)
        {
            this.random = new Random();
            this.state = new DebugTextualProcessorOutputState();
            this.symbolInterpretationMappings = symbols
                .Symbols
                .ToDictionary(
                    x => x,
                    x => interpretations.GetInterpretations(x));
        }

        public IProcessorOutputState Process(IProcessorInputState state)
        {
            if (!state.Continue)
            {
                this.Cleanup();
                this.state.Continue = false;
                return this.state;
            }

            int toChoose = this.random.Next(0, this.symbolInterpretationMappings.Count);
            ISymbol symbol = this.symbolInterpretationMappings.Keys.Skip(toChoose).First();

            Console.Write($"What is '{symbol.Key.Value}'?: ");
            string input = Console.ReadLine();

            this.state.TotalProcessed++;

            if (this.symbolInterpretationMappings[symbol].Any(x => x.Value == input))
            {
                Console.WriteLine("Ye");
                this.state.TotalCorrect++;
            }
            else
            {
                Console.WriteLine("Ne");
            }

            return this.state;
        }

        private void Cleanup()
        {
            // Nothing
        }
    }
}
