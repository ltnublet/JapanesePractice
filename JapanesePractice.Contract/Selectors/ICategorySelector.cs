using JapanesePractice.Contract.Contexts;

namespace JapanesePractice.Contract.Selectors
{
    /// <summary>
    /// Represents a class which is applies some set of constraints to select <see cref="ISymbol"/>s from an <see cref="ICategory"/>.
    /// </summary>
    public interface ICategorySelector
    {
        /// <summary>
        /// Selects an <see cref="ICategory"/> from the supplied <see cref="IContext"/> <paramref name="context"/>.
        /// </summary>
        /// <param name="context">
        /// The <see cref="IContext"/> from which to select the <see cref="ICategory"/>.
        /// </param>
        /// <returns>
        /// An <see cref="ICategory"/> from the <see cref="IContext"/> <paramref name="context"/>.
        /// </returns>
        ICategory SelectFrom(IContext context);
    }
}
