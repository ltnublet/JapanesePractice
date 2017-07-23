using System.Collections.Generic;

namespace JapanesePractice.Contexts
{
    /// <summary>
    /// Represents a set of <see cref="Category"/>s.
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// The <see cref="Category"/>s this <see cref="IContext"/> contains.
        /// </summary>
        ICollection<Category> Categories { get; }
    }
}
