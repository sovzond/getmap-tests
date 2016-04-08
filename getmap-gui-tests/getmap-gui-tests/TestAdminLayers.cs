using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using adm = GetMapTest.GUI.Administration;
using OpenQA.Selenium;
using System.Collections.Generic;
namespace GetMapTest
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class TestAdminLayers
    {
        private IWebDriver driver;
        private const string locationOpenGroupLayers = "td.showGroupLayers";
        private const string locationNameLayers = "td.dgrid-column-title.field-title";
        private const string locationGroupNameLayers = "table.standartTable span";
        private const string groupName = "Практиканты";
        private const string newGroupName = "Стажеры";
        private const string layerPoligon = "students_polygon";
        private const string layerPoint = "students_point";
        private const string layerLine = "students_line";
        private IList<IWebElement> listNameLayer;
        private IList<IWebElement> listGroupName;

        [TestInitialize]
        public void Setup()
        {
            Login();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CheckSearchClickAdminLayers()
        {
            adm.get(driver).MakeSearchClick("фАке");
            AssertOnSearch("Факел");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CheckSeracEnterAdminLayers()
        {
            adm.get(driver).MakeSearchEnter("фАке");
            AssertOnSearch("Факел");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CheckCancelResSearchAdminLayers()
        {
            adm.get(driver).MakeSearchClick("амБа");
            driver.FindElement(By.CssSelector(locationOpenGroupLayers)).Click();
            int before = adm.get(driver).CountLayers(listNameLayer, locationNameLayers);
            adm.get(driver).ClickCancelSearch();
            int after = adm.get(driver).CountLayers(listNameLayer, locationNameLayers);
            Assert.IsTrue(before != after, "После выполнения клика по кнопке 'Отмена' поиска, в результате поиск не отменился.");
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).MakeSearchClick("амБа");
            driver.FindElement(By.CssSelector(locationOpenGroupLayers)).Click();
            before = adm.get(driver).CountLayers(listNameLayer, locationNameLayers);
            adm.get(driver).ClearSearch();
            after = adm.get(driver).CountLayers(listNameLayer, locationNameLayers);
            Assert.IsTrue(before != after, "После очистки строки поиска путем удаления символов в ручную, в результате поиск не отменился.");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CheckCreateGroup()
        {
            bool assert = false;
            adm.get(driver).CreateGroup(groupName);
            listGroupName = driver.FindElements(By.CssSelector(locationGroupNameLayers));
            for (int i = 0; i < listGroupName.Count; i++)
            {
                if (listGroupName[i].Text == groupName)
                    assert = true;
            }
            Assert.IsTrue(assert, "После создания новыой группы, группа не создалась.");
            //удалить группы
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CheckCancelCreateGroup()
        {
            adm.get(driver).ClickInputFromValue(adm.get(driver).CmdCreateGroup);
            adm.get(driver).SetValueForGroupName(groupName);
            adm.get(driver).ClickInputFromValue(adm.get(driver).CmdCancel);
            listGroupName = driver.FindElements(By.CssSelector(locationGroupNameLayers));
            for (int i = 0; i < listGroupName.Count; i++)
            {
                Assert.IsFalse(listGroupName[i].Text == groupName, "После отмены создания группы слоев, группа действительно создалась.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CheckDeleteGroup()
        {
            //создать группы
            //удалить группу
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CheckEditGroup()
        {
            bool assert = false;
            adm.get(driver).CreateGroup(groupName);
            adm.get(driver).ClickOnEditGroup(groupName);
            adm.get(driver).SetValueForGroupName(newGroupName);
            adm.get(driver).ClickInputFromValue(adm.get(driver).CmdSave);
            listGroupName = driver.FindElements(By.CssSelector(locationGroupNameLayers));
            for (int i = 0; i < listGroupName.Count; i++)
            {
                if (listGroupName[i].Text == newGroupName)
                {
                    assert = true;
                    break;
                }

            }
            Assert.IsTrue(assert, "После выполнения редактирование 'Имя Группы' , изменения не были внесены. ");
            //удалить группу
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CheckAddLayers()
        {
            adm.get(driver).CreateGroup(groupName);
            bool assertPoint = false;
            bool assertPolygon = false;
            bool assertLine = false;
            AddLayerInGroup("students_line");
            AddLayerInGroup("students_point");
            AddLayerInGroup("students_polygon");
            ClickEyeButton(groupName);
            listNameLayer = driver.FindElements(By.CssSelector(locationNameLayers));
            for (int i = 0; i < listNameLayer.Count; i++)
            {
                if (listNameLayer[i].Text == layerLine)
                    assertLine = true;
                if (listNameLayer[i].Text == layerPoint)
                    assertPoint = true;
                if (listNameLayer[i].Text == layerPoligon)
                    assertPolygon = true;
            }
            Assert.IsTrue(assertLine, "После добавления слоя 'students_line' в группу 'Практиканты', слой не добавился.");
            Assert.IsTrue(assertPoint, "После добавления слоя 'students_point' в группу 'Практиканты', слой не добавился. ");
            Assert.IsTrue(assertPolygon, "После добавления слоя 'student_polygon' в группу 'Практиканты', слой не добавился. ");
            adm.get(driver).AccessClick();
            IList<IWebElement> listLayersInAccess = driver.FindElements(By.CssSelector("td.layerName"));
            for (int i = 0; i < listLayersInAccess.Count; i++)
            {
                if (listLayersInAccess[i].Text == layerLine)
                    assertLine = false;
                if (listLayersInAccess[i].Text == layerPoint)
                    assertPoint = false;
                if (listLayersInAccess[i].Text == layerPoligon)
                    assertPolygon = false;
            }
            Assert.IsFalse(assertLine, "После добавления слоя 'students_line' в группу слоев 'Практиканты' , на вкладке 'Права досутпа' слой не отобразился.");
            Assert.IsFalse(assertPoint, "После добавления слоя 'student_point' в группу слоев 'Практиканты' , на вкладке 'Права досутпа' слой не отобразился.");
            Assert.IsFalse(assertPolygon, "После добавления слоя 'students_polygon' в группу слоев 'Практиканты' , на вкладке 'Права досутпа' слой не отобразился.");
            adm.get(driver).UseFilter();
            adm.get(driver).ActivateLayerRead(groupName, 0);
            adm.get(driver).SaveExitCloseAccess();
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver, Settings.Instance.BaseUrl).login("pasha", "88");
            System.Threading.Thread.Sleep(2000);
            GUI.SlideMenu.get(driver).OpenLayers();
            System.Threading.Thread.Sleep(2000);
            IList<IWebElement> listTematicLayers = driver.FindElements(By.CssSelector("div.svzLayerManagerText"));
            for(int i=0;i<listTematicLayers.Count;i++)
            {
                if (listTematicLayers[i].Text == "Практиканты")
                {
                    listTematicLayers[i].Click();
                    break;
                }
                    
            }
            listTematicLayers = driver.FindElements(By.CssSelector("div.svzLayerManagerText"));
            for(int i=0;i<listTematicLayers.Count;i++)
            {
                if (listTematicLayers[i].Text == layerLine)
                    assertLine = true;
                if (listTematicLayers[i].Text == layerPoint)
                    assertPoint = true;
                if (listTematicLayers[i].Text == layerPoligon)
                    assertPolygon = true;
            }
            Assert.IsTrue(assertLine, "После добавления слоя 'students_line' в группу слоев 'Практиканты' , на портале этот слой не отобразился.");
            Assert.IsTrue(assertPoint, "После добавления слоя 'students_point' в группу слоев 'Практиканты' , на портале этото слой не отобразился.");
            Assert.IsTrue(assertPolygon, "После добавления слоя 'students_polygon' в группу слоев 'Практиканты' , на портале этот слой не отобразился.");
            //удалить группу          
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void Login()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver, Settings.Instance.AdminUrl).loginAsAdmin();
            Assert.AreEqual(Settings.Instance.AdminUrl, driver.Url, "Не отобразилась страница 'Администрирование'.");
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).LayersClick();
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual(Settings.Instance.LinkLayers, driver.Url, "Не отобразилась страница 'Слои'.");
            driver.Manage().Window.Maximize();
        }

        private void AssertOnSearch(string layerName)
        {
            driver.FindElement(By.CssSelector(locationGroupNameLayers)).Click();
            listGroupName = driver.FindElements(By.CssSelector(locationGroupNameLayers));
            listNameLayer = driver.FindElements(By.CssSelector(locationNameLayers));
            Assert.AreEqual(1, listGroupName.Count, "После поиска слоя " + layerName + " в действительности отобразилось: " + listGroupName.Count + " групп слоев.");
            Assert.AreEqual(1, listNameLayer.Count, "Посде поиска слоя " + layerName + " в действительности отобразилось: " + listNameLayer + "слоя.");
        }

        /// <summary>
        /// Выполняет команду в окне, в котором выбираем какой слой добавить в группу.
        /// </summary>
        /// <param name="cmd">Название команды('Добавить слой', 'Отмена')</param>
        private void AddCancelLayer(string cmd)
        {
            IList<IWebElement> listButtonsForCreateLayer = driver.FindElements(By.CssSelector("div.dopBtns > input"));
            for (int i = 0; i < listButtonsForCreateLayer.Count; i++)
            {
                if (listButtonsForCreateLayer[i].GetAttribute("value") == cmd)
                {
                    listButtonsForCreateLayer[i].Click();
                    break;
                }

            }
        }

        private void ChooseLayerForAddInGroup(string nameLayer)
        {
            driver.FindElement(By.CssSelector("#addLayer")).Click();
            IList<IWebElement> listLayers = driver.FindElements(By.CssSelector("div.container table.standartTable td"));
            for (int i = 0; i < listLayers.Count; i++)
            {
                if (listLayers[i].Text == nameLayer)
                {
                    listLayers[i].Click();
                    break;
                }
            }
        }

        private void AddLayerInGroup(string nameLayer)
        {
            ChooseLayerForAddInGroup(nameLayer);
            AddCancelLayer("Добавить слой");
        }

        private void ClickEyeButton(string nameLayer)
        {
            IList<IWebElement> listEyeButtons = driver.FindElements(By.CssSelector("tr[class^='group'] input.dijitReset.dijitCheckBoxInput"));
            listGroupName = driver.FindElements(By.CssSelector(locationGroupNameLayers));
            for (int i = 0; i < listGroupName.Count; i++)
            {
                if (listGroupName[i].Text == nameLayer)
                {
                    listEyeButtons[i].Click();
                    break;
                }
            }
        }
    }

}
