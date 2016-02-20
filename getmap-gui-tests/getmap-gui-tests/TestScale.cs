using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace GetMapTest
{
    /// <summary>
    /// Выполняет проверку на изменение масштаба после клика по кнопке 'Увеличить масштаб'  и 'Уменьшить масштаб'.
    /// </summary>
    [TestClass]
    public class TestScale
    {
        private IWebDriver driver;
        private IJavaScriptExecutor js;

        [TestInitialize]
        public void SetupTest()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
            js = driver as IJavaScriptExecutor;
        }

        /// <summary>
        /// Выполняет проверку на изменение масштаба после клика по кнопке 'Увеличить масштаб'.
        /// </summary>
        [TestMethod]
        public void TestScaleInc()
        {
            CheckScale('+');
        }

        /// <summary>
        /// Выполняет проверку на изменение масштаба после клика по кнопке 'Уменьшить масштаб'.
        /// </summary>
        [TestMethod]
        public void TestScaleDec()
        {
            CheckScale('-');
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void CheckScale(char simbol)
        {
            if(simbol == '+' || simbol == '-')
            {
                string getZoomBefore = (string)js.ExecuteScript("return window.portal.stdmap.map.getZoom().toString()");
                long convertGetZoomBefore = Convert.ToInt64(getZoomBefore);
                if (simbol == '-')
                    GUI.ScaleMenu.get(driver).DecrementButton();
                if (simbol == '+')
                    GUI.ScaleMenu.get(driver).IncrementButton();
                Thread.Sleep(2000);
                string getZoomAfter = (string)js.ExecuteScript("return window.portal.stdmap.map.getZoom().toString()");
                long convertGetZoomAfter = Convert.ToInt64(getZoomAfter);
                if (simbol == '-')
                {
                    if (convertGetZoomBefore <= convertGetZoomAfter)
                        Assert.Fail("После клика по кнопке 'Уменьшить масштаб', масштаб не уменьшился.");
                    if (convertGetZoomAfter + 1 != convertGetZoomBefore)
                        Assert.Fail("уровень Zoom'а не уменьшился на еденицу");
                }
                if (simbol == '+')
                {
                    if (convertGetZoomBefore >= convertGetZoomAfter)
                        Assert.Fail("После клика по кнопке 'Увеличить масштаб', масштаб не увеличился.");
                    if (convertGetZoomAfter - 1 != convertGetZoomBefore)
                        Assert.Fail("уровень Zoom'а не увеличился на еденицу");
                }
            }
  
        }
    }
}
