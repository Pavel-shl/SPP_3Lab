using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser.DescriptionsGenerators
{
    internal class NamespaceAnalizer
    {
        

        public NamespaceDescription GenerateNamespaceDescription(string name)
        {
            NamespaceDescription description = new NamespaceDescription();
            description.Name = name;
            return description;
        }
        

    }
}
