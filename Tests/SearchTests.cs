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
           Assert.IsTrue(homePage.FindItemInCategory(item, category).EnduseSearchResultsAreLoaded());
        }

        [Test]
        public void TestSearchWithoutSelectedCategory()
        {
            string category = "Wszystkie kategorie";
            string item = "Komputer";
            Assert.IsTrue(homePage.FindItemInCategory(item, category).EnduseSearchResultsAreLoaded());
        }


        [Test]
        public void CheckEmptySatetSearchResultWithoutSelectedCategory()
        {
            string category = "Wszystkie kategorie";
            string item = "ThisItemNotExistsTest";
            Assert.IsTrue(homePage.FindItemInCategory(item, category).EnsureEmptyStateIsDisplayed());
        }


        [Test]
        public void CheckEmptySatetSearchResultWithSelectedCategory()
        {
            string category = "Elektronika";
            string item = "ThisItemNotExistsTest";
            Assert.IsTrue(homePage.FindItemInCategory(item, category).EnsureEmptyStateIsDisplayed());
        }

    }
}
