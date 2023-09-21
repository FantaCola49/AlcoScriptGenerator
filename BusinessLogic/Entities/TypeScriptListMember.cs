namespace AlcoScriptGenerator.BusinessLogic.Entities
{
    public class TypeScriptListMember : BaseEntity
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
        /// Тип скрипта
        /// </summary>
        public ScriptType TypeOfScript { get; set; }
    }
}
