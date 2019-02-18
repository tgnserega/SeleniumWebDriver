using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests

{
    [TestFixture]
    public class Test1
    {
        private IWebDriver driver;
        private string baseURL;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost/litecart/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }

        [Test]
        public void LoginTest()
        {
            driver.Navigate().GoToUrl(baseURL + "admin/");
            Login();
        }

        [Test]
        public void CheckStickersTest()
        {
            driver.Navigate().GoToUrl(baseURL);

            IList<IWebElement> products = driver.FindElements(By.CssSelector(".product.column.shadow"));

            foreach (IWebElement product in products)
            {
                int stickerCount = product.FindElements(By.CssSelector(".sticker")).Count;
                Assert.AreEqual(1, stickerCount, "error " + product.FindElement(By.CssSelector(".name")).Text);
            }
        }

        [Test]
        public void CheckMenuTest()
        {
            driver.Navigate().GoToUrl(baseURL + "admin/");
            Login();

            int menuCount = driver.FindElements(By.Id("app-")).Count;

            for (int i = 0; i < menuCount; i++)
            {
                IList<IWebElement> elementMenu = driver.FindElements(By.CssSelector("#app-"));

                elementMenu[i].Click();

                Assert.True(IsElementPresent(driver, By.CssSelector("h1")));

                if (driver.FindElements(By.CssSelector("#app-.selected li")).Count > 0)
                {
                    int subMenuCount = driver.FindElements(By.CssSelector("#app-.selected li")).Count;

                    for (int k = 0; k < subMenuCount; k++)

                    {
                        IList<IWebElement> elementSubMenu = driver.FindElements(By.CssSelector("#app-.selected li"));
                        elementSubMenu[k].Click();

                        Assert.True(IsElementPresent(driver, By.CssSelector("h1")));              

                    }
                }
            }

        }

        [Test]
        public void CheckSortContriesTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/admin/?app=countries&doc=countries");
            Login();

            IList<IWebElement> listCountries = driver.FindElements(By.CssSelector("tr.row td a:not([title=Edit])"));
            List<String> ListCountriesNames = new List<String>();

            foreach (var country in listCountries)
            {
                var countryName = country.GetAttribute("textContent");
                ListCountriesNames.Add(countryName);
            }

            List<String> SortListCountriesNames = ListCountriesNames;
            SortListCountriesNames.Sort();
            Assert.AreEqual(ListCountriesNames, SortListCountriesNames);
        }

        public void Login()
        {
            driver.FindElement(By.Name("username")).Clear();
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
        }

        public bool IsElementPresent(IWebDriver driver, By locator)
        {
            try
            {
                driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //bool AreElementsPresent(IWebDriver driver, By locator)
        //{
        //    return driver.FindElements(locator).Count > 0;
        //}

        [TearDown]
        public void TeardownTest()
        {
            driver.Quit();
        }
    }
}
