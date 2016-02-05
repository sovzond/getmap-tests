using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace GetMapTest
{
    /// <summary>
    ///Данный класс проводит тест на  правильность нахождения точки по заданным координатам.
    /// </summary>
    [TestClass]
    public class TestMap
    {
        private IWebDriver driver;
        /// <summary>
        ///Данный метод заносит координаты и отображает их на карте путем указателя.
        ///</summary>
        [TestMethod]
        public void CheckXY()
        {
            //Тест №1
            LogOn();
            GUI.InputCoordWnd.get(driver).setLat(60, 50, 0).setLon(69, 59, 0).click();
            //InputCoordinates();
            //Тест выполнил Петров,Балов.
        }
        private void InputCoordinates_test()
        {
            driver.FindElement(By.Id("sovzond_widget_SimpleButton_100")).Click();
            driver.FindElement(By.Id("dijit_form_NumberTextBox_0")).SendKeys("60");
            driver.FindElement(By.Id("dijit_form_NumberTextBox_1")).SendKeys("50");
            driver.FindElement(By.Id("dijit_form_NumberTextBox_2")).SendKeys("0");
            driver.FindElement(By.Id("dijit_form_NumberTextBox_3")).SendKeys("69");
            driver.FindElement(By.Id("dijit_form_NumberTextBox_4")).SendKeys("59");
            driver.FindElement(By.Id("dijit_form_NumberTextBox_5")).SendKeys("0");
            driver.FindElement(By.Id("sovzond_widget_SimpleButton_3")).Click();


        }
        private void LogOn()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
        }
        private void InputCoordinates()
        {

            driver.FindElement(By.Id("sovzond_widget_SimpleButton_133")).Click();
            driver.FindElement(By.Id("dijit_form_NumberTextBox_0")).SendKeys("60");
            driver.FindElement(By.Id("dijit_form_NumberTextBox_1")).SendKeys("50");
            driver.FindElement(By.Id("dijit_form_NumberTextBox_2")).SendKeys("0");
            driver.FindElement(By.Id("dijit_form_NumberTextBox_3")).SendKeys("69");
            driver.FindElement(By.Id("dijit_form_NumberTextBox_4")).SendKeys("59");
            driver.FindElement(By.Id("dijit_form_NumberTextBox_5")).SendKeys("0");
            driver.FindElement(By.Id("sovzond_widget_SimpleButton_3")).Click();


        }



    }
}
