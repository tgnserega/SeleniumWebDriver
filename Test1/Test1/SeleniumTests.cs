using System;
using System.Collections.Generic;
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

            int stickerExist1 = driver.FindElements(By.CssSelector("#box-most-popular [title='Blue Duck'] .sticker")).Count;
            Assert.AreEqual(1, stickerExist1);

            int stickerExist2 = driver.FindElements(By.CssSelector("#box-most-popular [title='Purple Duck'] .sticker")).Count;
            Assert.AreEqual(1, stickerExist2);

            int stickerExist3 = driver.FindElements(By.CssSelector("#box-most-popular [title='Yellow Duck'] .sticker")).Count;
            Assert.AreEqual(1, stickerExist3);

            int stickerExist4 = driver.FindElements(By.CssSelector("#box-most-popular [title='Green Duck'] .sticker")).Count;
            Assert.AreEqual(1, stickerExist4);

            int stickerExist5 = driver.FindElements(By.CssSelector("#box-most-popular [title='Red Duck'] .sticker")).Count;
            Assert.AreEqual(1, stickerExist5);

            int stickerExist6 = driver.FindElements(By.CssSelector("#box-campaigns [title='Yellow Duck'] .sticker")).Count;
            Assert.AreEqual(1, stickerExist6);

            int stickerExist7 = driver.FindElements(By.CssSelector("#box-latest-products [title='Blue Duck'] .sticker")).Count;
            Assert.AreEqual(1, stickerExist7);

            int stickerExist8 = driver.FindElements(By.CssSelector("#box-latest-products [title='Purple Duck'] .sticker")).Count;
            Assert.AreEqual(1, stickerExist8);

            int stickerExist9 = driver.FindElements(By.CssSelector("#box-latest-products [title='Yellow Duck'] .sticker")).Count;
            Assert.AreEqual(1, stickerExist9);

            int stickerExist10 = driver.FindElements(By.CssSelector("#box-latest-products [title='Green Duck'] .sticker")).Count;
            Assert.AreEqual(1, stickerExist10);

            int stickerExist11 = driver.FindElements(By.CssSelector("#box-latest-products [title='Red Duck'] .sticker")).Count;
            Assert.AreEqual(1, stickerExist11);
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
