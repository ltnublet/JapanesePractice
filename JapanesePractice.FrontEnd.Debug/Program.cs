using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using JapanesePractice.Core;
using JapanesePractice.Contract;
using JapanesePractice.Contract.Contexts;
using JapanesePractice.Contract.Interpretations;
using Microsoft.Win32;

namespace JapanesePractice.FrontEnd.Debug
{
    public class Program
    {
        private static Lazy<bool> windowsVersionRequiresPrintHack = new Lazy<bool>(
            () =>
            {
                try
                {
                    int windowsVersion = int.Parse(
                        Registry
                            .GetValue(
                                @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion",
                                "ReleaseId",
                                0)
                            .ToString());

                    return windowsVersion < 1709;
                }
                catch
                {
                    return false;
                }
            });

        private ApplicationContext applicationContext;
        private ConsoleSymbolSelector symbolSelector;
        private bool showCount;

        public Program(string[] args)
        {
            DirectoryInfo[] pluginLocations = new DirectoryInfo[]
            {
                new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            };

            this.symbolSelector = new ConsoleSymbolSelector();

            SessionBuilder builder = new SessionBuilder().UsingSymbolSelector(this.symbolSelector);

            this.applicationContext = new ApplicationContext(pluginLocations, builder);
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
            IContext context = this.applicationContext.Loaders["Textual"].LoadContextFromPath(file);

            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            ConsoleHelper.SetConsoleFont();

            this.PrintSeparator();
            foreach (ICategory category in context.Categories.ToList())
            {
                Console.WriteLine(category.ToString());
                Console.Write("Include? (y/n): ");
                string input = Console.ReadLine();
                if (input.Equals("n", StringComparison.InvariantCultureIgnoreCase))
                {
                    context.Categories.Remove(category);
                }
            }
            
            if (context.Categories.Count < 1)
            {
                throw new InvalidOperationException("No categories selected.");
            }

            Console.Write("For symbols with multiple interpretations, should a count be displayed? (y/n): ");
            this.showCount = Console.ReadLine().Equals("y", StringComparison.InvariantCultureIgnoreCase);

            Session session = this.applicationContext.CreateSession(context);

            this.Run(session);

            Console.ReadLine();
        }

        private void Run(Session session)
        {
            Random random = new Random();
            
            int counter = 0;
            int correct = 0;
            while (true)
            {
                ISymbol symbol;
                IInterpretation interpretation;

                symbol = session.SelectSymbol(session.SelectCategory());
                interpretation = session.SelectInterpretation(symbol);

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
                        this.symbolSelector.ExpectedSymbol = null;
                        this.symbolSelector.AddDisallowedOnNextSelectSymbol(symbol);
                        Console.WriteLine("Correct!");
                    }
                    else
                    {
                        this.symbolSelector.ExpectedSymbol = symbol;
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
            Console.WriteLine(new string('=', Console.BufferWidth));
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

            if (Program.windowsVersionRequiresPrintHack.Value)
            {
                // Hack for versions of windows prior to the Creators Update (figure out better solution)
                Console.Write(new string(' ', symbolName.Length));
            }
        }
    }
}
