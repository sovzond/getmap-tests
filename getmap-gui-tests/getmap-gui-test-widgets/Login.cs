using System;
using OpenQA.Selenium;
using System.Threading;

namespace GetMapTest.GUI
{
   public class Login
    {
        public static void login(IWebDriver driver, string baseUrl, String login, String passwd)
        {
            driver.Navigate().GoToUrl(baseUrl);
            driver.FindElement(By.Id("txtUser")).SendKeys(login);
            driver.FindElement(By.Id("txtPsw")).SendKeys(passwd);
            driver.FindElement(By.Id("cmdLogin")).Click();
        }
        public static void loginAsGuest(IWebDriver driver, string baseUrl)
        {
            login(driver, baseUrl, "guest", "guest");
        }
    }
}
