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
    public class BottomMenu
    {
        private IWebDriver driver;
        private const string locationLinks = "#menuOverview div.svzSimpleButton.linkBtn";
        private const string locationPrint = "#menuOverview div.svzSimpleButton.printBtn";
        private const string locationBookMarks = "#menuOverview div.svzSimpleButton.bookMarks";
        private const string locationTurnOnOff = "#turnResBtn";
        private const string locationClose = "#closeResBtn";
    
        private BottomMenu(IWebDriver driver)
        {
            this.driver = driver;
        }
        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static BottomMenu get(IWebDriver driver)
        {
            return new BottomMenu(driver);
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Ссылки' в нижней правой части экрана.
        /// </summary>
        /// <returns></returns>
        public BottomMenu OpenLinks()
        {
            driver.FindElement(By.CssSelector(locationLinks)).Click();
            return this;
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Печать' в нижней правой части экрана.
        /// </summary>
        /// <returns></returns>
        public BottomMenu OpenPrint()
        {
            driver.FindElement(By.CssSelector(locationPrint)).Click();
            return this;
        }

        /// <summary>
        /// ыполняет клик по кнопке 'Закладки' в нижней правой части экрана.
        /// </summary>
        /// <returns></returns>
        public BottomMenu OpenBookmarks()
        {
            driver.FindElement(By.CssSelector(locationBookMarks)).Click();
            return this;
        }

        /// <summary>
        /// ыполняет клик по кнопке 'Свернуть/развернуть' на таблице результатов поиска.
        /// </summary>
        /// <returns></returns>
        public BottomMenu TurnOnOff()
        {
            driver.FindElement(By.CssSelector(locationTurnOnOff)).Click();
            return this;
        }

        /// <summary>
        /// ыполняет клик по кнопке 'Закрыть' на таблице результатов поиска.
        /// </summary>
        /// <returns></returns>
        public BottomMenu Close()
        {
            driver.FindElement(By.CssSelector(locationClose)).Click();
            return this;
        }
    }
}
