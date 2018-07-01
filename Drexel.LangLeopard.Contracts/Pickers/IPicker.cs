using Drexel.LangLeopard.Contracts.Sources;

namespace Drexel.LangLeopard.Contracts.Pickers
{
    public interface IPicker<T>
    {
        PickerResult<T> PickFrom(ISymbolSource symbolSource, IInterpretationSource<T> interpretationSource);
    }
}
