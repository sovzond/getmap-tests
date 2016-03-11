using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;

namespace GetMapTest.GUI
{
    /// <summary>
    /// Дает доступ ко всем чекбоксам выпадающего меню 'Космические снимки.',
    /// а так же кнопке 'Настройка слоя'.
    /// </summary>
    public class CosmoPhotoLayer
    {
        private IWebDriver driver;
        private const string gazprom = "Gazprom_Base_map_NNG";
        private const string locationCheckBoxs = "div.svzLayerManagerItem.svzLayerManagerItem1 div";
        private Dictionary<string, IWebElement> dicCB;
        private Dictionary<string, IWebElement> dicSB;
        private IList<IWebElement> listCheckBoxs;        

        private CosmoPhotoLayer(IWebDriver driver)
        {
            this.driver = driver;
            SetValueList();
            SetValueElements();
        }

        private CosmoPhotoLayer SetValueList()
        {
            dicCB = new Dictionary<string, IWebElement>();
            dicSB = new Dictionary<string, IWebElement>();
            listCheckBoxs = driver.FindElements(By.CssSelector(locationCheckBoxs));
            return this;
        }

        private CosmoPhotoLayer SetValueElements()
        {
            for (int i = 0; i < listCheckBoxs.Count; i++)
            {
                if (listCheckBoxs[i].Text == "Gazprom_Base_map_NNG")
                {
                    Thread.Sleep(200);
                    dicCB.Add(gazprom,listCheckBoxs[i - 1]);
                    dicSB.Add(gazprom,listCheckBoxs[i + 1]);
                    break;
                }
            }
            return this;
        }

        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static CosmoPhotoLayer get(IWebDriver driver)
        {
            return new CosmoPhotoLayer(driver);
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'Gazprom_Base_map_NNG'.
        /// </summary>
        /// <returns></returns>
        public CosmoPhotoLayer GazpromClick()
        {
            dicCB[gazprom].Click();
            return this;
        }
        /*
                    /// <summary>
                    /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Gazprom_Base_map_NNG'.
                    /// </summary>
                    /// <returns></returns>
                    public CosmoPhotoClass GazpromSBClick()
                    {
                        dicSB[gazprom].Click();
                        return this;
                    }
                    */
    }
}
