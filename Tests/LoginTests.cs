using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ExampleSeleniumTests.Tests
{
    [TestFixture]
    class LoginTests : Test
    {
        [Test]
        public void TestPossibilityLoginToAllegro()
        {
            string login = "Login";
            string password = "Password";
            string code = "Code";
            Assert.IsTrue(homePage.ClickLoginButton()
                .InsertLogin(login)
                .InsertPassword(password)
                .ClickSignInButton()
                .InsertSmsCode(code)
                .ClickLoginLink()
                .OpenUserMenu()
                .OpenAccountCard()
                .EnsureUserLogged(login));
        }


        [Test]
        public void TestSecondVerifcationPageVisibility()
        {
            string login = "Login";
            string password = "Password";
            Assert.IsTrue(homePage.ClickLoginButton()
                .InsertLogin(login)
                .InsertPassword(password)
                .ClickSignInButton()
                .EnsureSmsVerificationPageLoaded());

        }

        [Test]
        public void TestLoginWithoutNickName()
        {
            string login = "Login";
            string password = "";
            Assert.IsTrue(homePage.ClickLoginButton()
                .InsertLogin(login)
                .InsertPassword(password)
                .TrySignIn()
                .EnsureUserCannotLoginWithWrongNickName());
        }

        [Test]
        public void TestLoginWithoutPassword()
        {
            string login = "Login";
            string password = "";
            Assert.IsTrue(homePage.ClickLoginButton()
                .InsertLogin(login)
                .InsertPassword(password)
                .TrySignIn()
                .EnsureUserCannotLoginWithoutPassword());
        }

        [Test]
        public void TestLoginWithWrongPasswordAndWrongNickName()
        {
            string login = "Login";
            string password = "Password";
            Assert.IsTrue(homePage.ClickLoginButton()
                .InsertLogin(login)
                .InsertPassword(password)
                .TrySignIn()
                .EnsureBothWarringsAreDisplayed());
        }


        [Test]
        public void TestLoginWitoutPasswordAndWithoutNickName()
        {
            string login = "";
            string password = "";
            Assert.IsTrue(homePage.ClickLoginButton()
                .InsertLogin(login)
                .InsertPassword(password)
                .TrySignIn()
                .EnsureBothWarringsAreDisplayed());
        }
    }
}
