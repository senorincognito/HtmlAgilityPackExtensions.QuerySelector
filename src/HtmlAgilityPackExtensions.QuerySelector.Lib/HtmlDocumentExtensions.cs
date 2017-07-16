using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
// ReSharper disable PossibleMultipleEnumeration

namespace HtmlAgilityPackExtensions.QuerySelector.Lib
{
    public static class HtmlDocumentExtensions
    {
        public static IEnumerable<HtmlNode> QuerySelector (this HtmlDocument document, string query)
        {
            var parts = new ManagedQuerySelector(query);
            return ExecuteSelectorRecursively(parts.Parts, document.DocumentNode.ChildNodes).Distinct();
        }

        private static IEnumerable<HtmlNode> ExecuteSelectorRecursively(IEnumerable<ManagedQueryPart> queryParts, HtmlNodeCollection items)
        {
            // abort condition
            if (items.NullOrNone() || queryParts.NullOrNone())
                yield break;

            foreach (var shallowItem in items)
            {
                if (FindMatch(shallowItem, queryParts.First()))
                {
                    if (queryParts.Count() == 1)
                        yield return shallowItem;
                    else
                        foreach (var deepItem in ExecuteSelectorRecursively(queryParts.Skip(1), shallowItem.ChildNodes))
                            yield return deepItem;
                }
                foreach (var deepItem in ExecuteSelectorRecursively(queryParts, shallowItem.ChildNodes))
                    yield return deepItem;
            }
        }

        private static bool FindMatch(HtmlNode node, ManagedQueryPart managedQueryPart)
        {
            var matchFound = true;

            // try matching the name if query part defined any
            if (!string.IsNullOrWhiteSpace(managedQueryPart.Name))
                matchFound = node.Name == managedQueryPart.Name;
            
            // if the name match failed we don't have to contiue
            if (matchFound)
                // match all provided properties
                foreach (var a in managedQueryPart.Attributes)
                {
                    if (node.Attributes.All(x => x.Name != a.Key))
                        matchFound = false;
                    if (!matchFound)
                        continue;
                    matchFound = node.Attributes.First(x => x.Name == a.Key).Value == a.Value;
                }
            return matchFound;
        }
    }
}
