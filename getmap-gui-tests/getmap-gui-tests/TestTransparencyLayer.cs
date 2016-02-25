﻿using System;
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
            DecTransparency("Факел");
            DecTransparency("Амбар");
            DecTransparency("Кустовые площадки");
            DecTransparency("ДНС");
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
            int x = listImgPointer[0].Location.X + listImgPointer[0].Size.Width * 2;
            int y = listImgPointer[0].Location.Y;
            area = new Rectangle(x, y, 300, 300);
        }

        private void DecTransparency(string nameLayer)
        {             
            Bitmap imagelVisible = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, area);
            Utils.ImageComparer compVisible = new Utils.ImageComparer(imagelVisible, imagelVisible);
            int nPixelsVisible = compVisible.nDifferentPixels;
            if (nameLayer == "Факел")
                GUI.SlideMenu.get(driver).ButtonDecTransparencyFakelClick(25);
            if (nameLayer == "Амбар")
                GUI.SlideMenu.get(driver).ButtonDecTransparencyAmbarClick(25);
            if (nameLayer == "Кустовые площадки")
                GUI.SlideMenu.get(driver).ButtonDecTransparencyPlacesClick(25);
            if (nameLayer == "ДНС")
                GUI.SlideMenu.get(driver).ButtonDecTransparencyDNSClick(25);
            Bitmap imageHalfVisible = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, area);
            Utils.ImageComparer compHalfVisible = new Utils.ImageComparer(imagelVisible, imageHalfVisible);
            bool equalHalfVisible = compHalfVisible.IsEqual();
            int nPixelsHalfVisible = compHalfVisible.nDifferentPixels;
            if (nPixelsVisible == nPixelsHalfVisible && equalHalfVisible == true)
                Assert.Fail("Слой " + nameLayer + " не стал прозрачным на половину, после сдвига ползунка.");
            if (nameLayer == "Факел")
                GUI.SlideMenu.get(driver).ButtonDecTransparencyFakelClick(30);
            if (nameLayer == "Амбар")
                GUI.SlideMenu.get(driver).ButtonDecTransparencyAmbarClick(30);
            if (nameLayer == "Кустовые площадки")
                GUI.SlideMenu.get(driver).ButtonDecTransparencyPlacesClick(30);
            if (nameLayer == "ДНС")
                GUI.SlideMenu.get(driver).ButtonDecTransparencyDNSClick(30);
            Bitmap imageNotVisible = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, area);
            Utils.ImageComparer compNotVisible = new Utils.ImageComparer(imageHalfVisible, imageNotVisible);
            bool equalNotVisible = compNotVisible.IsEqual();
            int nPixelsNotVisible = compNotVisible.nDifferentPixels;
            if (nPixelsHalfVisible == nPixelsNotVisible && equalNotVisible == true)
                Assert.Fail("Слой " + nameLayer + " не стал полностью прозрачным после сдвига ползунка.");
        }

    }
}