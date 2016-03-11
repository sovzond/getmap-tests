using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;

namespace GetMapTest
{
    /// <summary>
    /// Осуществляет проверку на изменение порядка отображения слоев.
    /// </summary>
    [TestClass]
    public class TestChangeSortLayers
    {
        private IWebDriver driver;
        Utils.TransformJS js;
        private int zIndex;
        private const string locationZindexFakel = "wms_Факелы";
        private const string locationZindexAmbar = "wms_Амбары";
        private const string locationZindexPlaces = "wms_Кустовые площадки";
        private const string locationZindexDNS = "wms_ДНС";
        private IList<IWebElement> listButtonsIncDec;
        private const string locationButtonsIncDec = "span.move img";
        private enum NumberButtons
        {
            Fakel = 0,
            Ambar = 2,
            Places = 4,
            Dns = 6
        }

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
            js = new Utils.TransformJS(driver);        
        }

        /// <summary>
        ///Перемещает каждый последний слой на позицию первого.
        ///</summary>
        [TestMethod]
        public void CheckIncrementLayers()
        {
            DataPreparation();
            IncrementLayer(NumberButtons.Dns, locationZindexDNS);
            IncrementLayer(NumberButtons.Places, locationZindexPlaces);
            IncrementLayer(NumberButtons.Ambar, locationZindexAmbar);
            IncrementLayer(NumberButtons.Fakel, locationZindexFakel);
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void DataPreparation()
        {
            GUI.SlideMenu.get(driver).OpenLayers();
            if (!GUI.Layers.get(driver).GetSelectedNeftyStruct)
                GUI.Layers.get(driver).NeftyStructCheckBoxClick();
            GUI.SlideMenu.get(driver).OpenLegenda();
            listButtonsIncDec = driver.FindElements(By.CssSelector(locationButtonsIncDec));
            zIndex = js.GetZIndex(locationZindexFakel);
        }
       
        private void IncrementLayer(NumberButtons nb,string locationzIndex)
        {
            for (int i = 0; i < 3; i++)
                listButtonsIncDec[(int)nb].Click();
            int zIndex = js.GetZIndex(locationzIndex);
            Assert.AreEqual(this.zIndex, zIndex, "Слой не отобразился выше предыдущего");
            Thread.Sleep(1000);
        }
    }
}
