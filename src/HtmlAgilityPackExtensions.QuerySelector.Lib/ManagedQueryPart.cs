using System;
using System.Collections.Generic;
using System.Linq;

namespace HtmlAgilityPackExtensions.QuerySelector.Lib
{
    public class ManagedQueryPart
    {
        public ManagedQueryPart(string queryPart)
        {
            Attributes = new Dictionary<string, string>();
            Name = string.Empty;

            var splitByHash = queryPart.Split(new[] { '#' }, StringSplitOptions.None);
            FindId(splitByHash, ref queryPart);

            var splitByDot = queryPart.Split(new[] { '.' }, StringSplitOptions.None);
            FindClasses(splitByDot, ref queryPart);

            var splitByBracket = queryPart.Split(new[] { '[' }, StringSplitOptions.None);
            FindAttributes(splitByBracket, ref queryPart);

            if (splitByHash.Length == 1
                && splitByDot.Length == 1
                && splitByBracket.Length == 1)
                Name = queryPart;
        }

        public string Name { get; set; }
        public Dictionary<string, string> Attributes { get; set; }

        private void SetNameIfNotSetAndNotNull(string newName)
        {
            if (string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(newName))
                Name = newName;
        }

        private void FindId(IReadOnlyList<string> splitByHash, ref string queryPart)
        {
            // no # found
            if (splitByHash.Count <= 1)
                return;

            SetNameIfNotSetAndNotNull(splitByHash[0]);

            // adjust queryPart
            queryPart = splitByHash.Last();

            // id is everything between the split and the next delimitter
            SetIdAttribute(splitByHash[1].Split(new[] { '.', '[' }, StringSplitOptions.None)[0]);
        }

        private void SetIdAttribute(string idValue)
        {
            Attributes.Add("id", idValue);
        }

        private void FindClasses(IReadOnlyList<string> splitByDot, ref string queryPart)
        {
            // no . found
            if (splitByDot.Count <= 1)
                return;

            SetNameIfNotSetAndNotNull(splitByDot[0]);

            // adjust queryPart
            queryPart = splitByDot.Last();

            //todo: we might need to handle classes differently since this enforces an order
            var combinedClasses = splitByDot
                .Skip(1) // skip all before the dot
                .Take(Math.Max(1, splitByDot.Count - 1)); // take all splitted by dots
            var classes = string.Join(" ", combinedClasses);

            // classes are everything between the split(ed) values and the next delimitter
            SetClassAttributes(classes.Split(new[] {'['}, StringSplitOptions.None)[0]);
        }

        private void SetClassAttributes(string classValues)
        {
            Attributes.Add("class", classValues);
        }

        private void FindAttributes(IReadOnlyList<string> splitByBracket, ref string queryPart)
        {
            // no attr found
            if (splitByBracket.Count <= 1)
                return;

            SetNameIfNotSetAndNotNull(splitByBracket[0]);

            // adjust queryParts
            queryPart = splitByBracket.Last();

            for (var i = 1; i < splitByBracket.Count; i++)
                CaptureAttributesBetweenBrackets(splitByBracket[i]);
        }

        private void CaptureAttributesBetweenBrackets(string bracketSplit)
        {
            var capturedBetweenBrackets = bracketSplit.Split(new[] { ']' }, StringSplitOptions.None);
            var capturedKeyValuePair = capturedBetweenBrackets[0].Split(new[] { '=' }, StringSplitOptions.None);

            var key = capturedKeyValuePair[0].Trim();
            var value = capturedKeyValuePair[1].Trim();

            // ignore id and class attributes since we handled those somewhere else already
            if (key == "id" || value == "class")
                return;

            Attributes.Add(key, value);
        }
    }
}