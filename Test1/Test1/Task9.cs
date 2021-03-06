﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWebDriver

{
    [TestFixture]
    public class Task9 : TestBase
    {      
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

        [Test]
        public void CheckSortZoneContriesTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/admin/?app=countries&doc=countries");
            Login();

            IList<IWebElement> listCountries = driver.FindElements(By.CssSelector(".dataTable tr.row"));
            List<String> ListNotNull = new List<String>();

            foreach (var country in listCountries)
            {
                var x = country.FindElement(By.CssSelector("td:nth-child(6)"));

                if (x.GetAttribute("textContent") != "0")
                {
                    var y = country.FindElement(By.CssSelector("td:nth-child(5) > a"));
                    var countryURL = y.GetAttribute("href");
                    ListNotNull.Add(countryURL);
                }
            }


            foreach (var zoneURL in ListNotNull)
            {
                driver.Navigate().GoToUrl(zoneURL);

                IList<IWebElement> listZoneCountries = driver.FindElements(By.CssSelector("#table-zones  td:nth-child(3)"));
                List<String> ListZoneCountriesNames = new List<String>();

                foreach (var z_country in listZoneCountries)
                {
                    var countryName = z_country.GetAttribute("textContent");
                    ListZoneCountriesNames.Add(countryName);
                }

                List<String> SortListZoneCountriesNames = ListZoneCountriesNames;
                SortListZoneCountriesNames.Sort();
                Assert.AreEqual(ListZoneCountriesNames, SortListZoneCountriesNames);
            }
        }

        [Test]
        public void CheckSortGeoZoneContriesTest()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=geo_zones&doc=edit_geo_zone&page=1&geo_zone_id=1");
            Login();

            IList<IWebElement> listCountries = driver.FindElements(By.CssSelector("#table-zones [name *='zone_code'] option"));
            List<String> ListCountriesNames = new List<String>();

            foreach (var country in listCountries)
            {
                var a = country.GetAttribute("selected");
                if (a == "true")
                {
                    ListCountriesNames.Add(country.GetAttribute("text"));
                }
            }

            List<String> SortListCountriesNames = ListCountriesNames;
            SortListCountriesNames.Sort();
            Assert.AreEqual(ListCountriesNames, SortListCountriesNames);
        }
    }
}
