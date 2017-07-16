using System.Linq;
using HtmlAgilityPackExtensions.QuerySelector.Lib.Tests.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlAgilityPackExtensions.QuerySelector.Lib.Tests.Query
{
    [TestClass]
    public class IdQueryTests
    {
        [TestMethod]
        public void Simple_Id_Name()
        {
            var result = TestHelper.RunQuerySelector(
                "<html><body><div><span id='result'>test</span></div></div></body></html>", 
                "html body div #result");

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("test", result.ElementAt(0).InnerText);
        }
    }
}
