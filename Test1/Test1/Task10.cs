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
    public class Task10 : TestBase
    {
        [Test]

        public void Tests()
        {
            driver.Navigate().GoToUrl(baseURL);

            string firstProductName = driver.FindElement(By.CssSelector("#box-campaigns li:first-child .name")).Text;

            var regularPriceMainPage = driver.FindElement(By.CssSelector("#box-campaigns li:first-child s"));
            var campaignPriceMainPage = driver.FindElement(By.CssSelector("#box-campaigns li:first-child strong"));

            Assert.AreEqual(regularPriceMainPage.GetAttribute("className"), "regular-price");
            Assert.AreEqual(campaignPriceMainPage.GetAttribute("className"), "campaign-price");
           
            var throughMainPage = regularPriceMainPage.GetCssValue("text-decoration");
            Assert.IsTrue(throughMainPage.Contains("line-through"));

            var fontWeight = campaignPriceMainPage.GetCssValue("font-weight");
            Assert.Contains(fontWeight, new[] { "bold", "700", "800", "900" });

            int HeightRegularPriceMainPage = regularPriceMainPage.Size.Height;
            int WidthRegularPriceMainPage = regularPriceMainPage.Size.Width;
            int HeightCampaignPriceMainPage = campaignPriceMainPage.Size.Height;
            int WidthCampaignPriceMainPage = campaignPriceMainPage.Size.Width;
            bool compareHeight = HeightRegularPriceMainPage < HeightCampaignPriceMainPage;
            bool compareWidth = WidthRegularPriceMainPage < WidthCampaignPriceMainPage;
            Assert.AreEqual(compareHeight, true);
            Assert.AreEqual(compareWidth, true);

            var valueRegularPriceMainPage = driver.FindElement(By.CssSelector("div#box-campaigns li:first-child s")).Text;
            var valueCampaignPriceMainPage = driver.FindElement(By.CssSelector("div#box-campaigns li:first-child strong")).Text;

            var colorRegularPriceMainPage = regularPriceMainPage.GetCssValue("color");
            var splitColorRegularMainPage = colorRegularPriceMainPage.Replace("rgba", "").Replace("rgb", "").Replace("(", "").Replace(")", "").Replace(" ", "").Split(',');            
            Assert.AreEqual(splitColorRegularMainPage[0], splitColorRegularMainPage[1]);
            Assert.AreEqual(splitColorRegularMainPage[1], splitColorRegularMainPage[2]);

            var colorCampaignPriceMainPage = campaignPriceMainPage.GetCssValue("color");
            var splitColorCampaignMainPage = colorCampaignPriceMainPage.Replace("rgba", "").Replace("rgb", "").Replace("(", "").Replace(")", "").Replace(" ", "").Split(',');

            Assert.AreEqual("0", splitColorCampaignMainPage[1]);
            Assert.AreEqual("0", splitColorCampaignMainPage[2]);

            campaignPriceMainPage.Click();

            var secondProductName = driver.FindElement(By.CssSelector("h1.title")).Text;
            Assert.AreEqual(firstProductName, secondProductName);

            var regularPriceProductPage = driver.FindElement(By.CssSelector(".information .price-wrapper s"));
            var campaignPriceProductPage = driver.FindElement(By.CssSelector(".information .price-wrapper strong"));

            Assert.AreEqual(regularPriceProductPage.GetAttribute("class"), "regular-price");
            Assert.AreEqual(campaignPriceProductPage.GetAttribute("class"), "campaign-price");

            var throughProductPage = regularPriceProductPage.GetCssValue("text-decoration");
            Assert.IsTrue(throughProductPage.Contains("line-through"));

            var fontWeightProductPage = campaignPriceProductPage.GetCssValue("font-weight");
            Assert.Contains(fontWeightProductPage, new[] { "bold", "700", "800", "900" });

            int HeightRegularPriceProductPage = regularPriceProductPage.Size.Height;
            int WidthRegularPriceProductPage = regularPriceProductPage.Size.Width;
            int HeightCampaignPriceProductPage = campaignPriceProductPage.Size.Height;
            int WidthCampaignPriceProductPage = campaignPriceProductPage.Size.Width;
            bool compareHeightProductPage = HeightRegularPriceProductPage < HeightCampaignPriceProductPage;
            bool compareWidthProductPage = WidthRegularPriceMainPage < WidthCampaignPriceProductPage;
            Assert.AreEqual(compareHeightProductPage, true);
            Assert.AreEqual(compareWidthProductPage, true);
 
            var valueRegularPriceProductPage = regularPriceProductPage.Text;
            Assert.AreEqual(valueRegularPriceMainPage, valueRegularPriceProductPage);
            var valueCampaignPriceProductPage = campaignPriceProductPage.Text;
            Assert.AreEqual(valueCampaignPriceMainPage, valueCampaignPriceProductPage);

            var colorRegularPriceProductPage = regularPriceProductPage.GetCssValue("color");
            var splitColorPriceProductPage = colorRegularPriceProductPage.Replace("rgba", "").Replace("rgb", "").Replace("(", "").Replace(")", "").Replace(" ", "").Split(',');
            Assert.AreEqual(splitColorPriceProductPage[0], splitColorPriceProductPage[1]);
            Assert.AreEqual(splitColorPriceProductPage[1], splitColorPriceProductPage[2]);

            var colorCampaignPriceProductPage = campaignPriceProductPage.GetCssValue("color");
            var splitColorCampaignPriceProductPage = colorCampaignPriceProductPage.Replace("rgba", "").Replace("rgb", "").Replace("(", "").Replace(")", "").Replace(" ", "").Split(',');
            Assert.AreEqual("0", splitColorCampaignPriceProductPage[1]);
            Assert.AreEqual("0", splitColorCampaignPriceProductPage[2]);
        }
    }
}
