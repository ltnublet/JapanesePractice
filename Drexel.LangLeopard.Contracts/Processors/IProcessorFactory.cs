using Drexel.LangLeopard.Contracts.Sources;

namespace Drexel.LangLeopard.Contracts.Processors
{
    public interface IProcessorFactory<in T>
    {
        IProcessor<T> GetInstance(ISymbolSource symbolSource, IInterpretationSource<T> interpretationSource);
    }
}
