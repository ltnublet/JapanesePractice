using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drexel.Configurables;
using Drexel.Configurables.Contracts;
using Drexel.Configurables.External;
using Drexel.LangLeopard.Contracts;
using Drexel.LangLeopard.Contracts.Loaders;

namespace Drexel.LangLeopard.Textual
{
    public class TextualFileLoaderFactory : ILoaderFactory<Localized>, ILoaderFactoryTypeProvider
    {
        private static readonly IConfigurationRequirement path;
        private static readonly IConfigurationRequirement[] requirements;
        private static readonly IReadOnlyList<Type> supportedTypes;

        static TextualFileLoaderFactory()
        {
            TextualFileLoaderFactory.path = ConfigurationRequirement.FilePath(
                "File Path",
                "Path to the file to load.");

            TextualFileLoaderFactory.requirements =
                new IConfigurationRequirement[]
                {
                    TextualFileLoaderFactory.path
                };

            TextualFileLoaderFactory.supportedTypes = new List<Type> { typeof(Localized) };
        }

        public IReadOnlyList<IConfigurationRequirement> Requirements => TextualFileLoaderFactory.requirements;

        public IEnumerable<Type> SupportedTypes => TextualFileLoaderFactory.supportedTypes;

        public ILoader<Localized> GetInstance(IReadOnlyDictionary<IConfigurationRequirement, object> mappings) =>
            new TextualFileLoader((FilePath)new Configuration(this, mappings, null)[TextualFileLoaderFactory.path]);
    }
}
