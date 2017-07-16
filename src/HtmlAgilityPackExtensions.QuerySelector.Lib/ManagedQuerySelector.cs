using System;
using System.Linq;

namespace HtmlAgilityPackExtensions.QuerySelector.Lib
{
    public class ManagedQuerySelector
    {
        public ManagedQuerySelector(string query)
        {
            Parts = query
                .Split(new [] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new ManagedQueryPart(x))
                .ToArray();
        }
        public ManagedQueryPart[] Parts { get; }
    }
}