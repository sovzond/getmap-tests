using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace GetMapTest
{
    /// <summary>
    /// Содержит данные для запуска браузера и базовую ссылку.
    /// </summary>
    public sealed class Settings
    {

        private static readonly Settings instance = new Settings();

        private Settings() { }
   
        /// <summary>
        /// Создает объект класса Settings для доступа к его методам, свойствам. 
        /// </summary>
        public static Settings Instance
        {
            get
            {
                return instance;
            }
        }
        /// <summary>
        /// Выполняет запуск браузера.
        /// </summary>
        /// <returns></returns>
        public IWebDriver createDriver()
        {
            return new FirefoxDriver();
        }

        private const string baseUrl = "http://91.143.44.249/sovzond_students/portal/";
        private const string adminUrl = "http://91.143.44.249/sovzond_students/admin/";
        private const string linkUsers = "http://91.143.44.249/sovzond_students/admin/Users";
        private const string linkRole = "http://91.143.44.249/sovzond_students/admin/Roles?SelectedRow=1";
        private const string linkAccess = "http://91.143.44.249/sovzond_students/admin/Permissions";
        private const string linkLayers = "http://91.143.44.249/sovzond_students/admin/";

        /// <summary>
        /// Адрес базовой ссылки.
        /// </summary>
        public string BaseUrl
        {
            get
            {
                return baseUrl;
            }
        }

        /// <summary>
        /// Адрес ссылки на администрирование.
        /// </summary>
        public string AdminUrl
        {
            get
            {
                return adminUrl;
            }
        }

        /// <summary>
        /// Адрес ссылки на вкладку 'Пользователи'.
        /// </summary>
        public string LinkUsers
        {
            get
            {
                return linkUsers;
            }
        }

        /// <summary>
        /// Адрес ссылки на вкладку 'Роли'.
        /// </summary>
        public string LinkRole
        {
            get
            {
                return linkRole;
            }
        }

        /// <summary>
        /// Адрес ссылки на вкладку 'Права доступа'.
        /// </summary>
        public string LinkAccess
        {
            get
            {
                return linkAccess;
            }
        }

        /// <summary>
        /// Адрес ссылки на вкладку 'Слои'.
        /// </summary>
        public string LinkLayers
        {
            get
            {
                return linkLayers;
            }
        }

        /// <summary>
        /// Осущевляет переход по ссылке.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <param name="url">Url по которому будет осуществлен переход.</param>
        public void Open(IWebDriver driver,string url)
        {
           driver.Navigate().GoToUrl(url);
        }
    }
}
