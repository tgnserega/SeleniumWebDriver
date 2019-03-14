using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace csharp_example
{
    public class Application
    {
        private IWebDriver driver;

        private RegistrationPage registrationPage;
        private AdminPanelLoginPage adminPanelLoginPage;
        private CustomerListPage customerListPage;

        private MainPage mainPage;
        private ProductPage productPage;
        private RecycleBinPage recycleBinPage;

        public Application()
        {
            driver = new ChromeDriver();
            registrationPage = new RegistrationPage(driver);
            adminPanelLoginPage = new AdminPanelLoginPage(driver);
            customerListPage = new CustomerListPage(driver);

            mainPage = new MainPage(driver);
            productPage = new ProductPage(driver);
            recycleBinPage = new RecycleBinPage(driver);
        }

        public void Quit()
        {
            driver.Quit();
        }

        internal void RegisterNewCustomer(Customer customer)
        {
            registrationPage.Open();
            registrationPage.FirstnameInput.SendKeys(customer.Firstname);
            registrationPage.LastnameInput.SendKeys(customer.Lastname);
            registrationPage.Address1Input.SendKeys(customer.Address);
            registrationPage.PostcodeInput.SendKeys(customer.Postcode);
            registrationPage.CityInput.SendKeys(customer.City);
            registrationPage.SelectCountry(customer.Country);
            registrationPage.SelectZone(customer.Zone);
            registrationPage.EmailInput.SendKeys(customer.Email);
            registrationPage.PhoneInput.SendKeys(customer.Phone);
            registrationPage.PasswordInput.SendKeys(customer.Password);
            registrationPage.ConfirmedPasswordInput.SendKeys(customer.Password);
            registrationPage.CreateAccountButton.Click();
        }

        internal ISet<string> GetCustomerIds()
        {
            if (adminPanelLoginPage.Open().IsOnThisPage())
            {
                adminPanelLoginPage.EnterUsername("admin").EnterPassword("admin").SubmitLogin();
            }
            return customerListPage.Open().GetCustomerIds();
        }

        internal void AddToRecycleBin(int productCount)
        {
            if (!mainPage.ExistOnPage())
                mainPage.Open();

            var products = mainPage.PopularProducts();

            Assert.IsTrue(products.Count > 0);

            for (int i = 0; i < productCount; i++)
            {
                mainPage.Open().MostPopularProduct(i < products.Count ? products[i] : products[products.Count - 1]).Click();
                productPage.AddToRecycleBin(1);
            }
        }

        internal void RemoveFromRecycleBin(int productCount)
        {
            if (!recycleBinPage.ExistOnPage())
                recycleBinPage.Open();
            for (int i = 0; i < productCount; i++)
                recycleBinPage.RemoveFromRecycleBin();
        }
    }
}