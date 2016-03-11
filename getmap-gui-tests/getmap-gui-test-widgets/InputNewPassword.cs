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
        private const string locationInputs = "input.dijitReset.dijitInputInner";
        private const string locationButtons = "div.container table button";
        private const string newPassword = "new password";
        private const string verify = "verify";
        private IList<IWebElement> listInputs;
        private IList<IWebElement> listButtons;
        private Dictionary<string, IWebElement> dicAreas;
        private enum Buttons
        {
            Input = 0,
            Cancel = 1
        }

        private InputNewPassword(IWebDriver driver)
        {
            this.driver = driver;
            SetValueList();
            SetValueElements();
        }

        private InputNewPassword SetValueList()
        {
            dicAreas = new Dictionary<string, IWebElement>();
            listInputs = driver.FindElements(By.CssSelector(locationInputs));
            listButtons = driver.FindElements(By.CssSelector(locationButtons));
            return this;
        }

        private InputNewPassword SetValueElements()
        {
            for (int i = 0; i < listInputs.Count; i++)
            {
                if (listInputs[i].GetAttribute("type") == "password")
                {
                    dicAreas.Add(newPassword,listInputs[i]);
                    break;
                }
            }
            for (int i = 0; i < listInputs.Count; i++)
            {
                if (listInputs[i].GetAttribute("type") == "password")
                {
                    dicAreas.Add(verify,listInputs[i]);
                }
            }
            return this;
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
            dicAreas[newPassword].SendKeys(sendkeys);
            return this;
        }

        /// <summary>
        /// Записать значение в текстовое поле 'Подтверждение пароля'.
        /// </summary>
        /// <param name="sendkeys">Введите пароль.</param>
        /// <returns></returns>
        public InputNewPassword VerifyPasswordSendKeys(string sendkeys)
        {
            dicAreas[verify].SendKeys(sendkeys);
            return this;
        }

        /// <summary>
        /// Выполнить клик по кнопке 'Ввод'.
        /// </summary>
        /// <returns></returns>
        public InputNewPassword ButtonInputClick()
        {
            listButtons[(int)Buttons.Input].Click();
            return this;
        }

        /// <summary>
        /// Выполнить клик по кнопке 'Отмена'.
        /// </summary>
        /// <returns></returns>
        public InputNewPassword ButtonCancelClick()
        {
            listButtons[(int)Buttons.Cancel].Click();
            return this;
        }

    }
}
