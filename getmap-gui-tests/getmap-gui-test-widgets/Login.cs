using System;
using OpenQA.Selenium;
using System.Threading;
using System.Collections.Generic;

namespace GetMapTest.GUI
{
    /// <summary>
    /// Выполняет авторизацию на сайт двумя способами: 
    /// произвольная авторизация пользовалетя 
    /// и авторизация пользователя 'guest'
    /// </summary>
    public class Login
    {
        private IWebDriver driver;
        private const string locationAuthButton = "#entry";
        private const string locationInputs = "#authForm input";
        private const string locationEntryButtons = "input";
        private const string _login = "login";
        private const string _pass = "pass";
        private const string _entry = "Войти";
        private Dictionary<string, IWebElement> dicAuth;
        private IList<IWebElement> listInputs;
        private IList<IWebElement> listButtons;

        private Login(IWebDriver driver, string baseUrl)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(baseUrl);
            driver.FindElement(By.CssSelector(locationAuthButton)).Click();
            SetValueList();
            SetValueElements();
        }

        private Login SetValueList()
        {
            dicAuth = new Dictionary<string, IWebElement>();
            listButtons = driver.FindElements(By.CssSelector(locationEntryButtons));
            listInputs = driver.FindElements(By.CssSelector(locationInputs));
            return this;
        }

        private Login SetValueElements()
        {
            for (int i = 0; i < listInputs.Count; i++)
            {
                if (listInputs[i].GetAttribute("name") == "login")
                    dicAuth.Add(_login, listInputs[i]);
                if (listInputs[i].GetAttribute("name") == "pass")
                    dicAuth.Add(_pass, listInputs[i]);
            }
            for (int i = 0; i < listButtons.Count; i++)
            {
                if (listButtons[i].GetAttribute("value") == "Войти")
                {
                    dicAuth.Add(_entry, listButtons[i]);
                    break;
                }

            }
            return this;
        }

        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора.</param>
        /// <param name="baseUrl">Ссылка, по которой хотите перейти.</param>
        /// <returns></returns>
        public static Login get(IWebDriver driver, string baseUrl)
        {
            return new Login(driver, baseUrl);
        }

        /// <summary>
        /// Выполняет авторазацию произвольного пользователя
        /// </summary>
        /// <param name="driver">Параметр типа IWebDriver для дальнейшей навигации по сайту</param>
        /// <param name="baseUrl">Ссылка на сайт, на который соответственно хотим авторизоваться</param>
        /// <param name="login">Произвольный логин для авторизации</param>
        /// <param name="passwd">Произвольный пароль для авторизации</param>
        public void login(String login, String passwd)
        {
            dicAuth[_login].SendKeys(login);
            dicAuth[_pass].SendKeys(passwd);
            dicAuth[_entry].Click();
        }

        /// <summary>
        /// Выполняет авторизацию на портал под пользователем 'Гость'.
        /// </summary>
        public void loginAsGuest()
        {
            login("guest", "guest");
        }

        /// <summary>
        /// Выполняет авторизацию на портал под пользователем 'Администратор'.
        /// </summary>
        public void loginAsAdmin()
        {
            login("admin", "111");
        }

        /// <summary>
        /// Выполняет авторизацию на портал под пользователем для тестов 'pasha'.
        /// </summary>
        public void loginAsPasha()
        {
            login("pasha", "88");
        }
    }
}
