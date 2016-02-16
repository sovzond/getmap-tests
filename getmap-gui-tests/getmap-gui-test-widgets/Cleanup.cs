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
        private IWebDriver driver;

        private Cleanup(IWebDriver driver)
        {
            this.driver = driver;
        }

        /// <summary>
        /// Принимает параметр типа IWebDriver для выполнения действий под атрибутом 'TestCleanup'.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static Cleanup get(IWebDriver driver)
        {
            return new Cleanup(driver);
        }

        /// <summary>
        /// Закрывает браузер спустя 2 секунду после окончания работы теста.
        /// </summary>
        public void Quit()
        {
            Thread.Sleep(2000);
            driver.Quit();
        }

        /// <summary>
        /// Закрывает текущую вкладку.
        /// </summary>
        public void Close()
        {
            driver.Close();
        }
    }
}
