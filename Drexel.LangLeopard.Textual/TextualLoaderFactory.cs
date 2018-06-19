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
    public class TextualFileLoaderFactory : ILoaderFactory<Localized>
    {
        private static readonly IConfigurationRequirement path;
        private static IConfigurationRequirement[] requirements;

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
        }

        public IReadOnlyList<IConfigurationRequirement> Requirements => TextualFileLoaderFactory.requirements;

        public ILoader<Localized> GetInstance(IReadOnlyDictionary<IConfigurationRequirement, object> mappings) =>
            new TextualFileLoader((FilePath)new Configuration(this, mappings, null)[TextualFileLoaderFactory.path]);
    }
}
