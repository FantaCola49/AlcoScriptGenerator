namespace AlcoScriptGenerator.BusinessLogic.Entities
{
    /// <summary>
    /// Тип скрипта по сфере применения
    /// </summary>
    public sealed class ScriptRelation : BaseEntity
    {
        /// <summary>
        /// Область применения скрипта
        /// </summary>
        public ScriptField ScriptField { get; set; }
    }

    /// <summary>
    /// Области применения скрипта
    /// </summary>
    public enum ScriptField
    {
        /// <summary>
        /// Применяется в Агроспотах
        /// </summary>
        Agrospot_Related,
        /// <summary>
        /// Применяется в АСКП
        /// </summary>
        ASKP_Related,
        /// <summary>
        /// Применяется на Заводах
        /// </summary>
        Zavod_Related,
    }
}
