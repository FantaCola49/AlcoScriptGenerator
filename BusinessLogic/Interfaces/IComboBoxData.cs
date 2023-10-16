using AlcoScriptGenerator.BusinessLogic.Entities;
using System.Collections.Generic;

namespace AlcoScriptGenerator.BusinessLogic.Interfaces
{
    /// <summary>
    /// Интерфейс данных для комбо-боксов
    /// </summary>
    public interface IComboBoxData
    {

        /// <summary>
        /// Получить список типов скриптов
        /// </summary>
        /// <returns></returns>
        public List<ScriptRelation> GetScriptTypes();

        /// <summary>
        /// Вернёт список скриптов в зависимости от типа
        /// </summary>
        public List<Script> GetScriptsByType(ScriptType type);

        /// <summary>
        /// Получить описание и название выбранного типа скрипта
        /// </summary>
        /// <param name="scr"></param>
        /// <returns></returns>
        public BaseEntity GetScriptRelationDescriptionAndTitleByType(Script scr);
    }
}
