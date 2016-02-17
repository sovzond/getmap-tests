using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
namespace GetMapTest.GUI
{
    
    /// <summary>
    /// Выполянет доступ к окну 'Изменение пароля'.
    /// </summary>
   public class InputNewPassword
    {
        private IWebDriver driver;
        private const string locationInputs = "div.container table input";
        private const string locationButtons = "div.container table button";
        private IList<IWebElement> listInputs;
        private IList<IWebElement> listButtons;

        private InputNewPassword(IWebDriver driver)
        {
            this.driver = driver;
            listInputs = driver.FindElements(By.CssSelector(locationInputs));
            listButtons = driver.FindElements(By.CssSelector(locationButtons));
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
        public static InputNewPassword get(IWebDriver driver)
        {
            return new InputNewPassword(driver);
        }

        /// <summary>
        /// Записать значение в текстовое поле 'Новый пароль'.
        /// </summary>
        /// <param name="sendkeys">Введите пароль.</param>
        /// <returns></returns>
        public InputNewPassword NewPasswordSendKeys(string sendkeys)
        {
            Sleep();
            listInputs[22].SendKeys(sendkeys);
            return this;
        }

        /// <summary>
        /// Записать значение в текстовое поле 'Подтверждение пароля'.
        /// </summary>
        /// <param name="sendkeys">Введите пароль.</param>
        /// <returns></returns>
        public InputNewPassword VerifyPasswordSendKeys(string sendkeys)
        {
            Sleep();
            listInputs[24].SendKeys(sendkeys);
            return this;
        }

        /// <summary>
        /// Выполнить клик по кнопке 'Ввод'.
        /// </summary>
        /// <returns></returns>
        public InputNewPassword ButtonInputClick()
        {
            Sleep();
            listButtons[0].Click();
            return this;
        }

        /// <summary>
        /// Выполнить клик по кнопке 'Отмена'.
        /// </summary>
        /// <returns></returns>
        public InputNewPassword ButtonCancelClick()
        {
            Sleep();
            listButtons[1].Click();
            return this;
        }

    }
}
