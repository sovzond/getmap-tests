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
    /// Выполняет проверку прозрачности всех слоев.
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
        ///Делает скриншот карты и проверяет, были ли внесены изменения прозрачности слоя,
        ///после сдвига ползунка влево.
        ///</summary>
        [TestMethod]
        public void CheckTransparency()
        {
            LogOn();
            driver.FindElement(By.CssSelector(locationLegenda)).Click();
            DecTransparency(locationFakelDecTransButton);
            DecTransparency(locationAmbarDecTransButton);
            DecTransparency(locationPlacesDecTransButton);
            DecTransparency(locationDNSDecTransButton);
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void LogOn()
        {
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
        }
 
        private void DecTransparency(string locationDecTransButton)
        {
            string nameLayer = "";
            if (locationFakelDecTransButton == locationDecTransButton)
                nameLayer = " 'Факел' ";
            if (locationAmbarDecTransButton == locationDecTransButton)
                nameLayer = " 'Амбар' ";
            if (locationPlacesDecTransButton == locationDecTransButton)
                nameLayer = " 'Кустовые площадки' ";
            if (locationDNSDecTransButton == locationDecTransButton)
                nameLayer = " 'ДНС' ";
            Bitmap imagelVisible = TakeScreenshot();
            Utils.ImageComparer compVisible = new Utils.ImageComparer(imagelVisible, imagelVisible);
            int nPixelsVisible = compVisible.nDifferentPixels;
            for (int i = 0; i < 25; i++)
                driver.FindElement(By.CssSelector(locationDecTransButton)).Click();
            Bitmap imageHalfVisible = TakeScreenshot();
            Utils.ImageComparer compHalfVisible = new Utils.ImageComparer(imagelVisible, imageHalfVisible);
            bool equalHalfVisible = compHalfVisible.IsEqual();
            int nPixelsHalfVisible = compHalfVisible.nDifferentPixels;
            if (nPixelsVisible == nPixelsHalfVisible && equalHalfVisible == true)
                Assert.Fail("Слой" + nameLayer + "не стал прозрачным на половину, после сдвига ползунка.");
            for (int i = 0; i < 30; i++)
                driver.FindElement(By.CssSelector(locationDecTransButton)).Click();
            Bitmap imageNotVisible = TakeScreenshot();
            Utils.ImageComparer compNotVisible = new Utils.ImageComparer(imageHalfVisible, imageNotVisible);
            bool equalNotVisible = compNotVisible.IsEqual();
            int nPixelsNotVisible = compNotVisible.nDifferentPixels;
            if (nPixelsHalfVisible == nPixelsNotVisible && equalNotVisible == true)
                Assert.Fail("Слой" + nameLayer + " не стал полностью прозрачным после сдвига ползунка.");
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