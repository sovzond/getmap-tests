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
        private const string locationSelectionButtonSquare = "div.svzDropMenuButtonMenu > div.svzSimpleButton.selectSquare";
        private const string locationSelectionButtonLine = "div.svzDropMenuButtonMenu > div.svzSimpleButton.editorAddLine";
        private const string locationSelectionButtonBuffer = "div.svzDropMenuButtonMenu > div.svzSimpleButton.selectBuffer";
        private const string locationSelectionButtonCancel = "div.svzDropMenuButtonMenu > div.svzSimpleButton.editorStop";
        private const string locationSelectionButtonPoligon = "div.svzDropMenuButtonMenu > div.svzSimpleButton.editorAddPoligon";
        private const string locationSelectionButton = "#menuNavigation div.svzSimpleButton.selectionSearch";
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
            driver.FindElement(By.CssSelector(locationFullExtentButton)).Click();
            return this;
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Перемещение'.
        /// </summary>
        /// <returns></returns>
        public MenuNavigation MoveButton()
        {
            driver.FindElement(By.CssSelector(locationMoveButton)).Click();
            return this;
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Приблежение области'. 
        /// </summary>
        /// <returns></returns>
        public MenuNavigation ZoomArea()
        {
            driver.FindElement(By.CssSelector(locationZoomAreaButton)).Click();
            return this;
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Увеличение фрагмента'.
        /// </summary>
        /// <returns></returns>
        public MenuNavigation MagnifyButton()
        {
            driver.FindElement(By.CssSelector(locationMagnifyButton)).Click();
            return this;
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Идентификация'.
        /// </summary>
        /// <returns></returns>
        public MenuNavigation IdentificationButton()
        {
            driver.FindElement(By.CssSelector(locationIdentificationButton)).Click();
            return this;
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Выборка'.
        /// </summary>
        /// <returns></returns>
        public MenuNavigation SelectionButton()
        {
            driver.FindElement(By.CssSelector(locationSelectionButton)).Click();
            return this;
        }

        public MenuNavigation SelectionButtonSquare()
        {
            driver.FindElement(By.CssSelector(locationSelectionButtonSquare)).Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Полигон' компонента 'Выборка'.
        /// </summary>
        /// <returns></returns>
        public MenuNavigation SelectionButtonPoligon()
        {
            driver.FindElement(By.CssSelector(locationSelectionButtonPoligon)).Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Линия' компонента 'Выборка'.
        /// </summary>
        /// <returns></returns>
        public MenuNavigation SelectionButtonLine()
        {
            driver.FindElement(By.CssSelector(locationSelectionButtonLine)).Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Буфер' компонента 'Выборка'.
        /// </summary>
        /// <returns></returns>
        public MenuNavigation SelectionButtonBuffer()
        {          
            driver.FindElement(By.CssSelector(locationSelectionButtonBuffer)).Click();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MenuNavigation SelectionButtonCancel()
        {
            driver.FindElement(By.CssSelector(locationSelectionButtonCancel)).Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Измерение'.
        /// </summary>
        /// <returns></returns>
        public MenuNavigation RuleButton()
        {
            driver.FindElement(By.CssSelector(locationRuleButton)).Click();
            return this;
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Переход по координатам'.
        /// </summary>
        /// <returns></returns>
        public MenuNavigation GotoCoordsButton()
        {
            driver.FindElement(By.CssSelector(locationGotoCoordsButton)).Click();
            return this;
        }
    }
}
