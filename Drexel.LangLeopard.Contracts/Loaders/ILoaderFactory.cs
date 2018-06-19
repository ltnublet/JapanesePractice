using System.Collections.Generic;
using Drexel.Configurables.Contracts;

namespace Drexel.LangLeopard.Contracts.Loaders
{
    public interface ILoaderFactory<out T> : IRequirementSource
    {
        ILoader<T> GetInstance(IReadOnlyDictionary<IConfigurationRequirement, object> mappings);
    }
}
