using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;


namespace GetMapTest.GUI
{
    /// <summary>
    /// Открывает плажку в левой части экрана (Данные и карты, Легенда). 
    /// Так же открывает вкладки данной плажки и прокликивает чекбосы.
    /// </summary>
    public class SlideMenu
    {
        private IWebDriver driver;
        private const string rosreestr = "Росреестр";
        private const string osm = "OpenStreetMap";
        private const string toposnova = "Топоснова";
        private const string sputnik = "Спутник";
        private const string scheme = "Схема";
        private const string gibrid = "Гибрид";
        private const string locationSlideMenu = "#menuSlide div.svzSimpleButton.slidePanelButton";
        private const string locationBaseLayers = "#layersCon div.svzSimpleButton.accordionButton";
        private const string locationLayersInGoogleLayersRB = "div.svzLayerManagerItem.svzLayerManagerItem1 > div";
        private const string locationGoogle = "#stdportal_LayerManagerBase_0 div.svzLayerManagerText";
        private const string locationLegenda = "#menuSlide div.svzSimpleButton.slidePanelLegendButton";
        private const string locationLayersInBaseLayersRB = "div.svzLayerManagerItem.svzLayerManagerItem0 > div";
        private const string locationDecTransparencyButtons = "div.dijitSliderDecrementIconH";
        private const string locationIncTransparencyButtons = "div.dijitSliderIncrementIconH";
        private Dictionary<string, IWebElement> dicRB;
        private IList<IWebElement> listLayersInGoogleLayers;
        private IList<IWebElement> listButtonsIncTransparency;
        private IList<IWebElement> listButtonsDecTransparency;
        private IList<IWebElement> listLayersInBaseLayers;
        private enum NumberButtonsIncDec
        {
            Fakel = 0,
            Ambar = 1,
            Places = 2,
            DNS = 3
        }

        private SlideMenu(IWebDriver driver)
        {
            this.driver = driver;
            SetValueList();
            SetValueElements();
        }

        private SlideMenu SetValueList()
        {
            dicRB = new Dictionary<string, IWebElement>();
            listLayersInBaseLayers = driver.FindElements(By.CssSelector(locationLayersInBaseLayersRB));
            listLayersInGoogleLayers = driver.FindElements(By.CssSelector(locationLayersInGoogleLayersRB));
            listButtonsIncTransparency = driver.FindElements(By.CssSelector(locationIncTransparencyButtons));
            listButtonsDecTransparency = driver.FindElements(By.CssSelector(locationDecTransparencyButtons));
            return this;
        }

        private SlideMenu SetValueElements()
        {
            for (int i = 0; i < listLayersInBaseLayers.Count; i++)
            {
                if (listLayersInBaseLayers[i].Text == "Росреестр")
                    dicRB.Add(rosreestr,listLayersInBaseLayers[i - 1]);
                if (listLayersInBaseLayers[i].Text == "OpenStreetMap")
                    dicRB.Add(osm,listLayersInBaseLayers[i - 1]);
                if (listLayersInBaseLayers[i].Text == "Топооснова")
                   dicRB.Add(toposnova,listLayersInBaseLayers[i - 1]);
            }
            for (int i = 0; i < listLayersInGoogleLayers.Count; i++)
            {
                if (listLayersInGoogleLayers[i].Text == "Схема")
                    dicRB.Add(scheme,listLayersInGoogleLayers[i - 1]);
                if (listLayersInGoogleLayers[i].Text == "Спутник")
                    dicRB.Add(sputnik,listLayersInGoogleLayers[i - 1]);
                if (listLayersInGoogleLayers[i].Text == "Гибрид")
                    dicRB.Add(gibrid,listLayersInGoogleLayers[i - 1]);
            }
            return this;
        }

        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static SlideMenu get(IWebDriver driver)
        {
            return new SlideMenu(driver);
        }

        /// <summary>
        /// Открывает плажку 'Данные и карты', выполняя клик по кнопке 'Выбор слоев'.
        /// </summary>
        /// <returns></returns>
        public SlideMenu OpenLayers()
        {
            driver.FindElement(By.CssSelector(locationSlideMenu)).Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Базовая карта' раздела 'Данные и карты'.
        /// </summary>
        /// <returns></returns>
        public SlideMenu OpenBaseLayers()
        {
            driver.FindElement(By.CssSelector(locationBaseLayers)).Click();
            return this;
        }

        /// <summary>
        /// Активирует чекбокс Схема(выполняет по нему клик).
        /// </summary>
        /// <returns></returns>
        public SlideMenu LayerSchemeClick()
        {
            dicRB[scheme].Click();
            return this;
        }

        /// <summary>
        /// Активирует чекбокс Спутник(выполняет по нему клик).
        /// </summary>
        /// <returns></returns>
        public SlideMenu LayerSputnikClick()
        {
            dicRB[sputnik].Click();
            return this;
        }

        /// <summary>
        /// Активирует чекбокс Гибрид(выполняет по нему клик).
        /// </summary>
        /// <returns></returns>
        public SlideMenu LayerGibridClick()
        {
            dicRB[gibrid].Click();
            return this;
        }

        /// <summary>
        /// Открывает владку 'Google' во вкладке 'Базовая карта'.
        /// </summary>
        /// <returns></returns>
        public SlideMenu OpenGoogle()
        {
            driver.FindElement(By.CssSelector(locationGoogle)).Click();
            return this;
        }

        /// <summary>
        /// Активирует чекбокс Росреестр(выполняет по нему клик).
        /// </summary>
        /// <returns></returns>
        public SlideMenu RosreestrClick()
        {
            dicRB[rosreestr].Click();
            return this;
        }

        /// <summary>
        /// Активирует чекбокс OpenStreetMap(выполняет по нему клик).
        /// </summary>
        /// <returns></returns>
        public SlideMenu OpenStreetMapClick()
        {
            dicRB[osm].Click();
            return this;
        }

        /// <summary>
        /// Активирует чекбокс Топоснова(выполняет по нему клик).
        /// </summary>
        /// <returns></returns>
        public SlideMenu TopOsnovaClick()
        {
            dicRB[toposnova].Click();
            return this;
        }

        /// <summary>
        /// Открывает вкладку 'Легенда' на плажке в левой части экрана.
        /// </summary>
        /// <returns></returns>
        public SlideMenu OpenLegenda()
        {
            driver.FindElement(By.CssSelector(locationLegenda)).Click();
            return this;
        }
        /*
                /// <summary>
                /// Выполняет клик по кнопке 'Поднять наверх слой' слоя 'Факел'.
                /// </summary>
                /// <returns></returns>
                public SlideMenu ButtonIncFakelClick()
                {
                    Sleep();
                    listButtonsIncDec[0].Click();
                    return this;
                }
        */
        /// <summary>
        /// Увеличивает прозрачность слоя  'Факел'.
        /// </summary>
        /// <param name="count">Колическов кликов по кнопке 'Увеличить прозрачность'.</param>
        /// <returns></returns>
        public SlideMenu ButtonIncTransparencyFakelClick(int count)
        {
            for (int i = 0; i < count; i++)
            {
                listButtonsIncTransparency[(int)NumberButtonsIncDec.Fakel].Click();
            }
            return this;
        }

        /// <summary>
        /// Уменьшает прозрачность слоя  'Факел'.
        /// </summary>
        /// <param name="count">Колическов кликов по кнопке 'Уменьшить прозрачность'.</param>
        /// <returns></returns>
        public SlideMenu ButtonDecTransparencyFakelClick(int count)
        {
            for (int i = 0; i < count; i++)
            {
                listButtonsDecTransparency[(int)NumberButtonsIncDec.Fakel].Click();
            }
            return this;
        }
        /*
                /// <summary>
                /// Выполняет клик по кнопке 'Поднять наверх слой' слоя 'Амбар'.
                /// </summary>
                /// <returns></returns>
                public SlideMenu ButtonIncAmbarClick()
                {
                    Sleep();
                    listButtonsIncDec[2].Click();
                    return this;
                }
        */
        /// <summary>
        /// Увеличивает прозрачность слоя  'Амбар'.
        /// </summary>
        /// <param name="count">Колическов кликов по кнопке 'Увеличить прозрачность'.</param>
        /// <returns></returns>
        public SlideMenu ButtonIncTransparencyAmbarClick(int count)
        {
            for (int i = 0; i < count; i++)
            {
                listButtonsIncTransparency[(int)NumberButtonsIncDec.Ambar].Click();
            }
            return this;
        }

        /// <summary>
        /// Уменьшает прозрачность слоя  'Амбар'.
        /// </summary>
        /// <param name="count">Колическов кликов по кнопке 'Уменьшить прозрачность'.</param>
        /// <returns></returns>
        public SlideMenu ButtonDecTransparencyAmbarClick(int count)
        {
            for (int i = 0; i < count; i++)
            {
                listButtonsDecTransparency[(int)NumberButtonsIncDec.Ambar].Click();
            }
            return this;
        }
        /*
                /// <summary>
                /// Выполняет клик по кнопке 'Поднять наверх слой' слоя 'Кустовые площадки'.
                /// </summary>
                /// <returns></returns>
                public SlideMenu ButtonIncPlacesClick()
                {
                    Sleep();
                    listButtonsIncDec[4].Click();
                    return this;
                }
        */
        /// <summary>
        /// Увеличивает прозрачность слоя  'Кустовые площадки'.
        /// </summary>
        /// <param name="count">Колическов кликов по кнопке 'Увеличить прозрачность'.</param>
        /// <returns></returns>
        public SlideMenu ButtonIncTransparencyPlacesClick(int count)
        {
            for (int i = 0; i < count; i++)
            {
                listButtonsIncTransparency[(int)NumberButtonsIncDec.Places].Click();
            }
            return this;
        }

        /// <summary>
        /// Уменьшает прозрачность слоя  'Кустовые площадки'.
        /// </summary>
        /// <param name="count">Колическов кликов по кнопке 'Уменьшить прозрачность'.</param>
        /// <returns></returns>
        public SlideMenu ButtonDecTransparencyPlacesClick(int count)
        {
            for (int i = 0; i < count; i++)
            {
                listButtonsDecTransparency[(int)NumberButtonsIncDec.Places].Click();
            }
            return this;
        }
        /*
                /// <summary>
                /// Выполняет клик по кнопке 'Поднять наверх слой' слоя 'ДНС'.
                /// </summary>
                /// <returns></returns>
                public SlideMenu ButtonIncDNSClick()
                {
                    Sleep();
                    listButtonsIncDec[6].Click();
                    return this;
                }
        */
        /// <summary>
        /// Увеличивает прозрачность слоя  'ДНС'.
        /// </summary>
        /// <param name="count">Колическов кликов по кнопке 'Увеличить прозрачность'.</param>
        /// <returns></returns>
        public SlideMenu ButtonIncTransparencyDNSClick(int count)
        {
            for (int i = 0; i < count; i++)
            {
                listButtonsIncTransparency[(int)NumberButtonsIncDec.DNS].Click();
            }
            return this;
        }

        /// <summary>
        /// Уменьшает прозрачность слоя  'ДНС'.
        /// </summary>
        /// <param name="count">Колическов кликов по кнопке 'Уменьшить прозрачность'.</param>
        /// <returns></returns>
        public SlideMenu ButtonDecTransparencyDNSClick(int count)
        {
            for (int i = 0; i < count; i++)
            {
                listButtonsDecTransparency[(int)NumberButtonsIncDec.DNS].Click();
            }
            return this;
        }
    }
}