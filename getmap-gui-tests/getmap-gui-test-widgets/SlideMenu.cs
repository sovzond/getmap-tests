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
}