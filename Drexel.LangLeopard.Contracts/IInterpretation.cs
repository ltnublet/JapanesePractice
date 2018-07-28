using System;

namespace Drexel.LangLeopard.Contracts
{
    /// <summary>
    /// Represents a mapping between a <see cref="Localized"/> and a <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The <see cref="Type"/> to which the <see cref="Localized"/> is mapped.
    /// </typeparam>
    public interface IInterpretation<out T>
    {
        /// <summary>
        /// The key.
        /// </summary>
        Localized Key { get; }

        /// <summary>
        /// The value.
        /// </summary>
        T Value { get; }
    }
}
