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
            CheckVectorMenuLayerFakel();

        }

        /// <summary>
        /// Выполняет проверку на отображение меню растрового слоя раздела 'Московская область'.
        /// </summary>
        [TestMethod]
        public void CheckVectorMenuMoscowArea()
        {
            CheckVectorMenuLayerLandsat();
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void CheckVectorMenuLayerFakel()
        {
            GUI.SlideMenu.get(driver).OpenLayers();
            GUI.Layers.get(driver).NeftyStructOpenCloseList();
            GUI.Layers.NeftyStructClass.get(driver).FakelSBClick();
            try
            {
                GUI.Layers.VectorButtonsClass.get(driver).StatisticsLayerClick();
            }
            catch (Exception)
            {
                Assert.Fail("Над слоем 'Факелы' не отобразилась  кнопка  - 'Статистика слоя'.");
            }
            try
            {
                GUI.Layers.VectorButtonsClass.get(driver).ZoomToLayerExtent();
            }
            catch (Exception)
            {
                Assert.Fail("Над слоем 'Факелы' не отобразилась  кнопка  - 'Приблежение к экстенту слоя'.");
            }
        }

        private void CheckVectorMenuLayerLandsat()
        {
            listButtons = driver.FindElements(By.CssSelector(locationListButtons));
            GUI.SlideMenu.get(driver).OpenLayers();
            GUI.Layers.get(driver).MoscowAreaOpenCloseList();
            GUI.Layers.MoscowAreaClass.get(driver).LandsatSBClick();
            try
            {
                for (int i = 0; i < listButtons.Count; i++)
                {
                    if (listButtons[i].Text == "Мозаика Landsat")
                    {
                        System.Threading.Thread.Sleep(500);
                        listButtons[i - 2].Click();
                    }
                }
            }
            catch(Exception)
            {
                Assert.Fail("Над слоем 'Мозаика Landsat' не отобразилась кнопка - 'Приблежение к экстенту слоя'. ");
            }
        }
    }
}
