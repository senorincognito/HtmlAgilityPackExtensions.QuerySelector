using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace HtmlAgilityPackExtensions.QuerySelector.Lib.Tests.Helper
{
    public static class TestHelper
    {
        private static HtmlDocument FromString(string htmlString)
        {
            var document = new HtmlDocument();
            document.LoadHtml(htmlString);
            return document;
        }

        public static ICollection<HtmlNode> RunQuerySelector(string htmlString, string querySelector)
        {
            return FromString(htmlString).QuerySelector(querySelector).ToList();
        }
    }
}
