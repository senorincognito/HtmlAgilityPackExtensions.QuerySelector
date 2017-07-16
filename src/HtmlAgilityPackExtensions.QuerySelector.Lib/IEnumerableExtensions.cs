using System.Collections;

namespace HtmlAgilityPackExtensions.QuerySelector.Lib
{
    public static class LinqExtensions
    {
        public static bool NullOrNone(this IEnumerable items)
        {
            if (items == null)
                return true;
            return !items.GetEnumerator().MoveNext();
        }
    }
}
