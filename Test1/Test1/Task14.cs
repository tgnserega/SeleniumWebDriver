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
    public class Task14 : TestBase
    {
        [Test]

        public void WindowsClose()
        {
            driver.Navigate().GoToUrl(baseURL + "admin/");

            Login();

            driver.FindElement(By.XPath("//*[@id='app-']//span[contains(.,'Countries')]")).Click();

            driver.FindElement(By.XPath("//a[contains(.,'Add New Country')]")).Click();

            int countLink = driver.FindElements(By.CssSelector("form a[target=_blank]")).Count;

            string mainWindow = driver.CurrentWindowHandle;

            ICollection<string> existingWindows = driver.WindowHandles;

            for (int i = 0; i < countLink; i++)
            {
                var externalPages = driver.FindElements(By.CssSelector("form  a[target=_blank]"));

                externalPages[i].Click();

                string newWindow = wait.Until(anyWindowOtherThan(existingWindows));

                driver.SwitchTo().Window(newWindow);

                driver.Close();

                driver.SwitchTo().Window(mainWindow);
            }
        }
    }
}