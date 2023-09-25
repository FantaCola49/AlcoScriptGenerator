using System.Text.RegularExpressions;

namespace AlcoScriptGenerator.BusinessLogic.Entities
{
    /// <summary>
    /// Валидация вводимого
    /// </summary>
    public class InputValidation
    {
        /// <summary>
        /// Разрешение на ввод цифры
        /// </summary>
        public readonly Regex digitalFilter = new Regex(@"\d");

        /// <summary>
        /// Заполнены не все аргументы
        /// </summary>
        public readonly string NotEnoughArgs = "Скрипт не сгенерирован!\n" +
                                               "Проверьте заполненность аргументов!";
    }
}
