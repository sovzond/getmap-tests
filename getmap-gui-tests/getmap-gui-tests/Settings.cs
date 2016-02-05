using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace GetMapTest
{
    public sealed class Settings
    {
        private static readonly Settings instance = new Settings();

        private Settings() { }

        public static Settings Instance
        {
            get
            {
                return instance;
            }
        }

        public IWebDriver createDriver()
        {
            return new FirefoxDriver();
        }

        private const string baseUrl = "http://91.143.44.249/sovzond_test/portal/";

        public string BaseUrl
        {
            get
            {
                return baseUrl;
            }
        }
    }
}
