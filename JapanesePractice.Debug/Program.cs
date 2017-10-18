using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using JapanesePractice.Contexts;
using JapanesePractice.Interpretations;
using JapanesePractice.Loaders;

namespace JapanesePractice.FrontEnd.Debug
{
    public class Program
    {
        private Dictionary<string, ILoader> Loaders;
        private bool showCount;

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

            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

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

            Console.Write("For symbols with multiple interpretations, should a count be displayed? (y/n): ");
            this.showCount = Console.ReadLine().Equals("y", StringComparison.InvariantCultureIgnoreCase);

            this.Run(activeCategories);

            Console.ReadLine();
        }

        private void Run(List<Category> categories)
        {
            Random random = new Random();
            
            int counter = 0;
            int correct = 0;
            IInterpretation mostRecent = null;
            while (true)
            {
                Category category;
                Symbol symbol;
                IInterpretation interpretation;

                do
                {
                    category = categories[random.Next(0, categories.Count)];
                    symbol = category.Symbols[random.Next(0, category.Symbols.Count)];
                    interpretation = symbol.Interpretations[random.Next(0, symbol.Interpretations.Count)];
                } while (mostRecent == interpretation);

                mostRecent = interpretation;

                List<string> input;
                bool matched = false;
                do
                {
                    input = new List<string>();

                    for (int lengthBuffer = interpretation.GetPermittedInterpretations().Count();
                        input.Count < lengthBuffer;
                        input.Add(Console.ReadLine()))
                    {
                        this.PromptSymbol(input.Count + 1, lengthBuffer, symbol.Name);
                    }

                    matched = interpretation.Compare(new Textual.TextualInterpretation(input));
                    if (matched)
                    {
                        correct++;
                        Console.WriteLine("Correct!");
                    }

                    Console.WriteLine(string.Join(
                        ", ",
                        symbol.Interpretations.Select(x =>
                            string.Format(
                                "{{{0}}}",
                                string.Join(
                                    ", ",
                                    x.GetPermittedInterpretations().Select(y => y.ToString()))))));

                    Console.WriteLine(string.Format("Correct: {0}/{1}", correct, ++counter));
                } while (!matched);
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

        private void PrintSpaces(int numberOfSpaces)
        {
            // Naive implementation, please don't judge, I know I should write to stream instead.
            for (int counter = 0; counter < numberOfSpaces; counter++)
            {
                Console.Write(' ');
            }
        }

        private void PromptSymbol(int currentInterpretationIndex, int totalNumberOfInterpretations, string symbolName)
        {
            if (this.showCount)
            {
                Console.Write(string.Format(
                    "({0}/{1}) {2}: ",
                    currentInterpretationIndex,
                    totalNumberOfInterpretations,
                    symbolName));
            }
            else
            {
                Console.Write(string.Format("{0}: ", symbolName));
            }

            // Hack for versions of windows prior to the Creators Update (figure out better solution)
            this.PrintSpaces(symbolName.Length);
        }
    }
}
