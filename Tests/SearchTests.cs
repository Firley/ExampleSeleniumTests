using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ExampleSeleniumTests.Tests
{
    [TestFixture]
    class TestSearch : Test
    {

        [Test]
        public void TestSearchByCategory()
        {
            string category = "Elektronika";
            string item = "Komputer";
            Assert.IsTrue(homePage
                .TypeInSearch(item)
                .SelectCategory(category)
                .ClickSubmitSearch(item, category)
                .EnduseSearchResultsAreLoaded());
        }

        [Test]
        public void TestSearchWithoutSelectedCategory()
        {
            string category = "Wszystkie kategorie";
            string item = "Komputer";
            Assert.IsTrue(homePage
                .TypeInSearch(item)
                .SelectCategory(category)
                .ClickSubmitSearch(item, category)
                .EnduseSearchResultsAreLoaded());
        }


        [Test]
        public void CheckEmptySatetSearchResultWithoutSelectedCategory()
        {
            string category = "Wszystkie kategorie";
            string item = "ThisItemNotExistsTest";
            Assert.IsTrue(homePage
                .TypeInSearch(item)
                .SelectCategory(category)
                .ClickSubmitSearch(item, category)
                .EnsureEmptyStateIsDisplayed());
        }


        [Test]
        public void CheckEmptySatetSearchResultWithSelectedCategory()
        {
            string category = "Elektronika";
            string item = "ThisItemNotExistsTest";
            Assert.IsTrue(homePage
                .TypeInSearch(item)
                .SelectCategory(category)
                .ClickSubmitSearch(item, category)
                .EnsureEmptyStateIsDisplayed());
        }


        [Test]
        public void TestSearchUsingSuggestion()
        {
            string category = "Elektronika";
            string item = "Komputer";
            Assert.IsTrue(homePage
                .TypeInSearch(item)
                .EnduseSearchSuggestionsAreDisplayed()
                FindItemInCategory(item, category).EnduseSearchResultsAreLoaded());
        }


    }
}
