﻿namespace AlcoScriptGenerator.BusinessLogic.Entities
{
    public sealed class Script : BaseEntity
    {
        /// <summary>
        /// Тип скрипта
        /// </summary>
        public ScriptType TypeOfScript { get; init; }

        /// <summary>
        /// Содержит аргументы для внесения?
        /// </summary>
        public bool СontainsArguments { get; init; }

        /// <summary>
        /// Идентификатор скриптов
        /// </summary>
        public ScriptId ScriptId { get; init; }
    }

    /// <summary>
    /// Все типы скриптов
    /// </summary>
    public enum ScriptType
    {
        Other,
        /// <summary>
        /// Скрипт Агроспота
        /// </summary>
        Agrospot,
        /// <summary>
        /// Скрипт АСКП
        /// </summary>
        ASKP,
        /// <summary>
        /// Скрипт Завода
        /// </summary>
        Zavod
    }

    /// <summary>
    /// Идентификаторы скриптов
    /// </summary>
    public enum ScriptId
    {

        #region Agrospot
        /// <summary>
        /// (Агроспот) Удаление GPS навигации
        /// </summary>
        DeleteGpsNavigation = 13,

        /// <summary>
        /// (Агроспот) Выгрузка GPS навигации
        /// </summary>
        GpsNavigationSearch = 14,
        #endregion

        #region Zavod

        /// <summary>
        /// (Zavod) Сессии завода
        /// </summary>
        ZavodSessionsMinMaxDate = 21,

        /// <summary>
        /// (Zavod) Выгрузка суточных
        /// </summary>
        ZavodDailies = 23,

        /// <summary>
        /// (Zavod) Дискреты Remastered
        /// </summary>
        ZavodDiscreteFullRemastered = 24,

        /// <summary>
        /// (Zavod) Расходомеры по линиям
        /// </summary>
        ZavodFlowmeteresByLines = 25,

        #endregion

        #region ASKP

        /// <summary>
        /// (ASKP) Поиск контроллера по его номеру
        /// </summary>
        SearchControllerByItsNumber = 31,

        /// <summary>
        /// (ASKP) Выгрузка сессии с указанием
        /// </summary>
        VehicleSessions = 32,

        /// <summary>
        /// (ASKP) История контроллеров
        /// </summary>
        VehicleEvents = 33,

        /// <summary>
        /// (ASKP) GpsPoint_Data_Controller + Organization
        /// </summary>
        VehicleGpsPointDataControllerAndOrganization = 34,

        /// <summary>
        /// (ASKP) Добавить сессию агроспота в АСКП
        /// </summary>
        AddAgrospotSession = 35,

        /// <summary>
        /// (ASKP) Суточные файлы по организации
        /// </summary>
        DailyFilesByOrganization = 38,

        #endregion
    }
}
