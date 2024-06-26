﻿using AlcoScriptGenerator.BusinessLogic.Entities;
using AlcoScriptGenerator.BusinessLogic.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AlcoScriptGenerator.BusinessLogic.WindowElements
{
    public class ComboBoxData : IComboBoxData
    {
        /// <summary>
        /// Получить список типов скриптов
        /// </summary>
        /// <returns></returns>
        public List<ScriptRelation> GetScriptTypes() => _scriptTypes;

        /// <summary>
        /// Получить описание и название выбранного типа скрипта
        /// </summary>
        /// <param name="scr"></param>
        /// <returns></returns>
        public BaseEntity GetScriptRelationDescriptionAndTitleByType(Script scr)
        {
            BaseEntity entity = scr.TypeOfScript switch
            {
                ScriptType.Agrospot => new BaseEntity { Title = GetScriptTypes().First().Title, Description = GetScriptTypes().First().Description },
                ScriptType.Zavod    => new BaseEntity { Title = GetScriptTypes()[1].Title,      Description = GetScriptTypes()[1].Description      },
                ScriptType.ASKP     => new BaseEntity { Title = GetScriptTypes().Last().Title,  Description = GetScriptTypes().Last().Description  },
                _ => new BaseEntity { Title = "Выберите скрипт", Description = null},
            };
            return entity;
        }

        /// <summary>
        /// Подготовит выпадающий список скриптов для агроспотов
        /// </summary>
        public List<Script> GetScriptsByType(ScriptType type)
        {
            List<Script> scripts = type switch
            {
                ScriptType.Agrospot => _agrospotScripts,
                ScriptType.Zavod    => _zavodScripts,
                ScriptType.ASKP     => _askpScripts,
            };
            if (scripts.Count.Equals(0))
                return null;
            
            return scripts;
        }

        #region Private Members

        // Типы скриптов (их прикладная область)
        private List<ScriptRelation> _scriptTypes = new List<ScriptRelation>()
        {
            new ScriptRelation
            {
                Title = "Агроспот",
                Description = "Комплексы Агроспот. GPS, ReplyId",
                ScriptField = ScriptType.Agrospot,
            },
            new ScriptRelation
            {
                Title = "Заводы",
                Description = "Комплексы АСИИУ Алкоспот, установленные на заводах. Дискреты, продукты и т.д.",
                ScriptField = ScriptType.Zavod,
            },
            new ScriptRelation
            {
                Title = "Аскп",
                Description = "Напрямую связана с сайтом АСКП. История контроллеров, продукты контроллеров, добавление сессии Агроспотов",
                ScriptField = ScriptType.ASKP,
            },
        };

        // Скрипты Агроспотов
        private List<Script> _agrospotScripts = new List<Script>()
        {
            new Script
            {
                Title = "Сессии агроспот",
                Description = "Выгрузка сессий агроспота",
                СontainsArguments = false,
                TypeOfScript = ScriptType.Agrospot,
                ScriptId = ScriptId.AgrospotSessions,
            },
            new Script
            {
                Title = "Суточные+ReplyId",
                Description = "Выгрузка суточных, и их ReplyId (Тикеты)",
                СontainsArguments = false,
                TypeOfScript = ScriptType.Agrospot,
                ScriptId = ScriptId.DailiesAndReplyId,
            },
            new Script
            {
                Title = "Удаление GPS navigation",
                Description = "Удаление GPS navigation. Необходим в случае ошибки типа XML(0,0) в логах службы отчётности",
                СontainsArguments = true,
                TypeOfScript = ScriptType.Agrospot,
                ScriptId = ScriptId.DeleteGpsNavigation,
            },

            new Script
            {
                Title = "GPS Navigation",
                Description = "Скрипт для выгрузки GPS Navigation (Пятиминутки агроспота)",
                СontainsArguments = true,
                TypeOfScript = ScriptType.Agrospot,
                ScriptId = ScriptId.GpsNavigationSearch,
            },
        };

        // Скрипты АСКП
        private List<Script> _askpScripts = new List<Script>()
        {
            // 5
            new Script
            {
                Title = "AddAgrospotSession",
                Description = "Добавление сесси агроспота в АСКП на основе скопированных данных сессии",
                СontainsArguments = true,
                TypeOfScript = ScriptType.ASKP,
                ScriptId = ScriptId.AskpAddAgrospotSession,
            },
            // 1
            new Script
            {
                Title = "FindController",
                Description = "Поиск контроллера по его номеру",
                СontainsArguments = true,
                TypeOfScript = ScriptType.ASKP,
                ScriptId = ScriptId.AskpSearchControllerByItsNumber,
            },
            // 2
            new Script
            {
                Title = "ВыгрузкаСессий",
                Description = "Выгрузка сессий с указанием номера ТС",
                СontainsArguments = true,
                TypeOfScript = ScriptType.ASKP,
                ScriptId = ScriptId.AskpSearchControllerByItsNumber,
            },
            // 3
            new Script
            {
                Title = "История событий",
                Description = "Выгрузка истории событий контроллера",
                СontainsArguments = true,
                TypeOfScript = ScriptType.ASKP,
                ScriptId = ScriptId.AskpVehicleEvents,
            },
            // 4
            new Script
            {
                Title = "GpsPoint+Controller",
                Description = "Выгрузка данных о пятиминутках контроллера организации",
                СontainsArguments = true,
                TypeOfScript = ScriptType.ASKP,
                ScriptId = ScriptId.AskpVehicleGpsPointDataControllerAndOrganization,
            },
            // 6
            new Script
            {
                Title = "ВсеСессииПродутовПеревозчиков",
                Description = "Выгрузка всех сессий по всем продуктам перевозчиков с добавлением их крепости", 
                СontainsArguments = false,
                TypeOfScript = ScriptType.ASKP,
                ScriptId = ScriptId.AskpAllSessionsAndProductsWithProof,
            },
            //7
            new Script
            {
                Title = "ВсеПродуктыОрганизации",
                Description = "Выгрузка всех продуктов компаний-перевозчиков с указанием крепости",
                СontainsArguments = false,
                TypeOfScript = ScriptType.ASKP,
                ScriptId = ScriptId.AskpAllProductsWithProofByOrganization,
            },
            // 8
            new Script
            {
                Title = "СуточныеФайлыОрганизации",
                Description = "Выгрузка суточных файлов по коду организации",
                TypeOfScript = ScriptType.ASKP,
                СontainsArguments = true,
                ScriptId = ScriptId.AskpDailyFilesByOrganization,
            }
        };

        // Скрипты Заводов
        private List<Script> _zavodScripts = new List<Script>()
        {
            new Script
            {
                Title = "СессийЗаводаMinMaxDate",
                Description = "Сессии завода за указанный период",
                СontainsArguments = true,
                TypeOfScript = ScriptType.Zavod,
                ScriptId = ScriptId.ZavodSessionsMinMaxDate,
            },

            new Script
            {
                Title = "НастройкиПоЛиниям",
                Description = "Настройки по линиям для продуктов",
                СontainsArguments = false,
                TypeOfScript = ScriptType.Zavod,
                ScriptId = ScriptId.ZavodLineProductAdjustmentWithProductsNames,
            },

            new Script
            {
                Title = "Суточные",
                Description = "Выгрузить суточные по указанной дате",
                СontainsArguments = true,
                TypeOfScript = ScriptType.Zavod,
                ScriptId = ScriptId.ZavodDailies,
            },

            new Script
            {
                Title = "Дискреты",
                Description = "Выгрузка дискретов за указанный период для анализа",
                СontainsArguments = true,
                TypeOfScript = ScriptType.Zavod,
                ScriptId = ScriptId.ZavodDiscreteFullRemastered,
            },

            new Script
            {
                Title = "РасходомерыПоЛиниям",
                Description = "Названия расходомеров, прикреплённых к линиям",
                СontainsArguments = false,
                TypeOfScript = ScriptType.Zavod,
                ScriptId = ScriptId.ZavodFlowmeteresByLines
            },

            new Script
            {
                Title = "UzProduct",
                Description = "Добавить дохуя продуктов",
                СontainsArguments = true,
                TypeOfScript = ScriptType.Zavod,
                ScriptId = ScriptId.ZavodUzProducts
            }
        };
        #endregion
    }
}
