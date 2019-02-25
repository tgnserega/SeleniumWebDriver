using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWebDriver
{
    public class Task12 : TestBase
    {
        [Test]

        public void AddProductTest()
        {
            driver.Navigate().GoToUrl(baseURL + "admin/");

            Login();

            driver.FindElement(By.XPath("//ul[@id='box-apps-menu']//span[contains(.,'Catalog')]")).Click();

            int countOldProductList = driver.FindElements(By.XPath("//*[@id='content']/form/table/tbody//td[3]/a[contains(.,'NewProduct1')]")).Count();

            IList<IWebElement> oldProductList = driver.FindElements(By.CssSelector("#content table tbody tr.row"));

            driver.FindElement(By.XPath("//a[@class='button'][contains(.,'Add New Product')]")).Click();

            driver.FindElement(By.XPath("//td[contains(.,'Status')]/label[contains(.,'Enabled')]")).Click();

            driver.FindElement(By.XPath("//div[@id='tab-general']//td[contains(.,'Name')]//input")).SendKeys("NewProduct1");

            driver.FindElement(By.XPath("//div[@id='tab-general']//td[contains(.,'Code')]//input")).SendKeys("NewProduct1Code");

            driver.FindElement(By.XPath("//div[@id='tab-general']//td[contains(.,'Male')]//input[@value='1-2']")).Click();

            driver.FindElement(By.XPath("//div[@id='tab-general']//td[contains(.,'Quantity')]//input")).SendKeys("1");

            var pathToPicture = Path.Combine(TestContext.CurrentContext.WorkDirectory + @"\NewProduct1.png");
            driver.FindElement(By.XPath("//div[@id='tab-general']//td[contains(.,'Upload Images')]//input")).SendKeys(pathToPicture);

            driver.FindElement(By.XPath("//div[@id='tab-general']//td[contains(.,'Date Valid From')]//input")).SendKeys(Keys.Home + "12.12.2018");

            driver.FindElement(By.XPath("//div[@id='tab-general']//td[contains(.,'Date Valid To')]//input")).SendKeys(Keys.Home + "11.11.2020");

            driver.FindElement(By.XPath("//ul[@class='index']//a[contains(.,'Information')]")).Click();

            IList<IWebElement> links = (IList<IWebElement>)((IJavaScriptExecutor)driver)
           .ExecuteScript(@"arguments[0].value = '1'; arguments[0].dispatchEvent(new Event('change')); ", driver.FindElement(By.Name("manufacturer_id")));

            driver.FindElement(By.Name("keywords")).SendKeys("NewProduct1");

            driver.FindElement(By.Name("short_description[en]")).SendKeys("Utka");

            driver.FindElement(By.Name("head_title[en]")).SendKeys("NewProduct1Head");

            driver.FindElement(By.Name("meta_description[en]")).SendKeys("NewProductMeta");

            driver.FindElement(By.XPath("//ul[@class='index']//a[contains(.,'Prices')]")).Click();

            driver.FindElement(By.Name("purchase_price")).SendKeys(Keys.Home + "1");

            var purchasePriceCode = driver.FindElement(By.Name("purchase_price_currency_code"));

            var selectPurchasePriceCode = new SelectElement(purchasePriceCode);

            selectPurchasePriceCode.SelectByValue("USD");

            driver.FindElement(By.Name("prices[USD]")).SendKeys("7");

            driver.FindElement(By.Name("prices[EUR]")).SendKeys("8");

            driver.FindElement(By.Name("save")).Click();

            int countNewProductList =  driver.FindElements(By.XPath("//*[@id='content']/form/table/tbody//td[3]/a[contains(.,'NewProduct1')]")).Count();

            Assert.AreEqual(countNewProductList, countOldProductList + 1);
        }
    }
}