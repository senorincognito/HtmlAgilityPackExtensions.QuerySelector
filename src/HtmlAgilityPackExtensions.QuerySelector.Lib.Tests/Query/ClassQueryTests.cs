using System.Linq;
using HtmlAgilityPackExtensions.QuerySelector.Lib.Tests.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlAgilityPackExtensions.QuerySelector.Lib.Tests.Query
{
    [TestClass]
    public class ClassQueryTests
    {
        [TestMethod]
        public void Simple_Class_Name()
        {
            var result = TestHelper.RunQuerySelector(
                    "<html><body><div><span class='result'>test</span></div></div></body></html>",
                    "html body div .result");

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("test", result.ElementAt(0).InnerText);
        }

        [TestMethod]
        public void Multiple_Class_Names()
        {
            var result = TestHelper.RunQuerySelector(
                "<html><body><div><span class='result1 result2'>test</span><span class='result1'>wrong</span>" +
                "<span class='result2'>wrong</span></div></div></body></html>", 
                "html body div .result1.result2");

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("test", result.ElementAt(0).InnerText);
        }
    }
}
