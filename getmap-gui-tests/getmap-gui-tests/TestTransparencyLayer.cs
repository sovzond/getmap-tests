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
        private IList<IWebElement> listImgPointer;
        private const int numberImgForScreen = 0;
        private const string layerFakel = "Факел";
        private const string layerAmbar = "Амбар";
        private const string layerPlaces = "Кустовые площадки";
        private const string layerDns = "ДНС";
        private const string locationPointer = ".olAlphaImg";
        private const string locationRadioButtons = "div.svzLayerManagerItem input";
        private Rectangle area;

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
        }

        /// <summary>
        ///Делает скриншот карты и проверяет, были ли внесены изменения прозрачности слоя,
        ///после сдвига ползунка влево.
        ///</summary>
        [TestMethod]
        public void CheckTransparency()
        {
            DataPreparation();
            DecTransparency(layerFakel);
            DecTransparency(layerAmbar);
            DecTransparency(layerPlaces);
            DecTransparency(layerDns);
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void DataPreparation()
        {
            GUI.InputCoordWnd.get(driver).setLon(60, 53, 0).setLat(69, 55, 0).click();
            listImgPointer = driver.FindElements(By.CssSelector(locationPointer));
            GUI.ScaleMenu.get(driver).IncrementButton();
            GUI.SlideMenu.get(driver).OpenLayers().OpenBaseLayers().TopOsnovaClick();
            if (!GUI.Layers.get(driver).GetSelectedNeftyStruct)
                GUI.Layers.get(driver).NeftyStructCheckBoxClick();
            GUI.SlideMenu.get(driver).OpenLegenda();
            int x = listImgPointer[numberImgForScreen].Location.X + listImgPointer[numberImgForScreen].Size.Width * 2;
            int y = listImgPointer[numberImgForScreen].Location.Y;
            int square = listImgPointer[numberImgForScreen].Size.Width * 9;
            area = new Rectangle(x, y, square, square);
        }

        private void DecTransparency(string nameLayer)
        {
            Bitmap imagelVisible = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, area);
            Utils.ImageComparer compVisible = new Utils.ImageComparer(imagelVisible, imagelVisible);
            if (nameLayer == layerFakel)
                GUI.SlideMenu.get(driver).ButtonDecTransparencyFakelClick(25);
            if (nameLayer == layerAmbar)
                GUI.SlideMenu.get(driver).ButtonDecTransparencyAmbarClick(25);
            if (nameLayer == layerPlaces)
                GUI.SlideMenu.get(driver).ButtonDecTransparencyPlacesClick(25);
            if (nameLayer == layerDns)
                GUI.SlideMenu.get(driver).ButtonDecTransparencyDNSClick(25);
            Bitmap imageHalfVisible = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, area);
            Utils.ImageComparer compHalfVisible = new Utils.ImageComparer(imagelVisible, imageHalfVisible);
            Assert.IsFalse(compHalfVisible.IsEqual(), "Слой " + nameLayer + " не стал прозрачным на половину, после сдвига ползунка.");
            if (nameLayer == layerFakel)
                GUI.SlideMenu.get(driver).ButtonDecTransparencyFakelClick(30);
            if (nameLayer == layerAmbar)
                GUI.SlideMenu.get(driver).ButtonDecTransparencyAmbarClick(30);
            if (nameLayer == layerPlaces)
                GUI.SlideMenu.get(driver).ButtonDecTransparencyPlacesClick(30);
            if (nameLayer == layerDns)
                GUI.SlideMenu.get(driver).ButtonDecTransparencyDNSClick(30);
            Bitmap imageNotVisible = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, area);
            Utils.ImageComparer compNotVisible = new Utils.ImageComparer(imageHalfVisible, imageNotVisible);
            Assert.IsFalse(compNotVisible.IsEqual(), "Слой " + nameLayer + " не стал полностью прозрачным после сдвига ползунка.");
        }

    }
}