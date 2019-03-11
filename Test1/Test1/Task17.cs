using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWebDriver
{
    public class Task17 : TestBase
    {
        [Test]

        public void LogsTest()
        {
            driver.Navigate().GoToUrl(baseURL + "admin/?app=catalog&doc=catalog&category_id=1/");

            Login();

            IList<IWebElement> listProduct = driver.FindElements(By.XPath(".//tr[.//a[contains(@href, 'product_id')]]"));

            List<string> listProductLink = new List<string>();

            for (int i = 0; i < listProduct.Count; i++)

                listProductLink.Add(listProduct[i].FindElement(By.CssSelector("a[href*=product_id]")).GetAttribute("href"));

            for (int i = 0; i < listProductLink.Count; i++)

            {
                driver.Url = listProductLink[i];

                foreach (LogEntry l in driver.Manage().Logs.GetLog("browser"))
                {
                    Console.WriteLine(l);
                }
            }
        }
    }
}