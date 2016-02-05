using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace GetMapTest.GUI
{
    /// <summary>
    /// Выполняет клик по кнопкам в правой верхней части экрана (Предыдущик экстент, следующий экстент).
    /// </summary>
    public class MenuNavigationHistory
    {
        private IWebDriver driver;
        private const string locationBackButton = "#menuNavigationHistory div.svzSimpleButton.previousState";
        private const string locationNextButton = "menuNavigationHistory div.svzSimpleButton.nextState";
        private MenuNavigationHistory(IWebDriver driver)
        {
            this.driver = driver;
        }
        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static MenuNavigationHistory get(IWebDriver driver)
        {
            return new MenuNavigationHistory(driver);
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Предыдущий экстент'.
        /// </summary>
        /// <returns></returns>
        public MenuNavigationHistory Back()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationBackButton)).Click();
            return this;
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Следующий экстент'.
        /// </summary>
        /// <returns></returns>
        public MenuNavigationHistory Next()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationNextButton)).Click();
            return this;
        }
    }
}
