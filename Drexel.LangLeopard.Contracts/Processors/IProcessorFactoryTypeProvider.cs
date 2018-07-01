using System;
using System.Collections.Generic;

namespace Drexel.LangLeopard.Contracts.Processors
{
    public interface IProcessorFactoryTypeProvider
    {
        IEnumerable<Type> SupportedTypes { get; }
    }
}
