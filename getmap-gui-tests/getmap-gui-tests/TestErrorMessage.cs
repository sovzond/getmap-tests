using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace GetMapTest
{
    /// <summary>
    /// Данный тест осуществляет проверку на ввод символов в ячейку для поиска координат.
    /// </summary>
    [TestClass]
    public class TestErrorMessage
    {
        private IWebDriver driver;     
        private const string charError = "Указано недопустимое значение.";
        private const string intError = "Это значение вне диапазона.";

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
        }

        /// <summary>
        ///Данный метод проводит проверку на наличае всплывающего окна при вводе цифры привышающей диапазон.
        ///</summary>
        [TestMethod]
        public void TestInt()
        {
            LogOn();
            InputInt();
        }

        /// <summary>
        ///Данный метод проводит проверку на наличае всплывающего окна при вводе недопустимого значение.
        ///</summary>
        [TestMethod]
        public void TestChar()
        {
            LogOn();
            InputChar();
        }

        [TestCleanup]
        public void Clean()
        {
            System.Threading.Thread.Sleep(2000);
            driver.Quit();
        }

        private void LogOn()
        {
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
        }
        private void InputChar()
        {
            IWebElement el = null;
            driver.FindElement(By.Id("menuNavigation"))
                .FindElement(By.CssSelector("div.svzSimpleButton.gotoCoordsButton")).Click();
            try
            {
                driver.FindElement(By.Id("dijit_form_NumberTextBox_0")).SendKeys("j");
               el = driver.FindElement(By.CssSelector("div.dijitTooltip > div.dijitTooltipContents"));
            }
            catch (Exception e)
            {
                Assert.Fail("Отсутствует напоминание о неправильном вводе параметров" + e.Message);
            }
            Assert.AreEqual(charError, el.Text, "Высветился неправильный текст об ошибке");

        }
        private void InputInt()
        {
            IWebElement el = null;
            driver.FindElement(By.Id("menuNavigation")).FindElement(By.CssSelector("div.svzSimpleButton.gotoCoordsButton")).Click();
            try
            {
                driver.FindElement(By.Id("dijit_form_NumberTextBox_0")).SendKeys("100");

               el = driver.FindElement(By.CssSelector("div.dijitTooltip > div.dijitTooltipContents"));
            }
            catch (Exception e)
            {
                Assert.Fail("Отсутствует напоминание о неправильном вводе параметров" + e.Message);
            }
            Assert.AreEqual(intError, el.Text, "Высветился неправильный текст об ошибке");

        }
    }
}
