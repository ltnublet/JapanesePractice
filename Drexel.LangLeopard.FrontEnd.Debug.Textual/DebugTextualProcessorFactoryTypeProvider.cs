using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drexel.LangLeopard.Contracts;
using Drexel.LangLeopard.Contracts.Processors;

namespace Drexel.LangLeopard.FrontEnd.Debug.Textual
{
    public class DebugTextualProcessorFactoryTypeProvider : IProcessorFactoryTypeProvider
    {
        private static IEnumerable<Type> supportedTypes = new Type[] { typeof(Localized) };

        public IEnumerable<Type> SupportedTypes => DebugTextualProcessorFactoryTypeProvider.supportedTypes;
    }
}
