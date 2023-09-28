using AlcoScriptGenerator.BusinessLogic.Entities;
using AlcoScriptGenerator.BusinessLogic.Interfaces;
using System;

namespace AlcoScriptGenerator.BusinessLogic.ScriptGeneration
{
    public class ComplexScriptsGenerator : AddAgrospotSessionToAskpScript, IComplexScriptsGenerator
    {
        /// <summary>
        /// Сгенерировать скрипт для завода в зависимости от выбранного элемента
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public string GenerateComplexScriptForZavod(DateTime? startDate, DateTime? endDate)
        {
            return ScriptDto.SelectedScript.ScriptId switch
            {
                ScriptId.ZavodSessionsMinMaxDate => ZavodSessionsMinMaxDate(startDate, endDate),
                ScriptId.ZavodDailies => ZavodDailies(startDate, endDate),
                ScriptId.ZavodDiscreteFullRemastered => ZavodDiscreteFullRemastered(startDate, endDate)

            };
        }


        #region Agrospot
        /// <summary>
        /// Удаление gps навигации
        /// </summary>
        /// <param name="gpsName"></param>
        /// <returns></returns>
        public string DeleteGpsNavigation(string gpsName)
        {
            if (!gpsName.Contains(".UTM") ||
                !gpsName.Contains("."))
                gpsName += ".UTM";


            return
                $@"  SELECT BaseDailyReportLog.Id FROM [AlcoSpotMainServiceDB].[dbo].[BaseDailyReportLog] WHERE [FileName] LIKE '%{gpsName}%'

                    DECLARE @requiredId INT
                    SET @requiredId =
                    ( SELECT TOP 1 [Id]
                    FROM [AlcoSpotMainServiceDB].[dbo].[BaseDailyReportLog]
                    WHERE [FileName] LIKE '%{gpsName}%')
                    DELETE FROM [AlcoSpotMainServiceDB].[dbo].[XmlDailyReportLog] WHERE [Id] = @requiredId
                    DELETE FROM [AlcoSpotMainServiceDB].[dbo].[BaseDailyReportLog] WHERE [Id] = @requiredId";
        }

        /// <summary>
        /// Поиск и выгрузка gps навигации
        /// </summary>
        /// <param name="startDate">Начало</param>
        /// <param name="endDate">Конец</param>
        /// <param name="vehicleNumber">Номер ТС</param>
        /// <returns></returns>
        public string GpsNavigationSearch(DateTime? startDate, DateTime? endDate, string vehicleNumber)
            =>
            @$" --****Скрипт выгрузки пятиминуток****--
                DECLARE @StartDate DATE;
                DECLARE @EndDate DATE;

                SET @StartDate = '{startDate}'
                SET @EndDate = '{endDate}'

                SELECT *
                FROM [AskpDb].[dbo].[GpsPoint] as a FULL OUTER JOIN
                [AskpDb].[dbo].[LevelMeterData] as b
                on a.Id = b.GpsPointId 
                WHERE a.ControllerId = (SELECT Id FROM AskpDB.dbo.Controller WHERE VehicleIdentificationNumber LIKE '%{vehicleNumber}%')
				AND a.GmtTime BETWEEN @StartDate and @EndDate
				ORDER BY a.GmtTime";

        #endregion

        #region Zavod

        /// <summary>
        /// Скрипт cессий pавода MinMaxDate
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private string ZavodSessionsMinMaxDate(DateTime? startDate, DateTime? endDate)
            => @$"DECLARE @MinDate DATE
                declare @maxdate date
                SET @MinDate = '{startDate}'
                set @maxdate = '{endDate}'

                SELECT Session.[Id]
                      ,WorkMode.Title as WorkMode
	                  ,[ProductId]
	                  ,Line.Id as LineId
                      ,Line.Title as LineTitle
                      ,[StartTimestamp]
                      ,[FinishTimestamp]
                      ,[AqueousAlcoholicSolutionOnStart]
                      ,[AqueousAlcoholicSolutionOnEnd]
	                  ,(AqueousAlcoholicSolutionOnEnd - AqueousAlcoholicSolutionOnStart) as AqDelta
                      ,[AnhydrousAlcoholicSolutionOnStart]
                      ,[AnhydrousAlcoholicSolutionOnEnd]
	                  ,(AnhydrousAlcoholicSolutionOnEnd - AnhydrousAlcoholicSolutionOnStart) as AnhDelta
                      ,[ProofAverage]
	                  ,[BottleCountOnStart]
                      ,[BottleCountOnEnd]
	                  ,(BottleCountOnEnd - BottleCountOnStart) as BottleDelta
                      ,[BottleCountCurrent]
                      ,[TemperatureOnStart]
                      ,[TemperatureOnFinish]
                      ,[TemperatureAverage]
                      ,[DensityAverage]
                      ,[MassTotalizerOnStart]
                      ,[MassTotalizerOnFinish]
                      ,[VolumetricTotalizerOnStart]
                      ,[VolumetricTotalizerOnFinish]
	                  ,(VolumetricTotalizerOnFinish - VolumetricTotalizerOnStart) as TotalizerDelta
                      ,[MassTotalizerCurrent]
                      ,[MassFlowAverage]
                      ,[VolumetricTotalizerCurrent]
                      ,[VolumeFlowAverage]   
	                  ,[VolumeDosingCutoff]   
                  FROM AlcoSpotMainServiceDB.[dbo].[Session]
                   INNER JOIN AlcoSpotMainServiceDB.[dbo].Line
                   ON Line.Id = Session.LineId
                   INNER JOIN AlcoSpotMainServiceDB.[dbo].WorkMode
                   ON AlcoSpotMainServiceDB.[dbo].WorkMode.Id = AlcoSpotMainServiceDB.[dbo].[Session].Mode  
                  WHERE StartTimestamp >= @MinDate
                  --and StartTimestamp <= @maxdate
  
  
                  ORDER BY Id ASC";

        /// <summary>
        /// Выгрузка суточных с завода
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private string ZavodDailies(DateTime? startDate, DateTime? endDate)
            => @$"SELECT * FROM [AlcoSpotMainServiceDB].[dbo].[BaseDailyReportLog]
                LEFT JOIN [AlcoSpotMainServiceDB].[dbo].[XmlDailyReportLog]
                ON XmlDailyReportLog.Id = BaseDailyReportLog.Id
                WHERE XmlDailyReportLog.DateSendToUtm BETWEEN '{startDate}' and '{endDate}' 
                AND XmlReportStatus NOT IN (30) 
                AND ReportType NOT IN (20,30)
                ORDER BY DateSendToUtm ASC";

        /// <summary>
        /// Скрипт дискретов завода
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private string ZavodDiscreteFullRemastered(DateTime? startDate, DateTime? endDate)
            => $@"DECLARE @MinDate DATE;
                DECLARE @MaxDate DATE;
                SET @MinDate = '{startDate}'; --YYYY-MM-DD
                SET @MaxDate = '{endDate}'; --YYYY-MM-DD

                SELECT Discrete.[Id]
	                  ,Line.Id as LineId
	                  ,Line.Title as LineTitle
                      ,[AqueousAlcoholicSolution]
                      ,[AnhydrousAlcoholicSolution]
                      ,[Proof]
                      ,[MassFlow]
                      ,[VolumeFlow]
                      ,[MassTotalizer]
                      ,[VolumeTotalizer]
                      ,[Temperature]
	                  ,[DensityUI]
                      ,[Density]
	                  ,[Density20]
                      ,[FlowMeterIsFaulted]      
	                  ,WorkMode.Title as WorkMode
	                  ,[SessionId]
                      ,Discrete.[CreationTimestamp]
	                  ,BottleCountFromSessionEnd
	                  ,BottleCountFromBottleCounter
	                  ,BottleCounterIsFaulted
                  FROM [AlcoSpotMainServiceDB].[dbo].[Discrete]
                  INNER JOIN [AlcoSpotMainServiceDB].[dbo].[Session]
                  ON [AlcoSpotMainServiceDB].[dbo].[Session].Id = [AlcoSpotMainServiceDB].[dbo].[Discrete].SessionId
                  INNER JOIN [AlcoSpotMainServiceDB].[dbo].[WorkMode]
                  ON [AlcoSpotMainServiceDB].[dbo].[Session].Mode = [AlcoSpotMainServiceDB].[dbo].[WorkMode].Id
                  INNER JOIN [AlcoSpotMainServiceDB].[dbo].[Line]
                  ON [AlcoSpotMainServiceDB].[dbo].[Line].Id = [AlcoSpotMainServiceDB].[dbo].[Session].LineId
                  --WHERE lineid = 2
                  AND Discrete.CreationTimestamp >= @MinDate 
                  AND Discrete.CreationTimestamp <= @MaxDate
                  ORDER BY Lineid,Discrete.Id ASC";
        #endregion

        #region ASKP
        /// <summary>
        /// Контроллер по его номеру
        /// </summary>
        /// <param name="vehicleNumber"></param>
        /// <returns></returns>
        public string GetControllerByItsNumber(string vehicleNumber)
            => $@"/****** Скрипт для команды SelectTopNRows из среды SSMS  ******/
                SELECT TOP 1000 [Id]
                      ,[OrganizationId]
                      ,[Imei]
                      ,[VehicleIdentificationNumber]
                      ,[SimCardPaidTillUtc]
                      ,[MaintenanceServicePaidTillUtc]
                      ,[IsDeleted]
                      ,[SyncMonitoringDate]
                      ,[SyncAskpRarDate]
                      ,[DeleteUtcDateTime]
                      ,[SerialNumber]
                      ,[SyncRarDailyDate]
                      ,[SyncRarWayBillDate]
                      ,[RecursiaControllerGuid]
                      ,[RecursiaControllerId]
                      ,[PreviousControllerId]
                      ,[ReplacementPreviousControllerDateTimeUTC]
                  FROM [AskpDb].[dbo].[Controller]

                  WHere VehicleIdentificationNumber LIKE '%{vehicleNumber}%'";

        /// <summary>
        /// Сессии контроллера по его номеру за определённый период
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="vehicleNumber"></param>
        /// <returns></returns>
        public string GetControllerSessions(DateTime? startDate, DateTime? endDate, string vehicleNumber)
            => $@"/****** Скрипт для команды SelectTopNRows из среды SSMS  ******/
                SELECT TOP 1000 s. [Id]
                      ,[ControllerId]
	                  ,contr.[VehicleIdentificationNumber]
	                  ,contr.[SerialNumber]
                      ,[Mode]
                      ,[ProductCode]
                      ,[StartTimestamp]
                      ,[FinishTimestamp]
                      ,[ServerAcceptingUtcTime]
                      ,[ExternalId]
                      ,[AcceptingFromRarGmtTime]
                      ,[AcceptedByRar]
                      ,[TemperatureAverage]
                      ,[DensityAverage]
                      ,[AqueousAlcoholicSolutionOnStart]
                      ,[AqueousAlcoholicSolutionOnEnd]
                      ,[AnhydrousAlcoholicSolutionOnStart]
                      ,[AnhydrousAlcoholicSolutionOnEnd]
                      ,[LineType]
                      ,[ProductId]
                      ,[TotalizerOnStart]
                      ,[TotalizerOnFinish]
                      ,[SendingToUtmGmtTime]
                      ,[AcceptedByUtm]
                      ,[ReplyId]
                      ,[UtmMessage]
                      ,[RarMessage]
                      ,[ProofAverage]
                      ,[FilePath]
                      ,[ProofAverageOverVolumesRatio]
                      ,[ProofCalculationType]
                  FROM [AskpDb].[dbo].[Session] as s
                  inner join [AskpDb].[dbo].[Controller] 
                  as contr on contr.Id = s.ControllerId
                  where ControllerId = 
                  (Select Id FROM AskpDB.dbo.Controller 
                  WHERE VehicleIdentificationNumber LIKE '%{vehicleNumber}%')
                  and  StartTimeStamp > '{startDate}'
                  AND FinishTimestamp > '{endDate}'
                  Order BY StartTimeStamp asc, id asc";

        /// <summary>
        /// История контроллера
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="vehicleNumber"></param>
        /// <returns></returns>
        public string GetControllerEventHistory(DateTime? startDate, DateTime? endDate, string vehicleNumber)
            =>
            $@"/****** Скрипт для выборки событий по ТС  ******/
            SELECT eh.[Id]
                  ,[ControllerId]
                  ,[GmtTime]
                  ,eh.[Comment]
                  ,[ServerAcceptingUtcTime]
                  ,[EventId]
	              ,e.Comment as EventType
              FROM [AskpDb].[dbo].[EventHistory] as eh
              JOIN [AskpDb].[dbo].[Event] as e on e.Id = eh.EventId
              where ControllerId = 
              (select Id FROM [AskpDb].[dbo].[Controller] 
              where VehicleIdentificationNumber LIKE '%{vehicleNumber}%')
              AND
              GmtTime BETWEEN '{startDate} 00:00:00' AND '{endDate} 00:00:00'
              ORDER BY GmtTime ASC";

        /// <summary>
        /// Получить Gps координаты с характеристиками уровнемеров
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="vehicleNumber"></param>
        /// <returns></returns>
        public string GetControllerGpsPointData(DateTime? startDate, DateTime? endDate, string vehicleNumber)
            => $@"DECLARE @StartDate DATE;
                 DECLARE @EndDate DATE;
                 DECLARE @VehicleNumber VARCHAR;

                 SET @StartDate = '{startDate} 13:39' --YYYY-MM-DD HH:mm
                 SET @EndDate =   '{endDate} 20:54'


                  SELECT a.[Id],
                  c.VehicleIdentificationNumber as [Номер ТС],
                  org.Title as [Организация],
                  a.GmtTime as [Время GMT],
                  a.Longitude [Долгота],
                  a.Latitude [Широта],
                  a.SateliteNumber [Число Спутников],
                  a.Accuracy [Точность],
                  a.Speed [Скорость],
                  a.Course [Курс],
                  a.ServerAcceptingUtcTime [Время принятия ответа],
                  a.ExternalId [Внешний Id],
                  a.AcceptingFromRarGmtTime [Время принятия РАРом GMT],
                  a.AcceptedByRar as [Принят РАР],
                  a.SendingToUtmGmtTime as [Время отправки на УТМ GMT],
                  a.AcceptedByUtm as [Принят УТМ],
                  a.ReplyId as [Ответный Id],
                  a.RarMessage as [Сообщение от РАР],
                  b.GpsPointId,
                  b.CellId as [Отсек],
                  b.Density as [Плотность],
                  b.Level as [Уровень, %],
                  b.Temperature as [Температура, C],
                  b.Volume as [Объём]

                  FROM [AskpDb].[dbo].[GpsPoint] as a FULL OUTER JOIN
                  [AskpDb].[dbo].[LevelMeterData] as b on a.Id = b.GpsPointId 
                  LEFT JOIN [AskpDB].[dbo].[Controller] as c on c.Id = a.ControllerId
                  LEFT Join [AskpDB].[dbo].[AskpOrganization] as org on org.Id = c.OrganizationId
                  WHERE a.ControllerId = 
                  (SELECT Id FROM [AskpDB].[dbo].[Controller]
                  WHERE VehicleIdentificationNumber 
                  --Указать номер машины в процентах
                  LIKE '%{vehicleNumber}%' -- '%NUMB%'
                  AND IsDeleted = 0)
                  AND a.GmtTime BETWEEN @StartDate and @EndDate ORDER BY a.GmtTime";

        /// <summary>
        /// Добавление сессии агроспота
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public string AddAgrospotSession(string inputData, string vehicleNum)
        {
            try
            {
                return base.AddAgrospotSessionTest(inputData, vehicleNum);
            }
            catch (Exception ex)
            {
                return $"{InputValidation.AskpScriptViolation()}\n\n{ex.Message}\n\n{ex.StackTrace}";
            }
        }

        /// <summary>
        /// Суточные контроллеров по названию орагнизации
        /// </summary>
        /// <param name="organizationName"></param>
        /// <returns></returns>
        public string GetDailiesByOrganization(string organizationName)
            => $@"/****** Скрипт для получения мониторинга суточных файлов в АСКП по организации  ******/
                SELECT df.[Id]
	                  ,c.VehicleIdentificationNumber
                      ,[ReportingDateUtcTime] AS [Дата создания отчёта GMT]
                      ,[ServerAcceptingUtcTime] AS [Время принятия сервером АСКП GMT]
	                  ,[AcceptedByUtm] AS [Принят Утм?]
                      ,[SendingToUtmGmtTime] AS [Время отправки в УТМ GMT]
	                  ,[AcceptedByRar] AS [Принят в РАР?]
                      ,[AcceptingFromRarGmtTime] AS [Время принятия РАРом GMT]
                      ,[RarMessage] AS [Ответ от РАР]
                      --,[Content] AS [Содержание]
                      ,[LineType] AS [Линия]
	                  ,[ExternalId] AS [Id отправки отчёта]
                      ,[ReplyId] AS [Id ответа от РАР]
                  FROM [AskpDb].[dbo].[DailyFile] as df
                  LEFT JOIN [AskpDb].[dbo].[Controller] as c on c.Id = df.ControllerId
                  WHERE ControllerId IN 
	                   (SELECT [Id] FROM [AskpDb].[dbo].[Controller] 
		                WHERE OrganizationId = 
		                   (Select TOP 1 [Id]
			                FROM [AskpDb].[dbo].[AskpOrganization]
			                WHERE IsDeleted = 0 AND Title LIKE '%{organizationName}%'))

                  ORDER BY ControllerId ASC, ReportingDateUtcTime ASC";
        #endregion
    }
}
