using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace GetMapTest
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class TestSelection
    {
        private IWebDriver driver;
        private IJavaScriptExecutor js;
        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver, Settings.Instance.BaseUrl).loginAsGuest();
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
            js = driver as IJavaScriptExecutor;
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Check()
        {
            GUI.MenuNavigation.get(driver).SelectionButton();
            IWebElement el = driver.FindElement(By.CssSelector("#map"));
            string s = (string)js.ExecuteScript("return window.portal.stdmap.map.getLonLatFromPixel(new OpenLayers.Pixel(80,80)).toString()");
            string ss = (string)js.ExecuteScript("return window.portal.stdmap.map.getPixelFromLonLat(new OpenLayers.LonLat(7508523.6297172,4187105.3444576)).toString()");

            // var builder = new Actions(driver);
            // builder.MoveToElement(el, 80, 80).ClickAndHold().MoveByOffset(1, 1).Release().Perform();
        }
        //кливает по элементу в определенных координатах, потом перемещает на количество  экранных пикселей и отпускает.
        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

    }

}
