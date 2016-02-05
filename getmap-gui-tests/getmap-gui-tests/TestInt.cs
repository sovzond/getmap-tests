using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;

namespace GetMapTest
{
    /// <summary>
    /// Данный тест проводит проверку на появление всплывающего окна при вводе неверных координат.
    /// </summary>
    [TestClass]
    public class TestInt
    {
        private IWebDriver driver;
        /// <summary>
        /// Данный метод реализует ввод неверных данные в ячейки для поиска координат.
        /// </summary>
        [TestMethod]
        public void TestTitle()
        {
            //Тест №2
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
        private void InputValue()
        {

            driver.FindElement(By.Id("sovzond_widget_SimpleButton_100")).Click();
            try
            {
                driver.FindElement(By.Id("dijit_form_NumberTextBox_0")).SendKeys("100");
                driver.FindElement(By.Id("dijit_form_NumberTextBox_1")).SendKeys("100");
                driver.FindElement(By.Id("dijit_form_NumberTextBox_2")).SendKeys("100");
                driver.FindElement(By.Id("dijit_form_NumberTextBox_3")).SendKeys("100");
                driver.FindElement(By.Id("dijit_form_NumberTextBox_4")).SendKeys("100");
                driver.FindElement(By.Id("dijit_form_NumberTextBox_5")).SendKeys("100");
                System.Threading.Thread.Sleep(1000);
                driver.FindElement(By.Id("dijit_form_NumberTextBox_5")).Click();
            }
            catch (Exception)
            {
                Assert.AreEqual("Появление напоминающего окна", "dijit__MasterTooltip_0", "Напоминающее окно не отобразилось.");
            }

        }
    }
    
}
