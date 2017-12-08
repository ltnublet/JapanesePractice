using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JapanesePractice.FrontEnd.WinForms
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Program program = new Program();
            program.Start();
        }

        public ResourceSession ResourceSession { get; set; }

        public void Start()
        {
            this.ResourceSession = new ResourceSession(
                new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));

            Application.Run(new MainWindow(this));
        }
    }
}
