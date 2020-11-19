using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace ExampleSeleniumTests
{

    [TestFixture]
   abstract class Test
    {
       protected IWebDriver Driver { get;  set; }

       protected HomePage homePage;


        [SetUp]
        public void StartBrowser()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Driver.Manage().Window.Maximize();

            homePage = new HomePage(Driver);
            homePage.OpenUrl();
        }

        [TearDown]
        public void CloseBrowser()
        {
            Driver.Quit();
        }

    }
}