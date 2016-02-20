using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;

namespace GetMapTest.GUI
{
    /// <summary>
    /// Открывает выпадающее меню и активирует чекбоксы раздела 'Слои' .
    /// </summary>
    public class Layers
    {
        private IWebDriver driver;
        private IWebElement elementTestLayerDDM;
        private IWebElement elementNewGroupDDM;
        private IWebElement elementZoyaTestDDM;
        private IWebElement elementMoscowAreaDDM;
        private IWebElement elementGasStructDDM;
        private IWebElement elementEnergyStructDDM;
        private IWebElement elementNeftyStructDDM;
        private IWebElement elementTematicMapDDM;
        private IWebElement elementCosmoPhotoDDM;
        private IWebElement elementTestLayerCB;
        private IWebElement elementNewGroupCB;
        private IWebElement elementZoyaTestCB;
        private IWebElement elementMoscowAreaCB;
        private IWebElement elementGasStructCB;
        private IWebElement elementEnergyStructCB;
        private IWebElement elementNeftyStructCB;
        private IWebElement elementTematicMapCB;
        private IWebElement elementCosmoPhotoCB;
        private IList<IWebElement> listDropDowmMenu;
        private IList<IWebElement> listCheckBoxes;
        private const string locationDropDownMenu = "div.svzLayerManagerText";
        private const string locationCheckBoxes = "div.svzLayerManagerItem.svzLayerManagerItemSection div";
        private Layers(IWebDriver driver)
        {
            this.driver = driver;
            SetValueList();
            SetValueElements();
        }

        private void Sleep()
        {
            Thread.Sleep(2000);
        }

        private Layers SetValueList()
        {
            listDropDowmMenu = driver.FindElements(By.CssSelector(locationDropDownMenu));
            listCheckBoxes = driver.FindElements(By.CssSelector(locationCheckBoxes));
            return this;
        }

        private Layers SetValueElements()
        {
            foreach (var el in listDropDowmMenu)
            {
                if (el.Text == "Тест")
                    elementTestLayerDDM = el;
                if (el.Text == "Новая группа")
                    elementNewGroupDDM = el;
                if (el.Text == "Зоя. Тест")
                    elementZoyaTestDDM = el;
                if (el.Text == "Московская область")
                    elementMoscowAreaDDM = el;
                if (el.Text == "Газовая инфраструктура")
                    elementGasStructDDM = el;
                if (el.Text == "Энергетическая инфраструктура")
                    elementEnergyStructDDM = el;
                if (el.Text == "Нефтяная инфраструктура")
                    elementNeftyStructDDM = el;
                if (el.Text == "Тематические карты")
                    elementTematicMapDDM = el;
                if (el.Text == "Космические снимки")
                    elementCosmoPhotoDDM = el;
            }
            for (int i = 0; i < listCheckBoxes.Count; i++)
            {
                if (listCheckBoxes[i].Text == "Тест")
                {
                    Thread.Sleep(500);
                    elementTestLayerCB = listCheckBoxes[i - 1];
                }
                if (listCheckBoxes[i].Text == "Новая группа")
                {
                    Thread.Sleep(500);
                    elementNewGroupCB = listCheckBoxes[i - 1];
                }
                if (listCheckBoxes[i].Text == "Зоя. Тест")
                {
                    Thread.Sleep(500);
                    elementZoyaTestCB = listCheckBoxes[i - 1];
                }
                if (listCheckBoxes[i].Text == "Московская область")
                {
                    Thread.Sleep(500);
                    elementMoscowAreaCB = listCheckBoxes[i - 1];
                }
                if (listCheckBoxes[i].Text == "Газовая инфраструктура")
                {
                    Thread.Sleep(500);
                    elementGasStructCB = listCheckBoxes[i - 1];
                }
                if (listCheckBoxes[i].Text == "Энергетическая инфраструктура")
                {
                    Thread.Sleep(500);
                    elementEnergyStructCB = listCheckBoxes[i - 1];
                }
                if (listCheckBoxes[i].Text == "Нефтяная инфраструктура")
                {
                    Thread.Sleep(500);
                    elementNeftyStructCB = listCheckBoxes[i - 1];
                }
                if (listCheckBoxes[i].Text == "Тематические карты")
                {
                    Thread.Sleep(500);
                    elementTematicMapCB = listCheckBoxes[i - 1];
                }
                if (listCheckBoxes[i].Text == "Космические снимки")
                {
                    Thread.Sleep(500);
                    elementCosmoPhotoCB = listCheckBoxes[i - 1];
                }
            }
            return this;
        }

        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static Layers get(IWebDriver driver)
        {
            return new Layers(driver);

        }

        private bool getSelectedTest()
        {
            if (elementTestLayerDDM.Selected)
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает значение 'true' если чекбокс слоя 'Тест' активен.
        /// </summary>
        public bool GetSelectedTest
        {
            get
            {
                return getSelectedTest();
            }
        }

        private bool getSelectedNewGroup()
        {
            if (elementNewGroupDDM.Selected)
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает значение 'true' если чекбокс слоя 'Новая группа' активен.
        /// </summary>
        public bool GetSelectedNewGroup
        {
            get
            {
                return getSelectedNewGroup();
            }
        }

        private bool getSelectedZoyaTest()
        {
            if (elementZoyaTestDDM.Selected)
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает значение 'true' если чекбокс слоя 'Зоя. Тест' активен.
        /// </summary>
        public bool GetSelectedZoyaTest
        {
            get
            {
                return getSelectedZoyaTest();
            }
        }

        private bool getSelectedMoscowArea()
        {
            if (elementMoscowAreaDDM.Selected)
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает значение 'true' если чекбокс слоя 'Московская область' активен.
        /// </summary>
        public bool GetSelectedMoscowArea
        {
            get
            {
                return getSelectedMoscowArea();
            }
        }

        private bool getSelectedGasStruct()
        {
            if (elementGasStructDDM.Selected)
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает значение 'true' если чекбокс слоя 'Газовая инфраструктура' активен.
        /// </summary>
        public bool GetSelectedGasStruct
        {
            get
            {
                return getSelectedGasStruct();
            }
        }

        private bool getSelectedEnergyStruct()
        {
            if (elementEnergyStructDDM.Selected)
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает значение 'true' если чекбокс слоя 'Энергетическая инфраструктура' активен.
        /// </summary>
        public bool GetSelectedEnergyStruct
        {
            get
            {
                return getSelectedEnergyStruct();
            }
        }

        private bool getSelectedNeftyStruct()
        {
            if (elementNeftyStructDDM.Selected)
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает значение 'true' если чекбокс слоя 'Нефтяная инфраструктура' активен.
        /// </summary>
        public bool GetSelectedNeftyStruct
        {
            get
            {
                return getSelectedNeftyStruct();
            }
        }
        private bool getSelectedTimaticMap()
        {
            if (elementTematicMapDDM.Selected)
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает значение 'true' если чекбокс слоя 'Тематические карты' активен.
        /// </summary>
        public bool GetSelectedTematicMap
        {
            get
            {
                return getSelectedTimaticMap();
            }
        }

        private bool getSelectedCosmoPhoto()
        {
            if (elementCosmoPhotoDDM.Selected)
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает значение 'true' если чекбокс слоя 'Космические снимки' активен.
        /// </summary>
        public bool GetSelectedCosmoPhoto
        {
            get
            {
                return getSelectedCosmoPhoto();
            }
        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Тест'.
        /// </summary>
        /// <returns></returns>
        public Layers TestCheckBoxClick()
        {
            Sleep();
            elementTestLayerCB.Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Тест'.
        /// </summary>
        /// <returns></returns>
        public Layers TestOpenCloseList()
        {
            Sleep();
            elementTestLayerDDM.Click();
            return this;
        }


        /// <summary>
        /// Дает доступ ко всем чекбоксам выпадающего меню 'Тест.'
        /// </summary>
        public class TestLayerClass
        {
            private IWebDriver driver;
            private IWebElement elementAmerica;
            private IWebElement elementBase_raster;
            private IWebElement elementAmbar;
            private IWebElement elementaa_states_4326;
            private IWebElement elementAmericaSB;
            private IWebElement elementBase_rasterSB;
            private IWebElement elementAmbarSB;
            private IWebElement elementaa_states_4326SB;
            private IList<IWebElement> listCheckBoxs;
            private const string locationCheckBoxs = "div.svzLayerManagerItem.svzLayerManagerItem1 div";

            private TestLayerClass(IWebDriver driver)
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
            public static TestLayerClass get(IWebDriver driver)
            {
                return new TestLayerClass(driver);
            }

            private void Sleep()
            {
                Thread.Sleep(2000);
            }

            private TestLayerClass SetValueList()
            {
                listCheckBoxs = driver.FindElements(By.CssSelector(locationCheckBoxs));
                return this;
            }

            private TestLayerClass SetValueElements()
            {
                for (int i = 0; i < listCheckBoxs.Count; i++)
                {
                    if (listCheckBoxs[i].Text == "aa_states_4326")
                    {
                        Thread.Sleep(500);
                        elementaa_states_4326 = listCheckBoxs[i - 1];
                        elementaa_states_4326SB = listCheckBoxs[i + 1];
                    }
                    if (listCheckBoxs[i].Text == "США")
                    {
                        Thread.Sleep(500);
                        elementAmerica = listCheckBoxs[i - 1];
                        elementAmericaSB = listCheckBoxs[i + 1];
                    }
                    if (listCheckBoxs[i].Text == "rtk:base_raster")
                    {
                        Thread.Sleep(500);
                        elementBase_raster = listCheckBoxs[i - 1];
                        elementBase_rasterSB = listCheckBoxs[i + 1];
                    }
                    if (listCheckBoxs[i].Text == "ambar")
                    {
                        Thread.Sleep(500);
                        elementAmbar = listCheckBoxs[i - 1];
                        elementAmbarSB = listCheckBoxs[i + 1];
                    }
                }
                return this;
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'aa_states_4326'.
            /// </summary>
            /// <returns></returns>
            public TestLayerClass aa_states_4326Click()
            {
                Sleep();
                elementaa_states_4326.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'aa_states_4326'.
            /// </summary>
            /// <returns></returns>
            public TestLayerClass aa_states_4326SBClick()
            {
                Sleep();
                elementaa_states_4326SB.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'США'.
            /// </summary>
            /// <returns></returns>
            public TestLayerClass AmericaClick()
            {
                Sleep();
                elementAmerica.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'США'.
            /// </summary>
            /// <returns></returns>
            public TestLayerClass AmericaSBClick()
            {
                Sleep();
                elementAmericaSB.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'rtk:base_raster'.
            /// /// </summary>
            /// <returns></returns>
            public TestLayerClass Base_RasterClick()
            {
                Sleep();
                elementBase_raster.Click();
                return this;
            }
/*
            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'rtk:base_raster'.
            /// </summary>
            /// <returns></returns>
            public TestLayerClass Bse_RasterSBClick()
            {
                Sleep();
                elementBase_rasterSB.Click();
                return this;
            }
*/
            /// <summary>
            /// Выполняет клик по чекбоксу 'ambar'.
            /// </summary>
            /// <returns></returns>
            public TestLayerClass AmbarClick()
            {
                Sleep();
                elementAmbar.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'ambar'.
            /// </summary>
            /// <returns></returns>
            public TestLayerClass AmbarSBClick()
            {
                Sleep();
                elementAmbarSB.Click();
                return this;
            }
        }




        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Новая группа'.
        /// </summary>
        /// <returns></returns>
        public Layers NewGroupCheckBoxClick()
        {
            Sleep();
            elementNewGroupCB.Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Новая группа'.
        /// </summary>
        /// <returns></returns>
        public Layers NewGroupOpenCloseList()
        {
            Sleep();
            elementNewGroupDDM.Click();
            return this;
        }


        /// <summary>
        /// Дает доступ ко всем чекбоксам выпадающего меню 'Новая группа'.
        /// </summary>
        public class NewGroupClass
        {
            private IWebDriver driver;
            private IWebElement elementTsp_25;
            private IWebElement elementTsp_25SB;
            private IList<IWebElement> listCheckBoxs;
            private const string locationCheckBoxs = "div.svzLayerManagerItem.svzLayerManagerItem1 div";

            private NewGroupClass(IWebDriver driver)
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
            public static NewGroupClass get(IWebDriver driver)
            {
                return new NewGroupClass(driver);
            }

            private NewGroupClass SetValueList()
            {
                listCheckBoxs = driver.FindElements(By.CssSelector(locationCheckBoxs));
                return this;
            }

            private NewGroupClass SetValueElements()
            {
                for (int i = 0; i < listCheckBoxs.Count; i++)
                {
                    if (listCheckBoxs[i].Text == "tsp_25")
                    {
                        Thread.Sleep(500);
                        elementTsp_25 = listCheckBoxs[i - 1];
                        elementTsp_25SB = listCheckBoxs[i + 1];
                    }
                }

                return this;
            }

            private void Sleep()
            {
                Thread.Sleep(2000);
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'tsp_25'.
            /// </summary>
            /// <returns></returns>
            public NewGroupClass Tsp_25Click()
            {
                Sleep();
                elementTsp_25.Click();
                return this;
            }
/*
            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'tsp_25'.
            /// </summary>
            /// <returns></returns>
            public NewGroupClass Tsp_25SBClick()
            {
                Sleep();
                elementTsp_25SB.Click();
                return this;
            }
            */
        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Зоя. Тест'.
        /// </summary>
        /// <returns></returns>
        public Layers ZoyaTestCheckBoxClick()
        {
            Sleep();
            elementZoyaTestCB.Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Зоя. Тест'.
        /// </summary>
        /// <returns></returns>
        public Layers ZoyaTestOpenCloseList()
        {
            Sleep();
            elementZoyaTestDDM.Click();
            return this;
        }

        /// <summary>
        /// Дает доступ ко всем чекбоксам выпадающего меню 'Зоя. Тест'.
        /// </summary>
        public class ZoyaTestClass
        {
            private IWebDriver driver;
            private IWebElement elementMO_RE;
            private IWebElement elementL8_MO;
            private IWebElement elementMO_RESB;
            private IWebElement elementL8_MOSB;
            private const string locationCheckBoxes = "div.svzLayerManagerItem.svzLayerManagerItem1 div";
            private IList<IWebElement> listCheckBoxes;

            private ZoyaTestClass(IWebDriver driver)
            {
                this.driver = driver;
                SetValueList();
                SetValueElements();
            }

            private void Sleep()
            {
                Thread.Sleep(2000);
            }

            private ZoyaTestClass SetValueList()
            {
                listCheckBoxes = driver.FindElements(By.CssSelector(locationCheckBoxes));
                return this;
            }

            private ZoyaTestClass SetValueElements()
            {
                for (int i = 0; i < listCheckBoxes.Count; i++)
                {
                    if (listCheckBoxes[i].Text == "GetMap_MO_RE")
                    {
                        Thread.Sleep(500);
                        elementMO_RE = listCheckBoxes[i - 1];
                        elementMO_RESB = listCheckBoxes[i + 1];
                    }
                    if (listCheckBoxes[i].Text == "GetMap_L8_MO")
                    {
                        Thread.Sleep(500);
                        elementL8_MO = listCheckBoxes[i - 1];
                        elementL8_MOSB = listCheckBoxes[i + 1];
                    }
                }
                return this;
            }

            /// <summary>
            /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
            /// </summary>
            /// <param name="driver">Передает аргумент для закрытого конструктора</param>
            /// <returns></returns>
            public static ZoyaTestClass get(IWebDriver driver)
            {
                return new ZoyaTestClass(driver);
            }

            /// <summary>
            /// Выполянет клик по чекбоксу 'GetMap_MO_RE'.
            /// </summary>
            /// <returns></returns>
            public ZoyaTestClass MO_REClick()
            {
                Sleep();
                elementMO_RE.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'GetMap_MO_RE'.
            /// </summary>
            /// <returns></returns>
            public ZoyaTestClass MO_RESBClick()
            {
                Sleep();
                elementMO_RESB.Click();
                return this;
            }

            /// <summary>
            /// Выполянет клик по чекбоксу 'GetMap_L8_MO'.
            /// </summary>
            /// <returns></returns>
            public ZoyaTestClass L8_MO()
            {
                Sleep();
                elementL8_MO.Click();
                return this;
            }

            /// <summary>
            ///Выполняет клик по кнопке 'Настройка слоя' слоя 'GetMap_L8_MO'.
            /// </summary>
            /// <returns></returns>
            public ZoyaTestClass L8_MOSBClick()
            {
                Sleep();
                elementL8_MOSB.Click();
                return this;
            }

        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Московская область'.
        /// </summary>
        /// <returns></returns>
        public Layers MoscowAreaCheckBoxClick()
        {
            Sleep();
            elementMoscowAreaCB.Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Московская область'.
        /// </summary>
        /// <returns></returns>
        public Layers MoscowAreaOpenCloseList()
        {
            Sleep();
            elementMoscowAreaDDM.Click();
            return this;
        }

        public class MoscowAreaClass
        {
            private IWebDriver driver;
            private IWebElement elementLandsat;
            private IWebElement elementRapidEye;
            private IWebElement elementLandsatSB;
            private IWebElement elementRapidEyeSB;
            private const string locationCheckBoxes = "div.svzLayerManagerItem.svzLayerManagerItem1 div";
            private IList<IWebElement> listCheckBoxes;

            private MoscowAreaClass(IWebDriver driver)
            {
                this.driver = driver;
                SetValueList();
                SetValueElements();
            }

            private void Sleep()
            {
                Thread.Sleep(2000);
            }

            private MoscowAreaClass SetValueList()
            {
                listCheckBoxes = driver.FindElements(By.CssSelector(locationCheckBoxes));
                return this;
            }

            private MoscowAreaClass SetValueElements()
            {
                for (int i = 0; i < listCheckBoxes.Count; i++)
                {
                    if (listCheckBoxes[i].Text == "Мозаика Landsat")
                    {
                        Thread.Sleep(500);
                        elementLandsat = listCheckBoxes[i - 1];
                        elementLandsatSB = listCheckBoxes[i + 1];

                    }
                    if (listCheckBoxes[i].Text == "Мозаика RapidEye")
                    {
                        Thread.Sleep(500);
                        elementRapidEye = listCheckBoxes[i - 1];
                        elementRapidEyeSB = listCheckBoxes[i + 1];
                    }
                }
                return this;
            }

            /// <summary>
            /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
            /// </summary>
            /// <param name="driver">Передает аргумент для закрытого конструктора</param>
            /// <returns></returns>
            public static MoscowAreaClass get(IWebDriver driver)
            {
                return new MoscowAreaClass(driver);
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'Мозаика Landsat'.
            /// </summary>
            /// <returns></returns>
            public MoscowAreaClass LandsatClick()
            {
                Sleep();
                elementLandsat.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Мозаика Landsat'.
            /// </summary>
            /// <returns></returns>
            public MoscowAreaClass LandsatSBClick()
            {
                Sleep();
                elementLandsatSB.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'Мозаика RapidEye'.
            /// </summary>
            /// <returns></returns>
            public MoscowAreaClass RapidEyeClick()
            {
                Sleep();
                elementRapidEye.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Мозаика RapidEye'.
            /// </summary>
            /// <returns></returns>
            public MoscowAreaClass RapidEyeSBClick()
            {
                Sleep();
                elementRapidEyeSB.Click();
                return this;
            }

        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Газовая инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers GasStructCheckBoxClick()
        {
            Sleep();
            elementGasStructCB.Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Газовая инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers GasStructOpenCloseList()
        {
            Sleep();
            elementGasStructDDM.Click();
            return this;
        }


        /// <summary>
        /// Дает доступ ко всем чекбоксам выпадающего меню 'Газовая инфраструктура'.
        /// </summary>
        public class GasStructClass
        {
            private IWebDriver driver;
            private IWebElement elemenGPZPoint;
            private IWebElement elementGazoprovod;
            private IWebElement elementGPZPoligon;
            private IWebElement elemenGPZPointSB;
            private IWebElement elementGazoprovodSB;
            private IWebElement elementGPZPoligonSB;
            private IList<IWebElement> listCheckBoxs;
            private const string locationCheckBoxs = "div.svzLayerManagerItem.svzLayerManagerItem1 div";

            private GasStructClass(IWebDriver driver)
            {
                this.driver = driver;
                SetValueList();
                SetValueElements();
            }

            private void Sleep()
            {
                Thread.Sleep(2000);
            }

            private GasStructClass SetValueList()
            {
                listCheckBoxs = driver.FindElements(By.CssSelector(locationCheckBoxs));
                return this;
            }

            private GasStructClass SetValueElements()
            {
                for (int i = 0; i < listCheckBoxs.Count; i++)
                {
                    if (listCheckBoxs[i].Text == "ГПЗ (точка)")
                    {
                        Thread.Sleep(500);
                        elemenGPZPoint = listCheckBoxs[i - 1];
                        elemenGPZPointSB = listCheckBoxs[i + 1];
                    }
                    if (listCheckBoxs[i].Text == "Газопровод")
                    {
                        Thread.Sleep(500);
                        elementGazoprovod = listCheckBoxs[i - 1];
                        elementGazoprovodSB = listCheckBoxs[i + 1];
                    }
                    if (listCheckBoxs[i].Text == "ГПЗ (полигон)")
                    {
                        Thread.Sleep(500);
                        elementGPZPoligon = listCheckBoxs[i - 1];
                        elementGPZPoligonSB = listCheckBoxs[i + 1];
                    }
                }
                return this;
            }

            /// <summary>
            /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
            /// </summary>
            /// <param name="driver">Передает аргумент для закрытого конструктора</param>
            /// <returns></returns>
            public static GasStructClass get(IWebDriver driver)
            {
                return new GasStructClass(driver);
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'ГПЗ (точка)'.
            /// </summary>
            /// <returns></returns>
            public GasStructClass GPZPointClick()
            {
                Sleep();
                elemenGPZPoint.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'ГПЗ (точка)'.
            /// </summary>
            /// <returns></returns>
            public GasStructClass GPZPointSBClick()
            {
                Sleep();
                elemenGPZPointSB.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'Газопровод'.
            /// </summary>
            /// <returns></returns>
            public GasStructClass GazoprovodClick()
            {
                Sleep();
                elementGazoprovod.Click();
                return this;
            }
            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Газопровод'.
            /// </summary>
            /// <returns></returns>
            public GasStructClass GazoprovodSBClick()
            {
                Sleep();
                elementGazoprovodSB.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'ГПЗ (полигон)'.
            /// </summary>
            /// <returns></returns>
            public GasStructClass GPZPoligonClick()
            {
                Sleep();
                elementGPZPoligon.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'ГПЗ (полигон)'.
            /// </summary>
            /// <returns></returns>
            public GasStructClass GPZPoligonSBClick()
            {
                Sleep();
                elementGPZPoligonSB.Click();
                return this;
            }
        }


        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Энергетическая инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers EnergyStructCheckBoxClick()
        {
            Sleep();
            elementEnergyStructCB.Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел  'Энергетическая инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers EnergyStructOpenCloseList()
        {
            Sleep();
            elementEnergyStructDDM.Click();
            return this;
        }


        /// <summary>
        /// Дает доступ ко всем чекбоксам выпадающего меню 'Энергетичская инфраструктура'.
        /// </summary>
        public class EnergyStructClass
        {
            private IWebDriver driver;
            private IWebElement elementElectroStationPoint;
            private IWebElement elementPodstationPoint;
            private IWebElement elementLEP;
            private IWebElement elementElectroStationPoligon;
            private IWebElement elementPodstationPoligon;
            private IWebElement elementElectroStationPointSB;
            private IWebElement elementPodstationPointSB;
            private IWebElement elementLEPSB;
            private IWebElement elementElectroStationPoligonSB;
            private IWebElement elementPodstationPoligonSB;
            private IList<IWebElement> listCheckBoxs;
            private const string locationCheckBoxs = "div.svzLayerManagerItem.svzLayerManagerItem1 div";

            private EnergyStructClass(IWebDriver driver)
            {
                this.driver = driver;
                SetValueList();
                SetValueElements();
            }

            private void Sleep()
            {
                Thread.Sleep(2000);
            }

            private EnergyStructClass SetValueList()
            {
                listCheckBoxs = driver.FindElements(By.CssSelector(locationCheckBoxs));
                return this;
            }

            private EnergyStructClass SetValueElements()
            {
                for (int i = 0; i < listCheckBoxs.Count; i++)
                {
                    if (listCheckBoxs[i].Text == "Электростанции (точка)")
                    {
                        Thread.Sleep(500);
                        elementElectroStationPoint = listCheckBoxs[i - 1];
                        elementElectroStationPointSB = listCheckBoxs[i + 1];
                    }
                    if (listCheckBoxs[i].Text == "Подстанции (точка)")
                    {
                        Thread.Sleep(500);
                        elementPodstationPoint = listCheckBoxs[i - 1];
                        elementPodstationPointSB = listCheckBoxs[i + 1];
                    }
                    if (listCheckBoxs[i].Text == "ЛЭП")
                    {
                        Thread.Sleep(500);
                        elementLEP = listCheckBoxs[i - 1];
                        elementLEPSB = listCheckBoxs[i + 1];
                    }
                    if (listCheckBoxs[i].Text == "Электростанции (полигон)")
                    {
                        Thread.Sleep(500);
                        elementElectroStationPoligon = listCheckBoxs[i - 1];
                        elementElectroStationPoligonSB = listCheckBoxs[i + 1];

                    }
                    if (listCheckBoxs[i].Text == "Подстанции (полигон)")
                    {
                        Thread.Sleep(500);
                        elementPodstationPoligon = listCheckBoxs[i - 1];
                        elementPodstationPoligonSB = listCheckBoxs[i + 1];
                    }
                }
                return this;
            }

            /// <summary>
            /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
            /// </summary>
            /// <param name="driver">Передает аргумент для закрытого конструктора</param>
            /// <returns></returns>
            public static EnergyStructClass get(IWebDriver driver)
            {
                return new EnergyStructClass(driver);
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'Электростанция (точка)'.
            /// </summary>
            /// <returns></returns>
            public EnergyStructClass ElectroStationPointClick()
            {
                Sleep();
                elementElectroStationPoint.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Электростанция (точка)'.
            /// </summary>
            /// <returns></returns>
            public EnergyStructClass ElectroStationPointSBClick()
            {
                Sleep();
                elementElectroStationPointSB.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'Подстанция (точка)'.
            /// </summary>
            /// <returns></returns>
            public EnergyStructClass PodstationPointClick()
            {
                Sleep();
                elementPodstationPoint.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Подстанция (точка)'.
            /// </summary>
            /// <returns></returns>
            public EnergyStructClass PodstationPointSBClick()
            {
                Sleep();
                elementPodstationPointSB.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'ЛЭП'.
            /// </summary>
            /// <returns></returns>
            public EnergyStructClass LEPClick()
            {
                Sleep();
                elementLEP.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'ЛЭЫ'.
            /// </summary>
            /// <returns></returns>
            public EnergyStructClass LEPSBClick()
            {
                Sleep();
                elementLEPSB.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'Электростанция (полигон)'.
            /// </summary>
            /// <returns></returns>
            public EnergyStructClass ElectroStationPoligonClick()
            {
                Sleep();
                elementElectroStationPoligon.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Электростанция (полигон)'.
            /// </summary>
            /// <returns></returns>
            public EnergyStructClass ElectroStationPoligonSBClick()
            {
                Sleep();
                elementElectroStationPoligonSB.Click();
                return this;
            }
        
            /// <summary>
            /// Выполняет клик по чекбоксу 'Подстанции (полигон)'.
            /// </summary>
            /// <returns></returns>
            public EnergyStructClass PodstationPoligon()
            {
                Sleep();
                elementPodstationPoligon.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Полстанции (полигон)'.
            /// </summary>
            /// <returns></returns>
            public EnergyStructClass PodstationPoligonSBClick()
            {
                Sleep();
                elementPodstationPoligonSB.Click();
                return this;
            }

        }


        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Нефтяная инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers NeftyStructCheckBoxClick()
        {
            Sleep();
            elementNeftyStructCB.Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Нефтяная инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers NeftyStructOpenCloseList()
        {
            Sleep();
            elementNeftyStructDDM.Click();
            return this;
        }


        /// <summary>
        /// Дает доступ ко всем чекбоксам выпадающего меню 'Нефтяная инфраструктура'.
        /// </summary>
        public class NeftyStructClass
        {
            private IWebDriver driver;
            private IWebElement elementFakel;
            private IWebElement elementAmbar;
            private IWebElement elementPlaces;
            private IWebElement elementDNS;
            private IWebElement elementFakelSB;
            private IWebElement elementAmbarSB;
            private IWebElement elementPlacesSB;
            private IWebElement elementDNSSB;
            private IList<IWebElement> listCheckBoxs;
            private const string locationCheckBoxs = "div.svzLayerManagerItem.svzLayerManagerItem1 div";

            private NeftyStructClass(IWebDriver driver)
            {
                this.driver = driver;
                SetValueList();
                SetValueElements();
            }

            private void Sleep()
            {
                Thread.Sleep(2000);
            }

            private NeftyStructClass SetValueList()
            {
                listCheckBoxs = driver.FindElements(By.CssSelector(locationCheckBoxs));
                return this;
            }

            private NeftyStructClass SetValueElements()
            {
                for (int i = 0; i < listCheckBoxs.Count; i++)
                {
                    if (listCheckBoxs[i].Text == "Факелы")
                    {
                        Thread.Sleep(500);
                        elementFakel = listCheckBoxs[i - 1];
                        elementFakelSB = listCheckBoxs[i + 1];
                    }
                    if (listCheckBoxs[i].Text == "Амбары")
                    {
                        Thread.Sleep(500);
                        elementAmbar = listCheckBoxs[i - 1];
                        elementAmbarSB = listCheckBoxs[i + 1];
                    }
                    if (listCheckBoxs[i].Text == "Кустовые площадки")
                    {
                        Thread.Sleep(500);
                        elementPlaces = listCheckBoxs[i - 1];
                        elementPlacesSB = listCheckBoxs[i + 1];
                    }
                    if (listCheckBoxs[i].Text == "ДНС")
                    {
                        Thread.Sleep(500);
                        elementDNS = listCheckBoxs[i - 1];
                        elementDNSSB = listCheckBoxs[i + 1];
                    }
                }
                return this;
            }

            /// <summary>
            /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
            /// </summary>
            /// <param name="driver">Передает аргумент для закрытого конструктора</param>
            /// <returns></returns>
            public static NeftyStructClass get(IWebDriver driver)
            {
                return new NeftyStructClass(driver);
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'Факелы'.
            /// </summary>
            /// <returns></returns>
            public NeftyStructClass FakelClick()
            {
                Sleep();
                elementFakel.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Факелы'.
            /// </summary>
            /// <returns></returns>
            public NeftyStructClass FakelSBClick()
            {
                Sleep();
                elementFakelSB.Click();
                return this;
            }


            /// <summary>
            /// Выполняет клик по чекбоксу 'Амбары'.
            /// </summary>
            /// <returns></returns>
            public NeftyStructClass AmbarClick()
            {
                Sleep();
                elementAmbar.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Амбары'.
            /// </summary>
            /// <returns></returns>
            public NeftyStructClass AmbarSBClick()
            {
                Sleep();
                elementAmbarSB.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'Кустовые площадки'.
            /// </summary>
            /// <returns></returns>
            public NeftyStructClass PlacesClick()
            {
                Sleep();
                elementPlaces.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Кустовые площадки'.
            /// </summary>
            /// <returns></returns>
            public NeftyStructClass PlacesSBClick()
            {
                Sleep();
                elementPlacesSB.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'ДНС'.
            /// </summary>
            /// <returns></returns>
            public NeftyStructClass DNSClick()
            {
                Sleep();
                elementDNS.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'ДНС'.
            /// </summary>
            /// <returns></returns>
            public NeftyStructClass DNSSBClick()
            {
                Sleep();
                elementDNSSB.Click();
                return this;
            }

        }


        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Тематические карты'.
        /// </summary>
        /// <returns></returns>
        public Layers TematicMapCheckBoxClick()
        {
            Sleep();
            elementTematicMapCB.Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел  'Тематические карты'.
        /// </summary>
        /// <returns></returns>
        public Layers TematicMapOpenCloseList()
        {
            Sleep();
            elementTematicMapDDM.Click();
            return this;
        }


        /// <summary>
        /// Дает доступ ко всем чекбоксам выпадающего меню 'Тематические карты'.
        /// </summary>
        public class TematicMapClass
        {
            private IWebDriver driver;
            private IWebElement elementCheckBox1;
            private IWebElement elementCheckBox2;
            private IWebElement elementCheckBox3;
            private IWebElement elementCheckBox4;
            private IWebElement elementCheckBox5;
            private IWebElement elementCheckBox6;
            private IWebElement elementCheckBox1SB;
            private IWebElement elementCheckBox2SB;
            private IWebElement elementCheckBox3SB;
            private IWebElement elementCheckBox4SB;
            private IWebElement elementCheckBox5SB;
            private IWebElement elementCheckBox6SB;
            private IList<IWebElement> listCheckBoxs;
            private const string locationCheckBoxs = "div.svzLayerManagerItem.svzLayerManagerItem1 div";

            private TematicMapClass(IWebDriver driver)
            {
                this.driver = driver;
                SetValueList();
                SetValueElements();
            }

            private void Sleep()
            {
                Thread.Sleep(2000);
            }

            private TematicMapClass SetValueList()
            {
                listCheckBoxs = driver.FindElements(By.CssSelector(locationCheckBoxs));
                return this;
            }

            private TematicMapClass SetValueElements()
            {
                for (int i = 0; i < listCheckBoxs.Count; i++)
                {
                    if (listCheckBoxs[i].Text == "Площадки разведочной скважины 2006 г.")
                    {
                        Thread.Sleep(500);
                        elementCheckBox1 = listCheckBoxs[i - 1];
                        elementCheckBox1SB = listCheckBoxs[i + 1];
                    }
                    if (listCheckBoxs[i].Text == "Площадки разведочной скважины 2008 г.")
                    {
                        Thread.Sleep(500);
                        elementCheckBox2 = listCheckBoxs[i - 1];
                        elementCheckBox2SB = listCheckBoxs[i + 1];
                    }
                    if (listCheckBoxs[i].Text == "Нефтяные разливы 2006 г.")
                    {
                        Thread.Sleep(500);
                        elementCheckBox3 = listCheckBoxs[i - 1];
                        elementCheckBox3SB = listCheckBoxs[i + 1];
                    }
                    if (listCheckBoxs[i].Text == "Нефтяные разливы 2008 г.")
                    {
                        Thread.Sleep(500);
                        elementCheckBox4 = listCheckBoxs[i - 1];
                        elementCheckBox4SB = listCheckBoxs[i + 1];
                    }
                    if (listCheckBoxs[i].Text == "Гидронамывные карьеры 2006 г.")
                    {
                        Thread.Sleep(500);
                        elementCheckBox5 = listCheckBoxs[i - 1];
                        elementCheckBox5SB = listCheckBoxs[i + 1];
                    }
                    if (listCheckBoxs[i].Text == "Гидронамывные карьеры 2008 г.")
                    {
                        Thread.Sleep(500);
                        elementCheckBox6 = listCheckBoxs[i - 1];
                        elementCheckBox6SB = listCheckBoxs[i + 1];
                    }
                }
                return this;
            }

            /// <summary>
            /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
            /// </summary>
            /// <param name="driver">Передает аргумент для закрытого конструктора</param>
            /// <returns></returns>
            public static TematicMapClass get(IWebDriver driver)
            {
                return new TematicMapClass(driver);
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'Площадки разведочной скважины 2006 г.'.
            /// </summary>
            /// <returns></returns>
            public TematicMapClass CheckBox1Click()
            {
                Sleep();
                elementCheckBox1.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Площадки разведочной скважины 2006 г.'.
            /// </summary>
            /// <returns></returns>
            public TematicMapClass CheckBox1SBClick()
            {
                Sleep();
                elementCheckBox1SB.Click();
                return this;
            }
            /// <summary>
            /// Выполняет клик по чекбоксу 'Площадки разведочной скважины 2008 г.'.
            /// </summary>
            /// <returns></returns>
            public TematicMapClass CheckBox2Click()
            {
                Sleep();
                elementCheckBox2.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Площадки разведочной скважины 2008 г.'
            /// </summary>
            /// <returns></returns>
            public TematicMapClass CheckBox2SBClick()
            {
                Sleep();
                elementCheckBox2SB.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'Нефтяные разливы 2006 г.'.
            /// </summary>
            /// <returns></returns>
            public TematicMapClass CheckBox3Click()
            {
                Sleep();
                elementCheckBox3.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Нефтяные разливы 2006 г.'.
            /// </summary>
            /// <returns></returns>
            public TematicMapClass CheckBox3SBClick()
            {
                Sleep();
                elementCheckBox3SB.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'Нефтяные разливы 2008 г.'.
            /// </summary>
            /// <returns></returns>
            public TematicMapClass CheckBox4Click()
            {
                Sleep();
                elementCheckBox4.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Нефтяные разливы 2008 г.'.
            /// </summary>
            /// <returns></returns>
            public TematicMapClass CheckBox4SBClick()
            {
                Sleep();
                elementCheckBox4SB.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'Гидронамывные карьеры 2006 г.'.
            /// </summary>
            /// <returns></returns>
            public TematicMapClass CheckBox5Click()
            {
                Sleep();
                elementCheckBox5.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Гидронамывные карьеры 2006 г.'.
            /// </summary>
            /// <returns></returns>
            public TematicMapClass CheckBox5SBClick()
            {
                Sleep();
                elementCheckBox5SB.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'Гидро намывные карьеры 2008 г.'.
            /// </summary>
            /// <returns></returns>
            public TematicMapClass CheckBox6Click()
            {
                Sleep();
                elementCheckBox6.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Гидронамывные карьеры 2008 г.'
            /// </summary>
            /// <returns></returns>
            public TematicMapClass CheckBox6SBClick()
            {
                Sleep();
                elementCheckBox6SB.Click();
                return this;
            }
        }


        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Космические снимки'.
        /// </summary>
        /// <returns></returns>
        public Layers CosmoPhotoCheckBoxClick()
        {
            Sleep();
            elementCosmoPhotoCB.Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел  'Космические снимки.'.
        /// </summary>
        /// <returns></returns>
        public Layers CosmoPhotoOpenCloseList()
        {
            Sleep();
            elementCosmoPhotoDDM.Click();
            return this;
        }


        /// <summary>
        /// Дает доступ ко всем чекбоксам выпадающего меню 'Космические снимки.'.
        /// </summary>
        public class CosmoPhotoClass
        {
            private IWebDriver driver;
            private IWebElement elementGazprom;
            private IWebElement elementGazpromSB;
            private IList<IWebElement> listCheckBoxs;
            private const string locationCheckBoxs = "div.svzLayerManagerItem.svzLayerManagerItem1 div";

            private CosmoPhotoClass(IWebDriver driver)
            {
                this.driver = driver;
                SetValueList();
                SetValueElements();
            }

            private void Sleep()
            {
                Thread.Sleep(2000);
            }

            private CosmoPhotoClass SetValueList()
            {
                listCheckBoxs = driver.FindElements(By.CssSelector(locationCheckBoxs));
                return this;
            }

            private CosmoPhotoClass SetValueElements()
            {
                for (int i = 0; i < listCheckBoxs.Count; i++)
                {
                    if (listCheckBoxs[i].Text == "Gazprom_Base_map_NNG")
                    {
                        Thread.Sleep(500);
                        elementGazprom = listCheckBoxs[i - 1];
                        elementGazpromSB = listCheckBoxs[i + 1];
                    }
                }
                return this;
            }

            /// <summary>
            /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
            /// </summary>
            /// <param name="driver">Передает аргумент для закрытого конструктора</param>
            /// <returns></returns>
            public static CosmoPhotoClass get(IWebDriver driver)
            {
                return new CosmoPhotoClass(driver);
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'Gazprom_Base_map_NNG'.
            /// </summary>
            /// <returns></returns>
            public CosmoPhotoClass GazpromClick()
            {
                Sleep();
                elementGazprom.Click();
                return this;
            }
/*
            /// <summary>
            /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Gazprom_Base_map_NNG'.
            /// </summary>
            /// <returns></returns>
            public CosmoPhotoClass GazpromSBClick()
            {
                Sleep();
                elementGazpromSB.Click();
                return this;
            }
            */
        }

        /// <summary>
        /// Выполняет клик по векторным кнопкам любого слоя.
        /// </summary>
        public class VectorButtonsClass
        {
            private IWebDriver driver;
            private IWebElement elementStatisticsLayer;
            private IWebElement elementZoomToLayerExtext;
            private IList<IWebElement> listButtonsVectorLayer;
            private const string locationButtonsVectorLayer = "div.userLayerMenuContainer.userLayerMenuContainerActive > div.svzSimpleButton";

            private VectorButtonsClass(IWebDriver driver)
            {
                this.driver = driver;
                SetValueList();
                SetValueElements();
            }

            private void Sleep()
            {
                Thread.Sleep(2000);
            }

            private VectorButtonsClass SetValueList()
            {
                listButtonsVectorLayer = driver.FindElements(By.CssSelector(locationButtonsVectorLayer));
                return this;
            }

            private VectorButtonsClass SetValueElements()
            {
                Sleep();
                elementStatisticsLayer = listButtonsVectorLayer[0];
                elementZoomToLayerExtext = listButtonsVectorLayer[1];
                return this;
            }

            /// <summary>
            /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
            /// </summary>
            /// <param name="driver">Передает аргумент для закрытого конструктора</param>
            /// <returns></returns>
            public static VectorButtonsClass get(IWebDriver driver)
            {
                return new VectorButtonsClass(driver);
            }

            /// <summary>
            /// Выполянет клик по векторной кнопке 'Статистика слоя'.
            /// </summary>
            /// <returns></returns>
            public VectorButtonsClass StatisticsLayerClick()
            {
                Sleep();
                elementStatisticsLayer.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по векторной  кнопке 'Приближение к экстенту слоя'.
            /// </summary>
            /// <returns></returns>
            public VectorButtonsClass ZoomToLayerExtent()
            {
                Sleep();
                elementZoomToLayerExtext.Click();
                return this;
            }
        }

    }
}
