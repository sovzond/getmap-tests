using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Drawing;

namespace GetMapTest
{
    /// <summary>
    /// Выполняет различные проверки с изменением пароля.
    /// </summary>
    [TestClass]
    public class TestChangePassword
    {
        private IWebDriver driver;
        private const string errorMessage = "Пароли не совпадают.";
        private const string loginUrl = "http://91.143.44.249/sovzond_test/portal/login.aspx?ReturnUrl=%2fsovzond_test%2fportal%2f";
        private const string locationErrorMEssage = ".dijitTooltipContainer";

        [TestInitialize]
        public void SetupTest()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver,Settings.Instance.BaseUrl).login("student", "123");
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию.");
        }

        /// <summary>
        /// Изменяет пароль и проверяет, изменился ли он действительно.
        /// </summary>
        [TestMethod]
        public void TestPswOn()
        {
            CheckChangePassword("12345");
            CheckChangePassword("123");
        }

        /// <summary>
        /// Вводит пароль и нажимает кнопку 'Отмена', затем проверяет, действительно ли то, что пароль не изменился.
        /// </summary>
        [TestMethod]
        public void TestPswOff()
        {
            CheckNotChangePassword("12315");
        }

        /// <summary>
        /// Проверяет, действительно ли отображается всплывающее окно  при вводе разных паролей.
        /// </summary>
        [TestMethod]
        public void TestPswFalse()
        {
            GUI.HeaderLinks.get(driver).ChangePasswordClick();
            GUI.HeaderLinks.get(driver).ChangePasswordClick();
            GUI.InputNewPassword.get(driver).NewPasswordSendKeys("12345")
                .VerifyPasswordSendKeys("123");
            Thread.Sleep(2000);
            Assert.AreEqual(errorMessage, driver.FindElement(By.CssSelector(locationErrorMEssage)).Text, "После ввода разных паролей, всплывающее окно не было отображено.");
        }

        [TestCleanup]
        public void Clean()
        {
            Thread.Sleep(2000);
            driver.Quit();
        }

        private void ChangePassswordInput(string password)
        {
            GUI.HeaderLinks.get(driver).ChangePasswordClick();
            GUI.InputNewPassword.get(driver).NewPasswordSendKeys(password)
                .VerifyPasswordSendKeys(password)
                .ButtonInputClick();
            GUI.HeaderLinks.get(driver).ExitClick();
            Sleep();
        }

        private void ChangePasswordCancel(string password)
        {
            GUI.HeaderLinks.get(driver).ChangePasswordClick();
            GUI.InputNewPassword.get(driver).NewPasswordSendKeys(password)
                .VerifyPasswordSendKeys(password)
                .ButtonCancelClick();
            GUI.HeaderLinks.get(driver).ExitClick();
            Sleep();
        }

        private void IsFail(string password)
        {
            GUI.Login.get(driver,Settings.Instance.BaseUrl).login("student", password);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Изменение пароля не прошло удачно");
        }

        private void IsNotFail(string password)
        {
            GUI.Login.get(driver, Settings.Instance.BaseUrl).login("student", password);
            Assert.AreEqual(loginUrl, driver.Url, "После нажатия кнопки 'Отмена' , пароль все - таки был изменен.");
        }

        private void CheckChangePassword(string password)
        {
            ChangePassswordInput(password);
            IsFail(password);
        }

        private void CheckNotChangePassword(string password)
        {
            ChangePasswordCancel(password);
            IsNotFail(password);
        }

        private void Sleep()
        {
            Thread.Sleep(2000);
        }
    

    }
}