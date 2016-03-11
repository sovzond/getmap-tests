using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Drawing;

namespace GetMapTest
{
    /// <summary>
    /// Выполняет проверку на отображение меню растрового слоя.
    /// </summary>
    [TestClass]
    public class TestVectorMenuLayer
    {
        private IWebDriver driver;
        private IList<IWebElement> listButtons;
        private const string locationListButtons = "div.svzLayerManagerItem.svzLayerManagerItem1 div";

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
        }

        /// <summary>
        /// Выполняет проверку на отображение меню растрового слоя раздела 'Нефтяная инфраструктура'.
        /// </summary>
        [TestMethod]
        public void CheckVectorMenuNeftyStruct()
        {
            GUI.SlideMenu.get(driver).OpenLayers();
            GUI.Layers.get(driver).NeftyStructOpenCloseList();
            GUI.NeftyStructLayer.get(driver).FakelSBClick();
            try
            {
                GUI.VectorButtonsLayer.get(driver).StatisticsLayerClick();
            }
            catch (Exception)
            {
                Assert.Fail("Над слоем 'Факелы' не отобразилась  кнопка  - 'Статистика слоя'.");
            }
            try
            {
                GUI.VectorButtonsLayer.get(driver).ZoomToLayerExtent();
            }
            catch (Exception)
            {
                Assert.Fail("Над слоем 'Факелы' не отобразилась  кнопка  - 'Приблежение к экстенту слоя'.");
            }
        }

        /// <summary>
        /// Выполняет проверку на отображение меню растрового слоя раздела 'Московская область'.
        /// </summary>
        [TestMethod]
        public void CheckVectorMenuMoscowArea()
        {
            listButtons = driver.FindElements(By.CssSelector(locationListButtons));
            GUI.SlideMenu.get(driver).OpenLayers();
            GUI.Layers.get(driver).MoscowAreaOpenCloseList();
            GUI.MoscowAreaLayer.get(driver).LandsatSBClick();
            try
            {
                for (int i = 0; i < listButtons.Count; i++)
                {
                    if (listButtons[i].Text == "Мозаика Landsat")
                    {
                        System.Threading.Thread.Sleep(500);
                        listButtons[i - 2].Click();
                        break;
                    }
                }
            }
            catch (Exception)
            {
                Assert.Fail("Над слоем 'Мозаика Landsat' не отобразилась кнопка - 'Приблежение к экстенту слоя'. ");
            }
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

    }
}
