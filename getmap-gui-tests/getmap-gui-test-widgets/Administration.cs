using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace GetMapTest.GUI
{
    public class Administration
    {
        private IWebDriver driver;
        private IWebElement elementUsers;
        private IWebElement elementRole;
        private IWebElement elementAccess;
        private IWebElement elementLayers;
        private IWebElement elementButtonSearch;
        int idx;
        int idxAfter;
        private const string locationUsers = "#headTable td";
        private const string locationButtonSearch = "form.searchGrid input";
        private const string locationSearchArea = "input.searchString";
        private const string locationNameUsers = "td.field-name";
        private const string locationSettingsButtons = "table.dgrid-row-table span";
        private const string locationButtonExit = "table.noBorder #exit";
        private const string locationLoginForCreateUser = "#login";
        private const string locationMailForCreateUser = "#email";
        private const string locationFIOForCreateUser = "#name";
        private const string locationPWForCreateUser = "#password";
        private const string locationPW2ForCreateUser = "#password2";
        private const string locationNameRoleForChange = "#newName";
        private const string locationGroupNameForChange = "#userFormContent #title";
        private const string locationGroupNameLayers = "table.standartTable span";
        private const string locationInputs = "#fieldset input";
        private const string locationRoleList = "#rolesList > option";
        private const string locationUserRolesList = "#userRolesList > option";
        private const string locationButtonAddRoleUser = "table.noBorder span.arrowR";
        private const string locationButtonDeleteRoleUser = "table.noBorder span.arrowL";
        private const string locationThLogin = "th.dgrid-cell.dgrid-column-login.field-login.dgrid-sortable";
        private const string locationThEmail = "th.dgrid-cell.dgrid-column-email.field-email.dgrid-sortable";
        private const string locationThName = "th.dgrid-cell.dgrid-column-name.field-name.dgrid-sortable";
        private const string locationUserList = "#userList > option";
        private const string locationCBInFilter = "input.dijitReset.dijitCheckBoxInput";
        private const string locationInputsInFilter = "div.container input";
        private const string locationButtonCancelSearch = "#searchClear";
        private const string locationGroupLayersName = "td.groupName";
        private const string locationHeaderLinks = "td.headerLink a";
        private const string locationAllItemsInTable = "tr.permissionRow > td";
        private const string cmdCreateUser = "Создать пользователя";
        private const string cmdSave = "Сохранить";
        private const string cmdCancel = "Отмена";
        private const string cmdCreateRole = "Создать роль";
        private const string cmdCreateGroup = "Создать группу";
        private IList<IWebElement> listRoles;
        private IList<IWebElement> listUsers;
        private IList<IWebElement> listUserRoles;
        private IList<IWebElement> listHeaderTable;
        private IList<IWebElement> listButtons;
        private IList<IWebElement> listName;
        private IList<IWebElement> listSettingsButtons;
        private IList<IWebElement> listInputs;
        private IList<IWebElement> listGroupName;
        private IList<IWebElement> listLayers;
        private List<IWebElement> listRead;
        private List<IWebElement> listEdit;

        private Administration(IWebDriver driver)
        {
            this.driver = driver;
            SetValue();
            listRead = new List<IWebElement>();
            listEdit = new List<IWebElement>();
        }

        private void SetValue()
        {
            listHeaderTable = driver.FindElements(By.CssSelector(locationUsers));
            for (int i = 0; i < listHeaderTable.Count; i++)
            {
                if (listHeaderTable[i].Text == "Пользователи")
                    elementUsers = listHeaderTable[i];
                if (listHeaderTable[i].Text == "Роли")
                    elementRole = listHeaderTable[i];
                if (listHeaderTable[i].Text == "Права доступа")
                    elementAccess = listHeaderTable[i];
                if (listHeaderTable[i].Text == "Слои")
                    elementLayers = listHeaderTable[i];
            }

        }
        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static Administration get(IWebDriver driver)
        {
            return new Administration(driver);
        }

        /// <summary>
        /// Открыть вкладку 'Пользователи'.
        /// </summary>
        /// <returns></returns>
        public Administration UsersClick()
        {
            elementUsers.Click();
            return this;
        }

        /// <summary>
        /// Создает пользователя "pasha" с паролем "88"
        /// </summary>
        /// <returns></returns>
        public Administration CreateUser()
        {
            get(driver).ClickInputFromValue(get(driver).CmdCreateUser);
            get(driver).SetValueForCreateUser("pasha", "mail@mail.ru", "Петрик", "88", "88");
            get(driver).ClickInputFromValue(get(driver).CmdSave);
            return this;
        }

        /// <summary>
        /// Заполняет все необходимые значение для создания пользователя.
        /// </summary>
        /// <param name="login">Введите логин</param>
        /// <param name="mail">Введите E-Mail</param>
        /// <param name="fio">Введите ФИО</param>
        /// <param name="pw">Введите новый пароль</param>
        /// <param name="pw2">Повторите новый пароль</param>
        /// <returns></returns>
        public Administration SetValueForCreateUser(string login, string mail, string fio, string pw, string pw2)
        {
            SetValueInLogin(login);
            SetValueInMail(mail);
            SetValueInFIO(fio);
            SetValueInPassword(pw, pw2);
            return this;
        }

        /// <summary>
        /// Вводит значение в текстовое поле 'Логин'.
        /// </summary>
        /// <param name="value">Значение, которое будет переданно текстовому полю.</param>
        /// <returns></returns>
        public Administration SetValueInLogin(string value)
        {
            driver.FindElement(By.CssSelector(locationLoginForCreateUser)).Click();
            driver.FindElement(By.CssSelector(locationLoginForCreateUser)).Clear();
            driver.FindElement(By.CssSelector(locationLoginForCreateUser)).SendKeys(value);
            return this;
        }

        /// <summary>
        /// Вводит значение в текстовое поле 'E-Mail'.
        /// </summary>
        /// <param name="value">Значение, которое будет переданно текстовому полю.</param>
        /// <returns></returns>
        public Administration SetValueInMail(string value)
        {
            driver.FindElement(By.CssSelector(locationMailForCreateUser)).Click();
            driver.FindElement(By.CssSelector(locationMailForCreateUser)).Clear();
            driver.FindElement(By.CssSelector(locationMailForCreateUser)).SendKeys(value);
            return this;
        }

        /// <summary>
        /// Вводит значение в текстовое поле 'ФИО'.
        /// </summary>
        /// <param name="value">Значение, которое будет переданно текстовому полю.</param>
        /// <returns></returns>
        public Administration SetValueInFIO(string value)
        {
            driver.FindElement(By.CssSelector(locationFIOForCreateUser)).Click();
            driver.FindElement(By.CssSelector(locationFIOForCreateUser)).Clear();
            driver.FindElement(By.CssSelector(locationFIOForCreateUser)).SendKeys(value);
            return this;
        }

        /// <summary>
        /// Вводит значение в текстовое поле 'Подтверждение пароля'.
        /// </summary>
        /// <param name="pw">Значение, которое будет переданно текстовому полю 'Новый пароль'.</param>
        /// <param name="pw2">Значение, которое будет переданно текстовому полю 'Подтверждение пароля'.</param>
        /// <returns></returns>
        public Administration SetValueInPassword(string pw, string pw2)
        {
            driver.FindElement(By.CssSelector(locationPWForCreateUser)).Click();
            driver.FindElement(By.CssSelector(locationPWForCreateUser)).Clear();
            driver.FindElement(By.CssSelector(locationPWForCreateUser)).SendKeys(pw);
            driver.FindElement(By.CssSelector(locationPW2ForCreateUser)).Click();
            driver.FindElement(By.CssSelector(locationPW2ForCreateUser)).Clear();
            driver.FindElement(By.CssSelector(locationPW2ForCreateUser)).SendKeys(pw2);
            return this;
        }

        /// <summary>
        /// Добавляет выбранную роль пользователю.
        /// </summary>
        /// <param name="name">Имя роли</param>
        public void AddRoleInUser(string name)
        {
            listRoles = driver.FindElements(By.CssSelector(locationRoleList));
            for (int i = 0; i < listRoles.Count; i++)
            {
                if (listRoles[i].Text == name)
                {
                    listRoles[i].Click();
                    break;
                }
            }
            driver.FindElement(By.CssSelector(locationButtonAddRoleUser)).Click();
        }

        /// <summary>
        /// Удаляет выбранную роль пользователя.
        /// </summary>
        /// <param name="name">Имя роли</param>
        public void DeleteRoleInUser(string name)
        {
            listUserRoles = driver.FindElements(By.CssSelector(locationUserRolesList));
            for (int i = 0; i < listUserRoles.Count; i++)
            {
                if (listUserRoles[i].Text == name)
                {
                    listUserRoles[i].Click();
                    break;
                }
            }
            driver.FindElement(By.CssSelector(locationButtonDeleteRoleUser)).Click();
        }

        /// <summary>
        /// Открыть вкладку 'Роли'.
        /// </summary>
        /// <returns></returns>
        public Administration RoleClick()
        {
            elementRole.Click();
            return this;
        }

        /// <summary>
        /// Создает роль "ivan"
        /// </summary>
        /// <param name="type">Тип роли ('admin' - администратор, 'user' - пользовател).</param>
        public Administration CreateRole(string type)
        {
            get(driver).ClickInputFromValue(get(driver).CmdCreateRole);
            get(driver).SetValueForCreateRole("ivan", type);
            get(driver).ClickInputFromValue(get(driver).CmdSave);
            return this;
        }

        /// <summary>
        /// Вводит значение  в текстовое поле 'Имя роли'.
        /// </summary>
        /// <param name="value">Значение, которое будет переданно текстовому полю 'Имя роли'.</param>
        /// <returns></returns>
        public Administration ChangeValueInNameRole(string value)
        {
            driver.FindElement(By.CssSelector(locationNameRoleForChange)).Click();
            driver.FindElement(By.CssSelector(locationNameRoleForChange)).Clear();
            driver.FindElement(By.CssSelector(locationNameRoleForChange)).SendKeys(value);
            return this;
        }

        private Administration ClickRB(string type)
        {
            string location = "#" + type;
            driver.FindElement(By.CssSelector(location)).Click();
            return this;
        }

        private Administration SetValueInNameRole(string value)
        {
            driver.FindElement(By.CssSelector(locationFIOForCreateUser)).Click();
            driver.FindElement(By.CssSelector(locationFIOForCreateUser)).Clear();
            driver.FindElement(By.CssSelector(locationFIOForCreateUser)).SendKeys(value);
            return this;
        }

        /// <summary>
        /// Заполняет все необходимые значение для создания 
        /// </summary>
        /// <param name="name">Имя роли</param>
        /// <param name="type">Тип роли ('admin' - администратор, 'user' - пользовател).</param>
        /// <returns></returns>
        public Administration SetValueForCreateRole(string name, string type)
        {
            SetValueInNameRole(name);
            ClickRB(type);
            return this;
        }

        /// <summary>
        /// Добавляет выбранного пользователя в роль.
        /// </summary>
        /// <param name="name">Имя пользователя(логин).</param>
        public void AddUserInRole(string name)
        {
            listUsers = driver.FindElements(By.CssSelector(locationUserList));
            for (int i = 0; i < listUsers.Count; i++)
            {
                if (listUsers[i].Text == name)
                {
                    listUsers[i].Click();
                    break;
                }
            }
            driver.FindElement(By.CssSelector(locationButtonAddRoleUser)).Click();
        }

        /// <summary>
        /// Открывает вкладку 'Права доступа'.
        /// </summary>
        /// <returns></returns>
        public Administration AccessClick()
        {
            elementAccess.Click();
            return this;
        }

        private void DataPreparationForActiveLayer(string groupName)
        {
            listGroupName = driver.FindElements(By.CssSelector(locationGroupLayersName));
            string layerNeft = "";
            string layerAfterNeft = "";
            for (int i = 0; i < listGroupName.Count; i++)
            {
                if (listGroupName[i].Text == groupName)
                {
                    layerNeft = listGroupName[i].Text;
                    layerAfterNeft = listGroupName[i + 1].Text;
                    break;
                }
            }
            listLayers = driver.FindElements(By.CssSelector(locationAllItemsInTable));
            idx = 0;
            idxAfter = 0;
            for (int i = 0; i < listLayers.Count; i++)
            {
                if (listLayers[i].Text == layerNeft)
                    idx = i;
                if (listLayers[i].Text == layerAfterNeft)
                {
                    idxAfter = i;
                    break;
                }
            }
        }

        /// <summary>
        /// Выполняет клик по чек боксу 'Чтение' во вкладке 'Права доступа'.
        /// </summary>
        /// <param name="groupName">Название группы</param>
        /// <param name="idx">Название слоя (Передавать через enum Layers).</param>
        public void ActivateLayerRead(string groupName, int idx)
        {
            DataPreparationForActiveLayer(groupName);
            for (int i = this.idx; i < idxAfter; i++)
            {
                if (listLayers[i].GetAttribute("class") == "readTd")
                    listRead.Add(listLayers[i]);
            }
            listRead[idx].Click();
        }

        /// <summary>
        /// Выполняет клик по чек боксу 'Чтение' во вкладке 'Права досутпа'.
        /// </summary>
        /// <param name="groupName">Название группы</param>
        /// <param name="idx">Название слоя (Передавать через enum Layers).</param>
        public void ActiveLayerEdit(string groupName, int idx)
        {
            DataPreparationForActiveLayer(groupName);
            for (int i = this.idx; i < idxAfter; i++)
            {
                if (listLayers[i].GetAttribute("class") == "editTd")
                    listEdit.Add(listLayers[i]);
            }
            listEdit[idx].Click();
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Фильтр' на вкладке 'Права доступа'.
        /// </summary>
        /// <returns></returns>
        public Administration FilterClickAccess()
        {
            ClickOnInput("Фильтр");
            return this;
        }

        /// <summary>
        /// Выполняет фильтрацию на вкладке 'Права доступа',а точнее оставляет только одну роль : 'ivan'.
        /// </summary>
        public void UseFilter()
        {
            get(driver).FilterClickAccess();
            IList<IWebElement> listCB = driver.FindElements(By.CssSelector(locationCBInFilter));
            for (int i = 0; i < listCB.Count; i++)
            {
                if (listCB[i].GetAttribute("value") != "ivan")
                    listCB[i].Click();
            }
            IList<IWebElement> listContainerInputs = driver.FindElements(By.CssSelector(locationInputsInFilter));
            for (int i = 0; i < listContainerInputs.Count; i++)
            {
                if (listContainerInputs[i].GetAttribute("value") == "Сохранить")
                {
                    listContainerInputs[i].Click();
                    break;
                }
            }

        }

        /// <summary>
        /// Выполняет клик по кнопке 'Сохранить' на вкладке 'Права доступа'.
        /// </summary>
        /// <returns></returns>
        public Administration SaveClickAccess()
        {
            ClickOnInput("Сохранить");
            return this;
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Отмена' на вкладке 'Права доступа'.
        /// </summary>
        /// <returns></returns>
        public Administration CancelClickAccess()
        {
            ClickOnInput("Отмена");
            return this;
        }

        /// <summary>
        /// Сохраняет изменения, затем выходит из пользователя и закрывает вкладку.
        /// Данная функция относится ко вкладке 'Права доступа'.
        /// <returns></returns>
        public Administration  SaveExitCloseAccess()
        {
            get(driver).SaveClickAccess();
            get(driver).ExitClick();
            Cleanup.get(driver).Close();
            return this;
        }

        /// <summary>
        /// Выполняет клик по вкладке 'Слои'.
        /// </summary>
        /// <returns></returns>
        public Administration LayersClick()
        {
            elementLayers.Click();
            return this;
        }
        
        /// <summary>
        /// Запоняет поле 'Имя Группы'.
        /// </summary>
        /// <param name="value">Значение, которое хотите внести в поле.</param>
        /// <returns></returns>
        public Administration SetValueForGroupName(string value)
        {
            driver.FindElement(By.CssSelector(locationGroupNameForChange)).Clear();
            driver.FindElement(By.CssSelector(locationGroupNameForChange)).SendKeys(value);
            return this;
        }

        /// <summary>
        /// Создает новую группу.
        /// </summary>
        /// <param name="groupName">Название группы</param>
        public Administration CreateGroup(string groupName)
        {
            get(driver).ClickInputFromValue(get(driver).CmdCreateGroup);
            SetValueForGroupName(groupName);
            get(driver).ClickInputFromValue(get(driver).CmdSave);
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Редактировать'группы слоев.
        /// </summary>
        /// <param name="name">Название группы</param>
        public void ClickOnEditGroup(string name)
        {
            IList<IWebElement> listSB = driver.FindElements(By.CssSelector("tr[class^='group'] div.mapStructEdit"));
            IList<IWebElement> listGroup = driver.FindElements(By.CssSelector(locationGroupNameLayers));
            for (int i = 0; i < listGroup.Count; i++)
            {
                if (listGroup[i].Text == name)
                {
                    listSB[i].Click();
                    break;
                }
            }

        }

        private void ClickOnInput(string name)
        {
            IWebElement elementForClick = null;
            IList<IWebElement> listButtons = driver.FindElements(By.CssSelector("input.button"));
            for (int i = 0; i < listButtons.Count; i++)
            {
                if (listButtons[i].GetAttribute("value") == name)
                {
                    elementForClick = listButtons[i];
                    break;
                }
            }
            elementForClick.Click();
        }

        /// <summary>
        /// Выполнить поиск путем клика по кнопке 'Поиск'.
        /// </summary>
        /// <param name="attribute"></param>
        public void MakeSearchClick(string attribute)
        {
            listButtons = driver.FindElements(By.CssSelector(locationButtonSearch));
            for (int i = 0; i < listButtons.Count; i++)
            {
                if (listButtons[i].GetAttribute("type") == "submit")
                {
                    elementButtonSearch = listButtons[i];
                    break;
                }
            }
            driver.FindElement(By.CssSelector(locationSearchArea)).Click();
            driver.FindElement(By.CssSelector(locationSearchArea)).Clear();
            driver.FindElement(By.CssSelector(locationSearchArea)).SendKeys(attribute);
            elementButtonSearch.Click();
        }

        /// <summary>
        /// Выполняет поиск путем нажатия клавиши 'Enter'.
        /// </summary>
        /// <param name="attribute"></param>
        public void MakeSearchEnter(string attribute)
        {
            driver.FindElement(By.CssSelector(locationSearchArea)).Click();
            driver.FindElement(By.CssSelector(locationSearchArea)).Clear();
            driver.FindElement(By.CssSelector(locationSearchArea)).SendKeys(attribute);
            driver.FindElement(By.CssSelector(locationSearchArea)).SendKeys(Keys.Enter);
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Редактировать'.
        /// </summary>
        /// <param name="userName">Имя(ФИО пользователя, Наименование роли и т.д.).</param>
        /// <returns></returns>
        public Administration ClickOnEdit(string userName)
        {
            int idx = -1;
            listName = driver.FindElements(By.CssSelector(locationNameUsers));
            listSettingsButtons = driver.FindElements(By.CssSelector(locationSettingsButtons));
            for (int i = 0; i < listName.Count; i++)
            {
                if (listName[i].Text == userName)
                {
                    idx = i;
                    break;
                }
            }
            if (idx == -1)
                throw new Exception("Вы ввели некорретное 'ФИО', проще говоря, искомое 'ФИО' не найдено.");
            idx = idx * 2;
            listSettingsButtons[idx].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Удалить'.
        /// </summary>
        /// <param name="userName">Имя(ФИО пользователя, Наименование роли и т.д.).</param>
        /// <returns></returns>
        public Administration ClickOnDelete(string userName)
        {
            int idx = -1;
            listName = driver.FindElements(By.CssSelector(locationNameUsers));
            listSettingsButtons = driver.FindElements(By.CssSelector(locationSettingsButtons));
            for (int i = 0; i < listName.Count; i++)
            {
                if (listName[i].Text == userName)
                {
                    idx = i;
                    break;
                }
            }
            if (idx == -1)
                throw new Exception("Вы ввели некорретное 'ФИО', проще говоря, искомое 'ФИО' не найдено.");
            idx = (idx * 2) + 1;
            listSettingsButtons[idx].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Выход'.
        /// </summary>
        /// <returns></returns>
        public Administration ExitClick()
        {
            driver.FindElement(By.CssSelector(locationButtonExit)).Click();
            return this;
        }     

        /// <summary>
        /// Выполняет клик по элементу input исходя из его текстового значения.
        /// </summary>
        /// <param name="value">Текстовое значение для поиска элемента input</param>
        public void ClickInputFromValue(string value)
        {
            List<string> listValues = new List<string>();
            listInputs = driver.FindElements(By.CssSelector(locationInputs));
            for (int i = 0; i < listInputs.Count; i++)
            {
                listValues.Add(listInputs[i].GetAttribute("value"));
            }
            for (int i = 0; i < listInputs.Count; i++)
            {
                if (listInputs[i].GetAttribute("value") == value)
                {
                    listInputs[i].Click();
                    break;
                }
            }
        }

        /// <summary>
        /// Передает комманду для клика по кнопке 'Создать пользователя'.
        /// </summary>
        public string CmdCreateUser
        {
            get
            {
                return cmdCreateUser;
            }
        }

        /// <summary>
        /// Передает комманду для клика по кнопке 'Создать роль'.
        /// </summary>
        public string CmdCreateRole
        {
            get
            {
                return cmdCreateRole;
            }
        }

        /// <summary>
        /// Передает комманду для клика по кнопке 'Сохранить'.
        /// </summary>
        public string CmdSave
        {
            get
            {
                return cmdSave;
            }
        }

        /// <summary>
        /// Передает комманду для клика по кнопке 'Отмена'.
        /// </summary>
        public string CmdCancel
        {
            get
            {
                return cmdCancel;
            }
        }

        /// <summary>
        /// Передает комманду для клика по кнопке 'Создать группу'.
        /// </summary>
        public string CmdCreateGroup
        {
            get
            {
                return cmdCreateGroup;
            }
        }

        /// <summary>
        /// Сохраняет изменения, затем выходит из пользователя и закрывает вкладку.
        /// Данная функция относится ко вкладке 'Пользователи' и  'Роли'.
        /// </summary>
        public void SaveExitClose()
        {
            get(driver).ClickInputFromValue(get(driver).CmdSave);
            get(driver).ExitClick();
            Cleanup.get(driver).Close();
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Сортировка'(логина, наименование).
        /// </summary>
        /// <returns></returns>
        public Administration ChangeSortLogin()
        {
            driver.FindElement(By.CssSelector(locationThLogin)).Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Сортировка'('email').
        /// </summary>
        /// <returns></returns>
        public Administration ChangeSortEmail()
        {
            driver.FindElement(By.CssSelector(locationThEmail)).Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Сортировка'('ФИО').
        /// </summary>
        /// <returns></returns>
        public Administration ChangeSortName()
        {
            driver.FindElement(By.CssSelector(locationThName)).Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Отмена поиска'.
        /// </summary>
        /// <returns></returns>
        public Administration ClickCancelSearch()
        {
            driver.FindElement(By.CssSelector(locationButtonCancelSearch)).Click();
            return this;
        }

        /// <summary>
        /// Возвращает 'true' , если у пользователя есть права администратора.
        /// </summary>
        /// <returns></returns>
        public Boolean AssertOnAdmin()
        {
            bool assert = false;
            IList<IWebElement> listHeaderLinks = driver.FindElements(By.CssSelector(locationHeaderLinks));
            for (int i = 0; i < listHeaderLinks.Count; i++)
            {
                if (listHeaderLinks[i].GetAttribute("title") == "Администрирование")
                    assert = true;
            }
            return assert;
        }
        
        /// <summary>
        /// Возвращает место нахождения строки для поиска.
        /// </summary>
        public string LocationSearchArea
        {
            get
            {
                return locationSearchArea;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Int32 CountLayers(IList<IWebElement> listLayers,string location)
        {
            listLayers = driver.FindElements(By.CssSelector(location));
            int count = listLayers.Count;
            return count;
        }

        /// <summary>
        /// Выполняет очистку поиска путем нажатий на клавищу 'Backspace'.
        /// </summary>
        /// <returns></returns>
        public Administration ClearSearch()
        {
            driver.FindElement(By.CssSelector(get(driver).LocationSearchArea)).Clear();
            driver.FindElement(By.CssSelector(get(driver).LocationSearchArea)).SendKeys(Keys.Backspace);
            return this;
        }
    }
}
