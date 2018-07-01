using Drexel.LangLeopard.Contracts.Sources;

namespace Drexel.LangLeopard.Contracts.Pickers
{
    public sealed class PickerResult<T>
    {
        public ISymbolSource SymbolSource { get; }

        public IInterpretationSource<T> InterpretationSource { get; }
    }
}
