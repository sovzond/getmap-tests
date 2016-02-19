using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;


namespace GetMapTest.GUI
{
    /// <summary>
    /// Открывает доступ ко всем элементам шапки.
    /// </summary>
   public class HeaderLinks
    {
        private IWebDriver driver;
        private const string locationHeaderLinks = "td.headerLinks a";
        private const string locationSearchArea = "input.searchPanel";
        private const string locationSearchButton = "#textSearch2";
        IList<IWebElement> listHeaderLinks;
            
        private HeaderLinks(IWebDriver driver)
        {
            this.driver = driver;
            listHeaderLinks = driver.FindElements(By.CssSelector(locationHeaderLinks));
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
        /// Выполняет поиск по геопорталу.
        /// </summary>
        /// <param name="attributeSearch">Искомый элемент</param>
        /// <returns></returns>
        public HeaderLinks MakeSearch(string attributeSearch)
        {
            driver.FindElement(By.CssSelector(locationSearchArea)).Click();
            driver.FindElement(By.CssSelector(locationSearchArea)).SendKeys(attributeSearch);
            driver.FindElement(By.CssSelector(locationSearchButton)).Click();
            return this;
        }

        /// <summary>
        /// Выполянет клик по кнопке 'Справка'.
        /// </summary>
        /// <returns></returns>
        public HeaderLinks HelpClick()
        {
            Sleep();
            listHeaderLinks[0].Click();
            return this;
        }

        /// <summary>
        /// Выполянет клик по кнопке 'Изменить пароль'.
        /// </summary>
        /// <returns></returns>
        public HeaderLinks ChangePasswordClick()
        {
            Sleep();
            listHeaderLinks[1].Click();
            return this;
        }

        /// <summary>
        /// Выполянет клик по кнопке 'Выход'.
        /// </summary>
        /// <returns></returns>
        public HeaderLinks ExitClick()
        {
            Sleep();
            listHeaderLinks[2].Click();
            return this;
        }

    }
}
