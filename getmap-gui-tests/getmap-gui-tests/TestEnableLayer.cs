using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Drawing;

namespace GetMapTest
{
    /// <summary>
    /// Выполняет проверку на отображение слоев  раздела 'Газовая инфраструктура'.
    /// </summary>
    [TestClass]
    public class TestEnableLayer
    {
        private IWebDriver driver;
        private const string locationImageForCheck = "#OpenLayers_Layer_OSM_2 img[src*='/10/711/292.png']";

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
        }

        /// <summary>
        /// Проверяет отображение слоев при активности или деактивности слоев раздела 'Газовая инфраструктура'.
        /// </summary>
        [TestMethod]
        public void CheckEnables()
        {
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            GUI.SlideMenu.get(driver).OpenLayers();
            CheckEnableGasStructAndEnableGPZPoint();
            CheckEnableGasStruckAndDisableGPZPoint();
            CheckDisableGasStructAndEnableGPZPoint();
            CheckDisableGasStructAndDisableGPZPoint();
        }
                
        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void CheckEnableGasStructAndEnableGPZPoint()
        {
            GUI.ScaleMenu.get(driver).DecrementButton();
            GUI.Layers.get(driver).GasStructClick();
            GUI.Layers.GasStructClass.get(driver).OpenCloseList().GPZPointClick();
            Bitmap imageGPZDisable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, locationImageForCheck);
            GUI.Layers.GasStructClass.get(driver).GPZPointClick();
            Bitmap imageGPZEnable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, locationImageForCheck);
            Utils.ImageComparer comp = new Utils.ImageComparer(imageGPZDisable, imageGPZEnable);
            bool equal = comp.IsEqual();
            if (equal)
                Assert.Fail("Чекбоксы 'Газованя инфраструктура' и  'ГПЗ (точка)' активны, но слой на карте не отображается. ");         
        }

        private void CheckEnableGasStruckAndDisableGPZPoint()
        {
            Bitmap imageFullCBEnable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver,locationImageForCheck);
            GUI.Layers.GasStructClass.get(driver).GPZPointClick();
            Bitmap imageGPZDisable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, locationImageForCheck);
            Utils.ImageComparer comp = new Utils.ImageComparer(imageFullCBEnable, imageGPZDisable);
            bool equal = comp.IsEqual();
            if (equal)
                Assert.Fail("После снятия активности чекбокса слоя ГПЗ (точка), он карте остался отображаться.");
        }

        private void CheckDisableGasStructAndEnableGPZPoint()
        {
            GUI.Layers.GasStructClass.get(driver).GPZPointClick();
            Bitmap imageFullCBEnable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, locationImageForCheck);
            GUI.Layers.get(driver).GasStructClick();
            Bitmap imageGasStruckDisable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, locationImageForCheck);
            Utils.ImageComparer comp = new Utils.ImageComparer(imageFullCBEnable, imageGasStruckDisable);
            bool equal = comp.IsEqual();
            if (equal)
                Assert.Fail("После снятия чекбокса 'Газовая инфраструктура' слой остался отображаться на карте.");
        }

        private void CheckDisableGasStructAndDisableGPZPoint()
        {
            GUI.Layers.GasStructClass.get(driver).GPZPointClick();
            Bitmap imageFullCBEnable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, locationImageForCheck);
            GUI.Layers.get(driver).GasStructClick();
            GUI.Layers.GasStructClass.get(driver).GPZPointClick();
            Bitmap imageFullCBDisable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, locationImageForCheck);
            Utils.ImageComparer comp = new Utils.ImageComparer(imageFullCBEnable, imageFullCBDisable);
            bool equal = comp.IsEqual();
            if (equal)
                Assert.Fail("После снятия активности чекбоксов 'Газовая инфраструктура' и 'ГПЗ (точка)' слой на карте остался отображенным. ");
        }
    }
     
}
