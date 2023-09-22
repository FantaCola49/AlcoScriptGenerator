using AlcoScriptGenerator.BusinessLogic.Interfaces;
using System;
using System.Windows.Controls;

namespace AlcoScriptGenerator.Pages
{
    /// <summary>
    /// Interaction logic for GpsNavigationSearch.xaml
    /// </summary>
    public partial class GpsNavigationSearch : Page, IBaseFrameLogic
    {
        public GpsNavigationSearch()
        {
            InitializeComponent();
        }

        public string GenerateScript()
        {
            return ScriptGeneration(StartDate.SelectedDate, EndDate.SelectedDate);
        }

        /// <summary>
        /// Непосредственно скрипт
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private string ScriptGeneration(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
                return string.Empty;

            return
            @$" DECLARE @StartDate DATE;
                DECLARE @EndDate DATE;

                SET @StartDate = '{startDate}'
                SET @EndDate = '{endDate}'

                SELECT *
                FROM [AskpDb].[dbo].[GpsPoint] as a FULL OUTER JOIN
                [AskpDb].[dbo].[LevelMeterData] as b
                on a.Id = b.GpsPointId 
                WHERE a.ControllerId = 581 AND a.GmtTime BETWEEN  and  ORDER BY a.GmtTime";
        }
    }
}
