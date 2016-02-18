﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;

namespace GetMapTest.GUI
{
    /// <summary>
    /// Открывает выпадающее меню раздела 'Слои'.
    /// </summary>
    public class Layers
    {
        private IWebDriver driver;
        private IWebElement elementTestLayer;
        private IWebElement elementNewGroup;
        private IWebElement elementZoyaTest;
        private IWebElement elementMoscowArea;
        private IWebElement elementGasStruct;
        private IWebElement elementEnergyStruct;
        private IWebElement elementNeftyStruct;
        private IWebElement elementTematicMap;
        private IWebElement elementCosmoPhoto;
        private IList<IWebElement> listCheckBoxs;
        private const string locationCheckBoxs = "div.svzLayerManagerText";

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
            listCheckBoxs = driver.FindElements(By.CssSelector(locationCheckBoxs));
            return this;
        }

        private Layers SetValueElements()
        {
            foreach (var el in listCheckBoxs)
            {
                if (el.Text == "Тест")
                    elementTestLayer = el;
                if (el.Text == "Новая группа")
                    elementNewGroup = el;
                if (el.Text == "Зоя. Тест")
                    elementZoyaTest = el;
                if (el.Text == "Московская область")
                    elementMoscowArea = el;
                if (el.Text == "Газовая инфраструктура")
                    elementGasStruct = el;
                if (el.Text == "Энергетическая инфраструктура")
                    elementEnergyStruct = el;
                if (el.Text == "Нефтяная инфраструктура")
                    elementNeftyStruct = el;
                if (el.Text == "Тематические карты")
                    elementTematicMap = el;
                if (el.Text == "Космические снимки")
                    elementCosmoPhoto = el;
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
            if (elementTestLayer.Selected)
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
            if (elementNewGroup.Selected)
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
            if (elementZoyaTest.Selected)
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
            if (elementMoscowArea.Selected)
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
            if (elementGasStruct.Selected)
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
            if (elementEnergyStruct.Selected)
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
            if (elementNeftyStruct.Selected)
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
            if (elementTematicMap.Selected)
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
            if (elementCosmoPhoto.Selected)
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
        /// Открывает раздел 'Тест'.
        /// </summary>
        /// <returns></returns>
        public Layers TestClick()
        {
            Sleep();
            elementTestLayer.Click();
            return this;
        }

        /*
        /// <summary>
        /// Дает доступ ко всем чекбоксам выпадающего меню 'Тест.'
        /// </summary>
        public class TestLayerClass
        {
            private IWebDriver driver;
            private IWebElement elementAmerica;
            private IWebElement elementBase_raster;
            private IWebElement elementAmbar;
            private IWebElement elementButtonForOpenList;
            private IList<IWebElement> listCheckBoxs;
            private IList<IWebElement> listButtonsLayers;
            private const string locationButtonsLayers = "#stdportal_LayerManagerBase_1 div.svzLayerManagerText";
            private const string locationCheckBoxs = "#stdportal_LayerManagerBase_1 div.dijit.dijitReset.dijitInline.dijitCheckBox input";

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
                listButtonsLayers = driver.FindElements(By.CssSelector(locationButtonsLayers));
                return this;
            }

            private TestLayerClass SetValueElements()
            {
                elementAmerica = listCheckBoxs[1];
                elementBase_raster = listCheckBoxs[2];
                elementAmbar = listCheckBoxs[3];
                elementButtonForOpenList = listButtonsLayers[0];
                return this;
            }

            /// <summary>
            /// Открывает или же закрывает выпадающее меню 'Тест' в зависимости от того, в каком состоянии оно было до вызова данного метода.
            /// </summary>
            /// <returns></returns>
            public TestLayerClass OpenCloseList()
            {
                elementButtonForOpenList.Click();
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
            /// Выполняет клик по чекбоксу 'rtk:base_raster'.
            /// /// </summary>
            /// <returns></returns>
            public TestLayerClass Base_RasterClick()
            {
                Sleep();
                elementBase_raster.Click();
                return this;
            }

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
        }
        */


        /// <summary>
        /// Открывает раздел 'Новая группа'.
        /// </summary>
        /// <returns></returns>
        public Layers NewGroupClick()
        {
            Sleep();
            elementNewGroup.Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Зоя. Тест'.
        /// </summary>
        /// <returns></returns>
        public Layers ZoyaTestClick()
        {
            Sleep();
            elementZoyaTest.Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Московская область'.
        /// </summary>
        /// <returns></returns>
        public Layers MoscowAreaClick()
        {
            Sleep();
            elementMoscowArea.Click();
            return this;
        }

        /*
        /// <summary>
        /// Дает доступ ко всем чекбоксам выпадающего меню 'Новая группа'.
        /// </summary>
        public class NewGroupClass
        {
            private IWebDriver driver;
            private IList<IWebElement> listCheckBoxs;
            private IWebElement elementTsp_25;
            private IWebElement elementButtonForOpenList;
            private IList<IWebElement> listButtonsLayers;
            private const string locationButtonsLayers = "#stdportal_LayerManagerBase_1 div.svzLayerManagerText";
            private const string locationCheckBoxs = "#stdportal_LayerManagerBase_1 div.dijit.dijitReset.dijitInline.dijitCheckBox input";

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
                listButtonsLayers = driver.FindElements(By.CssSelector(locationButtonsLayers));
                return this;
            }

            private NewGroupClass SetValueElements()
            {
                elementTsp_25 = listCheckBoxs[5];
                elementButtonForOpenList = listButtonsLayers[4];
                return this;
            }

            private void Sleep()
            {
                Thread.Sleep(2000);
            }

            /// <summary>
            /// Открывает или же закрывает выпадающее меню 'Новая группа' в зависимости от того, в каком состоянии оно было до вызова данного метода.
            /// </summary>
            /// <returns></returns>
            public NewGroupClass OpenCloseList()
            {
                elementButtonForOpenList.Click();
                return this;
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

        }
        */

        /// <summary>
        /// Открывает раздел 'Газовая инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers GasStructClick()
        {
            Sleep();
            elementGasStruct.Click();
            return this;
        }

        /*
        /// <summary>
        /// Дает доступ ко всем чекбоксам выпадающего меню 'Газовая инфраструктура'.
        /// </summary>
        public class GasStructClass
        {
            private IWebDriver driver;
            private IWebElement elemenGPZPoint;
            private IWebElement elementGazoprovod;
            private IWebElement elementGPZPoligon;
            private IWebElement elementButtonForOpenList;
            private IList<IWebElement> listCheckBoxs;
            private IList<IWebElement> listButtonsLayers;
            private const string locationButtonsLayers = "#stdportal_LayerManagerBase_1 div.svzLayerManagerText";
            private const string locationCheckBoxs = "#stdportal_LayerManagerBase_1 div.dijit.dijitReset.dijitInline.dijitCheckBox input";

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
                listButtonsLayers = driver.FindElements(By.CssSelector(locationButtonsLayers));
                return this;
            }

            private GasStructClass SetValueElements()
            {
                elemenGPZPoint = listCheckBoxs[7];
                elementGazoprovod = listCheckBoxs[8];
                elementGPZPoligon = listCheckBoxs[9];
                elementButtonForOpenList = listButtonsLayers[6];
                return this;
            }

            /// <summary>
            /// Открывает или же закрывает выпадающее меню 'Газовая инфраструктура' в зависимости от того, в каком состоянии оно было до вызова данного метода.
            /// </summary>
            /// <returns></returns>
            public GasStructClass OpenCloseList()
            {
                elementButtonForOpenList.Click();
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
            /// Выполняет клик по чекбоксу 'ГПЗ (полигон)'.
            /// </summary>
            /// <returns></returns>
            public GasStructClass GPZPoligonClick()
            {
                Sleep();
                elementGPZPoligon.Click();
                return this;
            }

        }
        */

        /// <summary>
        /// Открывает раздел  'Энергетическая инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers EnergyStruckClick()
        {
            Sleep();
            elementEnergyStruct.Click();
            return this;
        }

        /*
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
            private IWebElement elementButtonForOpenList;
            private IList<IWebElement> listCheckBoxs;
            private IList<IWebElement> listButtonsLayers;
            private const string locationButtonsLayers = "#stdportal_LayerManagerBase_1 div.svzLayerManagerText";
            private const string locationCheckBoxs = "#stdportal_LayerManagerBase_1 div.dijit.dijitReset.dijitInline.dijitCheckBox input";

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
                listButtonsLayers = driver.FindElements(By.CssSelector(locationButtonsLayers));
                return this;
            }

            private EnergyStructClass SetValueElements()
            {
                elementElectroStationPoint = listCheckBoxs[11];
                elementPodstationPoint = listCheckBoxs[12];
                elementLEP = listCheckBoxs[13];
                elementElectroStationPoligon = listCheckBoxs[14];
                elementPodstationPoligon = listCheckBoxs[15];
                elementButtonForOpenList = listButtonsLayers[10];
                return this;
            }

            /// <summary>
            /// Открывает или же закрывает выпадающее меню 'Энергетическая инфраструктура' в зависимости от того, в каком состоянии оно было до вызова данного метода.
            /// </summary>
            /// <returns></returns>
            public EnergyStructClass OpenCloseList()
            {
                elementButtonForOpenList.Click();
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
            /// Выполняет клик по чекбоксу 'Электростанция (полигон)'.
            /// </summary>
            /// <returns></returns>
            public EnergyStructClass ElectroStationPoligon()
            {
                Sleep();
                elementElectroStationPoligon.Click();
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

        }
        */

        /// <summary>
        /// Открывает раздел 'Нефтяная инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers NeftyStructClick()
        {
            Sleep();
            elementNeftyStruct.Click();
            return this;
        }

        /*
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
            private IWebElement elementButtonForOpenList;
            private IList<IWebElement> listCheckBoxs;
            private IList<IWebElement> listButtonsLayers;
            private const string locationButtonsLayers = "#stdportal_LayerManagerBase_1 div.svzLayerManagerText";
            private const string locationCheckBoxs = "#stdportal_LayerManagerBase_1 div.dijit.dijitReset.dijitInline.dijitCheckBox input";

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
                listButtonsLayers = driver.FindElements(By.CssSelector(locationButtonsLayers));
                return this;
            }

            private NeftyStructClass SetValueElements()
            {
                elementFakel = listCheckBoxs[17];
                elementAmbar = listCheckBoxs[18];
                elementPlaces = listCheckBoxs[19];
                elementDNS = listCheckBoxs[20];
                elementButtonForOpenList = listButtonsLayers[16];
                return this;
            }

            /// <summary>
            /// Открывает или же закрывает выпадающее меню 'Нефтяная инфраструктура' в зависимости от того, в каком состоянии оно было до вызова данного метода.
            /// </summary>
            /// <returns></returns>
            public NeftyStructClass OpenCloseList()
            {
                elementButtonForOpenList.Click();
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
            /// Выполняет клик по кнопкам 'Настройка слоя' всех слоев данного раздела.
            /// </summary>
            public class SettingsButtonsClass
            {
                private IWebDriver driver;
                private IWebElement elementFakelSettings;
                private IWebElement elementAmbatSettings;
                private IWebElement elementPlacesSettings;
                private IWebElement elementDNSSettings;
                private IList<IWebElement> listButtonsLayer;
                private const string locationButtonsLayer = "div.svzLayerManagerItem.svzLayerManagerItem1 > div.svzSimpleButton.layerContextMenu";

                private SettingsButtonsClass(IWebDriver driver)
                {
                    this.driver = driver;
                    SetValueList();
                    SetValueElements();
                }

                private SettingsButtonsClass SetValueList()
                {
                    listButtonsLayer = driver.FindElements(By.CssSelector(locationButtonsLayer));
                    return this;
                }

                private SettingsButtonsClass SetValueElements()
                {
                    elementFakelSettings = listButtonsLayer[10];
                    elementAmbatSettings = listButtonsLayer[11];
                    elementPlacesSettings = listButtonsLayer[12];
                    elementDNSSettings = listButtonsLayer[13];
                    return this;
                }

                private void Sleep()
                {
                    Thread.Sleep(2000);
                }

                /// <summary>
                /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
                /// </summary>
                /// <param name="driver">Передает аргумент для закрытого конструктора</param>
                /// <returns></returns>
                public static SettingsButtonsClass get(IWebDriver driver)
                {
                    return new SettingsButtonsClass(driver);
                }

                /// <summary>
                /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Факел'.
                /// </summary>
                /// <returns></returns>
                public SettingsButtonsClass FakelSettingsButtonClick()
                {
                    Sleep();
                    elementFakelSettings.Click();
                    return this;
                }

                /// <summary>
                /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Амбар'.
                /// </summary>
                /// <returns></returns>
                public SettingsButtonsClass AmbarSettingsButtonClick()
                {
                    Sleep();
                    elementAmbatSettings.Click();
                    return this;
                }

                /// <summary>
                /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Кустовые площадки'.
                /// </summary>
                /// <returns></returns>
                public SettingsButtonsClass PlacesSettingsClick()
                {
                    Sleep();
                    elementPlacesSettings.Click();
                    return this;
                }

                /// <summary>
                /// Выполняет клик по кнопке 'Настройка слоя' слоя 'ДНС'.
                /// </summary>
                /// <returns></returns>
                public SettingsButtonsClass DNSSettingsClick()
                {
                    Sleep();
                    elementDNSSettings.Click();
                    return this;
                }

            }

        }
        */

        /// <summary>
        /// Открывает раздел  'Тематические карты'.
        /// </summary>
        /// <returns></returns>
        public Layers TematicMapClick()
        {
            Sleep();
            elementTematicMap.Click();
            return this;
        }

        /*
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
            private IWebElement elementButtonForOpenList;
            private IList<IWebElement> listCheckBoxs;
            private IList<IWebElement> listButtonsLayers;
            private const string locationButtonsLayers = "#stdportal_LayerManagerBase_1 div.svzLayerManagerText";
            private const string locationCheckBoxs = "#stdportal_LayerManagerBase_1 div.dijit.dijitReset.dijitInline.dijitCheckBox input";

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
                listButtonsLayers = driver.FindElements(By.CssSelector(locationButtonsLayers));
                return this;
            }

            private TematicMapClass SetValueElements()
            {
                elementCheckBox1 = listCheckBoxs[22];
                elementCheckBox2 = listCheckBoxs[23];
                elementCheckBox3 = listCheckBoxs[24];
                elementCheckBox4 = listCheckBoxs[25];
                elementCheckBox5 = listCheckBoxs[26];
                elementCheckBox6 = listCheckBoxs[27];
                elementButtonForOpenList = listButtonsLayers[21];
                return this;
            }

            /// <summary>
            /// Открывает или же закрывает выпадающее меню 'Тематические карты' в зависимости от того, в каком состоянии оно было до вызова данного метода.
            /// </summary>
            /// <returns></returns>
            public TematicMapClass OpenCloseList()
            {
                elementButtonForOpenList.Click();
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
            public TematicMapClass CheckBox1()
            {
                Sleep();
                elementCheckBox1.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'Площадки разведочной скважины 2008 г.'.
            /// </summary>
            /// <returns></returns>
            public TematicMapClass CheckBox2()
            {
                Sleep();
                elementCheckBox2.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'Нефтяные разливы 2006 г.'.
            /// </summary>
            /// <returns></returns>
            public TematicMapClass CheckBox3()
            {
                Sleep();
                elementCheckBox3.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'Нефтяные разливы 2008 г.'.
            /// </summary>
            /// <returns></returns>
            public TematicMapClass CheckBox4()
            {
                Sleep();
                elementCheckBox4.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'Гидронамывные карьеры 2006 г.'.
            /// </summary>
            /// <returns></returns>
            public TematicMapClass CheckBox5()
            {
                Sleep();
                elementCheckBox5.Click();
                return this;
            }

            /// <summary>
            /// Выполняет клик по чекбоксу 'Гидро намывные карьеры 2008 г.'.
            /// </summary>
            /// <returns></returns>
            public TematicMapClass CheckBox6()
            {
                Sleep();
                elementCheckBox6.Click();
                return this;
            }
        }
        */

        /// <summary>
        /// Открывает раздел  'Космические снимки.'.
        /// </summary>
        /// <returns></returns>
        public Layers CosmoPhotoClick()
        {
            Sleep();
            elementCosmoPhoto.Click();
            return this;
        }

        /*
        /// <summary>
        /// Дает доступ ко всем чекбоксам выпадающего меню 'Космические снимки.'.
        /// </summary>
        public class CosmoPhotoClass
        {
            private IWebDriver driver;
            private IWebElement elementGazprom;
            private IWebElement elementButtonForOpenList;
            private IList<IWebElement> listCheckBoxs;
            private IList<IWebElement> listButtonsLayers;
            private const string locationButtonsLayers = "#stdportal_LayerManagerBase_1 div.svzLayerManagerText";
            private const string locationCheckBoxs = "#stdportal_LayerManagerBase_1 div.dijit.dijitReset.dijitInline.dijitCheckBox input";

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
                listButtonsLayers = driver.FindElements(By.CssSelector(locationButtonsLayers));
                return this;
            }

            private CosmoPhotoClass SetValueElements()
            {
                elementGazprom = listCheckBoxs[29];
                elementButtonForOpenList = listButtonsLayers[28];
                return this;
            }

            /// <summary>
            /// Открывает или же закрывает выпадающее меню 'Космические снимки' в зависимости от того, в каком состоянии оно было до вызова данного метода.
            /// </summary>
            /// <returns></returns>
            public CosmoPhotoClass OpenCloseList()
            {
                elementButtonForOpenList.Click();
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
        }
        */


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
