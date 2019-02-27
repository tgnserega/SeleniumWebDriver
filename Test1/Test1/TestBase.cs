using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWebDriver

{
    [TestFixture]
    public class TestBase
    {
        public IWebDriver driver;
        public string baseURL;
        public WebDriverWait wait;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            //driver = new FirefoxDriver();
            //driver = new InternetExplorerDriver();
            baseURL = "http://localhost/litecart/";
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));   
        }

        public Func<IWebDriver, string> anyWindowOtherThan(ICollection<string> oldWindows)
        {
            return p =>
            {
                List<string> handles = new List<string>(p.WindowHandles);

                foreach (string q in oldWindows)

                    handles.Remove(q);

                return handles.Count > 0 ? handles[0] : null;
            };
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

        [TearDown]
        public void TeardownTest()
        {
            driver.Quit();
        }
    }
}
