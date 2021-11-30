using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace AssemblyBrowser
{
    public class ClassDescription
    {

        public String Name { get; internal set; }
  

        public Dictionary<string, List<MemberDescription>> Members { get; set; } = new Dictionary<string, List<MemberDescription>>();

        public ClassDescription()
        {
     
        }
 
    }
}
