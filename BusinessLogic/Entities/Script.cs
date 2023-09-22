using System.Windows.Controls;

namespace AlcoScriptGenerator.BusinessLogic.Entities
{
    public sealed class Script : BaseEntity
    {
        /// <summary>
        /// Тип скрипта
        /// </summary>
        public ScriptType TypeOfScript { get; init; }

        /// <summary>
        /// Содержит аргументы для внесения?
        /// </summary>
        public bool СontainsArguments { get; init; }

        /// <summary>
        /// Логика страницы
        /// </summary>
        public Page PageBody { get; init; }
    }

    /// <summary>
    /// Все типы скриптов
    /// </summary>
    public enum ScriptType
    {
        Other,
        /// <summary>
        /// Скрипт Агроспота
        /// </summary>
        Agrospot,
        /// <summary>
        /// Скрипт АСКП
        /// </summary>
        ASKP,
        /// <summary>
        /// Скрипт Завода
        /// </summary>
        Zavod
    }
}
