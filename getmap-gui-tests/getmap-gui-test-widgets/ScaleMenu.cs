using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace GetMapTest.GUI
{
    /// <summary>
    /// Выполянет клик по кнопкам изменяющим масштаб карты в правой верхней части экрана.
    /// </summary>
    public class ScaleMenu
    {
        private IWebDriver driver;
        private const string locationIncrementButton = "#menuIncrement div.svzSimpleButton.zoomIncrement";
        private const string locationDecrementButton = "#menuIncrement div.svzSimpleButton.zoomDecrement";
        private ScaleMenu(IWebDriver driver)
        {
            this.driver = driver;
        }
        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static ScaleMenu get(IWebDriver driver)
        {
            return new ScaleMenu(driver);
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Приблежение'.
        /// </summary>
        /// <returns></returns>
        public ScaleMenu IncrementButton()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationIncrementButton)).Click();
            return this;
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Отдаление'.
        /// </summary>
        /// <returns></returns>
        public ScaleMenu DecrementButton()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationDecrementButton)).Click();
            return this;
        }
    }
}

