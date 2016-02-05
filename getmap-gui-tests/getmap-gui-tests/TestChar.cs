using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace GetMapTest
{
    /// <summary>
    /// Данный класс проводит тест на запись символа в поле градусов строки с.ш. в появившемся окне «Переход на координаты» и проверяет, будет ли отображено предупреждение.
    /// </summary>
    [TestClass]
    public class TestChar
    {
        private IWebDriver driver;
        /// <summary>
        /// Данный метод вносит символьное значение в ячейку с.ш. для проверки появдения сообщения.
        /// </summary>
        [TestMethod]
        public void InputCharValue()
        {
            //Тест №3
            LogOn();
            InputValue();
            //Тест выполнил Петров,Балов.
        }

        private void LogOn()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
        }
        private void InputValue_test()
        {

            driver.FindElement(By.Id("sovzond_widget_SimpleButton_100")).Click();
            try
            {
                driver.FindElement(By.Id("dijit_form_NumberTextBox_0")).SendKeys("h");
                driver.FindElement(By.Id("dijit_form_NumberTextBox_0")).Click();
            }
            catch (Exception)
            {
                Assert.AreEqual("Появление напоминающего окна", "dijit__MasterTooltip_0", "Напоминающее окно не отобразилось.");
            }

        }
        private void InputValue()
        {

            driver.FindElement(By.Id("sovzond_widget_SimpleButton_133")).Click();
            try
            {
                driver.FindElement(By.Id("dijit_form_NumberTextBox_0")).SendKeys("h");
                driver.FindElement(By.Id("dijit_form_NumberTextBox_0")).Click();
            }
            catch (Exception)
            {
                Assert.AreEqual("Появление напоминающего окна", "dijit__MasterTooltip_0", "Напоминающее окно не отобразилось.");
            }

        }
    }
}
