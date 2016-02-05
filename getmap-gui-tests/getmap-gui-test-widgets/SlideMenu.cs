using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;


namespace GetMapTest.GUI
{
    /// <summary>
    /// Открывает плажку в левой части экрана (Слои, Легенда). 
    /// Так же открывает вкладки данной плажки.
    /// </summary>
    public class SlideMenu
    {
        private IWebDriver driver;
        private const string locationSlideMenu = "#menuSlide div.svzSimpleButton.slidePanelButton";
        private const string locationBaseLayers = "#layersCon div.svzSimpleButton.accordionButton";
        private const string locationGoogle = "#stdportal_LayerManagerBase_0 div.svzLayerManagerText";
        private const string locationLegenda = "#menuSlide div.svzSimpleButton.slidePanelLegendButton";
        private SlideMenu(IWebDriver driver)
        {
            this.driver = driver;
        }
        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static SlideMenu get(IWebDriver driver)
        {
            return new SlideMenu(driver);
        }
        /// <summary>
        /// Открывает базовые слои вкладки 'СЛОИ'.
        /// </summary>
        /// <returns></returns>
        public SlideMenu OpenBaseLayers()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationSlideMenu)).Click();
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationBaseLayers)).Click();
            return this;
        }
        /// <summary>
        /// Открывает владку 'Google' во вкладке 'Базовые слои'.
        /// </summary>
        /// <returns></returns>
        public SlideMenu OpenGoogle()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationSlideMenu)).Click();
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationBaseLayers)).Click();
            driver.FindElement(By.CssSelector(locationGoogle)).Click();
            return this;
        }
        /// <summary>
        /// Открывает вкладку 'Легенда' на плажке в левой части экрана.
        /// </summary>
        /// <returns></returns>
        public SlideMenu OpenLegenda()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationLegenda)).Click();
            return this;
        }

    }
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

    public class MenuNavigationHistory
    {
        private IWebDriver driver;
        private const string locationBackButton = "#menuNavigationHistory div.svzSimpleButton.previousState";
        private const string locationNextButton = "menuNavigationHistory div.svzSimpleButton.nextState";
        private MenuNavigationHistory(IWebDriver driver)
        {
            this.driver = driver;
        }
        public static MenuNavigationHistory get(IWebDriver driver)
        {
            return new MenuNavigationHistory(driver);
        }
        public MenuNavigationHistory Back()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationBackButton)).Click();
            return this;
        }
        public MenuNavigationHistory Next()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationNextButton)).Click();
            return this;
        }
    }
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
        public static MenuNavigation get(IWebDriver driver)
        {
            return new MenuNavigation(driver);
        }
        public MenuNavigation FullExtentButton()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationFullExtentButton)).Click();
            return this;
        }
        public MenuNavigation MoveButton()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationMoveButton)).Click();
            return this;
        }
        public MenuNavigation ZoomArea()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationZoomAreaButton)).Click();
            return this;
        }
        public MenuNavigation MagnifyButton()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationMagnifyButton)).Click();
            return this;
        }
        public MenuNavigation IdentificationButton()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationIdentificationButton)).Click();
            return this;
        }
        public MenuNavigation SelectionButton()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationSelectionButton)).Click();
            return this;
        }
        public MenuNavigation RuleButton()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationRuleButton)).Click();
            return this;
        }
        public MenuNavigation GotoCoordsButton()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationGotoCoordsButton)).Click();
            return this;
        }
        public MenuNavigation SetLon(int degrees, int minutes, int seconds)
        {
            driver.FindElement(By.Id("dijit_form_NumberTextBox_0")).SendKeys(degrees.ToString());
            driver.FindElement(By.Id("dijit_form_NumberTextBox_1")).SendKeys(minutes.ToString());
            driver.FindElement(By.Id("dijit_form_NumberTextBox_2")).SendKeys(seconds.ToString());
            return this;
        }
        public MenuNavigation SetLat(int degrees, int minutes, int seconds)
        {
            driver.FindElement(By.Id("dijit_form_NumberTextBox_3")).SendKeys(degrees.ToString());
            driver.FindElement(By.Id("dijit_form_NumberTextBox_4")).SendKeys(minutes.ToString());
            driver.FindElement(By.Id("dijit_form_NumberTextBox_5")).SendKeys(seconds.ToString());
            driver.FindElement(By.CssSelector("#gotoCoords .button")).Click();
            return this;
        }
    }
}