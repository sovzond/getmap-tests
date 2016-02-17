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
        public void TestPswOn()                                                                                  
        {
            GUI.Login.login(driver, Settings.Instance.BaseUrl, "student", "123");
            driver.FindElement(By.CssSelector("#cmdChangePassword")).Click();                                    
            Thread.Sleep(2000);
            Assert.IsTrue(IsElementPresent(By.CssSelector("#stdportal_Window_5")));                              
            driver.FindElement(By.Id("dojox_form__NewPWBox_0")).Clear();
            driver.FindElement(By.Id("dojox_form__NewPWBox_0")).SendKeys("12345");  //поле ввода нового пороля                             
            driver.FindElement(By.Id("dojox_form__VerifyPWBox_0")).Clear();
            driver.FindElement(By.Id("dojox_form__VerifyPWBox_0")).SendKeys("12345"); //поле ввода проверки нового пароля                           
            driver.FindElement(By.CssSelector("button[type=\"button\"]")).Click();                               
            Assert.IsFalse(IsElementPresent(By.CssSelector("div.dijitTooltipContainer.dijitTooltipContents")));  
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("#exit")).Click();                                                 
            Assert.IsFalse(IsElementPresent(By.CssSelector("#contentpane")));
            Thread.Sleep(2000);
            GUI.Login.login(driver, Settings.Instance.BaseUrl, "student", "12345");
            Thread.Sleep(2000);
            Assert.IsTrue(IsElementPresent(By.CssSelector("#contentpane")));                                    
            driver.FindElement(By.Id("cmdChangePassword")).Click();                                             
            Assert.IsTrue(IsElementPresent(By.CssSelector("#stdportal_Window_5")));
            driver.FindElement(By.Id("dojox_form__NewPWBox_0")).Clear();
            driver.FindElement(By.Id("dojox_form__NewPWBox_0")).SendKeys("123"); //поле ввода нового пороля  
            driver.FindElement(By.Id("dojox_form__VerifyPWBox_0")).Clear();
            driver.FindElement(By.Id("dojox_form__VerifyPWBox_0")).SendKeys("123"); //поле ввода проверки нового пароля     
            driver.FindElement(By.CssSelector("button[type=\"button\"]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("#exit")).Click();
            Thread.Sleep(2000);
            Assert.IsFalse(IsElementPresent(By.CssSelector("#contentpane")));
            GUI.Login.login(driver, Settings.Instance.BaseUrl, "student", "123");
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("#cmdLogin")).Click();
            Assert.IsTrue(IsElementPresent(By.CssSelector("#contentpane")));                                 
        }

        [TestMethod]
        public void TestPswOff()                                                                  
        {
            GUI.Login.login(driver, Settings.Instance.BaseUrl, "student", "123");
            Thread.Sleep(2000);
            Assert.IsTrue(IsElementPresent(By.CssSelector("#contentpane")));
            driver.FindElement(By.Id("cmdChangePassword")).Click();                    
            driver.FindElement(By.Id("dojox_form__NewPWBox_0")).Clear();
            driver.FindElement(By.Id("dojox_form__NewPWBox_0")).SendKeys("12315");     //поле ввода нового пороля  
            driver.FindElement(By.Id("dojox_form__VerifyPWBox_0")).Clear();
            driver.FindElement(By.Id("dojox_form__VerifyPWBox_0")).SendKeys("12315"); //поле ввода проверки нового пароля     
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("(//button[@type='button'])[2]")).Click();    
            Thread.Sleep(2000);
            driver.FindElement(By.Id("exit")).Click();                                
            Assert.IsFalse(IsElementPresent(By.CssSelector("#contentpane")));
            GUI.Login.login(driver, Settings.Instance.BaseUrl, "student", "12315");
            Assert.IsFalse(IsElementPresent(By.CssSelector("#contentpane")));        
        }

        [TestMethod]
        public void TestPswFalse()                                                                                                              
        {
            GUI.Login.login(driver, Settings.Instance.BaseUrl, "student", "123");
            Thread.Sleep(2000);
            Assert.IsTrue(IsElementPresent(By.CssSelector("#contentpane")));
            driver.FindElement(By.Id("cmdChangePassword")).Click();                                                                             
            driver.FindElement(By.Id("dojox_form__NewPWBox_0")).Clear();
            driver.FindElement(By.Id("dojox_form__NewPWBox_0")).SendKeys("12345");      //поле ввода нового пороля                                                          
            driver.FindElement(By.Id("dojox_form__VerifyPWBox_0")).Clear();
            driver.FindElement(By.Id("dojox_form__VerifyPWBox_0")).SendKeys("1276");         //поле ввода проверки нового пароля                                                        
            driver.FindElement(By.CssSelector("button[type=\"button\"]")).Click();                                                             
            Assert.AreEqual("Пароли не совпадают.", driver.FindElement(By.CssSelector(".dijitTooltipContainer")).Text); 
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