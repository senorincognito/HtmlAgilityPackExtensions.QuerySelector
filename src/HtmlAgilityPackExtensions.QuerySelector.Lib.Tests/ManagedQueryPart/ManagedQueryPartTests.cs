using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlAgilityPackExtensions.QuerySelector.Lib.Tests.ManagedQueryPart
{
    [TestClass]
    public class ManagedQueryPartTests
    {
        private Lib.ManagedQueryPart CreateFrom(string queryPart)
        {
            return new Lib.ManagedQueryPart(queryPart);
        }

        [TestMethod]
        public void Test_Simple_Name()
        {
            Assert.AreEqual("name", CreateFrom("name").Name);
        }

        [TestMethod]
        public void Test_Simple_Class()
        {
            var cut = CreateFrom(".class");
            Assert.AreEqual(string.Empty, cut.Name);
            Assert.AreEqual("class", cut.Attributes["class"]);
        }

        [TestMethod]
        public void Test_Multiple_Class()
        {
            var cut = CreateFrom(".class1.class2.class3");
            Assert.AreEqual(string.Empty, cut.Name);
            Assert.AreEqual("class1 class2 class3", cut.Attributes["class"]);
        }

        [TestMethod]
        public void Test_Name_With_Class()
        {
            var cut = CreateFrom("name.class");
            Assert.AreEqual("name", cut.Name);
            Assert.AreEqual("class", cut.Attributes["class"]);
        }

        [TestMethod]
        public void Test_Simple_Id()
        {
            var cut = CreateFrom("#id");
            Assert.AreEqual(string.Empty, cut.Name);
            Assert.AreEqual("id", cut.Attributes["id"]);
        }

        [TestMethod]
        public void Test_Name_With_Id()
        {
            var cut = CreateFrom("name#id");
            Assert.AreEqual("name", cut.Name);
            Assert.AreEqual("id", cut.Attributes["id"]);
        }

        [TestMethod]
        public void Test_Name_With_Id_And_Class()
        {
            var cut = CreateFrom("name#id.class");
            Assert.AreEqual("name", cut.Name);
            Assert.AreEqual("id", cut.Attributes["id"]);
            Assert.AreEqual("class", cut.Attributes["class"]);
        }

        [TestMethod]
        public void Test_Simple_Attribute()
        {
            var cut = CreateFrom("[key=value]");
            Assert.AreEqual(string.Empty, cut.Name);
            Assert.AreEqual("value", cut.Attributes["key"]);
        }


        [TestMethod]
        public void Test_Multiple_Attributes()
        {
            var cut = CreateFrom("[key1=value1][key2=value2]");
            Assert.AreEqual(string.Empty, cut.Name);
            Assert.AreEqual("value1", cut.Attributes["key1"]);
            Assert.AreEqual("value2", cut.Attributes["key2"]);
        }

        [TestMethod]
        public void Test_With_Name_Id_Class_And_Multiple_Attributes()
        {
            var cut = CreateFrom("name#id.class[key1=value1][key2=value2]");
            Assert.AreEqual("name", cut.Name);
            Assert.AreEqual("id", cut.Attributes["id"]);
            Assert.AreEqual("class", cut.Attributes["class"]);
            Assert.AreEqual("value1", cut.Attributes["key1"]);
            Assert.AreEqual("value2", cut.Attributes["key2"]);
        }

        [TestMethod]
        public void Ignore_id_and_class_attributes_in_brackets()
        {
            var cut = CreateFrom("[id=id][class=class]");
            Assert.AreEqual(string.Empty, cut.Name);
            Assert.AreEqual(0, cut.Attributes.Count);
        }
    }
}
