using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using adm = GetMapTest.GUI.Administration;

namespace GetMapTest
{
    /// <summary>
    /// Тестирует вкладку 'Пользователи' раздела  'Администрирование'.
    /// </summary>
    [TestClass]
    public class TestSAdminUser
    {
        private IWebDriver driver;
        private const string locationLoginForCreateUser = "#login";
        private const string locationMailForCreateUser = "#email";
        private const string locationFIOForCreateUser = "#name";
        private const string locationPWForCreateUser = "#password";
        private const string locationPW2ForCreateUser = "#password2";
        private const string locationLogins = "table.dgrid-row-table td.field-login";
        private const string locationEmail = "table.dgrid-row-table td.field-email";
        private const string locationNames = "table.dgrid-row-table td.field-name";
        private const string locationLoginSortUp = "th.dgrid-column-login.field-login.dgrid-sort-up";
        private const string locationLoginSortDown = "th.dgrid-column-login.field-login.dgrid-sort-down";
        private const string locationEmailSortUp = "th.dgrid-column-email.field-email.dgrid-sort-up";
        private const string locationEmailSortDown = "th.dgrid-column-email.field-email.dgrid-sort-down";
        private const string locationNameSortUp = "th.dgrid-column-name.field-name.dgrid-sort-up";
        private const string locationNameSortDown = "th.dgrid-column-name.field-name.dgrid-sort-down";
        private const string locationAreaSearch = "input.searchString";
        private const string locationLoginErrorMessage = "input.required.error";
        private const string locationMessageNotFound = "div.dgrid-no-data";
        private const string locationMessageResNull = "div.dgrid-status";
        private const string locationUserNameInPortal = "td.headerLink #userName";
        private const string notFoundText = "Данных не найдено";

        private IList<IWebElement> listNames;

        [TestInitialize]
        public void Setup()
        {
            Login();
        }

        /// <summary>
        /// Выполняет проверку на поиск по 'ФИО' пользователя, при этом указает не полное 'ФИО' в разных регистрах.
        /// Поиск осуществляет путем клика по кнопке 'Поиск'.
        /// </summary>
        [TestMethod]
        public void CheckSearchClickAdminUsers()
        {
            System.Threading.Thread.Sleep(500);
            adm.get(driver).MakeSearchClick("зО");
            System.Threading.Thread.Sleep(1000);
            AssertForSearch("Зоя");
        }

        /// <summary>
        /// Выполняет проверку на поиск путем нажатия клавиши 'Enter'.
        /// </summary>
        [TestMethod]
        public void CheckSearchEnterAdminUsers()
        {
            driver.FindElement(By.CssSelector(locationAreaSearch)).Click();
            driver.FindElement(By.CssSelector(locationAreaSearch)).SendKeys("мвд");
            driver.FindElement(By.CssSelector(locationAreaSearch)).SendKeys(Keys.Enter);
            AssertForSearch("МВД");
        }

        /// <summary>
        /// Выполняет проверку на отмену поиска путем нажатия кнопки 'Отмена' в текстовом поле для поиска,
        ///  а так же путем удаления текста в ручную.
        /// </summary>
        [TestMethod]
        public void CheckCancelResSearchAdminUsers()
        {
            adm.get(driver).MakeSearchClick("мвд");
            System.Threading.Thread.Sleep(1000);
            listNames = driver.FindElements(By.CssSelector(locationNames));
            int countBefore = listNames.Count;
            adm.get(driver).ClickCancelSearch();
            listNames = driver.FindElements(By.CssSelector(locationNames));
            int countAfter = listNames.Count;
            Assert.IsFalse(countBefore == countAfter, "После клика по кнопке 'Отмена поиска', поиск не был отменен и в таблице осталось"
                + " количество строк, соответствующих искомому элементу.");
            adm.get(driver).MakeSearchClick("Туапсе");
            System.Threading.Thread.Sleep(1000);
            listNames = driver.FindElements(By.CssSelector(locationNames));
            countBefore = listNames.Count;
            adm.get(driver).ClearSearch();
            listNames = driver.FindElements(By.CssSelector(locationNames));
            countAfter = listNames.Count;
            Assert.IsFalse(countBefore == countAfter, "После удалению ключевого слова в поиске, в таблице осталось"
                + " количество строк, соответствующих искомому элементу.");
        }

        /// <summary>
        /// Выполняет проверку на создания нового пользователя.
        /// </summary>
        [TestMethod]
        public void CheckCreateUser()
        {
            adm.get(driver).CreateUser();
            adm.get(driver).SaveExitClose();
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver, Settings.Instance.BaseUrl).loginAsPasha();
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "После создания нового пользователя, не удалось авторизоваться под новой учетной записью.");
            GUI.Cleanup.get(driver).Close();
            Login();
            adm.get(driver).ClickOnDeleteUserOrRole(); 
        }

        /// <summary>
        /// Выполняет проверку на нажатия кнопки '' во время создания нового пользователя.
        /// </summary>
        [TestMethod]
        public void CheckCancelCreateUser()
        {
            adm.get(driver).ClickInputFromValue(adm.get(driver).CmdCreateUser);
            adm.get(driver).SetValueForCreateUser("pasha", "mail@mail.ru", "Петрик", "88", "88");
            adm.get(driver).ClickInputFromValue(adm.get(driver).CmdCancel);
            GUI.Cleanup.get(driver).Quit();
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver, Settings.Instance.BaseUrl).loginAsPasha();
            Assert.IsFalse(Settings.Instance.BaseUrl == driver.Url, "После нажатия кнопки 'Отмена' во время создания нового пользователя, пользователь не создался.");
        }

        /// <summary>
        /// Выполняет провеку на удаление пользователя.
        /// </summary>
        [TestMethod]
        public void CheckDeleteUser()
        {
           
            System.Threading.Thread.Sleep(2000);
            adm.get(driver).ClickOnDeleteUserOrRole();
            listNames = driver.FindElements(By.CssSelector(locationLogins));
            for(int i=0;i<listNames.Count;i++)
            {
                Assert.IsFalse(listNames[i].Text == "pasha", "После удаления пользователя, пользователь остался.");
            }
        }

        /// <summary>
        /// Выполняет проверку на редактирование пользователя параметра 'ФИО'.
        /// </summary>
        [TestMethod]
        public void CheckEditUser()
        {
            adm.get(driver).CreateUser();
            adm.get(driver).ClickOnEdit("Петрик").SetValueInFIO("Петруля");
            adm.get(driver).SaveExitClose();
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver, Settings.Instance.BaseUrl).loginAsPasha();
            System.Threading.Thread.Sleep(5000);
            IWebElement elementUserName = driver.FindElement(By.CssSelector(locationUserNameInPortal));
            Assert.AreEqual("Петруля", elementUserName.Text, "После изменение 'ФИО' пользователя, 'ФИО' не изменилось.");
            GUI.HeaderLinks.get(driver).ExitClick();
            GUI.Cleanup.get(driver).Close();
            Login();
            adm.get(driver).ClickOnEdit("Петруля").SetValueInFIO("Петрик");
            adm.get(driver).ClickInputFromValue(adm.get(driver).CmdSave);
            adm.get(driver).ClickOnDeleteUserOrRole();
        }

        /// <summary>
        /// Выполняет проверку на изменение сортировки по алфавиту колонки 'Логин'.
        /// </summary>
        [TestMethod]
        public void CheckChangeSortUsers()
        {
            IWebElement elDown = null;
            IWebElement elUp = null;
            IList<IWebElement> listLogins = driver.FindElements(By.CssSelector(locationLogins));
            IList<IWebElement> listEmails = driver.FindElements(By.CssSelector(locationEmail));
            IList<IWebElement> listNames = driver.FindElements(By.CssSelector(locationNames));
            string lastLoginBefore = listLogins[listLogins.Count - 1].Text;
            try
            {
                elUp = driver.FindElement(By.CssSelector(locationLoginSortUp));

            }
            catch (Exception)
            {
                Assert.Fail("В сортировке по логину отсутствует стрелка повернутая вверх.");
            }
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).ChangeSortLogin();
            try
            {
                elDown = driver.FindElement(By.CssSelector(locationLoginSortDown));
            }
            catch (Exception)
            {
                Assert.Fail("В обратной сортировке по логину отсутствует стрелка повернутая вниз.");
            }
            string firstLoginAfter = GetItemFromIndex(0, listLogins, locationLogins);
            Assert.AreEqual(lastLoginBefore, firstLoginAfter, "После выполнения сортировки по логину, сортировка не была произведена.");
            adm.get(driver).ChangeSortLogin();
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).ChangeSortEmail();
            lastLoginBefore = GetItemFromIndex(listEmails.Count - 1, listEmails, locationEmail);
            try
            {
                elUp = driver.FindElement(By.CssSelector(locationEmailSortUp));
            }
            catch (Exception)
            {
                Assert.Fail("В соритировке по Email отсутствует стрелка повернутая вврех.");
            }
            System.Threading.Thread.Sleep(2000);
            adm.get(driver).ChangeSortEmail();
            try
            {
                elDown = driver.FindElement(By.CssSelector(locationEmailSortDown));
            }
            catch (Exception)
            {
                Assert.Fail("В обратной сортировке по Email отсутствует стрелка повернутая вниз.");
            }
            firstLoginAfter = GetItemFromIndex(0, listEmails, locationEmail);
            Assert.AreEqual(lastLoginBefore, firstLoginAfter, "После выполнения сортировки по Email, сортировка не была произведена.");
            adm.get(driver).ChangeSortEmail();
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).ChangeSortName();
            lastLoginBefore = GetItemFromIndex(listNames.Count - 1, listNames, locationNames);
            try
            {
                elUp = driver.FindElement(By.CssSelector(locationNameSortUp));
            }
            catch (Exception)
            {
                Assert.Fail("В сортировке по ФИО отсутствует стрелка повернутая вверх.");
            }
            System.Threading.Thread.Sleep(2000);
            adm.get(driver).ChangeSortName();
            try
            {
                elDown = driver.FindElement(By.CssSelector(locationNameSortDown));
            }
            catch (Exception)
            {
                Assert.Fail("В обратной сортировке по ФИО отсутствует стрелка повернутая вниз.");
            }
            firstLoginAfter = GetItemFromIndex(0, listNames, locationNames);
            Assert.AreEqual(lastLoginBefore, firstLoginAfter, "После выполнения сортировки по ФИО , сортировка не была произведена.");
        }

        /// <summary>
        /// Выполняет проверку на все альтернативные варианты, а именно:
        /// 1.Создание пользователя с существующим логином.
        /// 2.Создание пользователя с существующим мейлом.
        /// 3.Создание нового пользователя с разными паролями(Пароль, подтверждение пароля).
        /// 4.Проверка на поиск не существующего пользователя.
        /// </summary>
        [TestMethod]
        public void CheckAltOptAdminUsers()
        {
            AssertAlternative("admin", "testmail@mail.ru", "88", "88", "После создания нового пользователя"
                 + " с существующим логином, пользователь действительно создался.", adm.get(driver).CmdSave);
            Login();
            AssertAlternative("ivan", "admin@mail.ru", "88", "88", "После создания нового пользователя"
                + " с существующим майлом, пользователь действительно создался", adm.get(driver).CmdSave);
            Login();
            AssertAlternative("ivan", "testmail@mail.ru", "11", "222", "После создания нового пользователя"
                 + " с разными паролями, пользователь действительно создался.", adm.get(driver).CmdSave);
            Login();
            AssertNotFound("фио отсутствующее в таблице");
        }

        /// <summary>
        /// Выполняет проверку на добавление роли в пользователя.
        /// </summary>
        [TestMethod]
        public void СheckAddRoleInUser()
        {
            adm.get(driver).CreateUser();
            adm.get(driver).ClickOnEdit("Петрик");
            adm.get(driver).AddRoleInUser("admin");
            adm.get(driver).SaveExitClose();
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver, Settings.Instance.BaseUrl).loginAsPasha();
            adm.get(driver).AssertOnAdmin();
            GUI.Cleanup.get(driver).Close();
            Login();
            System.Threading.Thread.Sleep(2000);
            adm.get(driver).ClickOnDeleteUserOrRole();
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void AssertAlternative(string login, string mail, string pw, string pw2, string errorMessage, string cmd)
        {
            adm.get(driver).ClickInputFromValue(adm.get(driver).CmdCreateUser);
            adm.get(driver).SetValueForCreateUser(login, mail, "Автотестирование", pw, pw2);
            adm.get(driver).ClickInputFromValue(cmd);
            if (pw != pw2)
            {
                try
                {
                    driver.FindElement(By.CssSelector(locationLoginErrorMessage));
                }
                catch (Exception)
                {
                    Assert.Fail("После ввода разных паролей, поле с паролем не подсветилось красный цветом");
                }
            }
            GUI.Cleanup.get(driver).Quit();
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver, Settings.Instance.AdminUrl).login(login, pw);
            Assert.IsFalse(Settings.Instance.BaseUrl == driver.Url, errorMessage);
            GUI.Cleanup.get(driver).Close();

        }

        private void AssertNotFound(string value)
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver, Settings.Instance.AdminUrl).loginAsAdmin();
            adm.get(driver).UsersClick();
            adm.get(driver).MakeSearchClick(value);
            IWebElement elementNotFound = driver.FindElement(By.CssSelector(locationMessageNotFound));
            IWebElement elementResNull = driver.FindElement(By.CssSelector(locationMessageResNull));
            Assert.AreEqual(notFoundText, elementNotFound.Text, "После ввода не существующего фио, в таблице не отобразился текст 'Данных не найдено'.");
            Assert.AreEqual('0', elementResNull.Text[0], "После ввода не существующего фио, в таблице не отобразился текст показывающий, что найдено результатов '0'.");
        }

        private void AssertForSearch(string user)
        {
            listNames = driver.FindElements(By.CssSelector(locationNames));
            Assert.AreEqual(1, listNames.Count, "После выполнения поиска пользователя" + user
                + " в таблице отобразился не только пользователь " + user);
            Assert.AreEqual(user, listNames[0].Text, "После выполнения поиска пользователя " + user
                + ", пользователь не найден.");
        }

        private void Login()
        {
            driver = Settings.Instance.createDriver();
            driver.Manage().Window.Maximize();
            GUI.Login.get(driver, Settings.Instance.AdminUrl).loginAsAdmin();
            Assert.AreEqual(Settings.Instance.AdminUrl, driver.Url, "Не отобразилась страница 'Администрирование'.");
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).UsersClick();
            Assert.AreEqual(Settings.Instance.LinkUsers, driver.Url, "После клика по вкладке 'Пользователи', вкладка не открылась.");       
        }

        private string GetItemFromIndex(int index, IList<IWebElement> list, string updateList)
        {
            list = driver.FindElements(By.CssSelector(updateList));
            List<string> text = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                text.Add(list[i].Text);
            }
            string item = list[index].Text;
            return item;
        }

    }

}
