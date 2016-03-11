using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;


namespace GetMapTest.GUI
{
    /// <summary>
    /// Открывает доступ ко всем элементам шапки.
    /// </summary>
    public class HeaderLinks
    {
        private IWebDriver driver;
        private const string locationHeaderLinks = "td.headerLinks a";
        private const string locationSearchArea = "input.searchPanel";
        private const string locationSearchButton = "#textSearch2";
        private const string help = "Справка";
        private const string changePassword = "Изменить пароль";
        private const string exit = "Выход";
        private Dictionary<string, IWebElement> dicButtons;
        IList<IWebElement> listHeaderLinks;

        private HeaderLinks(IWebDriver driver)
        {
            this.driver = driver;
            SetValueList();
            SetValueElements();
        }

        private HeaderLinks SetValueList()
        {
            dicButtons = new Dictionary<string, IWebElement>();
            listHeaderLinks = driver.FindElements(By.CssSelector(locationHeaderLinks));
            return this;
        }

        private HeaderLinks SetValueElements()
        {
            for (int i = 0; i < listHeaderLinks.Count; i++)
            {
                if (listHeaderLinks[i].GetAttribute("title") == "Справка")
                    dicButtons.Add(help,listHeaderLinks[i]);
                if (listHeaderLinks[i].GetAttribute("title") == "Изменить пароль")
                    dicButtons.Add(changePassword,listHeaderLinks[i]);
                if (listHeaderLinks[i].GetAttribute("title") == "Выход")
                    dicButtons.Add(exit,listHeaderLinks[i]);
            }
            return this;
        }

        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static HeaderLinks get(IWebDriver driver)
        {
            return new HeaderLinks(driver);
        }

        /// <summary>
        /// Выполняет поиск по геопорталу.
        /// </summary>
        /// <param name="attributeSearch">Искомый элемент</param>
        /// <returns></returns>
        public HeaderLinks MakeSearch(string attributeSearch)
        {
            driver.FindElement(By.CssSelector(locationSearchArea)).Click();
            driver.FindElement(By.CssSelector(locationSearchArea)).SendKeys(attributeSearch);
            driver.FindElement(By.CssSelector(locationSearchButton)).Click();
            return this;
        }

        /// <summary>
        /// Выполянет клик по кнопке 'Справка'.
        /// </summary>
        /// <returns></returns>
        public HeaderLinks HelpClick()
        {
            dicButtons[help].Click();
            return this;
        }

        /// <summary>
        /// Выполянет клик по кнопке 'Изменить пароль'.
        /// </summary>
        /// <returns></returns>
        public HeaderLinks ChangePasswordClick()
        {
            dicButtons[changePassword].Click();
            return this;
        }

        /// <summary>
        /// Выполянет клик по кнопке 'Выход'.
        /// </summary>
        /// <returns></returns>
        public HeaderLinks ExitClick()
        {
            dicButtons[exit].Click();
            return this;
        }

    }
}
