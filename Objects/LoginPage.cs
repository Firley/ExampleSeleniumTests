using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleSeleniumTests.Objects
{
    class LoginPage : Page
    {
        protected override string PageTitle => "Allegro Logowanie - Zaloguj się na swoje konto w Moje Allegro";
        protected override string PageUrl => @"https://allegro.pl/login/form?authorization";

        public LoginPage(IWebDriver driver)
        {
            Driver = driver;
        }

        protected IWebElement NickNameInput => Driver.FindElement(By.Id("username"));
        protected IWebElement PasswordInput => Driver.FindElement(By.Id("password"));
        protected IWebElement SignInButton => Driver.FindElement(By.Id("login-button"));
        protected IWebElement WrongLoginMessage => Driver.FindElement(By.Id("wrong-login-message"));
        protected bool isLoginWarringDisplayed => WrongLoginMessage.Displayed && WrongLoginMessage.Text == "Podaj login lub e-mail";
        protected IWebElement WrongPasswordMessage => Driver.FindElement(By.Id("wrong-password-message"));
        protected bool isPasswordWarringDisplayed =>  WrongPasswordMessage.Displayed && WrongPasswordMessage.Text == "Podaj hasło";
        protected IWebElement WrongMailOrPasswordMessage => Driver.FindElement(By.Id("wrong-password-error-title"));
        protected bool isWrongMailOrPasswordMessage => WrongMailOrPasswordMessage.Displayed && WrongMailOrPasswordMessage.Text == "Login, e-mail lub hasło są nieprawidłowe";
        protected IWebElement IdonRemeberPasswordLink => Driver.FindElement(By.LinkText("Nie pamiętasz hasła?"));
        protected IWebElement FacebookLoginButton => Driver.FindElement(By.CssSelector("button[ng-click='socialMedia.onFacebookIconClick()']"));
        protected IWebElement GoogleLoginButton => Driver.FindElement(By.CssSelector("button[ng-click='socialMedia.onGoogleIconClick()']"));

        protected IWebElement RegistrationLink => Driver.FindElement(By.Id("new-account-link"));
        

        public LoginPage InsertLogin(string login)
        {
            NickNameInput.SendKeys(login);
            return this;
        }



        public LoginPage InsertPassword(string password)
        {
            PasswordInput.SendKeys(password);
            return this;
        }

        public SmsVerificationPage ClickSignInButton()
        {
            SignInButton.Click();
            return new SmsVerificationPage(Driver);
        }

        public LoginPage TrySignIn()
        {
            SignInButton.Click();
            return new LoginPage(Driver);
        }

        public bool EnsureUserCannotLoginWithWrongNickName()
        {
            return isLoginWarringDisplayed;
        }

        public bool EnsureUserCannotLoginWithoutPassword()
        {
            return isPasswordWarringDisplayed;
        }

        public bool EnsureBothWarringsAreDisplayed()
        {
            return isLoginWarringDisplayed && isPasswordWarringDisplayed;
        }

        public bool EnsurehWarringIncorrectMailOrPasswordIsDisplayed()
        {
            return isWrongMailOrPasswordMessage;
        }

        public bool EnsureLoginPageLoaded()
        {
            EnsurePageLoaded();
            bool headerDisplayed = Driver.FindElement(By.TagName("h2")).Text == "Zaloguj się";
            if (!headerDisplayed)
            {
                throw new Exception($"Header is not dispalyed correctly. Header text is: \"{Driver.FindElement(By.TagName("h2")).Text}\"");
            }
            if (!NickNameInput.Displayed)
            {
                throw new Exception($"Login input is not displayed");
            }
            if (!PasswordInput.Displayed)
            {
                throw new Exception($"Password input is not displayed ");
            }
            if (!IdonRemeberPasswordLink.Displayed)
            {
                throw new Exception($"ResetPasword link is not displayed ");
            }
            if (!FacebookLoginButton.Displayed || !GoogleLoginButton.Displayed)
            {
                throw new Exception($"Social media button are not displayed correctly.");
            }
            if (!RegistrationLink.Displayed && RegistrationLink.Text == "zarejestruj się")
            {
                throw new Exception($"Registration link is not displayed correctly");
            }
            return true;
        }

    }
}
