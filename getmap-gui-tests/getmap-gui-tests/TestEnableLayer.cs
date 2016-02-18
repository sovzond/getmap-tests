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
        private const string locationGasStruct = "#dijit_form_CheckBox_13";
        private const string locationGPZPoint = "#dijit_form_CheckBox_14";

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
            driver.FindElement(By.CssSelector(locationGasStruct)).Click();
            GUI.Layers.get(driver).GasStructClick();
            IWebElement elementGPZPoint = driver.FindElement(By.CssSelector(locationGPZPoint));
            if (!elementGPZPoint.Selected)
                driver.FindElement(By.CssSelector(locationGPZPoint)).Click();
            Bitmap imageGPZDisable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, locationImageForCheck);
            if(elementGPZPoint.Selected)
                driver.FindElement(By.CssSelector(locationGPZPoint)).Click();
            Bitmap imageGPZEnable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, locationImageForCheck);
            Utils.ImageComparer comp = new Utils.ImageComparer(imageGPZDisable, imageGPZEnable);
            bool equal = comp.IsEqual();
            if (equal)
                Assert.Fail("Чекбоксы 'Газованя инфраструктура' и  'ГПЗ (точка)' активны, но слой на карте не отображается. ");
        }

        private void CheckEnableGasStruckAndDisableGPZPoint()
        {
            Bitmap imageFullCBEnable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, locationImageForCheck);
            driver.FindElement(By.CssSelector(locationGPZPoint)).Click();
            Bitmap imageGPZDisable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, locationImageForCheck);
            Utils.ImageComparer comp = new Utils.ImageComparer(imageFullCBEnable, imageGPZDisable);
            bool equal = comp.IsEqual();
            if (equal)
                Assert.Fail("После снятия активности чекбокса слоя ГПЗ (точка), он карте остался отображаться.");
        }

        private void CheckDisableGasStructAndEnableGPZPoint()
        {
            driver.FindElement(By.CssSelector(locationGPZPoint)).Click();
            Bitmap imageFullCBEnable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, locationImageForCheck);
            driver.FindElement(By.CssSelector(locationGasStruct)).Click();
            Bitmap imageGasStruckDisable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, locationImageForCheck);
            Utils.ImageComparer comp = new Utils.ImageComparer(imageFullCBEnable, imageGasStruckDisable);
            bool equal = comp.IsEqual();
            if (equal)
                Assert.Fail("После снятия чекбокса 'Газовая инфраструктура' слой остался отображаться на карте.");
        }

        private void CheckDisableGasStructAndDisableGPZPoint()
        {
            driver.FindElement(By.CssSelector(locationGPZPoint)).Click();
            Bitmap imageFullCBEnable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, locationImageForCheck);
            driver.FindElement(By.CssSelector(locationGasStruct)).Click();
            driver.FindElement(By.CssSelector(locationGPZPoint)).Click();
            Bitmap imageFullCBDisable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, locationImageForCheck);
            Utils.ImageComparer comp = new Utils.ImageComparer(imageFullCBEnable, imageFullCBDisable);
            bool equal = comp.IsEqual();
            if (equal)
                Assert.Fail("После снятия активности чекбоксов 'Газовая инфраструктура' и 'ГПЗ (точка)' слой на карте остался отображенным. ");
        }
    }

}
