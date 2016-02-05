using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace GetMapTest
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class TestTransparencyLayer
    {

        private IWebDriver driver;
        private const string locationLegenda = "#menuSlide div.svzSimpleButton.slidePanelLegendButton";
        private const string locationFakelDecTransButton = "#dojoUnique1 div.dijitSliderDecrementIconH";
        private const string locationAmbarDecTransButton = "#dojoUnique2 div.dijitSliderDecrementIconH";
        private const string locationPlacesDecTransButton = "#dojoUnique3 div.dijitSliderDecrementIconH";
        private const string locationDNSDecTransButton = "#dojoUnique4 div.dijitSliderDecrementIconH";
        private const string locationMap = "#OpenLayers_Layer_OSM_2 img[src*='/11/1422/584.png']";

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
        }

        /// <summary>
        ///
        ///</summary>
        [TestMethod]
        public void TestTransparency()
        {
            LogOn();
            DecTransparencyFakel();
        }

        [TestCleanup]
        public void Clean()
        {
            System.Threading.Thread.Sleep(2000);
            driver.Quit();
        }

        private void LogOn()
        {
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
        }

        private void DecTransparencyFakel()
        {         
            Bitmap imageFakelVisible = TakeScreenshot(); 
            driver.FindElement(By.CssSelector(locationLegenda)).Click();
            for (int i = 0; i < 55; i++)
                driver.FindElement(By.CssSelector(locationFakelDecTransButton)).Click();
            Bitmap imageFakelNotVisible = TakeScreenshot();

        }
        private Bitmap TakeScreenshot()
        {
            var screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            var bitmapScreen = new Bitmap(new MemoryStream(screenshot.AsByteArray));
            IWebElement element = driver.FindElement(By.CssSelector(locationMap));
            var cutArea = new Rectangle(element.Location, element.Size);
            return bitmapScreen.Clone(cutArea, bitmapScreen.PixelFormat);
        }
        private unsafe Bitmap EqualImage(Bitmap image1,Bitmap image2,Color color)
        {
            if (image1 == null || image2 == null)
                return null;
            if (image1.Width != image2.Width || image1.Height != image2.Height)
                return null;
            Bitmap diffImage = image2.Clone() as Bitmap;
            int height = image1.Height;
            int width = image1.Width;
            BitmapData data1 = image1.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData data2 = image2.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData diffData = diffImage.LockBits(new Rectangle(0, 0, width, height),
         ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            byte* data1Ptr = (byte*)data1.Scan0;
            byte* data2Ptr = (byte*)data2.Scan0;
            byte* diffPtr = (byte*)diffData.Scan0;
            byte[] swapColor = new byte[3];
            swapColor[0] = color.R;
            swapColor[1] = color.G;
            swapColor[2] = color.B;
            int rowPadding = data1.Stride - (image1.Width * 3);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int same = 0;
                    byte[] tmp = new byte[3];
                    for (int x = 0; x < 3; x++)
                    {
                        tmp[x] = data2Ptr[0];
                        if (data1Ptr[0] == data2Ptr[0])
                        {
                            same++;
                        }
                        data1Ptr++;
                        data2Ptr++;
                        for (int y = 0; y < 3; y++)
                        {
                            diffPtr[0] = (same == 3) ? swapColor[x] : tmp[x];
                            diffPtr++;
                            if (rowPadding > 0)
                            {
                                data1Ptr += rowPadding;
                                data2Ptr += rowPadding;
                                diffPtr += rowPadding;
                            }
                        }
                        image1.UnlockBits(data1);
                        image2.UnlockBits(data2);
                        diffImage.UnlockBits(diffData);
                       
                    }
                }
            }
             return diffImage;
        }
    }
}
