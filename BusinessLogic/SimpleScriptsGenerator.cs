using AlcoScriptGenerator.BusinessLogic.Entities;
using AlcoScriptGenerator.BusinessLogic.Interfaces;

namespace AlcoScriptGenerator.BusinessLogic
{
    public class SimpleScriptsGenerator : ISimpleScriptsGenerator
    {
        /// <summary>
        /// Сгенерировать простой скрипт (без аргументов)
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public void GetSimpleScript(Script script)
        {
            if (script == null) return;

            var type = script.TypeOfScript;

            string result = type switch
            {
                ScriptType.Agrospot => AgrospotScript(script),
                ScriptType.Zavod => ZavodScript(script),
                ScriptType.ASKP => AskpScript(script),
                _ => string.Empty,
            };
            ScriptDto.Script = result;
        }

        /// <summary>
        /// Скрипты агроспотов
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        private string AgrospotScript(Script script)
        {
            var type = script.Id;
            string res = type switch
            {
                2 => AgrospotSessionDisplay(),
                3 => AgrospotDailiesAndTickets(),
            };
            return res;
        }

        private string ZavodScript(Script script)
        {
            return string.Empty;
        }

        private string AskpScript(Script script)
        {
            return string.Empty;
        }

        #region Agrospot scripts

        /// <summary>
        /// Выгрузка сессий агроспота
        /// </summary>
        /// <returns></returns>
        private string AgrospotSessionDisplay()
        {
            return
                @"SELECT b.[Id]
                  ,[LineId]
                  ,l.Title as [Название линии]
                  ,[ReportType]
                  ,[LogDate]
                  ,[FileName]
                  ,[FlkMessage]
                  ,[FlkError]
                  ,[IsSentToIkt]
                  ,[IsUtmAnswerSentToIkt]
                  ,[IsRarAnswerSentToIkt]
                  ,[IsFileSentToIkt]
              FROM [AlcoSpotMainServiceDB].[dbo].[BaseDailyReportLog] as b


              LEFT JOIN [AlcoSpotMainServiceDB].[dbo].[Line] as l on l.Id =  b.LineId
                WHERE ReportType = 20";
        }

        /// <summary>
        /// Сценарий для выгрузки Гуидов по суточным с Агроспота
        /// </summary>
        /// <returns></returns>
        private string AgrospotDailiesAndTickets()
            =>
            @"  /****** Сценарий для выгрузки Гуидов по суточным с Агроспота ******/
                SELECT [Id]
                      ,[DateSendToUtm]
                      ,[UtmResponseGuid]
                      ,[UtmMessage]
                      ,[RarMessage]
                      ,[XmlReportStatus]
                      ,[CannotBeUseInCheckAndCreateDailyTask]
                  FROM [AlcoSpotMainServiceDB].[dbo].[XmlDailyReportLog]
                  WHERE UtmResponseGuid IS NOT NULL
                  order by Id asc";

        #endregion

        #region Zavod


        #endregion

        #region Askp

        #endregion
    }
}
