using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyBrowser;
using System.Collections.Generic;
namespace AssemblyVievTests
{
    [TestClass]
    public class AssemblyTests
    {
        private AssemblyViever assemblyViever;
        private static string testPath = @"..\..\..\Dlls for test\FakerLib.dll";

        public AssemblyTests()
        {
            assemblyViever = new AssemblyViever();
            assemblyViever.VievAssembly(testPath);
        }

        //Проверяет количесвто нэймспейсов в dll которую мы тестируем
        [TestMethod]
        public void NamespacesTest()
        {

            Assert.AreEqual(assemblyViever.namespaces.Count, 2);
            List<string> ns = new List<string>();
            foreach (NamespaceDescription description in assemblyViever.namespaces)
            {
                ns.Add(description.Name);
            }

            Assert.IsTrue(ns.Contains("FakerLib"));
            Assert.IsTrue(ns.Contains("FakerLib.Generator"));
        }

        //количество классов в длл
        [TestMethod]
        public void ClassesTest()
        {

            NamespaceDescription @namespace = assemblyViever.namespaces.Find( (NamespaceDescription ns)=>( ns.Name == "FakerLib") );

            List<string> classNames = new List<string>();
            foreach (ClassDescription description in @namespace.Classes)
            {
                classNames.Add(description.Name);
            }

            Assert.IsTrue(classNames.Contains("Faker"));
            Assert.IsTrue(classNames.Contains("FakerConfig"));
        }

        //проверяет методы
        [TestMethod]
        public void MethodsTest()
        {

            NamespaceDescription @namespace = assemblyViever.namespaces.Find((NamespaceDescription ns) => (ns.Name == "FakerLib"));
            ClassDescription @class = @namespace.Classes.Find((ClassDescription cl) => (cl.Name =="Faker"));
            Assert.IsTrue(@class.Members.ContainsKey("Methods"));
            Assert.AreEqual(@class.Members["Methods"].Count,5);
         
        }

        //свойства
        [TestMethod]
        public void PropertiesTest()
        {

            NamespaceDescription @namespace = assemblyViever.namespaces.Find((NamespaceDescription ns) => (ns.Name == "FakerLib.Generator"));
            ClassDescription @class = @namespace.Classes.Find((ClassDescription cl) => (cl.Name == "CityGenerator"));
            Assert.IsTrue(@class.Members.ContainsKey("Properties"));
            Assert.AreEqual(@class.Members["Properties"].Count, 1);

        }

        //поля
        [TestMethod]
        public void FieldsTest()
        {

            NamespaceDescription @namespace = assemblyViever.namespaces.Find((NamespaceDescription ns) => (ns.Name == "FakerLib.Generator"));
            ClassDescription @class = @namespace.Classes.Find((ClassDescription cl) => (cl.Name == "CityGenerator"));
            Assert.IsTrue(@class.Members.ContainsKey("Fields"));
            Assert.AreEqual(@class.Members["Fields"].Count, 0);

        }


    }
}
