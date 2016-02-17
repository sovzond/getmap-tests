using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;


namespace GetMapTest.GUI
{
    /// <summary>
    /// 
    /// </summary>
   public class HeaderLinks
    {
        private IWebDriver driver;
        private const string locationHeaderLinks = "td.headerLinks a";
        IList<IWebElement> list;

        private HeaderLinks(IWebDriver driver)
        {
            this.driver = driver;
            list = driver.FindElements(By.CssSelector(locationHeaderLinks));
        }
        private void Sleep()
        {
            System.Threading.Thread.Sleep(2000);
        }
        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static HeaderLinks get(IWebDriver driver)
        {
            return new HeaderLinks(driver);
        }

        /// <summary>
        /// Выполянет клик по кнопке 'Справка'.
        /// </summary>
        /// <returns></returns>
        public HeaderLinks HelpClick()
        {
            Sleep();
            list[0].Click();
            return this;
        }

        /// <summary>
        /// Выполянет клик по кнопке 'Изменить пароль'.
        /// </summary>
        /// <returns></returns>
        public HeaderLinks ChangePasswordClick()
        {
            Sleep();
            list[1].Click();
            return this;
        }

        /// <summary>
        /// Выполянет клик по кнопке 'Выход'.
        /// </summary>
        /// <returns></returns>
        public HeaderLinks ExitClick()
        {
            Sleep();
            list[2].Click();
            return this;
        }

    }
}
