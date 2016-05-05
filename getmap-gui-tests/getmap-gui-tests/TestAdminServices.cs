using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OpenQA.Selenium;
using adm = GetMapTest.GUI.Administration;
namespace GetMapTest
{
    /// <summary>
    /// Тестирует вкладку 'Сервисы' раздела  'Администрирование'.
    /// </summary>
    [TestClass]
    public class TestAdminService
    {
        private IWebDriver driver;
        private bool assert;
        private IWebElement sortPointerUp;
        private IWebElement sortPointerDown;
        private const string locationNewNameLayer = "students_newLine";
        private const string nameSourceData = "students";
        private const string newNameSourceData = "trainees";
        private const string nameLayerForSearch = "stuDe";
        private const string nameLayerForPreview = "students_polygon";
        private const string cmdPublication = "Публикация";
        private const string cmdSourceData = "Источники данных";
        private const string cmdStyles = "Стили";
        private const string messageIsNull = "Данных не найдено";
        private const string locationButtonAddSourceData = "#addSource";
        private const string locationAreaNameSourceData = "input.required";
        private const string locationAreaDescriptionSourceData = "description";
        private const string locationSelectProtocol = "protocol";
        private const string locationProtocols = "table.noBorder option";
        private const string locationButtonsInAddSourceData = "tr.userFormBottom input";
        private const string locationButtonsInUploadLayer = "#userFormBottom > input";
        private const string locationButtonOpenSourcesData = "td.dijitDownArrowButton";
        private const string locationSourcesData = "td.dijitMenuItemLabel";
        private const string locationDeleteButton = "div.mapStructDelete";
        private const string locationEditButton = "div.mapStructEdit";
        private const string locationPreviewButton = "div.mapStructPreview";
        private const string locationInformationButton = "div.mapStructInform";
        private const string baseSourceForSearch = @"http://192.168.11.150:8080/geoserver/sovzond/wms";
        private const string locationServicesItems = "td.field-layers";
        private const string locationHeadingItems = "td.field-title";
        private const string locationNameLayerItems = "td.field-name";
        private const string locationStyleItems = "td.field-styleName";
        private const string locationSortButtonServices = "th.field-layers";
        private const string locationSortButtonHeading = "th.field-title";
        private const string locationSortButtonNameLayer = "th.field-name";
        private const string locationSortButtonStyle = "th.field-styleName";
        private const string locationSortPointerUp = "th.dgrid-sort-up";
        private const string locationSortPointerDown = "th.dgrid-sort-down";
        private const string locationZoomPlusButton = "a.olControlZoomIn";
        private const string locationZoonMinusButton = "a.olControlZoomOut";
        private const string locationImgInPreview = "div.boxShadow img.olTileImage";
        private const string locationLinks = "div.Services > p";
        private const string locationNameLayerInInformation = "span.layerInfoLabel";
        private const string locationMessegeIsNull = "div.dgrid-no-data";
        private const string locationButtonAddLayer = "#loadLayer";
        private const string locationAreaNameLayer = "name";
        private const string locationErrorMessage = "div.windowBlockTitle";
        IList<IWebElement> listItems;
        IList<IWebElement> listTitle;

        [TestInitialize]
        public void Setup()
        {
            Login();
            assert = false;
            sortPointerDown = null;
            sortPointerUp = null;
        }

        /// <summary>
        /// Выполняет проверку на добавление нового источника данных.
        /// </summary>
        [TestMethod]
        public void CheckAddSourceData()
        {
            CreateCancelDataSource(adm.get(driver).CmdSave);
            Assert.IsTrue(AssertOnExistDataSourceInList(), "После добавления нового источника данных, источник не появился в списке.");
            DeleteSourceData(nameSourceData);
        }

        /// <summary>
        /// Выполняет проверку на отмену добавления нового источника данных.
        /// </summary>
        [TestMethod]
        public void CheckCancelAddSourceData()
        {
            CreateCancelDataSource(adm.get(driver).CmdCancel);
            Assert.IsFalse(AssertOnExistDataSourceInList(), "После отмены создания нового источника данных, он все таки создался.");
        }

        /// <summary>
        /// Выполняет проверку на редактикрование источника данных
        /// </summary>
        [TestMethod]
        public void CheckEditSourceData()
        {
            CreateCancelDataSource(adm.get(driver).CmdSave);
            ChooseDataSource(nameSourceData);
            driver.FindElement(By.CssSelector(locationEditButton)).Click();
            driver.FindElement(By.CssSelector(locationAreaNameSourceData)).Clear();
            driver.FindElement(By.CssSelector(locationAreaNameSourceData)).SendKeys(newNameSourceData);
            ClickOnButtonsInAddSourceData(adm.get(driver).CmdSave);
            OpenListSourcesData();
            GetItemsIn(locationSourcesData);
            for (int i = 0; i < listItems.Count; i++)
            {
                if (listItems[i].Text == newNameSourceData)
                {
                    assert = true;
                    break;
                }
            }
            Assert.IsTrue(assert, "После изменения наименования источника данных,"
                + " источник данных не изменился.");
            DeleteSourceData(newNameSourceData);
        }

        /// <summary>
        /// Выполняет проверку на удаление источника данных
        /// </summary>
        [TestMethod]
        public void CheckDeleteSourceData()
        {
            CreateCancelDataSource(nameSourceData);
            System.Threading.Thread.Sleep(1000);
            DeleteSourceData(nameSourceData);
            OpenListSourcesData();
            GetItemsIn(locationSourcesData);
            for (int i = 0; i < listItems.Count; i++)
            {
                Assert.IsFalse(listItems[i].Text == nameSourceData, "После удаления источника данный, источник не удалился.");
            }
        }

        /// <summary>
        /// Выполняет проверку на поиск по колонке 'Сервис'.
        /// </summary>
        [TestMethod]
        public void CheckSearchService()
        {
            ChooseDataSource(baseSourceForSearch);
            MakeSearch(locationServicesItems, nameLayerForSearch);
            AssertOnSearchLayers(locationServicesItems);
        }

        /// <summary>
        /// Выполняет проверку на поиск по колонке 'Заголовок'.
        /// </summary>
        [TestMethod]
        public void CheckSearchHeading()
        {
            ChooseDataSource(baseSourceForSearch);
            MakeSearch(locationHeadingItems, nameLayerForSearch);
            AssertOnSearchLayers(locationHeadingItems);
        }

        /// <summary>
        /// Выполняет проверку на сортировку колонки 'Сервисы'.
        /// </summary>
        [TestMethod]
        public void CheckSortService()
        {
            AssertOnSortDataSource(locationServicesItems, locationSortButtonServices);
        }

        /// <summary>
        /// Выполняет проверку на сортировку колонки 'Заголовок'.
        /// </summary>
        [TestMethod]
        public void CheckSortHeading()
        {
            driver.FindElement(By.CssSelector(locationSortButtonHeading)).Click();
            AssertOnSortDataSource(locationHeadingItems, locationSortButtonHeading);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CheckPreviewDataSource()
        {
            ChooseDataSource(baseSourceForSearch);
            MakeSearch(locationServicesItems, nameLayerForSearch);
            System.Threading.Thread.Sleep(200);
            AssertOnPreview(nameLayerForPreview);
        }

        /// <summary>
        /// Выполняет проверку на поиск по колонке 'Наименование слоя'.
        /// </summary>
        [TestMethod]
        public void CheckSearchNameLayer()
        {
            AssertOnSearchInPublication(locationNameLayerItems);
        }

        /// <summary>
        /// Выполняет проверку на поиск по колонке 'Заголовок слоя'.
        /// </summary>
        [TestMethod]
        public void CheckSearchHeadingLayer()
        {
            AssertOnSearchInPublication(locationHeadingItems);
        }

        /// <summary>
        /// Выполняет проверку на поиск по колонке 'Стили'.
        /// </summary>
        [TestMethod]
        public void CheckSearchStyle()
        {
            OpenInService(cmdPublication);
            MakeSearch(locationStyleItems, "polygon");
            GetItemsIn(locationStyleItems);
            for (int i = 1; i < listItems.Count; i++)
            {
                if (listItems[i].Text != "polygon")
                {
                    assert = true;
                    break;
                }
            }
            Assert.IsFalse(assert, "После выполнения поиска по запросу 'polygon',"
                + "таблица отобразила не только элементы соответствующие запросу.");
        }

        /// <summary>
        /// Выполняет проверку на сортировку колонки 'Наименование слоя'.
        /// </summary>
        [TestMethod]
        public void CheckSortNameLayer()
        {
            OpenInService(cmdPublication);
            AssertOnSortInPublication(locationNameLayerItems, locationSortButtonNameLayer);
        }

        /// <summary>
        /// Выполняет проверку на сортировку колонки 'Заголовок слоя'.
        /// </summary>
        [TestMethod]
        public void CheckSortHeadingLayer()
        {
            OpenInService(cmdPublication);
            driver.FindElement(By.CssSelector(locationSortButtonHeading)).Click();
            AssertOnSortInPublication(locationHeadingItems, locationSortButtonHeading);
        }

        /// <summary>
        /// Выполняет проверку на сортировку колонки 'Стили'.
        /// </summary>
        [TestMethod]
        public void CheckSortStyle()
        {
            OpenInService(cmdPublication);
            driver.FindElement(By.CssSelector(locationSortButtonStyle)).Click();
            AssertOnSortInPublication(locationStyleItems, locationSortButtonStyle);
        }

        /// <summary>
        /// Выполняет проверку на корректную работу кнопки 'Предпросмотр слоя' подраздела 'Публикация'.
        /// </summary>
        [TestMethod]
        public void CheckPreviewPublication()
        {
            OpenInService(cmdPublication);
            MakeSearch(locationNameLayerItems, nameLayerForSearch);
            ClickOnPreview(nameLayerForPreview);
            AssertOnPreview(nameLayerForPreview);
        }

        /// <summary>
        /// Выполняет проверку на корректную работу кнопки 'Информация слоя' подраздела 'Публикация'.
        /// </summary>
        [TestMethod]
        public void CheckInformation()
        {
            IWebElement elementInformation = null;
            OpenInService(cmdPublication);
            MakeSearch(locationNameLayerItems, nameLayerForSearch);
            ClickOnInformation(nameLayerForPreview);
            System.Threading.Thread.Sleep(2000);
            try
            {
                elementInformation = driver.FindElement(By.CssSelector(locationNameLayerInInformation));
            }
            catch (Exception)
            {
                Assert.Fail("После выполнения клика по кнопке 'Информация',"
                    + " не отобразилось данное окно.");
            }
            Assert.IsTrue(elementInformation.Text == nameLayerForPreview, "В окне 'Информация' отобразился не тот текст,"
                + " который соответствует данному слою.");
        }

        /// <summary>
        /// Выполняет проверку на все альтернативные варианты, а именно:
        /// 1.Поиск сервиса отсутствующего в таблице.
        /// 2.Поиск заголовка отсутствующего в таблице.
        /// 3.Поиск слоя отсутствующего в таблице.
        /// 4.Поиск заголовка слоя отсутствующего в таблице.
        /// 5.Поиск стиля отсутствующего в таблице.
        /// 6.Проверка на добавление слоя без подгруженного файла.
        /// 7.Проверка на добавление слоя без указания его наименования.
        /// </summary>
        [TestMethod]
        public void CheckAltOptServices()
        {
            AssertOnSearchIsNull(locationServicesItems, "Сервис отсутствующий в таблице");
            System.Threading.Thread.Sleep(1000);
            AssertOnSearchIsNull(locationHeadingItems, "Заголовок отсутствующий в таблице");
            OpenInService(cmdPublication);
            MakeSearch(locationNameLayerItems, "Слой отсутствующий в таблице");
            AssertOnTableIsNullPublication("После выполнения отсутствующего слоя в таблице,"
                + "таблице не отобразилась пустой");
            ClearSearchArea(locationNameLayerItems);
            System.Threading.Thread.Sleep(1000);
            MakeSearch(locationHeadingItems, "Заголовок слоя отсутствующий в таблице");
            AssertOnTableIsNullPublication("После выполнения отсутствующего заголовка слоя в таблице,"
                + "таблице не отобразилась пустой");
            ClearSearchArea(locationHeadingItems);
            System.Threading.Thread.Sleep(1000);
            MakeSearch(locationStyleItems, "Стиль отсутствующий в таблице");
            AssertOnTableIsNullPublication("После выполнения поиска отсутствующего стиля в таблице,"
                + "таблице не отобразилась пустой");
            ClearSearchArea(locationStyleItems);
            System.Threading.Thread.Sleep(1000);
            AssertOnNotChooseFile();
            AssertOnNullNameLayer();
        }

        private void AssertOnNotChooseFile()
        {
            driver.FindElement(By.CssSelector(locationButtonAddLayer)).Click();
            driver.FindElement(By.Name(locationAreaNameLayer)).SendKeys(locationNewNameLayer);
            ClickOnButtonsInUploadLayer("Загрузить");
            listTitle = driver.FindElements(By.CssSelector(locationErrorMessage));
            for (int i = 0; i < listTitle.Count; i++)
            {
                if (listTitle[i].Text == "Произошла ошибка при загрузке файла")
                {
                    assert = true;
                    break;
                }
            }
            Assert.IsTrue(assert, "После попытки загрузить слой до того,"
                + " как выбран файл '.shp', высветилось не правильное сообщение об ошибке.");

        }

        private void AssertOnNullNameLayer()
        {
            driver.FindElement(By.Name(locationAreaNameLayer)).Clear();
            ClickOnButtonsInUploadLayer("Загрузить");
            listTitle = driver.FindElements(By.CssSelector(locationErrorMessage));
            for (int i = 0; i < listTitle.Count; i++)
            {
                if (listTitle[i].Text == "Укажите название слоя")
                {
                    assert = false;
                    break;
                }
            }
            Assert.IsFalse(assert, "После попытки загрузить слой до того,"
                    + " как указано 'Наименование слоя', высветилось не правильное сообщение об ошибке.");
        }

        private void AssertOnSearchIsNull(string location, string message)
        {
            MakeSearch(location, message);
            AssertOnTableIsNullDataSource();
            ClearSearchArea(location);
        }

        private void AssertOnTableIsNullDataSource()
        {
            try
            {
                Assert.IsTrue(driver.FindElement(By.CssSelector(locationMessegeIsNull)).Text == messageIsNull
                    , "После выполнения поиска отсутствующего элемента, таблица не отобразила текст 'Данных не найдено'.");
            }
            catch (Exception)
            {
                Assert.Fail("После выполнения поиска отсутствующего элемента, таблица не отобразилась пустой.");
            }
        }

        private void AssertOnTableIsNullPublication(string error)
        {
            GetItemsIn(locationNameLayerItems);
            Assert.IsTrue(listItems.Count == 1, error);
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
            adm.get(driver).ServicesClick();
            Assert.AreEqual(Settings.Instance.LinkServices, driver.Url, "После клика по вкладке 'Сервисы', вкладка не открылась.");
        }

        /// <summary>
        /// Выполняет создание или отмену создания нового источника данных.
        /// </summary>
        /// <param name="cmd">Команда определяющая создать или отменить создание источника данных
        ///  ('Сохранить', 'Отменить'.)</param>
        private void CreateCancelDataSource(string cmd)
        {
            SetValueForAddSourceData();
            ClickOnButtonsInAddSourceData(cmd);
        }

        private void SetValueForAddSourceData()
        {
            driver.FindElement(By.CssSelector(locationButtonAddSourceData)).Click();
            driver.FindElement(By.CssSelector(locationAreaNameSourceData)).SendKeys(nameSourceData);
            driver.FindElement(By.Name(locationAreaDescriptionSourceData)).SendKeys("Студенты");
        }

        private void ClickOnButtonsInAddSourceData(string cmd)
        {
            IList<IWebElement> listButtonsInAddSourceData = driver.FindElements(By.CssSelector(locationButtonsInAddSourceData));
            for (int i = 0; i < listButtonsInAddSourceData.Count; i++)
            {
                if (listButtonsInAddSourceData[i].GetAttribute("value") == cmd)
                {
                    listButtonsInAddSourceData[i].Click();
                    break;
                }
            }
        }

        private void ClickOnButtonsInUploadLayer(string cmd)
        {
            IList<IWebElement> listButtons = driver.FindElements(By.CssSelector(locationButtonsInUploadLayer));
            for (int i = 0; i < listButtons.Count; i++)
            {
                if (listButtons[i].GetAttribute("value") == cmd)
                {
                    listButtons[i].Click();
                    break;
                }
            }
        }

        private void OpenListSourcesData()
        {
            driver.FindElement(By.CssSelector(locationButtonOpenSourcesData)).Click();
        }

        private void ChooseDataSource(string name)
        {
            OpenListSourcesData();
            System.Threading.Thread.Sleep(2000);
            GetItemsIn(locationSourcesData);
            for (int i = 0; i < listItems.Count; i++)
            {
                if (listItems[i].Text == name)
                {
                    listItems[i].Click();
                    break;
                }
            }
        }

        private void DeleteSourceData(string name)
        {
            ChooseDataSource(name);
            driver.FindElement(By.CssSelector(locationDeleteButton)).Click();
            adm.get(driver).ClickOnButtonOK();
        }

        private void MakeSearch(string location, string attribute)
        {
            ClearSearchArea(location);
            driver.FindElement(By.CssSelector(location + " > input")).SendKeys(attribute);
        }

        private void ClearSearchArea(string location)
        {
            driver.FindElement(By.CssSelector(location + " > input")).Clear();
            driver.FindElement(By.CssSelector(location + " > input")).SendKeys(Keys.Backspace);
        }

        private void ClickOnPreview(string layerName)
        {
            IList<IWebElement> listButtons = driver.FindElements(By.CssSelector(locationPreviewButton));
            GetItemsIn(locationHeadingItems);
            for (int i = 0; i < listItems.Count; i++)
            {
                if (listItems[i].Text == layerName)
                {
                    listButtons[i - 1].Click();
                    break;
                }
            }
        }

        private void ClickOnInformation(string layerName)
        {
            IList<IWebElement> listButtons = driver.FindElements(By.CssSelector(locationInformationButton));
            GetItemsIn(locationHeadingItems);
            for (int i = 0; i < listItems.Count; i++)
            {
                if (listItems[i].Text == layerName)
                {
                    listButtons[i - 1].Click();
                    break;
                }
            }
        }

        private void OpenInService(string cmd)
        {
            IList<IWebElement> listLinks = driver.FindElements(By.CssSelector(locationLinks));
            for (int i = 0; i < listLinks.Count; i++)
            {
                if (listLinks[i].Text == cmd)
                {
                    listLinks[i].Click();
                    break;
                }
            }
        }

        private Boolean AssertOnExistDataSourceInList()
        {
            OpenListSourcesData();
            GetItemsIn(locationSourcesData);
            for (int i = 0; i < listItems.Count; i++)
            {
                if (listItems[i].Text == nameSourceData)
                {
                    assert = true;
                    break;
                }
            }
            return assert;
        }

        private void AssertOnSearchLayers(string location)
        {
            GetItemsIn(location);
            for (int i = 1; i < listItems.Count; i++)
            {
                Assert.IsFalse(listItems[i].Text != "students_line"
                    && listItems[i].Text != "students_point"
                    && listItems[i].Text != "students_polygon"
                    , "После выполнения поиска по запросу 'stuDe', таблица отобразила не только искомые элементы");
            }
        }

        /// <summary>
        /// Выполняет проверку на присутствие черной стрелочки сортировки,
        ///  а так на её корректное направление.
        /// </summary>
        /// <param name="location">Место находение стрелочки смотрящей вверх или вниз.</param>
        /// <param name="message">Сообщение если стрелка отсутствует ('Вверх' или 'Вниз').</param>
        private void AssertOnPointer(string location, string message)
        {
            try
            {
                sortPointerDown = driver.FindElement(By.CssSelector(location));
            }
            catch (Exception)
            {
                Assert.Fail("Отсутствует черная стрелочка указывающая порядок сортировки " + message);
            }
        }

        private void AssertOnOpenPreview(string nameLayer)
        {
            try
            {
                driver.FindElement(By.CssSelector(locationZoomPlusButton)).Click();
                driver.FindElement(By.CssSelector(locationZoonMinusButton)).Click();
            }
            catch (Exception)
            {
                Assert.Fail("После выполнения клика по кнопке 'Предпросмотр слоя' слоя " + nameLayer
                    + ", окно не открылось.");
            }
        }

        private void AssertOnPreview(string nameLayer)
        {
            char[] arrayForSubstring;
            ClickOnPreview(nameLayerForPreview);
            System.Threading.Thread.Sleep(2000);
            AssertOnOpenPreview(nameLayer);
            IList<IWebElement> listImg = driver.FindElements(By.CssSelector(locationImgInPreview));
            List<string> listSrc = new List<string>();
            for (int i = 0; i < listImg.Count; i++)
            {
                listSrc.Add(listImg[i].GetAttribute("src"));
            }
            for (int i = 0; i < listSrc.Count; i++)
            {
                arrayForSubstring = listSrc[i].ToCharArray();
                for (int j = 0; j < arrayForSubstring.Length; j++)
                {
                    if (arrayForSubstring[j] == 's')
                        if (arrayForSubstring[j + 1] == 't')
                            if (arrayForSubstring[j + 2] == 'u')
                                if (arrayForSubstring[j + 9] == 'p')
                                    if (arrayForSubstring[j + 10] == 'o')
                                        if (arrayForSubstring[j + 11] == 'l')
                                            assert = true;

                }
            }
            Assert.IsTrue(assert, "После выполнения клика по кнопке 'Предпросмотр слоя' слоя 'students_polygon',"
               + " отобразился не верный экстент.");
        }

        private void AssertOnSearchInPublication(string location)
        {
            OpenInService(cmdPublication);
            MakeSearch(location, nameLayerForSearch);
            AssertOnSearchLayers(locationNameLayerItems);
        }

        private void AssertOnSortDataSource(string locationItems, string locationSortButton)
        {
            GetItemsIn(locationItems);
            AssertOnPointer(locationSortPointerUp, "Вверх");
            string before = listItems[1].Text;
            driver.FindElement(By.CssSelector(locationSortButton)).Click();
            GetItemsIn(locationItems);
            AssertOnPointer(locationSortPointerDown, "Вниз");
            Assert.IsTrue(before != listItems[1].Text, "После выполнения сортировки,"
                + " сортировка не была произведена.");
        }

        private void AssertOnSortInPublication(string locationItems, string locationSortButton)
        {
            AssertOnPointer(locationSortPointerUp, "Вверх");
            GetItemsIn(locationItems);
            string before = listItems[1].Text;
            driver.FindElement(By.CssSelector(locationSortButton)).Click();
            AssertOnPointer(locationSortPointerDown, "Вниз");
            GetItemsIn(locationItems);
            Assert.IsTrue(before != listItems[1].Text, "После выполнения сортировки,"
                + " сортировка не была произведена.");
        }

        private IList<IWebElement> GetItemsIn(string location)
        {
            listItems = driver.FindElements(By.CssSelector(location));
            return listItems;
        }

    }

}
