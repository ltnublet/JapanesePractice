using JapanesePractice.Contract.Contexts;

namespace JapanesePractice.Contract.Loaders
{
    /// <summary>
    /// Represents a class which can produce an <see cref="IContext"/> from a data source.
    /// </summary>
    public interface ILoader
    {
        /// <summary>
        /// Represents the set of types the <see cref="ILoader"/> implementation supports.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Performance",
            "CA1819:PropertiesShouldNotReturnArrays",
            Justification = "It is a property of the ILoader.")]
        string[] TypesSupported { get; }

        /// <summary>
        /// Creates an <see cref="ICategory"/> from the supplied JSON-formatted <see cref="string"/> <paramref name="categoryJson"/>.
        /// </summary>
        /// <param name="categoryJson">
        /// The JSON-formatted data from which to create the <see cref="ICategory"/>.
        /// </param>
        /// <returns>
        /// An <see cref="ICategory"/> created from the supplied <see cref="string"/> <paramref name="categoryJson"/>.
        /// </returns>
        ICategory CreateCategoryFromJson(string categoryJson);

        /// <summary>
        /// Creates an <see cref="ISymbol"/> from the supplied JSON-formatted <see cref="string"/> <paramref name="symbolJson"/>.
        /// </summary>
        /// <param name="symbolJson">
        /// The JSON-formatted data from which to create the <see cref="ISymbol"/>.
        /// </param>
        /// <returns>
        /// An <see cref="ISymbol"/> created from the supplied <see cref="string"/> <paramref name="symbolJson"/>.
        /// </returns>
        ISymbol CreateSymbolFromJson(string symbolJson);

        /// <summary>
        /// Creates an <see cref="IContext"/> from the file specified by <paramref name="path"/>, leaving interpretation up to the <see cref="ILoader"/>.
        /// </summary>
        /// <param name="path">
        /// The path the <see cref="ILoader"/> should create the <see cref="IContext"/> from.
        /// </param>
        /// <returns>
        /// An <see cref="IContext"/> created by the <see cref="ILoader"/>.
        /// </returns>
        IContext LoadContextFromPath(string path);
    }
}
