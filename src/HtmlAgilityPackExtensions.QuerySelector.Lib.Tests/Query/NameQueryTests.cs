using System.Linq;
using HtmlAgilityPackExtensions.QuerySelector.Lib.Tests.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlAgilityPackExtensions.QuerySelector.Lib.Tests.Query
{
    [TestClass]
    public class NameQueryTests
    {

        [TestMethod]
        public void Single_Result()
        {
            var result = TestHelper.RunQuerySelector(
                "<html><body><div>test</div></body></html>", 
                "html body div");

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("test", result.ElementAt(0).InnerText);
        }

        [TestMethod]
        public void Single_Query()
        {
            var result = TestHelper.RunQuerySelector(
                "<html><body><div>test</div></body></html>",
                "div");

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("test", result.ElementAt(0).InnerText);
        }

        [TestMethod]
        public void Deep_Nesting()
        {
            var result = TestHelper.RunQuerySelector(
                "<html><body><div>wrong<div>wrong<span>test</span></div></div></body></html>",
                "html body div span");

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("test", result.ElementAt(0).InnerText);
        }

        [TestMethod]
        public void Two_Results()
        {
            var result = TestHelper.RunQuerySelector(
                "<html><body><div><span>test1</span></div><div>wrong<div>wrong<span>test2</span></div></div></body></html>",
                "html body div span");
            
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("test1", result.ElementAt(0).InnerText);
            Assert.AreEqual("test2", result.ElementAt(1).InnerText);
        }
    }
}
