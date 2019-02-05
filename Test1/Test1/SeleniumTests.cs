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

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void StartBrowsertest()
        {
            driver.Url = "https://yandex.ru/";
        }

        [TearDown]
        public void TeardownTest()
        {
            driver.Quit();
        }
    }
}
