using ExampleSeleniumTests.Objects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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

        #region Search input elements
        protected IWebElement Search => Driver.FindElement(By.CssSelector("input[type='search']"));
        protected IWebElement SearchCategoryList => Driver.FindElement(By.CssSelector("select[data-role='filters-dropdown-toggle']"));
        protected IWebElement MultiSearchButton => Driver.FindElement(By.CssSelector("button[data-role='multisearch-button']"));
        protected IWebElement SearchSubmitButton => Driver.FindElement(By.CssSelector("button[type='submit']"));
        protected IWebElement LoginButton => Driver.FindElement(By.CssSelector("div[data-box-name='login button']"));
        

        #region Search Suggestions and hints elements
        private IWebElement SearchSuggestionsListbox => Driver.FindElement(By.Id("suggestions-listbox"));
        protected IReadOnlyCollection<IWebElement> SearchSuggestionsList => SearchSuggestionsListbox.FindElements(By.TagName("a"));
        protected IReadOnlyCollection<IWebElement> SuggestionsLabels => SearchSuggestionsListbox.FindElements(By.CssSelector("div[data-role='label']"));
        protected IWebElement SearchInDescriptionCheckBox => SearchSuggestionsListbox.FindElement(By.TagName("label"));


        public bool LabelsAreDisplayed
        {
            get

            {
                string selectedCategoryInSearch = new SelectElement(SearchCategoryList).SelectedOption.Text;

                if (SuggestionsLabels.First().Text != "PODOBNE WYSZUKIWANIA" || !SuggestionsLabels.First().Displayed)
                {
                    return false;
                }

                if (selectedCategoryInSearch == "Wszystkie kategorie")
                {
                    if (SuggestionsLabels.ElementAt(1).Text != "UŻYTKOWNICY" || !SuggestionsLabels.ElementAt(1).Displayed)
                    {
                        throw new Exception($"First label is not dispalyed correctly. Label text is: \"{SuggestionsLabels.ElementAt(1).Text}");
                    }
                }

                return true;
            }

        }
        #endregion
        #endregion


        #region User profile dropdown menu elements
        protected IWebElement UserDropdownMenuButton => Driver.FindElement(By.CssSelector("div[data-dropdown-id='user_dropdown']"));
        protected IWebElement AccountCardInDropdownMenu => Driver.FindElement(By.Id("user-menu-heading2"));
        protected string DisplayedLoginInAccountCard => Driver.FindElement(By.Id("h6[data-role='user-card-login']")).Text;
        #endregion

        public HomePage TypeInSearch(string searchedItem)
        {
            this.Search.SendKeys(searchedItem);
            return new HomePage(Driver);
        }

        public HomePage SelectCategory(string category)
        {
            SelectElement categories = new SelectElement(SearchCategoryList);
            categories.SelectByText(category);
            return new HomePage(Driver);
        }

        internal bool EnduseSearchSuggestionsAreDisplayed()
        {

            if (!LabelsAreDisplayed)
            {
                if (SuggestionsLabels.Count >= 2)
                {
                    throw new Exception($"First label is not dispalyed correctly. Label text is: \"{SuggestionsLabels.First().Text}\". Second label is not dispalyed correctly. Label text is: \"{SuggestionsLabels.First().Text}\"");
                }
                throw new Exception($"Label is not dispalyed correctly. Label text is: \"{SuggestionsLabels.First().Text}\"");
            }
            return true;
        }

        public virtual SearchPage ClickSubmitSearch(string searchedItem, string category)
        {
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


        public bool SelectSuggestions()
        {
            return true;
        }
    }
}
