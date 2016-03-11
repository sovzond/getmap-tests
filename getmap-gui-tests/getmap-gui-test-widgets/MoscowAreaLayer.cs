using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;

namespace GetMapTest.GUI
{
    /// <summary>
    ///  Дает доступ ко всем чекбоксам выпадающего меню 'Московская область',
    ///  а так же кнопке 'Настройка слоя'.
    ///  /// </summary>
    public class MoscowAreaLayer
    {
        private IWebDriver driver;
        private const string landsat = "Мозаика Landsat";
        private const string rapidEye = "Мозаика RapidEye";
        private const string locationCheckBoxes = "div.svzLayerManagerItem.svzLayerManagerItem1 div";
        private Dictionary<string, IWebElement> dicCB;
        private Dictionary<string, IWebElement> dicSB;
        private IList<IWebElement> listCheckBoxes;

        private MoscowAreaLayer(IWebDriver driver)
        {
            this.driver = driver;
            SetValueList();
            SetValueElements();
        }

        private MoscowAreaLayer SetValueList()
        {
            dicCB = new Dictionary<string, IWebElement>();
            dicSB = new Dictionary<string, IWebElement>();
            listCheckBoxes = driver.FindElements(By.CssSelector(locationCheckBoxes));
            return this;
        }

        private MoscowAreaLayer SetValueElements()
        {
            for (int i = 0; i < listCheckBoxes.Count; i++)
            {
                if (listCheckBoxes[i].Text == "Мозаика Landsat")
                {
                    Thread.Sleep(200);
                    dicCB.Add(landsat,listCheckBoxes[i - 1]);
                    dicSB.Add(landsat, listCheckBoxes[i + 1]);
                }
                if (listCheckBoxes[i].Text == "Мозаика RapidEye")
                {
                    Thread.Sleep(200);
                    dicCB.Add(rapidEye, listCheckBoxes[i - 1]);
                    dicSB.Add(rapidEye, listCheckBoxes[i + 1]);
                }
            }
            return this;
        }

        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static MoscowAreaLayer get(IWebDriver driver)
        {
            return new MoscowAreaLayer(driver);
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'Мозаика Landsat'.
        /// </summary>
        /// <returns></returns>
        public MoscowAreaLayer LandsatClick()
        {
            dicCB[landsat].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Мозаика Landsat'.
        /// </summary>
        /// <returns></returns>
        public MoscowAreaLayer LandsatSBClick()
        {
            dicSB[landsat].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'Мозаика RapidEye'.
        /// </summary>
        /// <returns></returns>
        public MoscowAreaLayer RapidEyeClick()
        {
            dicCB[rapidEye].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Мозаика RapidEye'.
        /// </summary>
        /// <returns></returns>
        public MoscowAreaLayer RapidEyeSBClick()
        {
            dicSB[rapidEye].Click();
            return this;
        }

    }
}
