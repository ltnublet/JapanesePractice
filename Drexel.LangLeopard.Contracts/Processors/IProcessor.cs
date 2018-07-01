namespace Drexel.LangLeopard.Contracts.Processors
{
    public interface IProcessor<in T>
    {
        IProcessorOutputState Process(IProcessorInputState state);
    }
}
