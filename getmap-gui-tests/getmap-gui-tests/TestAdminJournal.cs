using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using adm = GetMapTest.GUI.Administration;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace GetMapTest
{
    /// <summary>
    /// Тестирует вкладку 'Журнал' раздела 'Администрирование'.
    /// </summary>
    [TestClass]
    public class TestAdminJournal
    {
        private IWebDriver driver;
        private const string locationHeaderFieldTime = "th.field-date";
        private const string locationSortPointerUpTime = "th.field-date.dgrid-sort-up";
        private const string locationSortPointerDownTime = "th.field-date.dgrid-sort-down";
        private const string locationFieldsInTime = "td.field-date";
        private const string locationHeaderFieldEvent = "th.field-eventName";
        private const string locationSortPointerUpEvent = "th.field-eventName.dgrid-sort-up";
        private const string locationSortPointerDownEvent = "th.field-eventName.dgrid-sort-down";
        private const string locationFieldsInEvent = "td.field-eventName";
        private const string locationFieldsInMessage = "td.field-message";
        private const string locationFieldsInUser = "td.field-user";
        private const string locationButtonCalendar = "span.gdrid-filter-datepicker";
        private const string locationHeaderDateInCalendar = "div.calendarTo div.navToPanel";
        private const string locationMonthInHeaderCalendar = " span.dojoxCalendarMonthLabelNode";
        private const string locationYearsInHeaderCalendar = " span.dojoxCalendarDayYearLabel";
        private const string locationSelectedDayInCalendar = "td.dijitCalendarCurrentMonth.dijitCalendarDateTemplate.dijitCalendarSelectedDate > div";
        private const string locationMonthsInCalendar = "div.calendarTo div.dojoxCalendarMonthLabel";
        private const string locationYearsInCalendar = "div.calendarTo div.dojoxCalendarYearLabel";
        private const string locationLeftCalendarDays = "div.calendarFrom div.dijitCalendarDateLabel";
        private const string locationRightCalendarDays = "div.calendarTo div.dijitCalendarDateLabel";
        private const string locationLeftCalendar = "div.calendarFrom";
        private const string locationRightCalendar = "div.calendarTo";
        private const string locationButtonsInCalendr = "div.datePickerDiv button";
        private const string locationDateLeftCalendar = "div.calendarFrom div.dojoxCalendarFooter";
        private const string locationDateRightCalendar = "div.calendarTo div.dojoxCalendarFooter";
        private const string locationErrorMessage = "div.dgrid-no-data";
        private const string cmdOK = "OK";
        private const string cmdShow = "Показать";
        private IList<IWebElement> listTime;
        private IList<IWebElement> listEvents;
        private IList<IWebElement> listRowsAfterSearch;
        private IList<IWebElement> listMessage;
        private IList<IWebElement> listUser;

        [TestInitialize]
        public void Setup()
        {
            Login();
        }

        /// <summary>
        /// Выполняет проверку на изменение сортировки по времени события колонки 'Время'.
        /// </summary>
        [TestMethod]
        public void CheckSortToTime()
        {
            AssertOnTableIsNull();
            string before = GetFirstItemInTime();
            driver.FindElement(By.CssSelector(locationHeaderFieldTime)).Click();
            AssertOnPointerUp(locationSortPointerUpTime, "Время");
            Assert.IsTrue(before != GetFirstItemInTime(), "После выполнения сортировки по полю 'Время', сортировкане была произведена.");
            driver.FindElement(By.CssSelector(locationHeaderFieldTime)).Click();
            AssertOnPointerDown(locationSortPointerDownTime, "Время");
            driver.FindElement(By.CssSelector(locationHeaderFieldEvent)).Click();
            AssertOnPointerUp(locationSortPointerUpEvent, "Тип события");
            listEvents = driver.FindElements(By.CssSelector(locationFieldsInEvent));
            before = listEvents[1].Text;
            driver.FindElement(By.CssSelector(locationHeaderFieldEvent)).Click();
            listEvents = driver.FindElements(By.CssSelector(locationFieldsInEvent));
            Assert.IsTrue(before != listEvents[1].Text, "После выполнения сортировки по полю 'Тип события', сортировка не была произведена.");
            AssertOnPointerDown(locationSortPointerDownEvent, "Тип события");
        }

        private void AssertOnPointerUp(string location, string column)
        {
            IWebElement elementUp = null;
            try
            {
                elementUp = driver.FindElement(By.CssSelector(location));
            }
            catch (Exception)
            {
                Assert.Fail("После выполнения сортировки по '" + column + "' , не отобразлась черная стрелочка повернутая вверх.");
            }
        }

        private void AssertOnPointerDown(string location, string column)
        {
            IWebElement elementDown = null;
            try
            {
                elementDown = driver.FindElement(By.CssSelector(location));
            }
            catch (Exception)
            {
                Assert.Fail("После выполнения сортировки по '" + column + "', не отобразлась черная стрелочка повернутая вниз.");
            }
        }

        private String GetFirstItemInTime()
        {
            listTime = driver.FindElements(By.CssSelector(locationFieldsInTime));
            string item = listTime[1].Text;
            return item;
        }


        /// <summary>
        /// Выполняет проверку на корректную работу инструмента 'Календарь'.
        /// </summary>
        [TestMethod]
        public void CheckCalendar()
        {
            AssertOnTableIsNull();
            GetFirstItemInTime();
            string first = listTime[1].Text.Substring(0, 2);
            OpenCalendar();
            IList<IWebElement> listDaysLeft = driver.FindElements(By.CssSelector(locationLeftCalendarDays));
            for (int i = 0; i < listDaysLeft.Count; i++)
            {
                if (listDaysLeft[i].Text == first)
                {
                    listDaysLeft[i].Click();
                    break;
                }
            }
            int next = Convert.ToInt32(first);
            next++;
            ClickOnCalendar(locationRightCalendarDays, next);
            ClickButtonInCalendar(cmdShow);
            GetFirstItemInTime();
            for (int i = 1; i < listTime.Count; i++)
            {
                Assert.IsTrue(listTime[i].Text.StartsWith(first), "После выполения запроса с помощью календаря,"
                    + " таблица отобразила не только запрашиваемый день.");
            }
        }

        /// <summary>
        /// Выполняет проверку на текущую дату, которую отображает календарь.
        /// </summary>
        [TestMethod]
        public void CheckCleanCalendar()
        {
            OpenCalendar();
            AssertOnDateCalendar(locationDateLeftCalendar, "Левый");
            AssertOnDateCalendar(locationDateRightCalendar, "Правый");
        }

        private void AssertOnDateCalendar(string location, string nameCalendar)
        {
            string dateLeftCalendarFull = driver.FindElement(By.CssSelector(location)).Text;
            string dateLeftCalendarDay = "";
            string dateLeftCalendarMonth = "";
            string dateLeftCalendarYear = "";
            string dateCurrentDay = DateTime.Now.Day.ToString();
            string dateCurrentMonth = DateTime.Now.Month.ToString();
            string dateCurrentYear = DateTime.Now.Year.ToString();
            if (dateCurrentDay.Length == 2)
            {
                dateLeftCalendarDay = dateLeftCalendarFull.Substring(7, 2);
                dateLeftCalendarMonth = dateLeftCalendarFull.Substring(10, 3);
                dateLeftCalendarYear = dateLeftCalendarFull.Substring(15, 4);
            }
            if (dateCurrentDay.Length == 1)
            {
                dateLeftCalendarDay = dateLeftCalendarFull.Substring(7, 1);
                dateLeftCalendarMonth = dateLeftCalendarFull.Substring(9, 3);
                dateLeftCalendarYear = dateLeftCalendarFull.Substring(14, 4);
            }
            Assert.AreEqual(dateCurrentDay, dateLeftCalendarDay, nameCalendar + " календарь отображает не верную текущую  дату (не верный день).");
            Assert.AreEqual(GetMonthFromNumber(4), dateLeftCalendarMonth, nameCalendar + " календарь отображает не верную текущую дату (не верный месяц).");
            Assert.AreEqual(dateCurrentYear, dateLeftCalendarYear, nameCalendar + " календарь отображает не верную текущую дату (не верный год).");
        }

        private String GetMonthFromNumber(int number)
        {
            string[] mounts = new string[] {"","янв","фев","мар","апр","май","июн",
                "июл","авг","сен","окт","ноя","дек"};
            return mounts[number];
        }

        /// <summary>
        /// Выполняет проверку на отображение изменений даты запросы в обоих календарях.
        /// </summary>
        [TestMethod]
        public void CheckChangeDateSelection()
        {
            OpenCalendar();
            driver.FindElement(By.CssSelector(locationHeaderDateInCalendar)).Click();
            System.Threading.Thread.Sleep(2000);
            IList<IWebElement> listMonths = driver.FindElements(By.CssSelector(locationMonthsInCalendar));
            for (int i = 0; i < listMonths.Count; i++)
            {
                if (listMonths[i].Text == "Янв.")
                {
                    listMonths[i].Click();
                    break;
                }
            }
            System.Threading.Thread.Sleep(2000);
            IList<IWebElement> listYears = driver.FindElements(By.CssSelector(locationYearsInCalendar));
            for (int i = 0; i < listYears.Count; i++)
            {
                if (listYears[i].Text == "2015")
                {
                    listYears[i].Click();
                    break;
                }
            }
            ClickButtonInSelection("OK");
            System.Threading.Thread.Sleep(1000);
            ClickOnCalendar(locationRightCalendarDays, 1);
            AssertOnSelectedDate(locationLeftCalendar);
            AssertOnSelectedDate(locationRightCalendar);
        }

        private void AssertOnSelectedDate(string location)
        {
            Assert.AreEqual("Январь", driver.FindElement(By.CssSelector(location + locationMonthInHeaderCalendar)).Text,
           "После изменений даты запроса, в календаре месяц не поменялся.");
            Assert.AreEqual("2015", driver.FindElement(By.CssSelector(location + locationYearsInHeaderCalendar)).Text,
                "После изменений даты запроса, в каледаре год не поменялся.");
            Assert.AreEqual("1", driver.FindElement(By.CssSelector(location + " " + locationSelectedDayInCalendar)).Text,
                "После выбора числа запроса, число не изменилось.");
        }

        /// <summary>
        /// Выполняет проверку календаря  на не выполнение запроса без нажатия кнопки 'Показать'.
        /// </summary>
        [TestMethod]
        public void CheckSoloCalendar()
        {
            AssertOnTableIsNull();
            int before = GetItemFromTime().Count;
            OpenCalendar();
            ClickOnCalendar(locationRightCalendarDays, 8);
            OpenCalendar();
            int after = GetItemFromTime().Count;
            Assert.IsTrue(before == after, "После выполнения клика ко числу в календаре, без нажатия кнопки 'Показать', календарь все таки выполнил запрос.");
        }

        /// <summary>
        /// Выполняет проверку на корректную работу поиска колонки 'Тип события'.
        /// </summary>
        [TestMethod]
        public void CheckSearchToEvent()
        {
            AssertOnTableIsNull();
            GetRowsAfterSearch(locationFieldsInEvent,"вхО");
            for (int i = 1; i < listRowsAfterSearch.Count; i++)
            {
                Assert.IsTrue(listRowsAfterSearch[i].Text == "вход-выход в систему", "После ввода в строку для поиска колонки 'Тип события' значение 'рОл' ,"
                    + " таблица отобразила не только атрибуты соответствующее искомому элементу.");
            }
        }

        /// <summary>
        /// Выполняет проверку на корректную работу поиска колонки 'Сообщение'.
        /// </summary>
        [TestMethod]
        public void CheckSearchToMessage()
        {
            AssertOnTableIsNull();
            GetRowsAfterSearch(locationFieldsInMessage, "пользователь В");
            for (int i = 1; i < listRowsAfterSearch.Count; i++)
            {
                Assert.IsTrue(listRowsAfterSearch[i].Text == "Пользователь вошёл в систему"
                    || listRowsAfterSearch[i].Text == "Пользователь вышел из системы", "После поиска по сообщению, " 
                    +  " в этой колонке отобразились не только элементы соответствующие запросу.");
            }
        }

        /// <summary>
        /// Выполняет проверку на корректную работу поиска колонки 'Пользователи'.
        /// </summary>
        [TestMethod]
        public void CheckSearchToUser()
        {
            AssertOnTableIsNull();
            GetRowsAfterSearch(locationFieldsInUser, "aD");
            for(int i=1;i<listRowsAfterSearch.Count;i++)
            {
                Assert.IsTrue(listRowsAfterSearch[i].Text == "admin@mail.ru","После поиска по пользователю, " 
                    + "в этой колонке отобразились не только элементы соответствующие запросу. ");
            }
        }

        /// <summary>
        /// Выполняет проверку на отображение изменений в таблице после создания нового пользователя.
        /// </summary>
        [TestMethod]
        public void CheckLastChanges()
        {
            adm.get(driver).UsersClick();
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).CreateUser();
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).JournalClick();
            System.Threading.Thread.Sleep(1000);
            listTime = driver.FindElements(By.CssSelector(locationFieldsInTime));
            listEvents = driver.FindElements(By.CssSelector(locationFieldsInEvent));
            listMessage = driver.FindElements(By.CssSelector(locationFieldsInMessage));
            int idx = -1;
            for (int i = 0; i < listMessage.Count; i++)
            {
                if (listMessage[i].Text.StartsWith("Создание пользователя Петрик"))
                {
                    idx = i;
                    break;
                }
            }
            Assert.IsTrue(listTime[idx].Text.StartsWith(DateTime.Now.Day.ToString()), "После создания нового пользователя,"
                + " Вкладка 'Журнал' не отобразила событие");
            Assert.IsTrue(listEvents[idx].Text == "изменение пользователя", "После создания нового пользователя на вкладке 'Журнал'"
                + " колонка 'Тип события' отобразила не верное событие");
            Assert.IsTrue(listMessage[idx].Text == "Создание пользователя Петрик", "После создания нового пользователя на вкладке 'Журнал'"
                + " колонка 'Тип события' отобразило не верное сообщение");
            adm.get(driver).UsersClick();
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).ClickOnDeleteUserOrRole();
        }

        /// <summary>
        /// Выполняет проверку на все альтернативные варианты, а именно:
        /// 1.Ввод не корректной даты запроса в календаре.
        /// 2.Поиск не существующего события в колонке 'Тип события'.
        /// 3.Поиск не существующего события в колонке 'Сообщение'.
        /// </summary>
        [TestMethod]
        public void CheckAltOptAdminJournal()
        {
            AssertNotCorrectDate();
            AssertOnTableIsNull();
            AssertEventNotFound(locationFieldsInEvent, "Тип события, отсутствующий в таблице.");
            System.Threading.Thread.Sleep(1000);
            ClearSearchArea(locationFieldsInEvent);
            AssertEventNotFound(locationFieldsInMessage, "Сообщение отсутствующие в таблице.");
        }

        private void AssertNotCorrectDate()
        {
            OpenCalendar();
            ClickOnCalendar(locationLeftCalendarDays, 14);
            ClickOnCalendar(locationRightCalendarDays, 8);
            IList<IWebElement> listSelectedDays = driver.FindElements(By.CssSelector(locationSelectedDayInCalendar));
            for (int i = 0; i < listSelectedDays.Count; i++)
            {
                Assert.IsTrue(listSelectedDays[i].Text == "8", "После выбора сначала в левом календаре число '14',"
                    + " затем в правом число '8', оба каледаря не отображают выделенное число '8'.");
            }
        }

        private void AssertEventNotFound(string location, string attribute)
        {
            try
            {
                MakeSearch(location, attribute);
            }
            catch (Exception)
            {
                Assert.Fail("После поиска события отсутствующего в таблице, таблица не отобразилась пустой.");
            }
            Assert.AreEqual("Данных не найдено", driver.FindElement(By.CssSelector(locationErrorMessage)).Text, "После поиска события/сообщения отсутствующего в таблице,"
               + " в таблице не оторбазился текст 'Данных не найдено'.");
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
            adm.get(driver).JournalClick();
            Assert.AreEqual(Settings.Instance.LinkJournal, driver.Url, "Не отобразилась вкладка 'Журнал'.");          
        }

        private void AssertOnTableIsNull()
        {
            listTime = driver.FindElements(By.CssSelector(locationFieldsInTime));
            if (listTime.Count < 2)
                Assert.Fail("Невозможно провести тест, так как таблица пуста.");
        }

        private IList<IWebElement> GetItemFromTime()
        {
            listTime = driver.FindElements(By.CssSelector(locationFieldsInTime));
            return listTime;
        }

        private void OpenCalendar()
        {
            driver.FindElement(By.CssSelector(locationButtonCalendar)).Click();
        }

        private void ClickButtonInCalendar(string cmd)
        {
            IList<IWebElement> listButtons = driver.FindElements(By.CssSelector(locationButtonCalendar));
            for (int i = 0; i < listButtons.Count; i++)
            {
                if (listButtons[i].Text == cmd)
                {
                    listButtons[i].Click();
                    break;
                }
            }

        }

        /// <summary>
        /// Выполняет клик по кнопкам в календаре в момент выбора месяца и года.
        /// </summary>
        /// <param name="cmd">Название кнопки ('OK' или 'Cancel')</param>
        private void ClickButtonInSelection(string cmd)
        {
            IList<IWebElement> listButtonsCalendarInSelection = driver.FindElements(By.CssSelector("td.dojoxCal-MY-btns button"));
            for(int i = 0; i<listButtonsCalendarInSelection.Count;i++)
            {
                if (listButtonsCalendarInSelection[i].Text == "OK")
                    listButtonsCalendarInSelection[i].Click();
    }
}

        private void ClickOnCalendar(string location, int number)
        {
            IList<IWebElement> listDays = driver.FindElements(By.CssSelector(location));
            for (int i = 0; i < listDays.Count; i++)
            {
                if (listDays[i].Text == number.ToString())
                {
                    listDays[i].Click();
                    break;
                }
            }
        }

        private void MakeSearch(string location, string attribute)
        {
            ClearSearchArea(location);
            driver.FindElement(By.CssSelector(location + " > input")).SendKeys(attribute);
        }

        private void ClearSearchArea(string location)
        {
            driver.FindElement(By.CssSelector(location + " > input")).Clear();
            driver.FindElement(By.CssSelector(location + " > input")).SendKeys(Keys.Enter);
        }


        private IList<IWebElement> GetRowsAfterSearch(string location,string attribute)
        {
            MakeSearch(location, attribute);
            listRowsAfterSearch = driver.FindElements(By.CssSelector(location));
            return listRowsAfterSearch;
        }


    }

}
