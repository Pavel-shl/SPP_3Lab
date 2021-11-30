using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AssemblyBrowser
{
    public class NamespaceDescription
    {
        public string Name { get; internal set; }
        public List<ClassDescription> Classes { get; set; } = new List<ClassDescription>();
    }
}
