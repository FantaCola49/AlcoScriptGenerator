using AlcoScriptGenerator.BusinessLogic.Entities;
using AlcoScriptGenerator.BusinessLogic.Interfaces;
using System.Collections.Generic;

namespace AlcoScriptGenerator.BusinessLogic.WindowElements
{
    public class ComboBoxData : IComboBoxData
    {
        /// <summary>
        /// Получить список типов скриптов
        /// </summary>
        /// <returns></returns>
        public List<TypeScriptListMember> GetScriptTypes() => _scriptTypes;

        /// <summary>
        /// Вернёт список скриптов
        /// </summary>
        /// <returns></returns>
        public List<Script> GetScripts() => _scripts;


        #region Private Members

        private List<TypeScriptListMember> _scriptTypes = new List<TypeScriptListMember>()
        {
            new TypeScriptListMember
            {
                Title = "Агроспот",
                Description = "Комплексы Агроспот. GPS, ReplyId",
                Id = 1,
            },
            new TypeScriptListMember
            {
                Id = 2,
                Title = "Заводы",
                Description = "Комплексы АСИИУ Алкоспот, установленные на заводах. Дискреты, продукты и т.д.",
            },
            new TypeScriptListMember
            {
                Id = 2,
                Title = "Аскп",
                Description = "Напрямую связана с сайтом АСКП. История контроллеров, продукты контроллеров, добавление сессии Агроспотов",
            },
        };

        private List<Script> _scripts = new List<Script>()
        {
            new Script 
            { 
                Id = 1,
                Title = "",
                Description = ""
                // По задумке Тело скрипта будет обрабатываться фабрикой. Поэтому, добавляем только Название и описание
            },
        };
        
        #endregion
    }
}
