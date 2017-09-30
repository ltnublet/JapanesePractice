using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JapanesePractice.Contexts;
using JapanesePractice.Loaders;

namespace JapanesePractice.FrontEnd.Debug
{
    public class Program
    {
        [ImportMany(typeof(ILoader))]
        private IEnumerable<Lazy<ILoader>> loaders;

        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Start(args);
        }

        public void Start(string[] args)
        {
            // This assignment is just so that code analysis stops complaining about unused variables - the real value
            // comes from MEF.
            this.loaders = new List<Lazy<ILoader>>();

            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);

            this.Run();
        }

        public void Run()
        {
            string path = @"..\..\..\Skeleton.json";

            IContext context;
            using (TextReader reader = new StreamReader(path))
            {
                context = this.loaders.Single(loader => loader.Value.AcceptsType("Textual")).Value.FromFile(reader);
            }

            Console.WriteLine(context.ToString());
            foreach(Category category in context.Categories)
            {
                Console.WriteLine(category.ToString());
            }
            Console.ReadLine();
        }
    }
}
