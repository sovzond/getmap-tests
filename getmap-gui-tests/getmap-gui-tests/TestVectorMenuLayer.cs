using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Drawing;

namespace GetMapTest
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class TestVectorMenuLayer
    {
        private IWebDriver driver;
        private const string locationButtonsLayer = "div.svzLayerManagerItem.svzLayerManagerItem1 > div.svzSimpleButton.layerContextMenu";
        private const string locationButtonsVectorLayer = "div.userLayerMenuContainer.userLayerMenuContainerActive > div.svzSimpleButton";

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CheckVectorMenu()
        {
            CheckVectorMenuLayerFakel();
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void CheckVectorMenuLayerFakel()
        { 
            GUI.SlideMenu.get(driver).OpenLayers();
            System.Threading.Thread.Sleep(1000);
            GUI.Layers.NeftyStructClass.get(driver).OpenCloseList();
            GUI.Layers.NeftyStructClass.SettingsButtonsClass.get(driver).FakelSettingsButtonClick();
            try
            {
                GUI.Layers.NeftyStructClass.VectorButtonsClass.get(driver).StatisticsLayerClick().ZoomToLayerExtent();
            }
            catch(Exception)
            {
                Assert.Fail("Над слоем факелы не отобразились две кнопки  - 'Статистика слоя' и  'Приближение к экстенту слоя'.");
            }
        }
    }
}
