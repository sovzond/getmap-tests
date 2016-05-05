using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using adm = GetMapTest.GUI.Administration;
using OpenQA.Selenium;
using System.Collections.Generic;
namespace GetMapTest
{
    /// <summary>
    /// Тестирует вкладку 'Слои' раздела 'Администрирование'.
    /// </summary>
    [TestClass]
    public class TestAdminLayers
    {
        private IWebDriver driver;
        private bool assertPoint;
        private bool assertPolygon;
        private bool assertLine;
        private const string groupName = "Практиканты";
        private const string newGroupName = "Стажеры";
        private const string searchFakel = "фАке";
        private const string resFakel = "Факел";
        private const string locationOpenGroupLayers = "td.showGroupLayers";
        private const string locationNameLayers = "td.dgrid-column-title.field-title";
        private const string locationGroupNameLayers = "table.standartTable span";
        private const string locationActiveCBInPortal = "div.dijit.dijitReset.dijitInline.dijitCheckBox.dijitCheckBoxChecked.dijitChecked";
        private const string locationTematicLayers = "div.svzLayerManagerText";
        private const string locationLayersInAccess = "td.layerName";
        private const string locationPreviewCB = "#previewMap_Window";
        private const string locationResSearch = "#resultDiv #nothing";
        private const string locationMap = ".olMap";
        private const string locationTextInIdentification = "div.boxShadow div";
        private const string locationButtonsForCreateLayer = "div.dopBtns > input";
        private const string locationButtonAddLayer = "#addLayer";
        private const string locationListLayersForAdd = "div.container table.standartTable td";
        private const string locationEyeCB = "tr[class^='group'] input.dijitReset.dijitCheckBoxInput";
        private const string locationSearchCB = "td[class$='field-searchable'] div > input";
        private const string locationIdentificationCB = "td[class$='field-identify'] div > input";
        private const string layerPoligon = "students_polygon";
        private const string layerPoint = "students_point";
        private const string layerLine = "students_line";
        private const string newLayer = "student_newName";
        private IList<IWebElement> listNameLayers;
        private IList<IWebElement> listGroupName;
        private IList<IWebElement> listTematicLayers;
        private IList<IWebElement> listLayersInAccess;

        [TestInitialize]
        public void Setup()
        {
            Login();
            assertPoint = false;
            assertPolygon = false;
            assertLine = false;
        }

        /// <summary>
        /// Выполняет проверку на поиск по 'Наименование' слоя, при этом указает не полное 'Наименование' в разных регистрах.
        /// Поиск осуществляет путем клика по кнопке 'Поиск'.
        /// </summary>
        [TestMethod]
        public void CheckSearchClickAdminLayers()
        {
            adm.get(driver).MakeSearchClick(searchFakel);
            AssertOnSearch(resFakel);
        }

        /// <summary>
        /// Выполняет проверку на поиск путем нажатия клавиши 'Enter'.
        /// </summary>
        [TestMethod]
        public void CheckSeracEnterAdminLayers()
        {
            adm.get(driver).MakeSearchEnter(searchFakel);
            AssertOnSearch(resFakel);
        }

        /// <summary>
        /// Выполняет проверку на отмену поиска путем нажатия кнопки 'Отмена' в текстовом поле для поиска,
        ///  а так же путем удаления текста в ручную.
        /// </summary>
        [TestMethod]
        public void CheckCancelResSearchAdminLayers()
        {
            adm.get(driver).MakeSearchClick(searchFakel);
            OpenGroupLayer();
            int before = adm.get(driver).CountLayers(listNameLayers, locationNameLayers);
            adm.get(driver).ClickCancelSearch();
            int after = adm.get(driver).CountLayers(listNameLayers, locationNameLayers);
            Assert.IsTrue(before != after, "После выполнения клика по кнопке 'Отмена' поиска, в результате поиск не отменился.");
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).MakeSearchClick(searchFakel);
            OpenGroupLayer();
            before = adm.get(driver).CountLayers(listNameLayers, locationNameLayers);
            adm.get(driver).ClearSearch();
            after = adm.get(driver).CountLayers(listNameLayers, locationNameLayers);
            Assert.IsTrue(before != after, "После очистки строки поиска путем удаления символов в ручную, в результате поиск не отменился.");
        }

        /// <summary>
        /// Выполняет проверку на создание новой группы слоев.
        /// </summary>
        [TestMethod]
        public void CheckCreateGroup()
        {
            bool assert = false;
            adm.get(driver).CreateGroup(groupName);
            for (int i = 0; i < GetGroupNames().Count; i++)
            {
                if (listGroupName[i].Text == groupName)
                    assert = true;
            }
            Assert.IsTrue(assert, "После создания новыой группы, группа не создалась.");
            adm.get(driver).ClickOnDeleteGroup(groupName);
        }

        /// <summary>
        /// Выполняет проверку на нажатие кнопки 'Отмена' во время создания новой группу.
        /// </summary>
        [TestMethod]
        public void CheckCancelCreateGroup()
        {
            adm.get(driver).ClickInputFromValue(adm.get(driver).CmdCreateGroup);
            adm.get(driver).SetValueForGroupName("tet");
            adm.get(driver).ClickInputFromValue(adm.get(driver).CmdCancel);
            for (int i = 0; i < GetGroupNames().Count; i++)
            {
                Assert.IsFalse(listGroupName[i].Text == "tet", "После отмены создания группы слоев, группа действительно создалась.");
            }
        }

        /// <summary>
        /// Выполняет проверку на удаление группы.
        /// </summary>
        [TestMethod]
        public void CheckDeleteGroup()
        {
            adm.get(driver).CreateGroup(groupName);
            adm.get(driver).ClickOnDeleteGroup(groupName);
            GetGroupNames();
            for (int i = 0; i < listGroupName.Count; i++)
            {
                Assert.IsFalse(listGroupName[i].Text == groupName, "После удаления группы слоев,"
                    + " группа слоев осталась в таблице.");
            }
        }

        /// <summary>
        /// Выполняет проверку на редактирование группы, а точнее параметра 'Наименование'.
        /// </summary>
        [TestMethod]
        public void CheckEditGroup()
        {
            bool assert = false;
            adm.get(driver).CreateGroup(groupName);
            adm.get(driver).ClickOnEditGroup(groupName);
            adm.get(driver).SetValueForGroupName(newGroupName);
            adm.get(driver).ClickInputFromValue(adm.get(driver).CmdSave);
            GetGroupNames();
            for (int i = 0; i <listGroupName.Count; i++)
            {
                if (listGroupName[i].Text == newGroupName)
                {
                    assert = true;
                    break;
                }

            }
            Assert.IsTrue(assert, "После выполнения редактирование 'Имя Группы' , изменения не были внесены. ");
            adm.get(driver).ClickOnDeleteGroup(groupName);
        }

        /// <summary>
        /// Выполняет проверку на добавление слоев в группу слоев.
        /// </summary>
        [TestMethod]
        public void CheckAddLayers()
        {
            DataPreparationFull();
            ClickEyeButton(groupName);
            GetNameLayers();
            for (int i = 0; i < listNameLayers.Count; i++)
            {
                if (listNameLayers[i].Text == layerLine)
                    assertLine = true;
                if (listNameLayers[i].Text == layerPoint)
                    assertPoint = true;
                if (listNameLayers[i].Text == layerPoligon)
                    assertPolygon = true;
            }
            Assert.IsTrue(assertLine, "После добавления слоя " + layerLine + " в группу 'Практиканты', слой не добавился.");
            Assert.IsTrue(assertPoint, "После добавления слоя " + layerPoint + " в группу 'Практиканты', слой не добавился. ");
            Assert.IsTrue(assertPolygon, "После добавления слоя " + layerPoligon + " в группу 'Практиканты', слой не добавился. ");
            DataPreparationAccess();
            GetLayersInAccess();
            for (int i = 0; i <listLayersInAccess.Count; i++)
            {
                if (listLayersInAccess[i].Text == layerLine)
                    assertLine = false;
                if (listLayersInAccess[i].Text == layerPoint)
                    assertPoint = false;
                if (listLayersInAccess[i].Text == layerPoligon)
                    assertPolygon = false;
                if (assertLine == false
                    && assertPoint == false
                    && assertPolygon == false)
                    break;
            }
            Assert.IsFalse(assertLine, "После добавления слоя 'students_line' в группу слоев 'Практиканты' , на вкладке 'Права досутпа' слой не отобразился.");
            Assert.IsFalse(assertPoint, "После добавления слоя 'student_point' в группу слоев 'Практиканты' , на вкладке 'Права досутпа' слой не отобразился.");
            Assert.IsFalse(assertPolygon, "После добавления слоя 'students_polygon' в группу слоев 'Практиканты' , на вкладке 'Права досутпа' слой не отобразился.");
            GotoPortalOpenGroup();
            for (int i = 0; i < GetTematicLayers().Count; i++)
            {
                if (listTematicLayers[i].Text == layerLine)
                    assertLine = true;
                if (listTematicLayers[i].Text == layerPoint)
                    assertPoint = true;
                if (listTematicLayers[i].Text == layerPoligon)
                    assertPolygon = true;
            }
            Assert.IsTrue(assertLine, "После добавления слоя " + layerLine + " в группу слоев 'Практиканты' , на портале этот слой не отобразился.");
            Assert.IsTrue(assertPoint, "После добавления слоя " + layerPoint + " в группу слоев 'Практиканты' , на портале этото слой не отобразился.");
            Assert.IsTrue(assertPolygon, "После добавления слоя " + layerPoligon + " в группу слоев 'Практиканты' , на портале этот слой не отобразился.");
            DataClear();
        }

        /// <summary>
        /// Выполняет проверку на инструменты 'Глаз' (видимость слоя при открытии портала).
        /// </summary>
        [TestMethod]
        public void CheckEye()
        {
            DataPreparationFull();           
            ClickEyeButton(groupName);
            DataPreparationAccess();
            GotoPortalOpenGroup();
            IList<IWebElement> listCBInPortal = driver.FindElements(By.CssSelector(locationActiveCBInPortal));
            Assert.AreEqual(4, listCBInPortal.Count, "После активация чекбокса 'видимость слоя' группы слоев 'Практиканты',"
                + " на портале видимость слою не присвоена.");
            DataClear();
        }

        /// <summary>
        /// Выполняет проверку на удаление слоя из группу слоев.
        /// </summary>
        [TestMethod]
        public void CheckDeleteLayerInGroup()
        {
            DataPreparationFull();
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).ClickOnDeleteLayer(layerPoligon);
            GetNameLayers();
            for(int i=0;i< listNameLayers.Count;i++)
                Assert.IsFalse(listNameLayers[i].Text == layerPoligon,"После удаления слоя: " + layerPoligon 
                    + " слой отобржается в таблице во вкладке 'Слои'.");
            DataPreparationAccess();
            GetLayersInAccess();
            for (int i = 0; i< listLayersInAccess.Count;i++)
            {
                Assert.IsFalse(listLayersInAccess[i].Text == layerPoligon, "После удаления слоя: " + layerPoligon
                    + " слой отобржается в таблице во вкладке 'Права доступа'.");
            }
            GotoPortalOpenGroup();
            for(int i=0;i<listTematicLayers.Count;i++)
            {
                Assert.IsFalse(listTematicLayers[i].Text == layerPoligon, "После удаления слоя: " + layerPoligon
                    + " слой отобржается на портале в разделе 'Тематические слои'.");
            }
            DataClear();
        }

        /// <summary>
        /// Выполняет проверку на редактирование слоя , а точнее параметра 'Наименование'.
        /// </summary>
        [TestMethod]
        public void CheckEditLayerInGroup()
        {
            bool assert = false;
            DataPreparationFull();
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).ClickOnEditLayer(layerLine);
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).SetValueForGroupName(newLayer);
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).ClickInputFromValue(adm.get(driver).CmdSave);
            System.Threading.Thread.Sleep(1000);
            DataPreparationAccess();
            for (int i = 0; i < GetLayersInAccess().Count; i++)
            {
                if (listLayersInAccess[i].Text == newLayer)
                    assert = true;
            }
            Assert.IsTrue(assert, "После изменения наименования слоя, слой не изменился на вкладке 'Права доступа'.");
            GotoPortalOpenGroup();
            for (int i = 0; i < GetTematicLayers().Count; i++)
            {
                if (listTematicLayers[i].Text == newLayer)
                    assert = false;
            }
            Assert.IsFalse(assert, "После изменения наименования слоя, слой не изменился на портале.");
            DataClear();
        }

        /// <summary>
        /// Выполняет проверку на инструмент 'Предпросмотр слоя'.
        /// </summary>
        [TestMethod]
        public void CheckLookLayer()
        {
            DataPreparationGroupLayer();
            adm.get(driver).ClickOnPreviewLayer(layerLine);
            try
            {
                driver.FindElement(By.CssSelector(locationPreviewCB)).Click();
            }
            catch (Exception)
            {
                Assert.Fail("После клика по кнопке 'Предпросмотр' слоя " + layerLine + " , окно не отоьразлиось. ");
            }
            adm.get(driver).ClickOnDeleteGroup(groupName);
        }

        /// <summary>
        /// Выполняет проверку на нажатие кнопки 'Отмена' во время выполнения редактирования слоя, а точнее параментра 'Наименование'.
        /// </summary>
        [TestMethod]
        public void CheckEditLayerCancel()
        {
            DataPreparationGroupLayer();
            adm.get(driver).ClickOnEditLayer(layerLine);
            adm.get(driver).SetValueForGroupName(newLayer);
            adm.get(driver).ClickInputFromValue(adm.get(driver).CmdCancel);
            GetNameLayers();
            for (int i = 0; i < listNameLayers.Count; i++)
            {
                Assert.IsFalse(listNameLayers[i].Text == newLayer, "После клика по кнопке 'Отмена' во время редактирования слоя,"
                    + " слой все таки внес изменения.");
            }
            adm.get(driver).ClickOnDeleteGroup(groupName);
        }

        /// <summary>
        /// Выполняет проверку на корректную работу чек бокса 'Поиск' слоя.
        /// </summary>
        [TestMethod]
        public void CheckSearchLayerInGroup()
        {
            DataPreparationFull();
            ClickEyeButton(groupName);
            ClickSearchCB(layerLine);
            DataPreparationAccess();
            GUI.Cleanup.get(driver).Close();
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver, Settings.Instance.BaseUrl).loginAsPasha();
            GUI.HeaderLinks.get(driver).MakeSearch(layerLine);
            System.Threading.Thread.Sleep(3000);
            Assert.IsFalse(driver.FindElement(By.CssSelector(locationResSearch)).Text == "Ничего не найдено", "Чек бокс на 'Поиcк' в разделе 'Слои' активен."
                + " После выполнения поиска слоя 'students_line' , результирующая таблица говорит что слой не найден.");
            DataClear();
        }

        /// <summary>
        /// Выполняет проверку на корректную работу чек бокса 'Идентификация' слоя.
        /// </summary>
        [TestMethod]
        public void CheckIdenteficationLayer()
        {          
            bool assertIden = false;
            bool assertTrueLayer = false;
            DataPreparationFull();
            ClickEyeButton(groupName);
            ClickIdentificationCB(layerLine);
            driver.Close();
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver, Settings.Instance.BaseUrl).loginAsPasha();
            GUI.InputCoordWnd.get(driver).setLon(35, 24, 18).setLat(-107, 33, 34).click();
            GUI.MenuNavigation.get(driver).IdentificationButton();
            driver.FindElement(By.CssSelector(locationMap)).Click();
            System.Threading.Thread.Sleep(5000);
            IList<IWebElement> listTextInIdentification = driver.FindElements(By.CssSelector(locationTextInIdentification));
            for (int i = 0; i < listTextInIdentification.Count; i++)
            {
                if (listTextInIdentification[i].Text == "ИДЕНТИФИКАЦИЯ")
                    assertIden = true;
                if (listTextInIdentification[i].Text == layerPoligon)
                    assertTrueLayer = true;
            }
            Assert.IsTrue(assertIden, "Чек бокс на 'Идентификация' в разделе 'Слои' активен."
                + " После выполнения клика инструментов 'Идентификация' по  слою " + layerPoligon + " , окно идентификации не отобразилось.");
            Assert.IsTrue(assertTrueLayer, "Чек бокс на 'Идентификация' в разделе 'Слои' активен."
                + " После выполнения клика инструментов 'Идентификация' по  слою " + layerPoligon + " , в окне идентификации отобразился не тот слой.");
            DataClear();
        }

        /// <summary>
        /// Выполняет проверку на все альтернативыне варианты, а именно:
        /// 1. Поиск слоя  отсутствующего в таблице.
        /// </summary>
        [TestMethod]
        public void CheckAltOptAdminLayers()
        {
            adm.get(driver).MakeSearchClick("Слой, отсутствующий в таблице");
            GetGroupNames();
            Assert.IsTrue(listGroupName.Count == 0, "После выполнения поиска слоя, отсутствующего в таблице, в действительности таблице не отобразилась пустой.");
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void Login()
        {
            driver = Settings.Instance.createDriver();
            driver.Manage().Window.Maximize();
            GUI.Login.get(driver, Settings.Instance.AdminUrl).loginAsAdmin();
            Assert.AreEqual(Settings.Instance.AdminUrl, driver.Url, "Не отобразилась страница 'Администрирование'.");
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).LayersClick();
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual(Settings.Instance.LinkLayers, driver.Url, "Не отобразилась страница 'Слои'.");
        }

        private void DataPreparationFull()
        {
            DataPreparationRoleUser();
            System.Threading.Thread.Sleep(1000);
            DataPreparationGroupLayer();
        }

        private void DataPreparationRoleUser()
        {
            adm.get(driver).UsersClick();
            System.Threading.Thread.Sleep(2000);
            adm.get(driver).CreateUser();
            System.Threading.Thread.Sleep(2000);
            adm.get(driver).RoleClick();
            System.Threading.Thread.Sleep(2000);
            adm.get(driver).CreateRole("user").ClickOnEdit("ivan").AddUserInRole("pasha");
            adm.get(driver).ClickInputFromValue(adm.get(driver).CmdSave);
        }

        private void DataPreparationGroupLayer()
        {
            adm.get(driver).LayersClick();
            adm.get(driver).CreateGroup(groupName);
            AddLayersStudents();
        }

        private void DataPreparationAccess()
        {
            adm.get(driver).AccessClick();
            adm.get(driver).UseFilter();
            adm.get(driver).ActivateLayerRead(groupName, 0);
            adm.get(driver).SaveClickAccess();
        }

        private void DataClear()
        {
            GUI.Cleanup.get(driver).Quit();
            Login();
            adm.get(driver).ClickOnDeleteGroup(groupName);
            adm.get(driver).UsersClick();
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).ClickOnDeleteUserOrRole();
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).RoleClick();
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).ClickOnDeleteUserOrRole();
        }

        private void AssertOnSearch(string layerName)
        {
            OpenGroupLayer();
            GetGroupNames();
            GetNameLayers();
            Assert.AreEqual(1, listGroupName.Count, "После поиска слоя " + layerName + " в действительности отобразилось: " + listGroupName.Count + " групп слоев.");
            Assert.AreEqual(1, listNameLayers.Count, "Посде поиска слоя " + layerName + " в действительности отобразилось: " + listNameLayers + "слоя.");
        }

        /// <summary>
        /// Выполняет команду в окне, в котором выбираем какой слой добавить в группу.
        /// </summary>
        /// <param name="cmd">Название команды('Добавить слой', 'Отмена')</param>
        private void AddCancelLayer(string cmd)
        {
            IList<IWebElement> listButtonsForCreateLayer = driver.FindElements(By.CssSelector(locationButtonsForCreateLayer));
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
            driver.FindElement(By.CssSelector(locationButtonAddLayer)).Click();
            System.Threading.Thread.Sleep(5000);
            IList<IWebElement> listLayers = driver.FindElements(By.CssSelector(locationListLayersForAdd));
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


        private void AddLayersStudents()
        {
            AddLayerInGroup(layerLine);
            AddLayerInGroup(layerPoint);
            AddLayerInGroup(layerPoligon);
        }

        private void ClickEyeButton(string nameGroup)
        {
            IList<IWebElement> listEyeCB = driver.FindElements(By.CssSelector(locationEyeCB));
            GetGroupNames();
            for (int i = 0; i <listGroupName.Count; i++)
            {
                if (listGroupName[i].Text == nameGroup)
                {
                    listEyeCB[i].Click();
                    break;
                }
            }
        }

        private void ClickSearchCB(string nameLayer)
        {
            ClickCB(nameLayer, locationSearchCB);
        }

        private void ClickIdentificationCB(string nameLayer)
        {
            ClickCB(nameLayer, locationIdentificationCB);
        }

        /// <summary>
        /// Выполняет блик по чек боксу слоя ('Поиск','Идентификация').
        /// </summary>
        /// <param name="nameLayer">Наименование слоя</param>
        /// <param name="location">Место нахождение чек бокса(Вызывается константа.)</param>
        private void ClickCB(string nameLayer, string location)
        {
            IList<IWebElement> listCB = driver.FindElements(By.CssSelector(location));
            for (int i = 0; i < GetNameLayers().Count; i++)
            {
                if (listNameLayers[i].Text == nameLayer)
                {
                    if (listCB[i].GetAttribute("aria-checked") != "true")
                    {
                        listCB[i].Click();
                        break;
                    }
                }
            }
        }

        private void GotoPortalOpenGroup()
        {
            GUI.Cleanup.get(driver).Quit();
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver, Settings.Instance.BaseUrl).loginAsPasha();
            System.Threading.Thread.Sleep(2000);
            GUI.SlideMenu.get(driver).OpenLayers();
            System.Threading.Thread.Sleep(2000);
            for (int i = 0; i < GetTematicLayers().Count; i++)
            {
                if (listTematicLayers[i].Text == groupName)
                {
                    listTematicLayers[i].Click();
                    break;
                }

            }
        }

        private void OpenGroupLayer()
        {
            driver.FindElement(By.CssSelector(locationOpenGroupLayers)).Click();
        }

        private IList<IWebElement> GetGroupNames()
        {
            listGroupName = driver.FindElements(By.CssSelector(locationGroupNameLayers));
            return listGroupName;
        }

        private IList<IWebElement> GetNameLayers()
        {
            listNameLayers = driver.FindElements(By.CssSelector(locationNameLayers));
            return listNameLayers;
        }

        private IList<IWebElement> GetTematicLayers()
        {
            listTematicLayers = driver.FindElements(By.CssSelector(locationTematicLayers));
            return listTematicLayers;
        }

        private IList<IWebElement> GetLayersInAccess()
        {
            listLayersInAccess = driver.FindElements(By.CssSelector(locationLayersInAccess));
            return listLayersInAccess;
        }
    }

}
