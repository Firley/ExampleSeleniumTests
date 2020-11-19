using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleSeleniumTests
{
   abstract class Page
    {
        protected IWebDriver Driver;
        protected virtual string PageUrl { get; }
        protected virtual string PageTitle { get; }

        public By CoockiesAlert => By.CssSelector("div[aria-labelledby='dialog-title']");
        private IWebElement CoockiesAgreementButton => Driver.FindElement(By.CssSelector("div[slot] button[data-role='accept-consent']"));



        public virtual void OpenUrl()
        {
            Driver.Navigate().GoToUrl(PageUrl);
            EnsurePageLoaded();
            EnsureCoockiesConstantsAreAccepted();
        }


        public void EnsurePageLoaded(bool onlyCheckUrlStartsWithExpectedText = true)
        {
            bool urlIsCorrect;
            if (onlyCheckUrlStartsWithExpectedText)
            {
                urlIsCorrect = Driver.Url.StartsWith(PageUrl);
            }
            else
            {
                urlIsCorrect = Driver.Url == PageUrl;
            }

            bool pageHasLoaded = urlIsCorrect && (Driver.Title == PageTitle);
            if (!pageHasLoaded)
            {
                throw new Exception($"Failed to load page. Page URL = '{Driver.Url}' Page Source: \r\n {Driver.PageSource}");
            }
        }

        protected virtual void EnsureCoockiesConstantsAreAccepted()
        {
            if (Driver.FindElements(CoockiesAlert).Count > 0)
            {
                CoockiesAgreementButton.Click();
            }
        }





    }
}
