using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JapanesePractice.Contract.Contexts;
using JapanesePractice.Contract.ReferenceImplementation;
using JapanesePractice.Core;

namespace JapanesePractice.FrontEnd.WinForms
{
    public class ResourceSession
    {
        private List<DirectoryInfo> directoryInfo;
        private DirectoryInfo[] initialPluginPaths;

        public ResourceSession(DirectoryInfo initialPluginPath)
        {
            this.initialPluginPaths = new DirectoryInfo[] { initialPluginPath };

            this.ResourceLocations = new List<FileInfo>();

            this.PluginLocations = new List<DirectoryInfo>() { initialPluginPath };
            this.SessionBuilder = new SessionBuilder();

            this.ApplicationContext = new ApplicationContext(this.PluginLocations, this.SessionBuilder);
        }

        public ApplicationContext ApplicationContext { get; set; }

        public List<DirectoryInfo> PluginLocations
        {
            get
            {
                return this.directoryInfo;
            }
            set
            {
                this.directoryInfo = value.Concat(this.initialPluginPaths).Distinct().ToList();
            }
        }

        public List<FileInfo> ResourceLocations { get; set; }

        public SessionBuilder SessionBuilder { get; set; }

        public void TriggerRebuild()
        {
            this.ApplicationContext = new ApplicationContext(this.PluginLocations, this.SessionBuilder);
        }
    }
}
