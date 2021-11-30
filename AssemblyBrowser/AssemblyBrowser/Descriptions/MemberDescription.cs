using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
    public class MemberDescription
    {
        public string MemberSignature { get; private set; }
        public string MemberType { get; private set; }

        public MemberDescription(string signature, string type)
        {
            MemberSignature = signature;
            MemberType = type;
        }
    }
}
