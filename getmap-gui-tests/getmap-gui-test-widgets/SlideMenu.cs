using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;


namespace GetMapTest.GUI
{
    /// <summary>
    /// Открывает плажку в левой части экрана (Слои, Легенда). 
    /// Так же открывает вкладки данной плажки и прокликивает чекбосы.
    /// </summary>
    public class SlideMenu
    {
        private IWebDriver driver;
        private const string locationSlideMenu = "#menuSlide div.svzSimpleButton.slidePanelButton";
        private const string locationBaseLayers = "#layersCon div.svzSimpleButton.accordionButton";
        private const string locationGoogle = "#stdportal_LayerManagerBase_0 div.svzLayerManagerText";
        private const string locationLegenda = "#menuSlide div.svzSimpleButton.slidePanelLegendButton";
        private const string locationRadioButtons = "div.svzLayerManagerItem input";
        private IList<IWebElement> listLayersInBaseLayers;

        private SlideMenu(IWebDriver driver)
        {
            this.driver = driver;
            listLayersInBaseLayers = driver.FindElements(By.CssSelector(locationRadioButtons));
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
        /// Открывает саму плажку 'СЛОИ'.
        /// </summary>
        /// <returns></returns>
        public SlideMenu OpenLayers()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationSlideMenu)).Click();
            return this;
        }

        /// <summary>
        /// Открывает базовые слои вкладки 'СЛОИ'.
        /// </summary>
        /// <returns></returns>
        public SlideMenu OpenBaseLayers()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationBaseLayers)).Click();
            return this;
        }

        /// <summary>
        /// Активирует чекбокс Росреестр(выполняет по нему клик).
        /// </summary>
        /// <returns></returns>
        public SlideMenu RosreestrClick()
        {
            listLayersInBaseLayers[3].Click();
            return this;
        }

        /// <summary>
        /// Активирует чекбокс OpenStreetMap(выполняет по нему клик).
        /// </summary>
        /// <returns></returns>
        public SlideMenu OpenStreetMapClick()
        {
            listLayersInBaseLayers[4].Click();      
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
}