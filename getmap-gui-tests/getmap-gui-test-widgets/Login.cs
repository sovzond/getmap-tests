using System;
using OpenQA.Selenium;
using System.Threading;

namespace GetMapTest.GUI
{
    /// <summary>
    /// Выполняет авторизацию на сайт двумя способами: 
    /// произвольная авторизация пользовалетя 
    /// и авторизация пользователя 'guest'
    /// </summary>
   public class Login
    {
        /// <summary>
        /// Выполняет авторазацию произвольного пользователя
        /// </summary>
        /// <param name="driver">Параметр типа IWebDriver для дальнейшей навигации по сайту</param>
        /// <param name="baseUrl">Ссылка на сайт, на который соответственно хотим авторизоваться</param>
        /// <param name="login">Произвольный логин для авторизации</param>
        /// <param name="passwd">Произвольный пароль для авторизации</param>
        public static void login(IWebDriver driver, string baseUrl, String login, String passwd)
        {
            driver.Navigate().GoToUrl(baseUrl);
            driver.FindElement(By.Id("txtUser")).SendKeys(login);
            driver.FindElement(By.Id("txtPsw")).SendKeys(passwd);
            driver.FindElement(By.Id("cmdLogin")).Click();
        }
        /// <summary>
        /// Выполняет авторизацию на сайт пользователя 'guest'
        /// </summary>
        /// <param name="driver">Параметр типа IWebDriver для дальнейшей навигации по сайту</param>
        /// <param name="baseUrl">Ссылка на сайт, на который соответственно хотим авторизоваться</param>
        public static void loginAsGuest(IWebDriver driver, string baseUrl)
        {
            login(driver, baseUrl, "guest", "guest");
        }
    }
}
