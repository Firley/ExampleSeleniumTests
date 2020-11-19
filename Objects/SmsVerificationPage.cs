using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleSeleniumTests.Objects
{
    class SmsVerificationPage : Page
    {
        public SmsVerificationPage(IWebDriver driver)
        {
            Driver = driver;
        }


        private IWebElement SmsCodeInput => Driver.FindElement(By.Id("smsCode"));
        private IWebElement LoginButton => Driver.FindElement(By.Id("smsLoginButton"));
        private IWebElement CancelLink => Driver.FindElement(By.Id("smsLoginCancelLink"));
        private IWebElement TrustedDeviceLabel => Driver.FindElement(By.CssSelector("form label.m-label"));
        protected override string PageTitle => "Allegro Logowanie - Zaloguj się na swoje konto w Moje Allegro";
        protected override string PageUrl => @"https://allegro.pl/login/sms-verification";


        public SmsVerificationPage InsertSmsCode(string code)
        {
            SmsCodeInput.SendKeys(code);
            return this;
        }

        public HomePage ClickLoginLink()
        {
            LoginButton.Click();
            return new HomePage(Driver);
        }

        public LoginPage ClickCancelLink()
        {
            CancelLink.Click();
            return new LoginPage(Driver);
        }

        public bool EnsureSmsVerificationPageLoaded()
        {
            Console.WriteLine(Driver.Url);
            Console.WriteLine(Driver.Title);

            bool headerDisplayed = Driver.FindElement(By.TagName("h2")).Text == "Zaloguj się";
            if (!SmsCodeInput.Displayed)
            {
                throw new Exception($"SmsCodeInput is not dispalyed");
            }
            if (!LoginButton.Displayed)
            {
                throw new Exception($"Login input is not displayed");
            }
            if (!CancelLink.Displayed)
            {
                throw new Exception($"CancelLink input is not displayed ");
            }
            if (!TrustedDeviceLabel.Displayed)
            {
                throw new Exception($"TrustedDeviceLabel is not displayed ");
            }
            EnsurePageLoaded();
            return true;
        }

    }
}
