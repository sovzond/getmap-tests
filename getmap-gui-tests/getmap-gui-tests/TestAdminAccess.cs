using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using adm = GetMapTest.GUI.Administration;
using OpenQA.Selenium;

namespace GetMapTest
{
    /// <summary>
    /// Тестирует вкладку 'Права доступа' раздела 'Администрирование'.
    /// </summary>
    [TestClass]
    public class TestAdminAccess
    {
        private IWebDriver driver;
        private const string locationSearchArea = "input.layerSearch";
        private const string locationLayersInTable = "td.layerName";
        private const string locationButtonClearSearch = "span.searchClear";
        private const string locationTematicLayers = "div.svzLayerManagerText";
        private const string locationMenuEditLayer = "#menuLayerEdit";
        private const string locationSelectedCBRead = "td.readTd.selected";
        private const string locationSelectedCBEdit = "td.editTd.selected";
        private const string locationThRolesName = "table.standartTable.permissionsTop > tr > th";
        private const string locationAllItemsInTable = "tr.permissionRow > td";
        private const string locationGroupLayersName = "td.groupName";
        private IList<IWebElement> listLayers;
        private IList<IWebElement> listReadSelected;
        private IList<IWebElement> listEditSelected;
        private enum Layers
        {
            Нефтянная_инфраструктура = 0,
            ДНС = 1,
            Кустовые_площадки = 2,
            Амбары = 3,
            Факелы = 4
        }

        [TestInitialize]
        public void Setup()
        {
            Login();
        }

        /// <summary>
        ///  Выполняет проверку на поиск по 'Наименование' слоя, при этом указает не полное 'Наименование' в разных регистрах.
        /// Поиск осуществляет путем нажатия клавищи 'Enter'.
        /// </summary>
        [TestMethod]
        public void CheckSearchAdminAccess()
        {
            MakeSearch("амбА");
            AssertOnSearch();
        }

        /// <summary>
        /// Выполняет проверку на отмену поиска путем нажатия кнопки 'Отмена' в текстовом поле для поиска,
        ///  а так же путем удаления текста в ручную.
        /// </summary>
        [TestMethod]
        public void CheckCancelResSearchAdminAccess()
        {
            MakeSearch("Факел");
            int before = adm.get(driver).CountLayers(listLayers, locationLayersInTable);
            driver.FindElement(By.CssSelector(locationButtonClearSearch)).Click();
            int after = adm.get(driver).CountLayers(listLayers, locationLayersInTable);
            Assert.IsTrue(before != after, "После клика по кнопке 'Очистить поиск', в таблице остались отображаться искомые элементы.");
            string searchAttribute = "днс";
            MakeSearch(searchAttribute);
            before = adm.get(driver).CountLayers(listLayers, locationLayersInTable);
            driver.FindElement(By.CssSelector(locationSearchArea)).Clear();
            driver.FindElement(By.CssSelector(locationSearchArea)).SendKeys(Keys.Enter);
            after = adm.get(driver).CountLayers(listLayers, locationLayersInTable);
            Assert.IsTrue(before != after, "После очистки поля для поиск путем удаления, в таблице остались отображаться искомые элементы.");
        }

        /// <summary>
        /// Выполняет проверку на присутствие только прав 'Чтение' тематического слоя 'Нефтяная инфраструктура'
        /// пользователя   'pasha'  роли 'ivan' .
        /// </summary>
        [TestMethod]
        public void CheckAccessRead()
        {
            bool assert = false;
            bool neft = false;
            DataPreparation();
            adm.get(driver).ActivateLayerRead("Нефтяная инфраструктура", (int)Layers.Нефтянная_инфраструктура);
            adm.get(driver).SaveExitCloseAccess();
            OpenSettingsButtonLayer();
            IList<IWebElement> listTematicLayers = driver.FindElements(By.CssSelector(locationTematicLayers));
            try
            {
                GUI.VectorButtonsLayer.get(driver).EditLayer();
            }
            catch (Exception)
            {
                assert = true;
            }
            Assert.IsTrue(assert, "После присвоение прав роли 'ivan' пользователю 'pasha'"
                + " только на чтение, ему присвоились права и на редактирование.");
            for (int i = 0; i < listTematicLayers.Count; i++)
            {
                if (listTematicLayers[i].Text == "Нефтяная инфраструктура")
                    neft = true;
            }
            Assert.IsTrue(neft, "После присвоение прав роли 'ivan' пользователю 'pasha'"
                + " только на чтение/редактирование группы слоев 'Нефтяная инфраструктура' ,"
                + " фактически на портале не отобразилась группа слоев 'Нефтяная инфраструктура'.");
            ClearData();
        }

        /// <summary>
        /// Выполняет проверку на присутствие как прав 'Чтение', так и 'Редактирование' тематического слоя 'Нефтяная инфраструктура'
        /// пользователя   'pasha'  роли 'ivan' .
        /// </summary>
        [TestMethod]
        public void CheckAccessEdit()
        {
            DataPreparation();
            adm.get(driver).ActiveLayerEdit("Нефтяная инфраструктура", (int)Layers.Нефтянная_инфраструктура);
            adm.get(driver).SaveExitCloseAccess();
            OpenSettingsButtonLayer();
            try
            {
                GUI.VectorButtonsLayer.get(driver).EditLayer();
                driver.FindElement(By.CssSelector(locationMenuEditLayer)).Click();
            }
            catch (Exception)
            {
                Assert.Fail("После присвоение прав роли 'ivan' пользователю 'pasha' на редактирование слоев группы 'Нефтяная инфраструктура'"
                    + " у пользователя не появилось возможности редактировать слой.");
            }
            ClearData();
        }

        /// <summary>
        /// Выполняет проверку на корректную работу кнопки 'Отмена'.
        /// </summary>
        [TestMethod]
        public void CheckButtonCancel()
        {
            DataPreparation();
            listReadSelected = driver.FindElements(By.CssSelector(locationSelectedCBRead));
            listEditSelected = driver.FindElements(By.CssSelector(locationSelectedCBEdit));
            int countActiveCBBefore = 0;
            adm.get(driver).ActiveLayerEdit("Нефтяная инфраструктура", (int)Layers.Нефтянная_инфраструктура);
            adm.get(driver).CancelClickAccess();
            listReadSelected = driver.FindElements(By.CssSelector(locationSelectedCBRead));
            listEditSelected = driver.FindElements(By.CssSelector(locationSelectedCBEdit));
            int countActiveCBAfter = 0;
            Assert.AreEqual(countActiveCBBefore, countActiveCBAfter, "После выполнения клика по кнопке 'Отмена', не были отменены последние изменения.");
            ClearData();
        }

        /// <summary>
        /// Выполняет проверку на корректную работу кнопки 'Фильтр'.
        /// </summary>
        [TestMethod]
        public void CheckFilterRole()
        {
            adm.get(driver).UseFilter();
            IList<IWebElement> listTh = driver.FindElements(By.CssSelector(locationThRolesName));
            List<string> lisThRoles = new List<string>();
            for (int i = 0; i < listTh.Count; i++)
            {
                if (listTh[i].Text != "Слои")
                    lisThRoles.Add(listTh[i].Text);
            }
            Assert.AreEqual(1, lisThRoles.Count, "С помощью кнопки 'Фильтр', " +
               "оставили в таблице только роль 'ivan', но в результате в таблице не одна роль, а: " + lisThRoles.Count.ToString());
        }

        /// <summary>
        /// Выполняет проверку на то, что при снятия активности чек бокса 'Чтение' группы слоев 'Нефтяная инфраструктура',
        ///  снимается активность чек бокса 'Редактирование'.
        /// </summary>
        [TestMethod]
        public void CheckReadDisActiveCB()
        {
            bool assert = false;
            DataPreparation();
            adm.get(driver).ActiveLayerEdit("Нефтяная инфраструктура", (int)Layers.Нефтянная_инфраструктура);
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).ActivateLayerRead("Нефтяная инфраструктура", (int)Layers.Нефтянная_инфраструктура);
            adm.get(driver).SaveExitCloseAccess();
            listEditSelected = driver.FindElements(By.CssSelector(locationSelectedCBEdit));
            listReadSelected = driver.FindElements(By.CssSelector(locationSelectedCBRead));
            Assert.AreEqual(0, listReadSelected.Count, "После снятия галочки с чек бокса 'Чтение' группы слоев"
                + " 'Нефтяная инфраструктура'чек бокс остался в активном состоянии.");
            Assert.AreEqual(0, listEditSelected.Count, "После снятия галочки с чек бокса 'Чтение' группы слоев"
                + " 'Нефтяная инфраструктура', чек бокс 'Редактирование' остался в активном состоянии. ");
            try
            {
                OpenSettingsButtonLayer();
            }
            catch (Exception)
            {
                assert = true;
            }
            Assert.IsTrue(assert, "После снятия галочки с чек бокса 'Чтение' группы слоев 'Нефтяная инфраструктура'" +
                " на портале у пользователя осталась возможность просматривать эту группы слоев.");
            ClearData();
        }

        /// <summary>
        /// Выполняет проверку на то, что если поставить в активное состояние чек бокс слоя 'Амбар',
        /// то и чек бокс группы слоев слоя 'Амбар' будет стоять в активном состоянии. 
        /// </summary>
        [TestMethod]
        public void CheckReadActiveCB()
        {
            DataPreparation();
            adm.get(driver).ActivateLayerRead("Нефтяная инфраструктура", (int)Layers.Амбары);
            listReadSelected = driver.FindElements(By.CssSelector(locationSelectedCBRead));
            listLayers = driver.FindElements(By.CssSelector(locationLayersInTable));
            for (int i = 0; i < listLayers.Count; i++)
            {
                if (listLayers[i].Text == "Нефтяная инфраструктура")
                {
                    Assert.AreEqual("readTd selected", listLayers[i + 1].GetAttribute("class"), "После выполнения клика"
                        + " по чек боксу слоя 'Амбар' группы слоев"
                 + " 'Нефтяная инфраструктура', чек бокс 'Нефтяная инфраструктура' не перешел в активное состояние.");
                }
            }
            adm.get(driver).SaveExitCloseAccess();
            try
            {
                OpenSettingsButtonLayer();
            }
            catch (Exception)
            {
                Assert.Fail("После выполнения клика по чек боксу слоя 'Амбар' группы слоев"
                + " 'Нефтяная инфраструктура', на портале не отобразилась группа слоев 'Нефтяная инфраструктура'.");
            }
            ClearData();
        }

        /// <summary>
        /// Выполняет проверку на альтернативные варианты.
        /// </summary>
        [TestMethod]
        public void CheckAltOptAdminAccess()
        {
            MakeSearch("Слой, отсутствующий в таблице");
            IList<IWebElement> listResTable = driver.FindElements(By.CssSelector(locationAllItemsInTable));
            Assert.AreEqual(0, listResTable.Count, "После ввода в поиск наименование слоя, не существующего в таблице, таблица отобразила: " +
                listResTable.Count + " слоев.");
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
            adm.get(driver).AccessClick();
            Assert.AreEqual(Settings.Instance.LinkAccess, driver.Url, "Не отобразилась вкладка 'Права доступа'.");
        }

        private void DataPreparation()
        {
            adm.get(driver).UsersClick();
            System.Threading.Thread.Sleep(2000);
            adm.get(driver).CreateUser();
            System.Threading.Thread.Sleep(2000);
            adm.get(driver).RoleClick();
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).CreateRole("admin");
            adm.get(driver).ClickOnEdit("ivan").AddUserInRole("pasha");
            adm.get(driver).ClickInputFromValue(adm.get(driver).CmdSave);
            System.Threading.Thread.Sleep(2000);
            adm.get(driver).AccessClick();
            System.Threading.Thread.Sleep(2000);
            adm.get(driver).UseFilter();
        }

        private void ClearData()
        {
            GUI.Cleanup.get(driver).Close();
            Login();
            adm.get(driver).UsersClick();
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).ClickOnDeleteUserOrRole();
            adm.get(driver).RoleClick();
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).ClickOnDeleteUserOrRole();

        }

        private void OpenSettingsButtonLayer()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver, Settings.Instance.BaseUrl).loginAsPasha();
            System.Threading.Thread.Sleep(1000);
            GUI.SlideMenu.get(driver).OpenLayers();
            System.Threading.Thread.Sleep(1000);
            GUI.Layers.get(driver).NeftyStructOpenCloseList().NeftyStructCheckBoxClick();
            System.Threading.Thread.Sleep(1000);
            GUI.NeftyStructLayer.get(driver).AmbarSBClick();
            System.Threading.Thread.Sleep(1000);
        }

        private void AssertOnSearch()
        {
            IList<IWebElement> listNameLayers = driver.FindElements(By.CssSelector(locationLayersInTable));
            IList<IWebElement> listNameGroupLayers = driver.FindElements(By.CssSelector(locationGroupLayersName));
            int idx = listNameLayers.Count;
            int iterator = 0;
            Assert.IsFalse(listNameGroupLayers.Count < 1, "После выполнения поиска по ключу 'амбА',"
                + "в таблице не отобразлась группа слоев 'Нефтяная инфраструктура' принадлежащая к этому слою.");
            Assert.IsFalse(listNameGroupLayers.Count > 1, "После выполнения поиска по ключу 'амбА',"
                + "в таблице  отобразлась не только  группа слоев 'Нефтяная инфраструктура' принадлежащая к этому слою.");
            while (iterator < idx)
            {
                Assert.IsTrue(listNameLayers[iterator].Text.StartsWith("Амба"), "После выполнения поиска по ключу 'амбА',"
                    + " таблица отобразила не только искомые элементы.");
                iterator++;
            }
        }

        private void MakeSearch(string key)
        {
            driver.FindElement(By.CssSelector(locationSearchArea)).Click();
            driver.FindElement(By.CssSelector(locationSearchArea)).SendKeys(key);
            driver.FindElement(By.CssSelector(locationSearchArea)).SendKeys(Keys.Enter);
        }

    }
}
