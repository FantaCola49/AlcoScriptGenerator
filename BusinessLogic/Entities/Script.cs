namespace AlcoScriptGenerator.BusinessLogic.Entities
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
        /// (Агроспот) Сессии агроспот
        /// </summary>
        AgrospotSessions = 11,

        /// <summary>
        /// (Агроспот) Суточные+ReplyId
        /// </summary>
        DailiesAndReplyId = 12,


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
        /// Настройки по линиям с именами продуктов
        /// </summary>
        ZavodLineProductAdjustmentWithProductsNames = 22,

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
        AskpSearchControllerByItsNumber = 31,

        /// <summary>
        /// (ASKP) Выгрузка сессии с указанием
        /// </summary>
        AskpVehicleSessions = 32,

        /// <summary>
        /// (ASKP) История контроллеров
        /// </summary>
        AskpVehicleEvents = 33,

        /// <summary>
        /// (ASKP) GpsPoint_Data_Controller + Organization
        /// </summary>
        AskpVehicleGpsPointDataControllerAndOrganization = 34,

        /// <summary>
        /// (ASKP) Добавить сессию агроспота в АСКП
        /// </summary>
        AskpAddAgrospotSession = 35,

        /// <summary>
        /// Скрипт выгрузки всех сессий продуктов перевозчиков и их крепости
        /// </summary>
        AskpAllSessionsAndProductsWithProof = 36,

        /// <summary>
        /// Скрипт выгрузки продукта по коду перевозчиков и их крепости
        /// </summary>
        AskpAllProductsWithProofByOrganization = 37,

        /// <summary>
        /// (ASKP) Суточные файлы по организации
        /// </summary>
        AskpDailyFilesByOrganization = 38,

        #endregion
    }
}
