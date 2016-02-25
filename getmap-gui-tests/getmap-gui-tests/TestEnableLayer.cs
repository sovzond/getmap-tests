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
            int x = listImgPointer[0].Location.X + listImgPointer[0].Size.Width * 2;
            int y = listImgPointer[0].Location.Y;
            area = new Rectangle(x, y, 300, 300);
            GUI.SlideMenu.get(driver).OpenLayers();
        }

        private void CheckEnableGasStructAndEnableGPZPoint()
        {
            GUI.Layers.get(driver).GasStructCheckBoxClick().GasStructOpenCloseList();
            GUI.Layers.GasStructClass.get(driver).GPZPointClick();
            Bitmap imageGPZDisable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver,area);
            GUI.Layers.GasStructClass.get(driver).GPZPointClick();
            Bitmap imageGPZEnable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, area);
            Utils.ImageComparer comp = new Utils.ImageComparer(imageGPZDisable, imageGPZEnable);
            bool equal = comp.IsEqual();
            if (equal)
                Assert.Fail("Чекбоксы 'Газованя инфраструктура' и  'ГПЗ (точка)' активны, но слой на карте не отображается. ");
        }

        private void CheckEnableGasStruckAndDisableGPZPoint()
        {
            Bitmap imageFullCBEnable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, area);
            GUI.Layers.GasStructClass.get(driver).GPZPointClick();
            Bitmap imageGPZDisable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, area);
            Utils.ImageComparer comp = new Utils.ImageComparer(imageFullCBEnable, imageGPZDisable);
            bool equal = comp.IsEqual();
            if (equal)
                Assert.Fail("После снятия активности чекбокса слоя ГПЗ (точка), он карте остался отображаться.");
        }

        private void CheckDisableGasStructAndEnableGPZPoint()
        {
            GUI.Layers.GasStructClass.get(driver).GPZPointClick();
            Bitmap imageFullCBEnable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver,area);
            GUI.Layers.get(driver).GasStructCheckBoxClick();
            Bitmap imageGasStruckDisable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, area);
            Utils.ImageComparer comp = new Utils.ImageComparer(imageFullCBEnable, imageGasStruckDisable);
            bool equal = comp.IsEqual();
            if (equal)
                Assert.Fail("После снятия чекбокса 'Газовая инфраструктура' слой остался отображаться на карте.");
        }

        private void CheckDisableGasStructAndDisableGPZPoint()
        {
            GUI.Layers.GasStructClass.get(driver).GPZPointClick();
            Bitmap imageFullCBEnable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, area);
            GUI.Layers.get(driver).GasStructCheckBoxClick();
            GUI.Layers.GasStructClass.get(driver).GPZPointClick();
            Bitmap imageFullCBDisable = Utils.CreateScreenshot.Instance.TakeScreenshot(driver, area);
            Utils.ImageComparer comp = new Utils.ImageComparer(imageFullCBEnable, imageFullCBDisable);
            bool equal = comp.IsEqual();
            if (equal)
                Assert.Fail("После снятия активности чекбоксов 'Газовая инфраструктура' и 'ГПЗ (точка)' слой на карте остался отображенным. ");
        }
    }

}
