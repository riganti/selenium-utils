using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace Riganti.Selenium.Core
{
    public class SelectBy : By
    {
        public SelectBy(string selectMethodName)
        {
            SelectMethodName = selectMethodName;
        }
        public  string SelectMethodName { get; protected set; }
        public string Value { get; protected set; }

        public new static SelectBy CssSelector(string cssSelectorToFind)
        {
            if (cssSelectorToFind == null)
            {
                throw new ArgumentNullException("cssSelectorToFind", "Cannot find elements when name CSS selector is null.");
            }
            SelectBy by = new SelectBy("SelectBy.CssSelector");
            by.FindElementMethod =
                (ISearchContext context) => ((IFindsByCssSelector)context).FindElementByCssSelector(cssSelectorToFind);
            by.FindElementsMethod =
                (ISearchContext context) => ((IFindsByCssSelector)context).FindElementsByCssSelector(cssSelectorToFind);
            by.Value = cssSelectorToFind;
            return by;
        }

        /// <summary>
        /// Returns implementation of <see cref="OpenQA.Selenium.By"/> which takes css selector. 
        /// </summary>
      
        public static SelectBy CssSelector(string cssSelectorToFind, string formatString)
        {
            if (cssSelectorToFind == null)
            {
                throw new ArgumentNullException("cssSelectorToFind", "Cannot find elements when name CSS selector is null.");
            }
            var fullSelector = string.IsNullOrWhiteSpace(formatString) ? cssSelectorToFind : string.Format(formatString, cssSelectorToFind);
            SelectBy by = new SelectBy("SelectBy.CssSelector");
            by.FormatString = formatString;
            by.FindElementMethod = (ISearchContext context) => ((IFindsByCssSelector)context).FindElementByCssSelector(fullSelector);
            by.FindElementsMethod = (ISearchContext context) => ((IFindsByCssSelector)context).FindElementsByCssSelector(fullSelector);
            by.Value = fullSelector;
            return by;
        }

        public string FormatString { get; set; }
        /// <summary>
        /// Returns implementation of <see cref="OpenQA.Selenium.By"/> which takes name of html tag. 
        /// </summary>
        public new static SelectBy TagName(string tagNameToFind)
        {
            if (tagNameToFind == null)
            {
                throw new ArgumentNullException("tagNameToFind", "Cannot find elements when name tag name is null.");
            }
            SelectBy by = new SelectBy("SelectBy.TagName");
            by.FindElementMethod = (ISearchContext context) => ((IFindsByTagName)context).FindElementByTagName(tagNameToFind);
            by.FindElementsMethod = (ISearchContext context) => ((IFindsByTagName)context).FindElementsByTagName(tagNameToFind);
            by.Value = tagNameToFind;
            return by;
        }

        public override string ToString()
        {
            return $"SelectBy.{SelectMethodName}";
        }
    }
}