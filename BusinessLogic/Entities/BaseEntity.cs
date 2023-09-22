namespace AlcoScriptGenerator.BusinessLogic.Entities
{
    /// <summary>
    /// Базовый сущностный тип
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
    }
}
