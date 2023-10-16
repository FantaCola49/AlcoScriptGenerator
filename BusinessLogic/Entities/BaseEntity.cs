namespace AlcoScriptGenerator.BusinessLogic.Entities
{
    /// <summary>
    /// Базовый сущностный тип
    /// </summary>
    public class BaseEntity
    {
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
