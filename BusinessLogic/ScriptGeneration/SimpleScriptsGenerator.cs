using AlcoScriptGenerator.BusinessLogic.Entities;
using AlcoScriptGenerator.BusinessLogic.Interfaces;

namespace AlcoScriptGenerator.BusinessLogic.ScriptGeneration
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
            ScriptDto.SetNewScript(result);
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
                1 => AgrospotSessionDisplay(),
                2 => AgrospotDailiesAndTickets(),
                _ => string.Empty,
            };
            return res;
        }

        /// <summary>
        /// Скрипт Завода
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        private string ZavodScript(Script script)
        {
            var type = script.Id;
            string res = type switch
            {
                2 => ZavodLineProductAdjustmentWithNames(),
                5 => ZavodFlowmeterNamesByLines(),
                _ => string.Empty,
            };
            return res;
        }

        /// <summary>
        /// Скрипт АСКП
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        private string AskpScript(Script script)
        {
            var type = script.Id;
            string res = type switch
            {
                6 => AskpDriverProductsWithProve(),
                7 => AskpAllDriverSessionWithProductsAndProve(),
                _ => string.Empty,
            };
            return res;
        }

        #region Agrospot scripts

        /// <summary>
        /// Agrospot_Выгрузка сессий агроспота
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
        /// Agrospot_Сценарий для выгрузки Гуидов по суточным с Агроспота
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

        /// <summary>
        /// Zavod_Скрипт отображения настроек линий завода с именами продуктов
        /// </summary>
        /// <returns></returns>
        private string ZavodLineProductAdjustmentWithNames()
            => @"/****** Сценарий для команды SelectTopNRows среды SSMS  ******/
                SELECT TOP 1000 a.[Id]
	                  ,a.[ProductId]
	                  ,b.Title
                      ,a.[SpecificationProof]
                      ,a.[MinProof]
                      ,a.[MaxProof]
                      ,a.[EnableProofAsInSpecification]
                      ,a.[EnableFitToSpecificProof]
                      ,a.[FitToSpecificProofFluctuationPercent]
                      ,a.[EnableCustomDensity20]
                      ,a.[CustomDensity20]
                      ,a.[DensityMultiplayer]
                      ,a.[RawVolumeMultiplayer]
                      ,a.[MassMultiplayer]
                      ,a.[LineId]
                  FROM [AlcoSpotMainServiceDB].[dbo].[LineProductAdjustment] as a
                  JOIN [AlcoSpotMainServiceDB].[dbo].[Product] as b on a.ProductId = b.Id";

        /// <summary>
        /// Zavod_Скрипт Названия Расходомеров Завода По Линиям
        /// </summary>
        /// <returns></returns>
        private string ZavodFlowmeterNamesByLines()
            => @"/****** Сценарий для команды SelectTopNRows среды SSMS  ******/
                SELECT l.[Id]
                      ,[Number]
                      ,l.[Title] as [Название линии]
                      ,f.[Title] as [Расходомер]
                      ,f.[SerialNumber] as [Серийный номер]
                      ,[AqueousAlcoholicSolutionTotal]
                      ,[AnhydrousAlcoholicSolutionTotal]
                      ,[BottleCountTotal]
                      ,[FacilityDepartment]
                      ,[CreationTimestamp]
                      ,[DeletionTimestamp]
                      ,l.[IsDeleted]
                      ,[FlowMeterId]
                      ,[BottleCounterId]
                      ,[ValveId]
                      ,[DisplayOrder]
                      ,[ProcessCalculationType]
                      ,[DisableReports]
                      ,[MainTotalizer]
                      ,[UseAbsTotalDeltaInCalcs]
                      ,[DisableStartWithFlowmeterError]
                      ,[DisableShipmentDosing]
                      ,[DozingThresholdTypeEnum]
                      ,[ShipmentDosingStopThreshold]
                  FROM [AlcoSpotMainServiceDB].[dbo].[Line] as l
                  LEFT JOIN [AlcoSpotMainServiceDB].[dbo].[FlowMeter] as f on l.FlowMeterId = f.Id";

        #endregion

        #region Askp
        /// <summary>
        /// АСКП_Скрипт выгрузки всех сессий продуктов перевозчиков и их крепости
        /// </summary>
        /// <returns></returns>
        private string AskpAllDriverSessionWithProductsAndProve()
            => @"/****** Скрипт для команды SelectTopNRows из среды SSMS  ******/
                SELECT DISTINCT
                      c.VehicleIdentificationNumber as 'Номер машины'
	                  ,org.Title as 'Организация'
                      ,[Mode]
                      ,[ProductCode]
	                  ,atp.Name as 'Продукт'
                      ,[StartTimestamp]
                      ,[FinishTimestamp]
                      ,[AqueousAlcoholicSolutionOnStart]
                      ,[AqueousAlcoholicSolutionOnEnd]
                      ,[AnhydrousAlcoholicSolutionOnStart]
                      ,[AnhydrousAlcoholicSolutionOnEnd]
                      ,[ProofAverage]
                      ,[ProofAverageOverVolumesRatio]
                  FROM [AskpDb].[dbo].[Session] as s
                  LEFT JOIN [AlcospotTransportControlPanel].[dbo].[Products] as atp on atp.NameCode = s.ProductCode
                  LEFT JOIN [AskpDb].dbo.Controller as c on c.Id = s.ControllerId
                  LEFT JOIN [AskpDb].dbo.AskpOrganization as org on org.Id = c.OrganizationId

                  WHERE org.IsDeleted = 0
                  AND ProofAverage != 0

                  ORDER BY org.Title ASC, StartTimestamp ASC, ProductCode asc";

        /// <summary>
        /// АСКП_Скрипт выгрузки продукта по коду перевозчиков и их крепости
        /// </summary>
        /// <returns></returns>
        private string AskpDriverProductsWithProve()
            => @"/****** Скрипт для команды SelectTopNRows из среды SSMS  ******/
                SELECT DISTINCT
	                  -- s.[ControllerId]
	                  c.VehicleIdentificationNumber as 'Номер машины'
	                  --,org.Id
	                  ,org.Title as 'Организация'
	                  ,s.ProductCode
	                  ,atp.TypeCode
	                  ,atp.Name as 'Продукт'
                      ,s.[ProofAverage]
	                  ,atp.AlcVolume
	                  ,atp.AlcVolumeForDisplay

                  FROM [AskpDb].[dbo].[Session] as s
                  LEFT JOIN [AskpDb].dbo.Controller as c on c.Id = s.ControllerId
                  LEFT JOIN [AskpDb].dbo.AskpOrganization as org on org.Id = c.OrganizationId
                  LEFT JOIN [AlcospotTransportControlPanel].[dbo].[Products] as atp on atp.NameCode = s.ProductCode

                  WHERE org.Id != 7 AND org.IsDeleted = 0
                  AND ProofAverage != 0

                  ORDER BY org.Title ASC, VehicleIdentificationNumber ASC, s.ProductCode ASC, Name ASC, s.ProofAverage ASC";

        #endregion
    }
}
