using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;


namespace GetMapTest.GUI
{
    /// <summary>
    /// Открывает плажку в левой части экрана (Слои, Легенда). 
    /// Так же открывает вкладки данной плажки и прокликивает чекбосы.
    /// </summary>
    public class SlideMenu
    {
        private IWebDriver driver;
        private const string locationSlideMenu = "#menuSlide div.svzSimpleButton.slidePanelButton";
        private const string locationBaseLayers = "#layersCon div.svzSimpleButton.accordionButton";
        private const string locationGoogle = "#stdportal_LayerManagerBase_0 div.svzLayerManagerText";
        private const string locationLegenda = "#menuSlide div.svzSimpleButton.slidePanelLegendButton";
        private const string locationRadioButtons = "div.svzLayerManagerItem input";
        private const string locationIncDNS = "#dojoUnique4 img";
        private const string locationIncFakel = "#dojoUnique1 img";
        private const string locationIncAmbar = "#dojoUnique2 img";
        private const string locationIncPlaces = "#dojoUnique3 img";
        private const string locationDecButtons = "div.dijitSliderDecrementIconH";
        private const string locationIncButtons = "div.dijitSliderIncrementIconH";
        private IList<IWebElement> listButtonsInc;
        private IList<IWebElement> listButtonsDec;
        private IList<IWebElement> listLayersInBaseLayers;

        private SlideMenu(IWebDriver driver)
        {
            this.driver = driver;
            listLayersInBaseLayers = driver.FindElements(By.CssSelector(locationRadioButtons));
            listButtonsInc = driver.FindElements(By.CssSelector(locationIncButtons));
            listButtonsDec = driver.FindElements(By.CssSelector(locationDecButtons));
        }

        private void Sleep()
        {
            System.Threading.Thread.Sleep(2000);
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
        /// Открывает саму плажку 'СЛОИ'.
        /// </summary>
        /// <returns></returns>
        public SlideMenu OpenLayers()
        {
            Sleep();
            driver.FindElement(By.CssSelector(locationSlideMenu)).Click();
            return this;
        }

        /// <summary>
        /// Открывает базовые слои вкладки 'СЛОИ'.
        /// </summary>
        /// <returns></returns>
        public SlideMenu OpenBaseLayers()
        {
            Sleep();
            driver.FindElement(By.CssSelector(locationBaseLayers)).Click();
            return this;
        }

        /// <summary>
        /// Активирует чекбокс Схема(выполняет по нему клик).
        /// </summary>
        /// <returns></returns>
        public SlideMenu LayerSchemeClick()
        {
            Sleep();
            listLayersInBaseLayers[0].Click();
            return this;
        }

        /// <summary>
        /// Активирует чекбокс Спутник(выполняет по нему клик).
        /// </summary>
        /// <returns></returns>
        public SlideMenu LayerSputnikClick()
        {
            Sleep();
            listLayersInBaseLayers[1].Click();
            return this;
        }

        /// <summary>
        /// Активирует чекбокс Гибрид(выполняет по нему клик).
        /// </summary>
        /// <returns></returns>
        public SlideMenu LayerGibridClick()
        {
            Sleep();
            listLayersInBaseLayers[2].Click();
            return this;
        }

        /// <summary>
        /// Открывает владку 'Google' во вкладке 'Базовые слои'.
        /// </summary>
        /// <returns></returns>
        public SlideMenu OpenGoogle()
        {
            Sleep();
            driver.FindElement(By.CssSelector(locationGoogle)).Click();
            return this;
        }

        /// <summary>
        /// Активирует чекбокс Росреестр(выполняет по нему клик).
        /// </summary>
        /// <returns></returns>
        public SlideMenu RosreestrClick()
        {
            Sleep();
            listLayersInBaseLayers[3].Click();
            return this;
        }

        /// <summary>
        /// Активирует чекбокс OpenStreetMap(выполняет по нему клик).
        /// </summary>
        /// <returns></returns>
        public SlideMenu OpenStreetMapClick()
        {
            Sleep();
            listLayersInBaseLayers[4].Click();
            return this;
        }

        /// <summary>
        /// Открывает вкладку 'Легенда' на плажке в левой части экрана.
        /// </summary>
        /// <returns></returns>
        public SlideMenu OpenLegenda()
        {
            Sleep();
            driver.FindElement(By.CssSelector(locationLegenda)).Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Поднять наверх слой' слоя 'Факел'.
        /// </summary>
        /// <returns></returns>
        public SlideMenu ButtonIncFakelClick()
        {
            Sleep();
            driver.FindElement(By.CssSelector(locationIncFakel)).Click();
            return this;
        }

        /// <summary>
        /// Увеличивает прозрачность слоя  'Факел'.
        /// </summary>
        /// <param name="count">Колическов кликов по кнопке 'Увеличить прозрачность'.</param>
        /// <returns></returns>
        public SlideMenu ButtonIncTransparencyFakelClick(int count)
        {
            Sleep();
            for (int i = 0; i < count; i++)
            {
                listButtonsInc[0].Click();
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
            Sleep();
            for (int i = 0; i < count; i++)
            {
                listButtonsInc[1].Click();
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
            Sleep();
            for (int i = 0; i < count; i++)
            {
                listButtonsInc[2].Click();
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
            Sleep();
            for (int i = 0; i < count; i++)
            {
                listButtonsInc[3].Click();
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
            Sleep();
            for (int i = 0; i < count; i++)
            {
                listButtonsDec[0].Click();
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
            Sleep();
            for (int i = 0; i < count; i++)
            {
                listButtonsDec[1].Click();
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
            Sleep();
            for (int i = 0; i < count; i++)
            {
                listButtonsDec[2].Click();
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
            listButtonsDec = driver.FindElements(By.CssSelector(locationDecButtons));
            Sleep();
            for (int i = 0; i < count; i++)
            {
                listButtonsDec[3].Click();
            }
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Поднять наверх слой' слоя 'Амбар'.
        /// </summary>
        /// <returns></returns>
        public SlideMenu ButtonIncAmbarClick()
        {
            Sleep();
            driver.FindElement(By.CssSelector(locationIncAmbar)).Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Поднять наверх слой' слоя 'Кустовые площадки'.
        /// </summary>
        /// <returns></returns>
        public SlideMenu ButtonIncPlacesClick()
        {
            Sleep();
            driver.FindElement(By.CssSelector(locationIncPlaces)).Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Поднять наверх слой' слоя 'ДНС'.
        /// </summary>
        /// <returns></returns>
        public SlideMenu ButtonIncDNSClick()
        {
            Sleep();
            driver.FindElement(By.CssSelector(locationIncDNS)).Click();
            return this;
        }

    }
}