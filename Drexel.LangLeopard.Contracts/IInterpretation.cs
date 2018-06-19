namespace Drexel.LangLeopard.Contracts
{
    public interface IInterpretation<out T>
    {
        Localized Key { get; }

        T Value { get; }
    }
}
