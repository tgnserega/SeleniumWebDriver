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
    public class Task13 : TestBase
    {
        [Test]

        public void RecycleBin()
        {
            driver.Navigate().GoToUrl(baseURL);

            for (int i = 1; i <= 3; i++)
            {
                driver.FindElement(By.CssSelector("ul.listing-wrapper li.product")).Click(); //открыть первый товар

                var counterInCart = driver.FindElement(By.CssSelector("#cart span.quantity")).Text; // количество в корзине

                driver.FindElement(By.Name("add_cart_product")).Click(); //добавить товар в корзину

                wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.CssSelector("#cart span.quantity"), counterInCart)); // проверяем что количество в корзине изменилось

                driver.FindElement(By.Id("logotype-wrapper")).Click(); // домашняя страница
            }

            driver.FindElement(By.CssSelector("#cart a.link")).Click();

            while (driver.FindElements(By.Name("remove_cart_item")).Count > 0)
            {
                var removeButton = driver.FindElement(By.Name("remove_cart_item"));

                removeButton.Click();

                wait.Until(ExpectedConditions.StalenessOf(removeButton));
            }

            driver.FindElement(By.CssSelector("#checkout-cart-wrapper p em"));
        }
    }
}