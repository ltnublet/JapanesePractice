using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;

namespace Drexel.LangLeopard.FrontEnd.Debug
{
    public static class ExtensionMethods
    {
        private static FieldInfo directoryCatalogFieldInfo = typeof(DirectoryCatalog).GetField(
            "_assemblyCatalogs",
            BindingFlags.Instance | BindingFlags.NonPublic);

        public static IEnumerable<Assembly> GetAssemblies(this DirectoryCatalog catalog)
        {
            Dictionary<string, AssemblyCatalog> assemblies =
                directoryCatalogFieldInfo.GetValue(catalog) as Dictionary<string, AssemblyCatalog>;

            return (assemblies?.Values.Select(x => x.Assembly))
                ?? throw new InvalidOperationException("Failed to retrieve assemblies from DirectoryCatalog.");
        }
    }
}
