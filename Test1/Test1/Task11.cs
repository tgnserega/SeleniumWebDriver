using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWebDriver
{
    public class Task11 : TestBase
    {
        [Test]

        public void RegistrationTest()
        {

            driver.Navigate().GoToUrl(baseURL);

            driver.FindElement(By.CssSelector("#box-account-login a")).Click();

            driver.FindElement(By.CssSelector("[name='firstname']")).SendKeys("Ivanov");

            driver.FindElement(By.CssSelector("[name='lastname']")).SendKeys("Ivan");

            driver.FindElement(By.CssSelector("[name='address1']")).SendKeys("Russia");

            driver.FindElement(By.CssSelector("[name='postcode']")).SendKeys("35005");

            driver.FindElement(By.CssSelector("[name='city']")).SendKeys("Krasnodar");

            IList<IWebElement> links = (IList<IWebElement>)((IJavaScriptExecutor)driver)

            .ExecuteScript( @"arguments[0].value = 'US'; arguments[0].dispatchEvent(new Event('change')); ", driver.FindElement(By.CssSelector("[name='country_code']")));

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var uniqueEmail = new String(stringChars) + "@bk.ru";

            driver.FindElement(By.CssSelector("[name='email']")).SendKeys(uniqueEmail);

            driver.FindElement(By.CssSelector("[name='phone']")).SendKeys("8800500");

            driver.FindElement(By.CssSelector("[name='password']")).SendKeys("Password1");

            driver.FindElement(By.CssSelector("[name='confirmed_password']")).SendKeys("Password1");

            driver.FindElement(By.CssSelector("[name='create_account']")).Click();

            driver.FindElement(By.XPath("//div[@id='box-account']//a[contains(.,'Logout')]")).Click();

            driver.FindElement(By.CssSelector("[name='email']")).SendKeys(uniqueEmail);

            driver.FindElement(By.CssSelector("[name='password']")).SendKeys("Password1");

            driver.FindElement(By.CssSelector("form[name=login_form] button[name=login]")).Click();

            driver.FindElement(By.XPath("//div[@id='box-account']//a[contains(.,'Logout')]")).Click();
        }
    }
}