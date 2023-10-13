using System;

namespace AlcoScriptGenerator.BusinessLogic.Interfaces
{
    /// <summary>
    /// Интерфейс генерации скриптов с аргументами
    /// </summary>
    interface IComplexScriptsGenerator
    {
        #region Script Generators
        /// <summary>
        /// Сгенерировать скрипт для завода в зависимости от выбранного элемента
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public string GenerateComplexScriptForZavod(DateTime? startDate, DateTime? endDate);

        #region Askp Generators
        /// <summary>
        /// Сгенерировать скрипт для АСКП в зависимости от выбранного элемента
        /// </summary>
        /// <param name="inputData">Номер ТС иди название компании</param>
        /// <returns></returns>
        public string GenerateComplexScriptForAskp(string inputData);

        /// <summary>
        /// Сгенерировать скрипт для АСКП в зависимости от выбранного элемента
        /// </summary>
        /// <param name="vehicleNumber">Номер ТС</param>
        /// <param name="startDate">Дата начала</param>
        /// <param name="endDate">Дата окончания</param>
        /// <returns></returns>
        public string GenerateComplexScriptForAskp(string vehicleNumber, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// Сгенерировать скрипт для АСКП в зависимости от выбранного элемента
        /// </summary>
        /// <param name="inputData">Сессия</param>
        /// <param name="vehicleNumber">Номер ТС</param>
        /// <returns></returns>
        public string GenerateComplexScriptForAskp(string inputData, string vehicleNumber);
        #endregion

        #region Agrospot Generators

        /// <summary>
        /// Сгенерировать скрипт для Агроспота в зависимости от выбранного элемента
        /// </summary>
        /// <param name="vehicleNumber">Номер ТС</param>
        /// <param name="startDate">Дата начала</param>
        /// <param name="endDate">Дата окончания</param>
        /// <returns></returns>
        public string GenerateComplexScriptForAgrospot(DateTime? startDate, DateTime? endDate, string vehicleNumber);

        /// <summary>
        /// Сгенерировать скрипт для Агроспота в зависимости от выбранного элемента
        /// </summary>
        /// <param name="vehicleNumber">Номер ТС</param>
        /// <param name="startDate">Дата начала</param>
        /// <param name="endDate">Дата окончания</param>
        /// <returns></returns>
        public string GenerateComplexScriptForAgrospot(string gpsName);

        #endregion

        #endregion
    }
}
