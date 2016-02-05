using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;


namespace GetMapTest
{
    [TestClass]
    public class TestSearch
    {
        private IWebDriver driver;
        [TestMethod]
        public void testSearch()
        {
            search("амбар");
            
           
        }
        [TestMethod]
        public void testError()
        {
           search("проверка");
        }
        [TestMethod]
        public void testRegister()
        {
            testRegister("фАкЕл");
            
        }
        private void search(string attributeSearch)
        {
            
            string[] arraySearch = new string[4];
            arraySearch[0] = "факел";
            arraySearch[1] = "амбар";
            arraySearch[2] = "кустовая площадка";
            arraySearch[3] = "дожимная насосная станция";
            string localAttributeSearch = attributeSearch.ToLower();
            logIn();
            driver.FindElement(By.ClassName("searchPanel")).Click();
            driver.FindElement(By.ClassName("searchPanel")).SendKeys(localAttributeSearch);
            driver.FindElement(By.Id("textSearch2")).Click();
            for(int i=0;i<arraySearch.GetLength(0);i++)
            {
                if (!(localAttributeSearch==arraySearch[0] ||
                    localAttributeSearch == arraySearch[1] ||
                    localAttributeSearch == arraySearch[2] ||
                    localAttributeSearch == arraySearch[3]))
                {
                    Assert.Fail("Результат поиска: ничего не найдено");
                }
            }
          

        } 
        private void logIn()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
        }
        private void testRegister(string attributeSearch)
        {
            logIn();
            driver.FindElement(By.ClassName("searchPanel")).Click();
            driver.FindElement(By.ClassName("searchPanel")).SendKeys(attributeSearch);
            driver.FindElement(By.Id("textSearch2")).Click();


        }
    }
}
