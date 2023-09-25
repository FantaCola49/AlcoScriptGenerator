using AlcoScriptGenerator.BusinessLogic.Entities;
using AlcoScriptGenerator.BusinessLogic.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace AlcoScriptGenerator.Pages.Agrospot
{
    /// <summary>
    /// Interaction logic for DeleteGpsNavigation.xaml
    /// </summary>
    public partial class DeleteGpsNavigation : Page, IBaseFrameLogic
    {
        InputValidation _val;
        public DeleteGpsNavigation()
        {
            InitializeComponent();
            _val = new InputValidation();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ScriptDto.SetNewScript(GenerateScript());
        }

        public string GenerateScript()
        {
            return ScriptGeneration(GpsPointNameTB.Text);
        }

        private string ScriptGeneration(string gpsName)
        {
            if (string.IsNullOrEmpty(gpsName))
                return _val.NotEnoughArgs;

            if (!gpsName.Contains(".UTM") ||
                !gpsName.Contains("."))
                gpsName +=".UTM";


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
    }
}
