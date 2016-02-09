using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace GetMapTest.GUI
{
    /// <summary>
    /// Выполняет клик по кнопкам рядом с плажкой (Кнопки DOP).
    /// </summary>
    public class MenuDop
    {
        private IWebDriver driver;
        private const string locationLinks = "#menuDop div.svzSimpleButton.linkBtn";
        private const string locationPrint = "#menuDop div.svzSimpleButton.printBtn";
        private MenuDop(IWebDriver driver)
        {
            this.driver = driver;
        }
        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static MenuDop get(IWebDriver driver)
        {
            return new MenuDop(driver);
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Ссылки' в верхней левой части экрана. 
        /// </summary>
        /// <returns></returns>
        public MenuDop OpenLinks()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationLinks)).Click();
            return this;
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Печать' в верхней левой части экрана.
        /// </summary>
        /// <returns></returns>
        public MenuDop OpenPrint()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationPrint)).Click();
            return this;
        }
    }
}
