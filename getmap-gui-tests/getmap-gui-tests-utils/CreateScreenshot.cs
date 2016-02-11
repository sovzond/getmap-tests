using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using System.Drawing;
using System.IO;

namespace GetMapTest.Utils
{
    /// <summary>
    /// Выполняет скриншот определенной областки на карте.
    /// </summary>
     public class CreateScreenshot
    {
      
        private static readonly CreateScreenshot instance = new CreateScreenshot();

        /// <summary>
        /// Создает объект класса CreateScreenshot для доступа к его методам, свойствам. 
        /// </summary>
        public static CreateScreenshot Instance
        {
            get
            {
                return instance;
            }
        }
        
        /// <summary>
        /// Делает скриншот конктреной области на карте.
        /// </summary>
        /// <param name="driver">Обязательный параметр, экземпляр которого вызывает фукнции создания скриншота.</param>
        /// <param name="location">Путь к элементу, области которой будет сделан скриншот.</param>
        /// <returns></returns>
        public Bitmap TakeScreenshot(IWebDriver driver,string location)
        {
            var screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            var bitmapScreen = new Bitmap(new MemoryStream(screenshot.AsByteArray));
            IWebElement element = driver.FindElement(By.CssSelector(location));
            var cutArea = new Rectangle(element.Location, element.Size);
            return bitmapScreen.Clone(cutArea, bitmapScreen.PixelFormat);
        }
    }
}
