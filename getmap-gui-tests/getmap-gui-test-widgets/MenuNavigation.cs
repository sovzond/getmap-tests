using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace GetMapTest.GUI
{
    /// <summary>
    /// Выполняет клик по навигационным кнопкам в правой части экрана
    /// </summary>
    public class MenuNavigation
    {
        private IWebDriver driver;
        private const string locationFullExtentButton = "#menuNavigation div.svzSimpleButton.fullMap";
        private const string locationMoveButton = "#menuNavigation div.svzSimpleButton.pan";
        private const string locationZoomAreaButton = "#menuNavigation div.svzSimpleButton.zoomIn";
        private const string locationMagnifyButton = "#menuNavigation div.svzSimpleButton.zoomIn";
        private const string locationIdentificationButton = "#menuNavigation div.svzSimpleButton.searchMap";
        private const string locationSelectionButton = "#menuNavigation div.svzSimpleButton.searchMap";
        private const string locationRuleButton = "#menuNavigation div.svzSimpleButton.measureButton";
        private const string locationGotoCoordsButton = "#menuNavigation div.svzSimpleButton.gotoCoordsButton";
        private MenuNavigation(IWebDriver driver)
        {
            this.driver = driver;
        }
        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static MenuNavigation get(IWebDriver driver)
        {
            return new MenuNavigation(driver);
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Полный экстент'. 
        /// </summary>
        /// <returns></returns>
        public MenuNavigation FullExtentButton()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationFullExtentButton)).Click();
            return this;
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Перемещение'.
        /// </summary>
        /// <returns></returns>
        public MenuNavigation MoveButton()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationMoveButton)).Click();
            return this;
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Приблежение области'. 
        /// </summary>
        /// <returns></returns>
        public MenuNavigation ZoomArea()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationZoomAreaButton)).Click();
            return this;
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Увеличение фрагмента'.
        /// </summary>
        /// <returns></returns>
        public MenuNavigation MagnifyButton()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationMagnifyButton)).Click();
            return this;
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Идентификация'.
        /// </summary>
        /// <returns></returns>
        public MenuNavigation IdentificationButton()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationIdentificationButton)).Click();
            return this;
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Выборка'.
        /// </summary>
        /// <returns></returns>
        public MenuNavigation SelectionButton()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationSelectionButton)).Click();
            return this;
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Измерение'.
        /// </summary>
        /// <returns></returns>
        public MenuNavigation RuleButton()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationRuleButton)).Click();
            return this;
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Переход по координатам'.
        /// </summary>
        /// <returns></returns>
        public MenuNavigation GotoCoordsButton()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationGotoCoordsButton)).Click();
            return this;
        }
    }
}
