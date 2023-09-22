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
        public int Id { get; init; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; init; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; init; }
    }
}
