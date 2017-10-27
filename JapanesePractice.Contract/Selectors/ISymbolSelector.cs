namespace JapanesePractice.Contract.Selectors
{
    /// <summary>
    /// Represents a class which is applies some set of constraints to select <see cref="ISymbol"/>s from a <see cref="ICategory"/>.
    /// </summary>
    public interface ISymbolSelector
    {
        /// <summary>
        /// Selects a <see cref="ISymbol"/> from the supplied <see cref="ICategory"/> <paramref name="category"/>.
        /// </summary>
        /// <param name="category">
        /// The <see cref="ICategory"/> from which to select the <see cref="ISymbol"/>.
        /// </param>
        /// <returns>
        /// A <see cref="ISymbol"/> from the <see cref="ICategory"/> <paramref name="category"/>.
        /// </returns>
        ISymbol SelectFrom(ICategory category);
    }
}
