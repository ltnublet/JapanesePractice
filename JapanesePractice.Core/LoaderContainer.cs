using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using JapanesePractice.Contract.Loaders;
using System.Globalization;

namespace JapanesePractice.Core
{
    /// <summary>
    /// Represents a collection of <see cref="ILoader"/>s retrieved from plugins.
    /// </summary>
    public class LoaderCollection : IEnumerable<KeyValuePair<string, IEnumerable<ILoader>>>
    {
        private Dictionary<string, List<LoaderTypePair>> Loaders;
        
        /// <summary>
        /// Instantiates a new <see cref="LoaderCollection"/>, searching the specified <paramref name="pluginLocations"/> for plugins to load <see cref="ILoader"/>s from.
        /// </summary>
        /// <param name="pluginLocations">
        /// The paths in which to search for plugins which export <see cref="ILoader"/>(s).
        /// </param>
        public LoaderCollection(IEnumerable<string> pluginLocations)
        {
            if (pluginLocations == null)
            {
                throw new ArgumentNullException(nameof(pluginLocations));
            }

            MefLoaderSource loaderContainer = new MefLoaderSource();

            using (AggregateCatalog catalog = new AggregateCatalog())
            {
                foreach (string location in pluginLocations)
                {
                    Console.WriteLine(location);
                    catalog.Catalogs.Add(new DirectoryCatalog(Path.GetDirectoryName(location)));
                }

                using (CompositionContainer container = new CompositionContainer(catalog))
                {
                    container.ComposeParts(loaderContainer);
                }
            }

            this.Loaders = new Dictionary<string, List<LoaderTypePair>>(StringComparer.OrdinalIgnoreCase);
            foreach (ILoader loader in loaderContainer.Loaders)
            {
                foreach (string supportedType in loader.TypesSupported)
                {
                    if (!this.Loaders.ContainsKey(supportedType))
                    {
                        this.Loaders.Add(supportedType, new List<LoaderTypePair>());
                    }

                    this.Loaders[supportedType].Add(new LoaderTypePair(loader));
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="ILoader"/> associated with the specified key.
        /// </summary>
        /// <param name="key">
        /// The key of the <see cref="ILoader"/> to get.
        /// </param>
        /// <returns>
        /// The specified <see cref="ILoader"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when there are multiple <see cref="ILoader"/>s that support the specified key.
        /// </exception>
        /// <exception cref="KeyNotFoundException">
        /// The <see cref="LoaderCollection"/> did not contain a <see cref="ILoader"/> associated with the specified key.
        /// </exception>
        public ILoader this[string key]
        {
            get
            {
                if (this.Loaders.ContainsKey(key))
                {
                    List<LoaderTypePair> loaders = this.Loaders[key];
                    if (loaders.Count > 1)
                    {
                        throw new InvalidOperationException(
                            string.Format(
                                CultureInfo.InvariantCulture,
                                "The key '{0}' had multiple supported ILoaders: {1}",
                                key,
                                string.Join(", ", loaders.Select(x => x.Type))));
                    }
                    else
                    {
                        return this.Loaders[key][0].Loader;
                    }
                }
                else
                {
                    throw new KeyNotFoundException(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "The key '{0}' did not exist in the collection.",
                            key));
                }
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that iterates through the collection.
        /// </returns>
        public IEnumerator<KeyValuePair<string, IEnumerable<ILoader>>> GetEnumerator()
        {
            return this.Loaders
                .Select(x => new KeyValuePair<string, IEnumerable<ILoader>>(x.Key, x.Value.Select(y => y.Loader)))
                .GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that iterates through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class MefLoaderSource
        {
            [ImportMany(typeof(ILoader))]
            private ILoader[] loaders = new ILoader[0];

            public IEnumerable<ILoader> Loaders => this.loaders;
        }

        private class LoaderTypePair
        {
            public LoaderTypePair(ILoader loader)
            {
                this.Loader = loader;
                this.Type = loader.GetType().ToString();
            }

            public ILoader Loader { get; private set; }
            public string Type { get; private set; }
        }
    }
}
