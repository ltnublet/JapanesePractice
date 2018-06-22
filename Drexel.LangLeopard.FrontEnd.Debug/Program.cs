using Drexel.LangLeopard.Contracts.Loaders;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.ReflectionModel;
using System.ComponentModel.Composition.Registration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Drexel.LangLeopard.FrontEnd.Debug
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // RegistrationBuilder lets us specify how MEF will import/export parts.
            RegistrationBuilder builder = new RegistrationBuilder();
            // Fluently modify the RegistrationBuilder to "export" all ILoaderFactoryTypeProviders. We "export"
            // because the RegistrationBuilder is operating from the perspective of the assembly.
            builder.ForTypesDerivedFrom(typeof(ILoaderFactoryTypeProvider)).Export<ILoaderFactoryTypeProvider>();

            // Load all the assemblies in the current directory.
            string directory = Directory.GetCurrentDirectory();
            DirectoryCatalog catalog = new DirectoryCatalog(directory, builder);
            CompositionContainer container = new CompositionContainer(catalog);

            ImportBuffer buffer = new ImportBuffer();
            container.ComposeParts(buffer);

            Console.ReadLine();
        }

        public class ImportBuffer
        {
            [ImportMany(typeof(ILoaderFactoryTypeProvider))]
            public IEnumerable<ILoaderFactoryTypeProvider> TypeProviders { get; set; }
        }
    }
}
