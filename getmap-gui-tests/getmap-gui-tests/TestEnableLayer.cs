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
        private const int numberImgForScreen = 0;
        private const string locationPointer = ".olAlphaImg";
        private IList<IWebElement> listImgPointer;
        private Rectangle area;

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
            
        }

        /// <summary>
        /// Проверяет отображение слоев при активности или деактивности слоев раздела 'Газовая инфраструктура'.
        /// </summary>
        [TestMethod]
        public void CheckEnables()
        {
            DataPreparation();
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

        private void DataPreparation()
        {
            GUI.InputCoordWnd.get(driver).setLon(60, 44, 39).setLat(69, 51, 0).click();
            listImgPointer = driver.FindElements(By.CssSelector(locationPointer));
            int x = listImgPointer[numberImgForScreen].Location.X + listImgPointer[numberImgForScreen].Size.Width * 2;
            int y = listImgPointer[numberImgForScreen].Location.Y;
            int square = listImgPointer[numberImgForScreen].Size.Width * 9;
            area = new Rectangle(x, y, square, square);
            GUI.SlideMenu.get(driver).OpenLayers();
        }

        private void CheckEnableGasStructAndEnableGPZPoint()
        {
            GUI.Layers.get(driver).GasStructCheckBoxClick().GasStructOpenCloseList();
            GUI.GasStructLayer.get(driver).GPZPointClick();
            Bitmap imageGPZDisable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver,area);
            GUI.GasStructLayer.get(driver).GPZPointClick();
            Bitmap imageGPZEnable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, area);
            Utils.ImageComparer comp = new Utils.ImageComparer(imageGPZDisable, imageGPZEnable);
            Assert.IsFalse(comp.IsEqual(), "Чекбоксы 'Газованя инфраструктура' и  'ГПЗ (точка)' активны, но слой на карте не отображается. ");
        }

        private void CheckEnableGasStruckAndDisableGPZPoint()
        {
            Bitmap imageFullCBEnable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, area);
            GUI.GasStructLayer.get(driver).GPZPointClick();
            Bitmap imageGPZDisable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, area);
            Utils.ImageComparer comp = new Utils.ImageComparer(imageFullCBEnable, imageGPZDisable);         
            Assert.IsFalse(comp.IsEqual(),"После снятия активности чекбокса слоя ГПЗ (точка), он карте остался отображаться.");
        }

        private void CheckDisableGasStructAndEnableGPZPoint()
        {
            GUI.GasStructLayer.get(driver).GPZPointClick();
            Bitmap imageFullCBEnable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver,area);
            GUI.Layers.get(driver).GasStructCheckBoxClick();
            Bitmap imageGasStruckDisable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, area);
            Utils.ImageComparer comp = new Utils.ImageComparer(imageFullCBEnable, imageGasStruckDisable);
            Assert.IsFalse(comp.IsEqual(),"После снятия чекбокса 'Газовая инфраструктура' слой остался отображаться на карте.");
        }

        private void CheckDisableGasStructAndDisableGPZPoint()
        {
            GUI.GasStructLayer.get(driver).GPZPointClick();
            Bitmap imageFullCBEnable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, area);
            GUI.Layers.get(driver).GasStructCheckBoxClick();
            GUI.GasStructLayer.get(driver).GPZPointClick();
            Bitmap imageFullCBDisable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, area);
            Utils.ImageComparer comp = new Utils.ImageComparer(imageFullCBEnable, imageFullCBDisable);
            Assert.IsFalse(comp.IsEqual(),"После снятия активности чекбоксов 'Газовая инфраструктура' и 'ГПЗ (точка)' слой на карте остался отображенным. ");
        }
    }

}
