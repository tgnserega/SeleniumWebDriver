using System;
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
            baseURL = "http://localhost/litecart/admin/";
        }

        [Test]
        public void LoginTest()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.Name("username")).Clear();
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
        }

        [TearDown]
        public void TeardownTest()
        {
            driver.Quit();
        }
    }
}
