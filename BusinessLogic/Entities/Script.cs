namespace AlcoScriptGenerator.BusinessLogic.Entities
{
    /// <summary>
    /// Сущность скрипта
    /// </summary>
    public class Script : BaseEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Тело
        /// </summary>
        public string Body { get; set; }

    }
}
