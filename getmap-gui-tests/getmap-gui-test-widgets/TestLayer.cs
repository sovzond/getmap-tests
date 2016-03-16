using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;

namespace GetMapTest.GUI
{
    /// <summary>
    /// Дает доступ ко всем чекбоксам выпадающего меню 'Тест', 
    /// а так же кнопке 'Настройка слоя'.
    /// </summary>
    public class TestLayer
    {
        private IWebDriver driver;
        private const string america = "США";
        private const string base_raster = "rtk:base_raster";
        private const string ambar = "ambar";
        private const string aa_states = "aa_states_4326";
        private const string locationCheckBoxs = "div.svzLayerManagerItem.svzLayerManagerItem1 div";
        private Dictionary<string, IWebElement> dicCB;
        private Dictionary<string, IWebElement> dicSB;
        private IList<IWebElement> listCheckBoxs;

        private TestLayer(IWebDriver driver)
        {
            this.driver = driver;
            SetValueList();
            SetValueElements();
        }

        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static TestLayer get(IWebDriver driver)
        {
            return new TestLayer(driver);
        }

        private TestLayer SetValueList()
        {
            dicCB = new Dictionary<string, IWebElement>();
            dicSB = new Dictionary<string, IWebElement>();
            listCheckBoxs = driver.FindElements(By.CssSelector(locationCheckBoxs));
            return this;
        }

        private TestLayer SetValueElements()
        {
            for (int i = 0; i < listCheckBoxs.Count; i++)
            {
                if (listCheckBoxs[i].Text == "aa_states_4326")
                {
                    Thread.Sleep(200);
                    dicCB.Add(aa_states, listCheckBoxs[i - 1]);
                    dicSB.Add(aa_states, listCheckBoxs[i + 1]);
                }
                if (listCheckBoxs[i].Text == "США")
                {
                    Thread.Sleep(200);
                    dicCB.Add(america, listCheckBoxs[i - 1]);
                    dicSB.Add(america, listCheckBoxs[i + 1]);
                }
                if (listCheckBoxs[i].Text == "rtk:base_raster")
                {
                    Thread.Sleep(200);
                    dicCB.Add(base_raster, listCheckBoxs[i - 1]);
                    dicSB.Add(base_raster, listCheckBoxs[i + 1]);
                }
                if (listCheckBoxs[i].Text == "ambar")
                {
                    Thread.Sleep(200);
                    dicCB.Add(ambar, listCheckBoxs[i - 1]);
                    dicSB.Add(ambar, listCheckBoxs[i + 1]);
                }
            }
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'aa_states_4326'.
        /// </summary>
        /// <returns></returns>
        public TestLayer aa_states_4326Click()
        {
            dicCB[aa_states].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'aa_states_4326'.
        /// </summary>
        /// <returns></returns>
        public TestLayer aa_states_4326SBClick()
        {
            dicSB[aa_states].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'США'.
        /// </summary>
        /// <returns></returns>
        public TestLayer AmericaClick()
        {
            dicCB[america].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'США'.
        /// </summary>
        /// <returns></returns>
        public TestLayer AmericaSBClick()
        {
            dicSB[america].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'rtk:base_raster'.
        /// /// </summary>
        /// <returns></returns>
        public TestLayer Base_RasterClick()
        {
            dicCB[base_raster].Click();
            return this;
        }
        /*
                    /// <summary>
                    /// Выполняет клик по кнопке 'Настройка слоя' слоя 'rtk:base_raster'.
                    /// </summary>
                    /// <returns></returns>
                    public TestLayerClass Bse_RasterSBClick()
                    {
                        dicSB[base_raster].Click();
                        return this;
                    }
        */
        /// <summary>
        /// Выполняет клик по чекбоксу 'ambar'.
        /// </summary>
        /// <returns></returns>
        public TestLayer AmbarClick()
        {
            dicCB[ambar].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'ambar'.
        /// </summary>
        /// <returns></returns>
        public TestLayer AmbarSBClick()
        {
            dicSB[ambar].Click();
            return this;
        }
    }
}
