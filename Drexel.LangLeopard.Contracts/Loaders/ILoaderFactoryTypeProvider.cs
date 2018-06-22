using System;
using System.Collections.Generic;

namespace Drexel.LangLeopard.Contracts.Loaders
{
    public interface ILoaderFactoryTypeProvider
    {
        IEnumerable<Type> SupportedTypes { get; }
    }
}
