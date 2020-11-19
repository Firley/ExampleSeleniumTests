using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ExampleSeleniumTests
{
    class SearchPage : HomePage
    {

        public SearchPage(IWebDriver driver, string searchedItem, string category) : base(driver)
        {
            Driver = driver;
            SearchedItem = searchedItem;
            Category = category;
        }

        public string SearchedItem { get; protected set; }
        public string Category { get; protected set; }
        public bool isAllCategorySelected => Category == "Wszystkie kategorie";
        public string ResulsHeader => Driver.FindElement(By.TagName("h1")).Text;
        private IReadOnlyList<IWebElement> foundItems => Driver.FindElements(By.CssSelector("article[data-role]"));
        protected override string PageUrl => Category == "Wszystkie kategorie" ? $"https://allegro.pl/listing?string={SearchedItem}" : $"https://allegro.pl/kategoria/{Category.ToLower()}?string={SearchedItem}";
        protected override string PageTitle => Category == "Wszystkie kategorie" ? $"{SearchedItem} - Niska cena na Allegro.pl" : $"{SearchedItem} - {Category} - Allegro.pl";
        private string Breadcrumb
        {
            get
            {
                if (Driver.FindElements(By.CssSelector("div[data-box-name='Breadcrumb']")).Count != 0)
                {
                    return Driver.FindElement(By.CssSelector("div[data-box-name='Breadcrumb']")).Text;
                }
                return "";
            }
        }

        private IReadOnlyCollection<IWebElement> EmptyStateTextLines => Driver.FindElements(By.CssSelector("div.opbox-listing p"));

        public bool EnduseSearchResultsAreLoaded()
        {
            base.EnsurePageLoaded();
            string textInSearch = Search.GetAttribute("value");
            string selectedCategoryInSearch = new SelectElement(SearchCategory).SelectedOption.Text;

            if (textInSearch != SearchedItem || selectedCategoryInSearch != Category)
            {
                throw new Exception($"Failed to load data in search input. Searched Item = '{SearchedItem}' but was '{textInSearch}'" +
                    $"Selected category = \r\n {Category} but was {selectedCategoryInSearch}");
            }
            if (!ResulsHeader.Contains(SearchedItem))
            {
                throw new Exception($"Results table header should includes phrase = '{SearchedItem} but was = '{ResulsHeader}'");
            }
            if (isAllCategorySelected)
            {
                if (!String.IsNullOrEmpty(Breadcrumb))
                {
                    throw new Exception($"Breadcrum navigation was displayed. It shoudn't be");
                }
            }
            else
            {
                if (!Breadcrumb.StartsWith("Allegro") || !Breadcrumb.Contains(Category))
                {
                    throw new Exception($"Wrong breadcrum was displayed.");
                }
            }
            return foundItems.Count > 0 ? true : false;
        }



        public bool EnsureEmptyStateIsDisplayed()
        {
            string correctSecondLineInEmptyState = "Spróbuj wpisać to inaczej i sprawdź wyniki wyszukiwania.";
            if (isAllCategorySelected)
            {
                return EmptyStateTextLines.First().Text == ($"Czy na pewno szukasz „{SearchedItem}”?") && EmptyStateTextLines.Last().Text == correctSecondLineInEmptyState;
            }  
            return EmptyStateTextLines.First().Text == $"Teraz nie możemy znaleźć „{SearchedItem}” w kategorii {Category}." && EmptyStateTextLines.Last().Text == correctSecondLineInEmptyState;
        }

    }
}