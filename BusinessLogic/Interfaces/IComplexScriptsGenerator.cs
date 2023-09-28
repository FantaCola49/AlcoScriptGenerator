using System;

namespace AlcoScriptGenerator.BusinessLogic.Interfaces
{
    /// <summary>
    /// Интерфейс генерации скриптов с аргументами
    /// </summary>
    interface IComplexScriptsGenerator
    {
        /// <summary>
        /// Удаление gps навигации
        /// </summary>
        /// <param name="gpsName"></param>
        /// <returns></returns>
        public string DeleteGpsNavigation(string gpsName);

        /// <summary>
        /// Поиск и выгрузка gps навигации
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="vehicleNumber"></param>
        /// <returns></returns>
        public string GpsNavigationSearch(DateTime? startDate, DateTime? endDate, string vehicleNumber);

        /// <summary>
        /// Сгенерировать скрипт для завода в зависимости от выбранного элемента
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public string GenerateComplexScriptForZavod(DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// Контроллер по его номеру
        /// </summary>
        /// <param name="vehicleNumber"></param>
        /// <returns></returns>
        public string GetControllerByItsNumber(string vehicleNumber);

        /// <summary>
        /// Сессии контроллера по его номеру за определённый период
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="vehicleNumber"></param>
        /// <returns></returns>
        public string GetControllerSessions(DateTime? startDate, DateTime? endDate, string vehicleNumber);

        /// <summary>
        /// История контроллера
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="vehicleNumber"></param>
        /// <returns></returns>
        public string GetControllerEventHistory(DateTime? startDate, DateTime? endDate, string vehicleNumber);

        /// <summary>
        /// Получить Gps координаты с характеристиками уровнемеров
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="vehicleNumber"></param>
        /// <returns></returns>
        public string GetControllerGpsPointData(DateTime? startDate, DateTime? endDate, string vehicleNumber);

        /// <summary>
        /// Суточные контроллеров по названию орагнизации
        /// </summary>
        /// <param name="organizationName"></param>
        /// <returns></returns>
        public string GetDailiesByOrganization(string organizationName);

        /// <summary>
        /// Добавление сессии агроспота
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public string AddAgrospotSession(string inputData, string vehicleNum);
    }
}
