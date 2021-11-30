using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AssemblyBrowser.DescriptionsGenerators
{
    class ClassAnalizer
    {
        //создается что описание класса и передается все туда
        public ClassDescription GenerateClassDescription(Type thisClass)
        {
            ClassDescription description = new ClassDescription();
            description.Name = thisClass.Name;
            description.Members.Add("Properties", new List<MemberDescription>());
            description.Members.Add("Methods", new List<MemberDescription>());
            description.Members.Add("Fields", new List<MemberDescription>());

            this.GetMethodsDescriptions(thisClass,description);
            this.GetFieldsDescriptions(thisClass, description);
            this.GetPropertiesDescriptions(thisClass,description);
         
            return description;
        }

        //преобразования если дженерик
        public static string GetClassName(Type type)
        {
            if (!type.IsGenericType)
            {
                return type.Name;
            }
            else
            {

                string classname = "",
                    genericName = type.GetGenericTypeDefinition().Name;

                if (genericName.IndexOf("`") >= 0)
                {
                    genericName = genericName.Substring(0, genericName.IndexOf("`"));
                }

                classname += genericName;
                classname += '<';
                int i = 0;
                foreach (Type genericParameterType in type.GetGenericArguments())
                {
                    if (i > 0)
                    {
                        classname += ", ";
                    }
                    classname += ClassAnalizer.GetClassName(genericParameterType);
                    i++;
                }

                classname += '>';
                return classname;
            }

        }

        //для методов
        private void GetMethodsDescriptions( Type type, ClassDescription classDescription) 
        {
            MethodAnalizer methodAnalizer = new MethodAnalizer();

            foreach (MethodInfo info in type.GetMethods())
            {


                classDescription.Members["Methods"].Add(methodAnalizer.GenerateMethodDescription(info));
            }
        }

        //для свойств создает свойсоаналайзер и делает поиск
        private void GetPropertiesDescriptions(Type type, ClassDescription classDescription)
        {
            PropertyAnalizer propertyAnalizer = new PropertyAnalizer();
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
   
                classDescription.Members["Properties"].Add(propertyAnalizer.GeneratePropertyDescription(propertyInfo));
            }


        }

        private void GetFieldsDescriptions(Type type, ClassDescription classDescription)
        {
            FieldAnalizer fieldAnalizer = new FieldAnalizer();
            foreach (FieldInfo fieldInfo in type.GetFields())
            {

                classDescription.Members["Fields"].Add(fieldAnalizer.GetFieldDesctription(fieldInfo));
            }
        }

    }
}
