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
        public ScriptType ScriptField { get; init; }
    }
}
