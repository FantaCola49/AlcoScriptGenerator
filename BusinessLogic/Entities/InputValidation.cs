using System.Text.RegularExpressions;

namespace AlcoScriptGenerator.BusinessLogic.Entities
{
    /// <summary>
    /// Валидация вводимого
    /// </summary>
    public static class InputValidation
    {
        /// <summary>
        /// Разрешение на ввод цифры
        /// </summary>
        public static readonly Regex DigitalFilter = new Regex(@"\d");

        /// <summary>
        /// Заполнены не все аргументы
        /// </summary>
        public static readonly string NotEnoughArgs = "Скрипт не сгенерирован!\n" +
                                               "Проверьте заполненность аргументов!";

        /// <summary>
        /// Нераспознаваемые аргументы
        /// </summary>
        public static readonly string UnreconizableArgs = "Невозможно распознать введённые данные!\n" +
                                                "Проверьте вводные данные!";

        /// <summary>
        /// Строка пригодна для работы
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsGood(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            else return true;
        }
        /// <summary>
        /// Для неверные вводные данные скриптов АСКП
        /// </summary>
        /// <returns></returns>
        public static string AskpScriptViolation() =>
            "Не могу сгенерировать скрипт добавления сессии Агроспота в АСКП!\r\n\r\nПожалуйста удостоверьтесь, что Вы:\r\n\r\n1) Ввели скрипт с указанием заголовков\r\n2) Ввели 2 строчки (заголовки и значения)\r\n3) Ввели ТОЛЬКО данные, скопированные из агроспота\r\n4) Значения разделены табуляцией";
    }
}
