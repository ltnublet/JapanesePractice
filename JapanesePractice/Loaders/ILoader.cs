using System.IO;
using JapanesePractice.Contexts;

namespace JapanesePractice.Loaders
{
    /// <summary>
    /// Represents a class which can produce an <see cref="IContext"/> from a data source.
    /// </summary>
    public interface ILoader
    {
        /// <summary>
        /// Indicates whether this <see cref="ILoader"/> accepts the specified data source type <paramref name="type"/>.
        /// </summary>
        /// <param name="type">
        /// The data source's type.
        /// </param>
        /// <returns>
        /// True if the <see cref="ILoader"/> supports the supplied <paramref name="type"/>; false otherwise.
        /// </returns>
        bool AcceptsType(string type);

        /// <summary>
        /// Creates an <see cref="IContext"/> which represents the data source retrieved from the specified <paramref name="source"/>. Implementors of this method are expected to leave the <see cref="TextReader"/> <paramref name="source"/> open when finished.
        /// </summary>
        /// <param name="source">
        /// The data source to read from.
        /// </param>
        /// <returns>
        /// An <see cref="IContext"/> which represents the specified data source <paramref name="source"/>.
        /// </returns>
        IContext FromFile(TextReader source);
    }
}
