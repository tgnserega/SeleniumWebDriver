using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using OpenQA.Selenium.Support.PageObjects;

namespace csharp_example
{
    class RecycleBinPage : Page
    {
        public RecycleBinPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        internal RecycleBinPage Open()
        {
            driver.Url = "http://localhost/litecart/checkout";
            return this;
        }

        [FindsBy(How = How.CssSelector, Using = "table[class^=dataTable] tr")]
        internal IList<IWebElement> CartTableRows;

        [FindsBy(How = How.CssSelector, Using = "ul.shortcuts li")]
        internal IList<IWebElement> Shortcuts;

        internal IWebElement RecycleBinTable()
        {
            return driver.FindElement(By.CssSelector("table[class^=dataTable]"));
        }

        internal IWebElement RemoveButton()
        {
            return driver.FindElement(By.Name("remove_cart_item"));
        }

        internal bool ExistOnPage()
        {
            return ExpectedConditions.TitleContains("Checkout |").Invoke(driver);
        }

        internal RecycleBinPage RemoveFromRecycleBin()
        {
            if (Shortcuts.Count > 0)
                Shortcuts[0].Click();
            var table = RecycleBinTable();
            RemoveButton().Click();
            wait.Until(ExpectedConditions.StalenessOf(table));
            return this;
        }
    }
}
