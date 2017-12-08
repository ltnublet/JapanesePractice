using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JapanesePractice.Contract.Loaders;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace JapanesePractice.FrontEnd.WinForms
{
    public partial class MainWindow : Form
    {
        private Program instance;

        public MainWindow(Program parent)
        {
            this.instance = parent;

            this.InitializeComponent();
        }

        private void OpenResourceFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog openResourceFile = new CommonOpenFileDialog()
            {
                Multiselect = true,
                EnsureFileExists = true,
                EnsurePathExists = true
            };

            if (openResourceFile.ShowDialog(this.Handle) == CommonFileDialogResult.Ok)
            {
                this
                    .instance
                    .ResourceSession
                    .ResourceLocations
                    .AddRange(openResourceFile.FileNames.Select(x => new FileInfo(x)));
            }
        }

        private void OpenResourcePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog openResourceFile = new CommonOpenFileDialog()
            {
                Multiselect = true,
                IsFolderPicker = true,
                EnsureFileExists = true,
                EnsurePathExists = true
            };

            if (openResourceFile.ShowDialog(this.Handle) == CommonFileDialogResult.Ok)
            {
                List<DirectoryInfo> directories =
                    openResourceFile.FileNames.Select(x => new DirectoryInfo(x)).ToList();
                directories = directories
                    .Concat(directories.SelectMany(x => x.EnumerateDirectories("*", SearchOption.AllDirectories)))
                    .ToList();

                this
                    .instance
                    .ResourceSession
                    .ResourceLocations
                    .AddRange(directories.SelectMany(x => x.EnumerateFiles("*.json")));
            }
        }

        private void OpenResourceSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NewResourceSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OpenPluginSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OpenPluginDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog openResourceFile = new CommonOpenFileDialog()
            {
                Multiselect = true,
                IsFolderPicker = true,
                EnsureFileExists = true,
                EnsurePathExists = true
            };

            if (openResourceFile.ShowDialog(this.Handle) == CommonFileDialogResult.Ok)
            {
                List<DirectoryInfo> directories =
                    openResourceFile.FileNames.Select(x => new DirectoryInfo(x)).ToList();
                directories = directories
                    .Concat(directories.SelectMany(x => x.EnumerateDirectories("*", SearchOption.AllDirectories)))
                    .ToList();

                this
                    .instance
                    .ResourceSession
                    .PluginLocations
                    .AddRange(directories);
            }
        }

        private void StartSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.instance.ResourceSession.TriggerRebuild();

            RichTextBox box = new RichTextBox()
            {
                Dock = DockStyle.Fill
            };

            box.AppendText("Loaders:\n");
            foreach (string key in this.instance.ResourceSession.ApplicationContext.Loaders.Keys)
            {
                box.AppendText("\t" + key + ":\n");
                foreach (ILoader loader in this.instance.ResourceSession.ApplicationContext.Loaders.GetMultiple(key))
                {
                    box.AppendText("\t\t" + loader.ToString() + "\n");
                }
            }

            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(box);
        }
    }
}
