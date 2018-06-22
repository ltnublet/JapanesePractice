using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.ReflectionModel;
using System.ComponentModel.Composition.Registration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drexel.LangLeopard.FrontEnd.Debug
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RegistrationBuilder builder = new RegistrationBuilder();


            string directory = Directory.GetCurrentDirectory();
            AssemblyCatalog[] assemblyCatalogs = new DirectoryCatalog(directory)
                .Select(x => ReflectionModelServices.GetPartType(x).Value.Assembly)
                .Distinct()
                .Select(x => new AssemblyCatalog(x, reflectionContext: builder))
                .ToArray();

            AggregateCatalog catalog = new AggregateCatalog(assemblyCatalogs);
            CompositionContainer container = new CompositionContainer(catalog);
        }
    }
}
