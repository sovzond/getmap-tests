using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using OpenQA.Selenium;

namespace GetMapTest
{
    [TestClass]
    public class TestChangePassword
    {
        private IWebDriver driver;
        [TestInitialize]
        public void SetupTest()
        {
            driver = Settings.Instance.createDriver();
        }
        [TestMethod]
        public void TestPswOn()                                                                                  //необходимо сменить пароль
        {
            GUI.Login.login(driver, Settings.Instance.BaseUrl, "student", "123");
            driver.FindElement(By.CssSelector("#cmdChangePassword")).Click();                                    //"Изменить пароль"
            Thread.Sleep(2000);
            Assert.IsTrue(IsElementPresent(By.CssSelector("#stdportal_Window_5")));                              //проверка окно "Изменить пароль"
            driver.FindElement(By.Id("dojox_form__NewPWBox_0")).Clear();
            driver.FindElement(By.Id("dojox_form__NewPWBox_0")).SendKeys("12345");                               //новый пароль
            driver.FindElement(By.Id("dojox_form__VerifyPWBox_0")).Clear();
            driver.FindElement(By.Id("dojox_form__VerifyPWBox_0")).SendKeys("12345");                            //подтвердить новый пароль
            driver.FindElement(By.CssSelector("button[type=\"button\"]")).Click();                               //принять "Изменить пароль"
            Assert.IsFalse(IsElementPresent(By.CssSelector("div.dijitTooltipContainer.dijitTooltipContents")));  //проверка "принять "Изменить пароль""
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("#exit")).Click();                                                 //выход 
            Assert.IsFalse(IsElementPresent(By.CssSelector("#contentpane")));
            Thread.Sleep(2000);
            GUI.Login.login(driver, Settings.Instance.BaseUrl, "student", "12345");
            Thread.Sleep(2000);
            Assert.IsTrue(IsElementPresent(By.CssSelector("#contentpane")));                                    //проверка "вход под новым паролем"
            driver.FindElement(By.Id("cmdChangePassword")).Click();                                             //откат изменений
            Assert.IsTrue(IsElementPresent(By.CssSelector("#stdportal_Window_5")));
            driver.FindElement(By.Id("dojox_form__NewPWBox_0")).Clear();
            driver.FindElement(By.Id("dojox_form__NewPWBox_0")).SendKeys("123");
            driver.FindElement(By.Id("dojox_form__VerifyPWBox_0")).Clear();
            driver.FindElement(By.Id("dojox_form__VerifyPWBox_0")).SendKeys("123");
            driver.FindElement(By.CssSelector("button[type=\"button\"]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("#exit")).Click();
            Thread.Sleep(2000);
            Assert.IsFalse(IsElementPresent(By.CssSelector("#contentpane")));
            GUI.Login.login(driver, Settings.Instance.BaseUrl, "student", "123");
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("#cmdLogin")).Click();
            Assert.IsTrue(IsElementPresent(By.CssSelector("#contentpane")));                                 //проверка отката изменений
        }

        [TestMethod]
        public void TestPswOff()                                                         //нет входа под новым неподтвержденным паролем          
        {
            GUI.Login.login(driver, Settings.Instance.BaseUrl, "student", "123");
            Thread.Sleep(2000);
            Assert.IsTrue(IsElementPresent(By.CssSelector("#contentpane")));
            driver.FindElement(By.Id("cmdChangePassword")).Click();                    //"Изменить пароль"
            driver.FindElement(By.Id("dojox_form__NewPWBox_0")).Clear();
            driver.FindElement(By.Id("dojox_form__NewPWBox_0")).SendKeys("12315");     //новый пароль  
            driver.FindElement(By.Id("dojox_form__VerifyPWBox_0")).Clear();
            driver.FindElement(By.Id("dojox_form__VerifyPWBox_0")).SendKeys("12315"); //подтвердить новый пароль
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("(//button[@type='button'])[2]")).Click();    //отменить "Изменить пароль"
            Thread.Sleep(2000);
            driver.FindElement(By.Id("exit")).Click();                                //выход 
            Assert.IsFalse(IsElementPresent(By.CssSelector("#contentpane")));
            GUI.Login.login(driver, Settings.Instance.BaseUrl, "student", "12315");
            Assert.IsFalse(IsElementPresent(By.CssSelector("#contentpane")));        //проверка "вход под новым паролем не выполнен"
        }

        [TestMethod]
        public void TestPswFalse()                                                                                                              //"новый пароль" и "потвердить новый пароль" не совпадают
        {
            GUI.Login.login(driver, Settings.Instance.BaseUrl, "student", "123");
            Thread.Sleep(2000);
            Assert.IsTrue(IsElementPresent(By.CssSelector("#contentpane")));
            driver.FindElement(By.Id("cmdChangePassword")).Click();                                                                             //"Изменить пароль"
            driver.FindElement(By.Id("dojox_form__NewPWBox_0")).Clear();
            driver.FindElement(By.Id("dojox_form__NewPWBox_0")).SendKeys("12345");                                                              //новый пароль
            driver.FindElement(By.Id("dojox_form__VerifyPWBox_0")).Clear();
            driver.FindElement(By.Id("dojox_form__VerifyPWBox_0")).SendKeys("1276");                                                            //потвердить новый пароль (ошибка)
            driver.FindElement(By.CssSelector("button[type=\"button\"]")).Click();                                                              //принять "Изменить пароль"
            Assert.AreEqual("Пароли не совпадают.", driver.FindElement(By.CssSelector(".dijitTooltipContainer")).Text); //проверка на ошибку "принять "Изменить пароль""
        }
        [TestCleanup]
        public void Clean()
        {
            Thread.Sleep(2000);
            driver.Quit();
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {

                return false;
            }
        }

        
    }
}