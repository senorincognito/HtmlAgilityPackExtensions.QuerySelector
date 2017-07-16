### QuerySelector extension HtmlAgilityPack

A naive attempt at a jQuery-like QuerySelector extension method for HtmlAgilityPack.

Works for name, id, class and simple key-value properties. 
Any of these will work:
```
using HtmlAgilityPackExtensions.QuerySelector.Lib;

document.QuerySelector("html body div span");
document.QuerySelector("div#id");
document.QuerySelector("div.class1.class2");
document.QuerySelector("div[attr1=attrvalue1]");
document.QuerySelector("html body div#id1 div.class1 div[attr1=attrvalue1]");
```