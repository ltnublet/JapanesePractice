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
        private Dictionary<string, ILoader> Loaders;

        public Program(string[] args)
        {
            LoaderContainer loaderContainer = new LoaderContainer();

            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(loaderContainer);

            this.Loaders = new Dictionary<string, ILoader>();
            foreach (ILoader loader in loaderContainer.Loaders)
            {
                foreach (string supportedType in loader.TypesSupported)
                {
                    this.Loaders.Add(supportedType, loader);
                }
            }
        }

        public static void Main(string[] args)
        {
            Program program = new Program(args);
            program.Start();
        }

        public void Start()
        {
            const string root = @"..\..\..\Memrise";

            string file = this.GetFileToLoad(root);
            IContext context = this.Loaders["Textual"].LoadContextFromPath(file);

            this.PrintSeparator();
            List<Category> activeCategories = new List<Category>();
            foreach (Category category in context.Categories)
            {
                Console.WriteLine(category.ToString());
                Console.Write("Include? (y/n): ");
                string input = Console.ReadLine();
                if (input.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                {
                    activeCategories.Add(category);
                }
            }
            
            if (activeCategories.Count < 1)
            {
                throw new InvalidOperationException("No categories selected.");
            }

            this.Run(activeCategories);

            Console.ReadLine();
        }

        private void Run(List<Category> categories)
        {
            Random random = new Random();
            
            int counter = 0;
            int correct = 0;
            while (true)
            {
                Category category = categories[random.Next(0, categories.Count)];
                Symbol symbol = category.Symbols[random.Next(0, category.Symbols.Count)];

                List<string> input = new List<string>();
                while (input.Count != symbol.Interpretations[0].GetPermittedInterpretations().Count())
                {
                    Console.Write(string.Format(
                        "({0}/{1}) {2}: ",
                        input.Count + 1,
                        symbol.Interpretations[0].GetPermittedInterpretations().Count(),
                        symbol.Name));
                    input.Add(Console.ReadLine());
                }

                if (new Textual.TextualInterpretation(input).CompareAll(symbol.Interpretations))
                {
                    correct++;
                }
                else
                {
                    Console.WriteLine(string.Join(
                        ", ",
                        symbol.Interpretations.Select(x =>
                            string.Format(
                                "{{{0}}}",
                                string.Join(
                                    ", ",
                                    x.GetPermittedInterpretations().Select(y => y.ToString()))))));
                }

                Console.WriteLine(string.Format("Correct: {0}/{1}", correct, ++counter));
            }
        }

        private string GetFileToLoad(string current)
        {
            if (current.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                return current;
            }

            Dictionary<int, string> paths = new Dictionary<int, string>();
            int counter = 0;
            foreach (string directory in Directory.EnumerateDirectories(current))
            {
                paths.Add(counter++, directory);
            }

            foreach (string file in Directory.EnumerateFiles(current, "*.json"))
            {
                paths.Add(counter++, file);
            }

            foreach (KeyValuePair<int, string> pair in paths)
            {
                Console.WriteLine(string.Format("{0}: `{1}`", pair.Key, pair.Value));
            }

            return this.GetFileToLoad(paths[int.Parse(Console.ReadLine())]);
        }

        private void PrintSeparator()
        {
            Console.WriteLine("================================================================================");
        }
    }
}
