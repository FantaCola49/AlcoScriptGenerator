namespace AlcoScriptGenerator.BusinessLogic.Entities
{
    /// <summary>
    /// Сущность скрипта
    /// </summary>
    public class Script : TypeScriptListMember
    {
        /// <summary>
        /// Тело скрипта
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Содержит аргументы для внесения?
        /// </summary>
        public bool СontainsArguments { get; set; }

    }

    /// <summary>
    /// Все типы скриптов
    /// </summary>
    public enum ScriptType
    {
        Other,
        Agrospot,
        ASKP,
        Zavod,
    }
}
