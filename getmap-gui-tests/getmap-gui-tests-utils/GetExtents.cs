using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;


namespace GetMapTest.Utils
{
    /// <summary>
    /// Возвращает различные экстенты карты.
    /// </summary>
    public class GetExtents
    {
        private IWebDriver driver;
        private const string locationFullExtentButton = "#menuNavigation div.svzSimpleButton.fullMap";

        private GetExtents(IWebDriver driver)
        {
            this.driver = driver;
        }

        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static GetExtents get(IWebDriver driver)
        {
            return new GetExtents(driver);
        }

        /// <summary>
        /// Возвращает текущий экстент карты.
        /// </summary>
        public string[] GetCurrentExtent
        {
            get
            {
                return getCurrentExtent();
            }
        }

        /// <summary>
        /// Возвращает базовый экстент карты.
        /// </summary>
        public string[] GetBaseExtent
        {
            get
            {
                return getBaseExtent();
            }
        }

        private string[] getCurrentExtent()
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string onlyExtentCoordsCurrent = (string)js.ExecuteScript("return window.portal.stdmap.map.getExtent().toString()");
            string[] splitedExtentCoordsCurrent = onlyExtentCoordsCurrent.Split(',');
            return splitedExtentCoordsCurrent;
        }

        private string[] getBaseExtent()
        {
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationFullExtentButton)).Click();
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string onlyExtentCoordsCurrent = (string)js.ExecuteScript("return window.portal.stdmap.map.getExtent().toString()");
            string[] splitedExtentCoordsCurrent = onlyExtentCoordsCurrent.Split(',');
            return splitedExtentCoordsCurrent;
        }
    }
}
