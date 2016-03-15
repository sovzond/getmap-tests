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
        private const string _rosreestr = "Росреестр";
        private const string _osm = "OpenStreetMap";
        private const string _toposnova = "Топоснова";
        private const string _sputnik = "Спутник";
        private const string _scheme = "Схема";
        private const string _gibrid = "Гибрид"; 
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
        private enum NumberButtonsTransparencyIncDec
        {
            Fakel = 1,
            Ambar = 2,
            Places = 3,
            DNS = 4
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
                    dicRB.Add(_rosreestr, listLayersInBaseLayers[i - 1]);
                if (listLayersInBaseLayers[i].Text == "OpenStreetMap")
                    dicRB.Add(_osm, listLayersInBaseLayers[i - 1]);
                if (listLayersInBaseLayers[i].Text == "Топооснова")
                    dicRB.Add(_toposnova, listLayersInBaseLayers[i - 1]);
            }
            for (int i = 0; i < listLayersInGoogleLayers.Count; i++)
            {
                if (listLayersInGoogleLayers[i].Text == "Схема")
                    dicRB.Add(_scheme, listLayersInGoogleLayers[i - 1]);
                if (listLayersInGoogleLayers[i].Text == "Спутник")
                    dicRB.Add(_sputnik, listLayersInGoogleLayers[i - 1]);
                if (listLayersInGoogleLayers[i].Text == "Гибрид")
                    dicRB.Add(_gibrid, listLayersInGoogleLayers[i - 1]);
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
            dicRB[_scheme].Click();
            return this;
        }

        /// <summary>
        /// Активирует чекбокс Спутник(выполняет по нему клик).
        /// </summary>
        /// <returns></returns>
        public SlideMenu LayerSputnikClick()
        {
            dicRB[_sputnik].Click();
            return this;
        }

        /// <summary>
        /// Активирует чекбокс Гибрид(выполняет по нему клик).
        /// </summary>
        /// <returns></returns>
        public SlideMenu LayerGibridClick()
        {
            dicRB[_gibrid].Click();
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
            dicRB[_rosreestr].Click();
            return this;
        }

        /// <summary>
        /// Активирует чекбокс OpenStreetMap(выполняет по нему клик).
        /// </summary>
        /// <returns></returns>
        public SlideMenu OpenStreetMapClick()
        {
            dicRB[_osm].Click();
            return this;
        }

        /// <summary>
        /// Активирует чекбокс Топоснова(выполняет по нему клик).
        /// </summary>
        /// <returns></returns>
        public SlideMenu TopOsnovaClick()
        {
            dicRB[_toposnova].Click();
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

        /// <summary>
        /// Увеличивает прозрачность слоя  'Факел'.
        /// </summary>
        /// <param name="count">Колическов кликов по кнопке 'Увеличить прозрачность'.</param>
        /// <returns></returns>
        public SlideMenu ButtonIncTransparencyFakelClick(int count)
        {
            for (int i = 0; i < count; i++)
            {
                listButtonsIncTransparency[(int)NumberButtonsTransparencyIncDec.Fakel].Click();
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
                listButtonsDecTransparency[(int)NumberButtonsTransparencyIncDec.Fakel].Click();
            }
            return this;
        }

        /// <summary>
        /// Увеличивает прозрачность слоя  'Амбар'.
        /// </summary>
        /// <param name="count">Колическов кликов по кнопке 'Увеличить прозрачность'.</param>
        /// <returns></returns>
        public SlideMenu ButtonIncTransparencyAmbarClick(int count)
        {
            for (int i = 0; i < count; i++)
            {
                listButtonsIncTransparency[(int)NumberButtonsTransparencyIncDec.Ambar].Click();
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
                listButtonsDecTransparency[(int)NumberButtonsTransparencyIncDec.Ambar].Click();
            }
            return this;
        }

        /// <summary>
        /// Увеличивает прозрачность слоя  'Кустовые площадки'.
        /// </summary>
        /// <param name="count">Колическов кликов по кнопке 'Увеличить прозрачность'.</param>
        /// <returns></returns>
        public SlideMenu ButtonIncTransparencyPlacesClick(int count)
        {
            for (int i = 0; i < count; i++)
            {
                listButtonsIncTransparency[(int)NumberButtonsTransparencyIncDec.Places].Click();
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
                listButtonsDecTransparency[(int)NumberButtonsTransparencyIncDec.Places].Click();
            }
            return this;
        }

        /// <summary>
        /// Увеличивает прозрачность слоя  'ДНС'.
        /// </summary>
        /// <param name="count">Колическов кликов по кнопке 'Увеличить прозрачность'.</param>
        /// <returns></returns>
        public SlideMenu ButtonIncTransparencyDNSClick(int count)
        {
            for (int i = 0; i < count; i++)
            {
                listButtonsIncTransparency[(int)NumberButtonsTransparencyIncDec.DNS].Click();
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
                listButtonsDecTransparency[(int)NumberButtonsTransparencyIncDec.DNS].Click();
            }
            return this;
        }
    }
}