using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JapanesePractice.Contract;
using JapanesePractice.Contract.Contexts;
using JapanesePractice.Contract.Selectors;
using JapanesePractice.Contract.Loaders;

namespace JapanesePractice.Core
{
    /// <summary>
    /// Contains the running application's context.
    /// </summary>
    public class ApplicationContext
    {
        private SessionBuilder builder;
        private List<Session> sessions;

        /// <summary>
        /// Instantiates a new <see cref="ApplicationContext"/> using the supplied <paramref name="pluginLocations"/> as the source from which to load <see cref="ILoader"/>s.
        /// </summary>
        public ApplicationContext(IEnumerable<DirectoryInfo> pluginLocations, SessionBuilder builder)
        {
            this.Loaders = new LoaderCollection(pluginLocations.Select(x => x.FullName));
            this.builder = builder;
            this.sessions = new List<Session>();
        }

        /// <summary>
        /// The collection of <see cref="ILoader"/>s belonging to the <see cref="ApplicationContext"/>.
        /// </summary>
        public LoaderCollection Loaders { get; private set; }

        /// <summary>
        /// The collection of <see cref="Session"/>s the <see cref="ApplicationContext"/> contains.
        /// </summary>
        public IReadOnlyList<Session> Sessions => this.sessions;

        /// <summary>
        /// Creates a new <see cref="Session"/> using the <see cref="ApplicationContext"/>'s <see cref="SessionBuilder"/>.
        /// </summary>
        /// <param name="context">
        /// The <see cref="IContext"/> the <see cref="Session"/> should use.
        /// </param>
        /// <returns>
        /// A <see cref="Session"/> produced by the <see cref="SessionBuilder"/>.
        /// </returns>
        public Session CreateSession(IContext context)
        {
            return this.builder.BuildSession(context);
        }
    }
}
