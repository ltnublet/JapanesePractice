using JapanesePractice.Contract.Loaders;
using JapanesePractice.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        [ImportMany(typeof(ILoader))]
        private ILoader[] loaders = new ILoader[0];

        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Run2();

            Console.ReadLine();
        }

        public void Run()
        {
            using (AggregateCatalog catalog = new AggregateCatalog())
            {
                string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                Console.WriteLine(location);
                catalog.Catalogs.Add(new DirectoryCatalog(location));

                using (CompositionContainer container = new CompositionContainer(catalog))
                {
                    container.ComposeParts(this);
                }
            }

            foreach (ILoader loader in this.loaders)
            {
                Console.WriteLine(loader.ToString());
            }
        }

        public void Run2()
        {
            ApplicationContext context = new ApplicationContext(
                new DirectoryInfo[]
                {
                    new DirectoryInfo(Path.GetFullPath(Assembly.GetExecutingAssembly().Location))
                },
                new SessionBuilder());

            foreach (ILoader loader in context.Loaders.SelectMany(x => x.Value))
            {
                Console.WriteLine(loader.ToString());
            }
        }
    }
}
