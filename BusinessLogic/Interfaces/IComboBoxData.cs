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
        public List<TypeScriptListMember> GetScriptTypes();

        /// <summary>
        /// Вернёт список скриптов
        /// </summary>
        /// <returns></returns>
        public List<Script> GetScripts();
    }
}
