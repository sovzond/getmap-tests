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
        private IWebElement elementErrorMessage;
        private const string charError = "Указано недопустимое значение.";
        private const string intError = "Это значение вне диапазона.";
        private const string locationErrorMessage = "div.dijitTooltip > div.dijitTooltipContents";

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
            elementErrorMessage = null;
        }

        /// <summary>
        ///Данный метод проводит проверку на наличае всплывающего окна при вводе цифры привышающей диапазон.
        ///</summary>
        [TestMethod]
        public void CheckInt()
        {
            GUI.MenuNavigation.get(driver).GotoCoordsButton();
            try              
            {
                driver.FindElement(By.Id("dijit_form_NumberTextBox_0")).SendKeys("100");
                elementErrorMessage = driver.FindElement(By.CssSelector(locationErrorMessage));
                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                Assert.Fail("Отсутствует напоминание о неправильном вводе параметров" + e.Message);
            }
            Assert.AreEqual(intError, elementErrorMessage.Text, "Высветился неправильный текст об ошибке");
        }

        /// <summary>
        ///Данный метод проводит проверку на наличае всплывающего окна при вводе недопустимого значение.
        ///</summary>
        [TestMethod]
        public void CheckChar()
        {
            GUI.MenuNavigation.get(driver).GotoCoordsButton();
            try
            {
                driver.FindElement(By.Id("dijit_form_NumberTextBox_0")).SendKeys("k");
                elementErrorMessage = driver.FindElement(By.CssSelector(locationErrorMessage));
                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                Assert.Fail("Отсутствует напоминание о неправильном вводе параметров" + e.Message);
            }
            Assert.AreEqual(charError, elementErrorMessage.Text, "Высветился неправильный текст об ошибке");
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

    }
}
