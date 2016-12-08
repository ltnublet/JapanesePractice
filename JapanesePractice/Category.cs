using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapanesePractice
{
    /// <summary>
    /// Represents a category of <see cref="Symbol{T}"/>s.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Instantiates a <see cref="Category"/> using the supplied values.
        /// </summary>
        /// <param name="name">
        /// The name of the <see cref="Category"/>.
        /// </param>
        public Category(string name)
        {
            this.Name = name.ThrowIfNull(nameof(name));
        }

        /// <summary>
        /// The name of the <see cref="Category"/>.
        /// </summary>
        public string Name { get; private set; }
    }
}
