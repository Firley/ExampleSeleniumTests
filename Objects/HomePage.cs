using ExampleSeleniumTests.Objects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleSeleniumTests
{
     class HomePage : Page
    {
       public HomePage(IWebDriver driver)
        {
            Driver = driver;
        }
        
        protected override string PageUrl => "https://allegro.pl/";
        protected override string PageTitle => "Allegro - atrakcyjne ceny";
        protected IWebElement Search => Driver.FindElement(By.CssSelector("input[type='search']"));
        protected IWebElement SearchCategory => Driver.FindElement(By.CssSelector("select[data-role='filters-dropdown-toggle']"));
        protected IWebElement MultiSearchButton => Driver.FindElement(By.CssSelector("button[data-role='multisearch-button']"));
        protected IWebElement SearchSubmitButton => Driver.FindElement(By.CssSelector("button[type='submit']"));
        protected IWebElement LoginButton => Driver.FindElement(By.CssSelector("div[data-box-name='login button']"));
        protected IWebElement UserDropdownMenuButton => Driver.FindElement(By.CssSelector("div[data-dropdown-id='user_dropdown']"));
        protected IWebElement AccountCardInDropdownMenu => Driver.FindElement(By.Id("user-menu-heading2"));
        protected string DisplayedLoginInAccountCard => Driver.FindElement(By.Id("h6[data-role='user-card-login']")).Text;

        public SearchPage FindItemInCategory(string searchedItem, string category)
        {
            this.Search.SendKeys(searchedItem);
            SelectElement categories = new SelectElement(SearchCategory);

            categories.SelectByText(category);

            SearchSubmitButton.Click();

            return new SearchPage(Driver, searchedItem, category);
        }

        public virtual Page OpenMultiSearch()
        {
            MultiSearchButton.Click();
            return this;
        }


        public LoginPage ClickLoginButton()
        {
            LoginButton.Click();
            return new LoginPage(Driver);
        }

        public HomePage OpenUserMenu()
        {
            UserDropdownMenuButton.Click();
            return this;
        }

        public HomePage OpenAccountCard()
        {
            AccountCardInDropdownMenu.Click();
            return this;
        }

        public bool EnsureUserLogged(string login)
        {
            base.EnsurePageLoaded();
            return DisplayedLoginInAccountCard == login;
        }
    }
}
