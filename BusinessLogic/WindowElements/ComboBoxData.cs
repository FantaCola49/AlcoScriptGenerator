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
        /// Вернёт список скриптов агроспота
        /// </summary>
        /// <returns></returns>
        public List<Script> GetAgrospotScripts() => _agrospotScripts;

        /// <summary>
        /// Список скриптов для АСКП
        /// </summary>
        /// <returns></returns>
        public List<Script> GetAskpScripts() => _askpScripts;

        /// <summary>
        /// Список скриптов для заводов
        /// </summary>
        /// <returns></returns>
        public List<Script> GetZavodScripts() => _zavodScripts;


        #region Private Members

        private List<TypeScriptListMember> _scriptTypes = new List<TypeScriptListMember>()
        {
            new TypeScriptListMember
            {
                Title = "Агроспот",
                Description = "Комплексы Агроспот. GPS, ReplyId",
                Id = 1,
                TypeOfScript = ScriptType.Agrospot,
            },
            new TypeScriptListMember
            {
                Id = 2,
                Title = "Заводы",
                Description = "Комплексы АСИИУ Алкоспот, установленные на заводах. Дискреты, продукты и т.д.",
                TypeOfScript = ScriptType.Zavod,
            },
            new TypeScriptListMember
            {
                Id = 2,
                Title = "Аскп",
                Description = "Напрямую связана с сайтом АСКП. История контроллеров, продукты контроллеров, добавление сессии Агроспотов",
                TypeOfScript = ScriptType.ASKP,
            },
        };

        private List<Script> _agrospotScripts = new List<Script>()
        {
            new Script
            {
                Id = 1,
                Title = "GPS Navigation",
                Description = "Скрипт для выгрузки GPS Navigation (Пятиминутки агроспота)",
                СontainsArguments = true,
                TypeOfScript = ScriptType.Agrospot,
                Body = string.Empty,
                // По задумке Тело скрипта будет обрабатываться фабрикой. Поэтому, добавляем только Название и описание
            },
            new Script
            {
                Id = 2,
                Title = "Сессии агроспот",
                Description = "Выгрузка сессий агроспота",
                СontainsArguments = false,
                TypeOfScript = ScriptType.Agrospot,
                Body = string.Empty,
            },
            new Script
            {
                Id = 3,
                Title = "Суточные+ReplyId",
                Description = "Выгрузка суточных, и их ReplyId (Тикеты)",
                СontainsArguments = false,
                TypeOfScript = ScriptType.Agrospot,
                Body = string.Empty,
            },
            new Script
            {
                Id = 4,
                Title = "Удаление GPS navigation",
                Description = "Удаление GPS navigation. Необходим в случае ошибки типа XML(0,0) в логах службы отчётности",
                СontainsArguments = true, //название пятиминутки
                TypeOfScript = ScriptType.Agrospot,
                Body = string.Empty,
            },
        };

        //TODO: список всех скриптов?
        //TODO: Скрипты АСКП
        private List<Script> _askpScripts = new List<Script>()
        {
            new Script
            {
                Id = 1,
                Title = "Тест",
                Description = " ",
                СontainsArguments = true,
                TypeOfScript = ScriptType.ASKP,
                // По задумке Тело скрипта будет обрабатываться фабрикой. Поэтому, добавляем только Название и описание
            },
        };
        // TODO: Скрипты Заводов
        private List<Script> _zavodScripts = new List<Script>()
        {
            new Script
            {
                Id = 1,
                Title = "Тест",
                Description = " ",
                СontainsArguments = true,
                TypeOfScript = ScriptType.Zavod,
                // По задумке Тело скрипта будет обрабатываться фабрикой. Поэтому, добавляем только Название и описание
            },
        };
        #endregion
    }
}
