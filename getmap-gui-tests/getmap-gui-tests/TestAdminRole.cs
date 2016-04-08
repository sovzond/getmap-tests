using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using adm = GetMapTest.GUI.Administration;
using OpenQA.Selenium;

namespace GetMapTest
{
    /// <summary>
    /// Тестирует вкладку 'Роли' раздела  'Администрирование'.
    /// </summary>
    [TestClass]
    public class TestAdminRole
    {
        private IWebDriver driver;
        private const string locationRolesList = "#rolesList > option";
        private const string locationNames = "table.dgrid-row-table td.field-name";
        private const string locationLoginSortUp = "th.dgrid-column-name.field-name.dgrid-sort-up";
        private const string locationLoginSortDown = "th.dgrid-column-name.field-name.dgrid-sort-down";
        private const string locationAreaSearch = "input.searchString";
        private const string locationLinesTd = "table.dgrid-row-table td";
        private const string locationMessageNotFound = "div.dgrid-no-data";
        private const string locationMessageResNull = "div.dgrid-status";
        private const string notFoundText = "Данных не найдено";
        private IList<IWebElement> listNames;

        [TestInitialize]
        public void Setup()
        {
            Login();
        }

        /// <summary>
        /// Выполняет проверку на поиск по 'Наименование' роли, при этом указает не полное 'Наименование' в разных регистрах.
        /// Поиск осуществляет путем клика по кнопке 'Поиск'.
        /// </summary>
        [TestMethod]
        public void CheckSearchClickAdminRole()
        {
            adm.get(driver).CreateRole("admin");
            System.Threading.Thread.Sleep(500);
            adm.get(driver).MakeSearchClick("iVa");
            System.Threading.Thread.Sleep(1000);
            AssertForSearch("ivan");
            //удалить роль
        }

        /// <summary>
        /// Выполняет проверку на поиск путем нажатия клавиши 'Enter'.
        /// </summary>
        [TestMethod]
        public void CheckSearchEnterAdminRole()
        {
            adm.get(driver).CreateRole("admin");
            driver.FindElement(By.CssSelector(locationAreaSearch)).Click();
            driver.FindElement(By.CssSelector(locationAreaSearch)).SendKeys("iVa");
            driver.FindElement(By.CssSelector(locationAreaSearch)).SendKeys(Keys.Enter);
            AssertForSearch("ivan");
            //удалить роль
        }

        /// <summary>
        /// Выполняет проверку на отмену поиска путем нажатия кнопки 'Отмена' в текстовом поле для поиска,
        ///  а так же путем удаления текста в ручную.
        /// </summary>
        [TestMethod]
        public void CheckCancelResSearchAdminRole()
        {
            adm.get(driver).CreateRole("admin");
            adm.get(driver).MakeSearchClick("ivan");
            System.Threading.Thread.Sleep(1000);
            listNames = driver.FindElements(By.CssSelector(locationNames));
            int countBefore = listNames.Count;
            adm.get(driver).ClickCancelSearch();
            listNames = driver.FindElements(By.CssSelector(locationNames));
            int countAfter = listNames.Count;
            Assert.IsFalse(countBefore == countAfter, "После клика по кнопке 'Отмена поиска', поиск не был отменен и в таблице осталось"
                + " количество строк, соответствующих искомому элементу.");
            adm.get(driver).MakeSearchClick("ivan");
            System.Threading.Thread.Sleep(1000);
            listNames = driver.FindElements(By.CssSelector(locationNames));
            countBefore = listNames.Count;
            driver.FindElement(By.CssSelector(locationAreaSearch)).Clear();
            driver.FindElement(By.CssSelector(locationAreaSearch)).SendKeys(Keys.Delete);
            listNames = driver.FindElements(By.CssSelector(locationNames));
            countAfter = listNames.Count;
            Assert.IsFalse(countBefore == countAfter, "После удалению ключевого слова в поиске, в таблице осталось"
                + " количество строк, соответствующих искомому элементу.");
            //удалить роль
        }

        /// <summary>
        /// Выполняет проверку на создание новой роли.
        /// </summary>
        [TestMethod]
        public void CheckCreateRole()
        {
            bool assert = false;
            adm.get(driver).CreateRole("admin");
            listNames = driver.FindElements(By.CssSelector(locationNames));
            for(int i=0;i<listNames.Count;i++)
            {
                if (listNames[i].Text == "ivan")
                    assert = true;
            }
            Assert.IsTrue(assert, "После создания роли, роль не создалась.");
            //удалить роль
        }

        /// <summary>
        /// Выполняет проверку на добавление роли с типом роли 'Администратор' пользователю.
        /// </summary>
        [TestMethod]
        public void CheckAddAdminRoleUser()
        {
            Assert.IsTrue(DataPreparationForRoleAdd("admin"), "После добавления роли с типом ролей 'Администратор',"
                + " пользователю не присвоились права администратора.");
            //удалить роль
        }

        /// <summary>
        /// Выполняет проверку на добавление роли с типом роли 'Пользователь' пользователю.
        /// </summary>
        [TestMethod]
        public void CheckAddUserRoleUser()
        {            
            Assert.IsFalse(DataPreparationForRoleAdd("user"), "После добавления роли с типом ролей 'Пользователь',"
                + " пользователю  присвоились права администратора.");
            //удалить роль
        }

        /// <summary>
        /// Выполняет проверку на удаление роли.
        /// </summary>
        [TestMethod]
        public void CheckDeleteRole()
        {
            adm.get(driver).CreateRole("admin");
            //удалить роль           
        }
        /// <summary>
        /// Выполняет проверку на редактирование роли параметра 'Наименование'.
        /// </summary>
        [TestMethod]
        public void CheckEditRole()
        {
            bool assert = false;
            adm.get(driver).CreateRole("admin");
            adm.get(driver).ClickOnEdit("ivan").ChangeValueInNameRole("ivanko").ClickInputFromValue(adm.get(driver).CmdSave);
            adm.get(driver).UsersClick();
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).ClickOnEdit("Петрик");
            IList<IWebElement> listRoles = driver.FindElements(By.CssSelector(locationRolesList));
            for(int i=0;i<listRoles.Count;i++)
            {
                if (listRoles[i].Text == "ivanko")
                    assert = true;
            }
            Assert.IsTrue(assert, "После внесения изменений в наименование роли, "
                + " наименование не изменилось.");
            //удалить роль
        }

        /// <summary>
        /// Выполняет проверку на изменение сортировки по алфавиту колонки 'Наименование'.
        /// </summary>
        [TestMethod]
        public void CheckChangeSortRole()
        {
            IWebElement elDown = null;
            IWebElement elUp = null;
            System.Threading.Thread.Sleep(500);
            listNames = driver.FindElements(By.CssSelector(locationNames));
            string lastItemBefore = GetItemFromIndex(listNames.Count - 1);
            try
            {
                elUp = driver.FindElement(By.CssSelector(locationLoginSortUp));
            }
            catch (Exception)
            {
                Assert.Fail("В сортировке по наименованию отсутствует стрелка повернутая вверх.");
            }
            adm.get(driver).ChangeSortLogin();
            try
            {
                elDown = driver.FindElement(By.CssSelector(locationLoginSortDown));
            }
            catch (Exception)
            {
                Assert.Fail("В обратной сортировке по наименованию отсутствует стрелка повернутая вниз.");
            }
            listNames = driver.FindElements(By.CssSelector(locationNames));
            string firstItemAfter = GetItemFromIndex(0);
            Assert.AreEqual(lastItemBefore, firstItemAfter, "После выполнения сортировки по наименованию, сортировка не была произведена.");
        }

        /// <summary>
        /// Выполняет проверку на все альтернативные варианты, а именно:
        /// 1.Создание роли с существующим наименованием.2
        /// 2.Нажатие кнопки 'Отмена' во время создания новой роли.
        /// 3.Провера на поиск не существующей роли.
        /// </summary>
        [TestMethod]
        public void CheckAltOptAdminRole()
        {
            adm.get(driver).CreateRole("admin");
            AssertAlternative("После созданию роли с уже существующим логином, роль создалась.", adm.get(driver).CmdSave);
            AssertAlternative("После отмены создания роли, роль все равно создалась.", adm.get(driver).CmdCancel);
            AssertNotFound("Роль отсутствующая в таблице");
            //удалить пользователя
        }       

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void AssertAlternative(string message,string cmd)
        {        
            adm.get(driver).ClickInputFromValue(adm.get(driver).CmdCreateRole);
            adm.get(driver).SetValueForCreateRole("ivan", "user").ClickInputFromValue(cmd);
            GUI.Cleanup.get(driver).Close();
            Login();
            IList<IWebElement> listTd = driver.FindElements(By.CssSelector(locationLinesTd));
            for (int i = 0; i < listTd.Count; i++)
            {
                if (listTd[i].Text == "ivan")
                {
                    Assert.IsTrue(listTd[i + 1].Text == "Администратор", message);
                    break;
                }
            }          
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

        private void AssertForSearch(string role)
        {
            listNames = driver.FindElements(By.CssSelector(locationNames));
            Assert.AreEqual(1, listNames.Count, "После выполнения поиска роли" + role
                + " в таблице отобразилась не только роль " + role);
            Assert.AreEqual(role, listNames[0].Text, "После выполнения поиска роли " + role
                + ", роль не найдена.");
        }

        private void Login()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver,Settings.Instance.AdminUrl).loginAsAdmin();
            Assert.AreEqual(Settings.Instance.AdminUrl, driver.Url, "Не отобразилась страница 'Администрирование'.");
            System.Threading.Thread.Sleep(1000);
            adm.get(driver).RoleClick();
            Assert.AreEqual(Settings.Instance.LinkRole, driver.Url, "После клика по вкладке 'Роли', вкладка не открылась.");
            driver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Возвращает 'true' , если созданная роль, присвоенная пользователю соответствует типу переданному в качестве строкового параметра.
        /// </summary>
        /// <param name="type">Тип роли ('admin' - администратор, 'user' - пользовател).</param>
        /// <returns></returns>
        private Boolean DataPreparationForRoleAdd(string type)
        {
            adm.get(driver).CreateRole(type);
            adm.get(driver).ClickOnEdit("ivan");
            adm.get(driver).AddUserInRole("pasha");
            adm.get(driver).SaveExitClose();
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver, Settings.Instance.BaseUrl).login("pasha", "88");
            return adm.get(driver).AssertOnAdmin();
        }

        private string GetItemFromIndex(int index)
        {

            List<string> text = new List<string>();
            for (int i = 0; i < listNames.Count; i++)
            {
                text.Add(listNames[i].Text);
            }
            string item = listNames[index].Text;
            return item;
        }

    }

}
