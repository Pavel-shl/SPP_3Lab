using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AssemblyBrowser.DescriptionsGenerators;


namespace AssemblyBrowser
{
    public class AssemblyViever
    {
        //список нэймспейсов
        private List<NamespaceDescription> namespaces1 = new List<NamespaceDescription>();

        
        public List<NamespaceDescription> namespaces { get { return namespaces1; } }

        public string AssemblyName { get; private set; }

        //просмотреть сборку, передается путь dll
        public void VievAssembly( string path )
        {
            this.CloseAssembly();
            //загружается файл
            Assembly assembly =Assembly.LoadFrom(path); 
            // получаются все типы из сборки
            Type[] types = assembly.GetTypes();
            //анализаторы создаются экземпляры, для описания классов служат
            NamespaceAnalizer analizer = new NamespaceAnalizer();
            ClassAnalizer classAnalizer = new ClassAnalizer();

            NamespaceDescription current;
            ClassDescription currentClass;

            //цикл по всем типам, просматриваются все типы, добавляется их описание в структуру данных
            foreach (Type type in types)
            {
                //если не класс то пропускаем
                if (!type.IsClass)
                    continue;
                //смотрим нэймспейс есть есть то добавляем в существующий если нет то создаем новый
                current = namespaces.Find((NamespaceDescription description) => (description.Name == type.Namespace));
                if (  current == null ) 
                {
                    current = analizer.GenerateNamespaceDescription(type.Namespace);
                    namespaces.Add(current);
                } 
                     
                currentClass = classAnalizer.GenerateClassDescription(type);
                current.Classes.Add(currentClass);
            }
        }

        //для очистки программы
        public void CloseAssembly()
        {
            namespaces1 = new List<NamespaceDescription>();
            AssemblyName = "";

        }

    }
}
