using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using System.Threading;

namespace GetMapTest.GUI
{
    /// <summary>
    /// Необходим для чистки браузера после выполнения теста.
    /// </summary>
   public class Cleanup
    {
        /// <summary>
        /// Закрывает браузер спустя 2 секунду после окончания работы теста.
        /// </summary>
        public void Quit(IWebDriver driver)
        {
            Thread.Sleep(2000);
            driver.Quit();
        }
    }
}
