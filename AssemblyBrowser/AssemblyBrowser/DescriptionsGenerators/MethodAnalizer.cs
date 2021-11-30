using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AssemblyBrowser.DescriptionsGenerators
{
    internal class MethodAnalizer
    {
        public MemberDescription GenerateMethodDescription(MethodInfo methodInfo) 
        {

            
            StringBuilder builder = new StringBuilder();
            this.GetMethodSecurityDescriprion(methodInfo, builder);
            
            this.GetMethodModifiers(methodInfo,builder);
            this.GetMethodMainInfo(methodInfo, builder);
 
            this.GetMethodParameters(methodInfo, builder);
            MemberDescription dectription = new MemberDescription(builder.ToString(), "method");
            return dectription;
        }

        private void GetMethodMainInfo(MethodInfo methodInfo, StringBuilder builder) 
        {
            builder.Append(ClassAnalizer.GetClassName(methodInfo.ReturnType));
            builder.Append(" ");
            builder.Append(methodInfo.Name);
        }

        private void GetMethodSecurityDescriprion(MethodInfo methodInfo, StringBuilder builder) 
        {
            if (methodInfo.IsPublic)
            {
                builder.Append("public ");
            }
            else if (methodInfo.IsPrivate)
            {
                builder.Append("private ");

            }

        }

        private void GetMethodModifiers(MethodInfo methodInfo, StringBuilder builder) 
        {
            if (methodInfo.IsStatic)
            {
                builder.Append("static ");
            }
            if (methodInfo.IsAbstract)
            {
                builder.Append("abstract ");
            }
            if (methodInfo.IsVirtual)
            {
                builder.Append("virtual ");
            }
            if (methodInfo.IsFinal)
            {
                builder.Append("sealed ");
            }
        }


        private void GetMethodParameters(MethodInfo methodInfo, StringBuilder builder) 
        {
 
            builder.Append("(");
            int i = 0;
            foreach (ParameterInfo parameterInfo in methodInfo.GetParameters())
            {
                if (i > 0)
                {
                    builder.Append(", ");
                }
                if (parameterInfo.IsOut)
                {
                    builder.Append("out ");
                }
                if (parameterInfo.IsIn)
                {
                    builder.Append("in ");
                }
                builder.Append(ClassAnalizer.GetClassName(parameterInfo.ParameterType));
                builder.Append(" ");
                builder.Append(parameterInfo.Name);
                builder.Append(" ");
                i++;

            }


            builder.Append(")");

        }

    }
}
