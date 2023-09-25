using AlcoScriptGenerator.BusinessLogic.Entities;
using AlcoScriptGenerator.BusinessLogic.Interfaces;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AlcoScriptGenerator.Pages
{
    /// <summary>
    /// Interaction logic for GpsNavigationSearch.xaml
    /// </summary>
    public partial class GpsNavigationSearch : Page, IBaseFrameLogic
    {
        /// <summary>
        /// Валидация
        /// </summary>
        private InputValidation _val;
        public GpsNavigationSearch()
        {
            InitializeComponent();
            _val = new InputValidation();
        }

        public string GenerateScript()
        {
            return ScriptGeneration(StartDate.SelectedDate, EndDate.SelectedDate, VehicleNumberTB.Text);
        }

        /// <summary>
        /// Непосредственно скрипт
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private string ScriptGeneration(DateTime? startDate, DateTime? endDate, string vehicleNumber)
        {
            if (   startDate == null 
                || endDate == null
                || string.IsNullOrEmpty(vehicleNumber))
                return _val.NotEnoughArgs;

            return
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ScriptDto.SetNewScript(GenerateScript());
        }

        private void VehicleNumberTB_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (_val.digitalFilter.IsMatch(e.Text))
                e.Handled = false;
            else 
                e.Handled = true;
        }
    }
}
