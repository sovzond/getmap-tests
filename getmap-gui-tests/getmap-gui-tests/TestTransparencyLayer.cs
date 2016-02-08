using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace GetMapTest
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class TestTransparencyLayer
    {

        private IWebDriver driver;
        private const string locationLegenda = "#menuSlide div.svzSimpleButton.slidePanelLegendButton";
        private const string locationFakelDecTransButton = "#dojoUnique1 div.dijitSliderDecrementIconH";
        private const string locationAmbarDecTransButton = "#dojoUnique2 div.dijitSliderDecrementIconH";
        private const string locationPlacesDecTransButton = "#dojoUnique3 div.dijitSliderDecrementIconH";
        private const string locationDNSDecTransButton = "#dojoUnique4 div.dijitSliderDecrementIconH";
        private const string locationMap = "#OpenLayers_Layer_OSM_2 img[src*='/11/1422/584.png']";

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();        
        }

        /// <summary>
        ///
        ///</summary>
        [TestMethod]
        public void CheckTransparency()
        {
            LogOn();
            DecTransparencyFakel();
        }

        [TestCleanup]
        public void Clean()
        {
            System.Threading.Thread.Sleep(2000);
            driver.Quit();
        }

        private void LogOn()
        {
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
        }

        private void DecTransparencyFakel()
        {         
            Bitmap imageFakelVisible = TakeScreenshot(); 
            driver.FindElement(By.CssSelector(locationLegenda)).Click();
            for (int i = 0; i < 55; i++)
                driver.FindElement(By.CssSelector(locationFakelDecTransButton)).Click();
            Bitmap imageFakelNotVisible = TakeScreenshot();

        }
        private Bitmap TakeScreenshot()
        {
            var screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            var bitmapScreen = new Bitmap(new MemoryStream(screenshot.AsByteArray));
            IWebElement element = driver.FindElement(By.CssSelector(locationMap));
            var cutArea = new Rectangle(element.Location, element.Size);
            return bitmapScreen.Clone(cutArea, bitmapScreen.PixelFormat);
        }
    }
}
