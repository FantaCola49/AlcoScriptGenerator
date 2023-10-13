using AlcoScriptGenerator.BusinessLogic.ScriptGeneration;
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

        private IComplexScriptsGenerator _gen;
        public GpsNavigationSearch()
        {
            InitializeComponent();
            _gen = new ComplexScriptsGenerator();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ScriptDto.SetNewScript(GenerateScript());
        }

        public string GenerateScript()
        {
            return ScriptGeneration(StartDate.SelectedDate, EndDate.SelectedDate, VehicleNumberTB.Text);
        }

        /// <summary>
        /// Непосредственно скрипт из фабрики
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private string ScriptGeneration(DateTime? startDate, DateTime? endDate, string vehicleNumber)
        {
            if (   startDate == null 
                || endDate == null
                || string.IsNullOrEmpty(vehicleNumber))
                return InputValidation.NotEnoughArgs;

            if ( ScriptDto.SelectedScript.TypeOfScript.Equals(ScriptType.Agrospot))
                return _gen.GenerateComplexScriptForAgrospot(startDate, endDate, vehicleNumber);
            else if (ScriptDto.SelectedScript.TypeOfScript.Equals(ScriptType.ASKP))
                return _gen.GenerateComplexScriptForAskp(vehicleNumber, startDate, endDate);

            return string.Empty;
        }

        /// <summary>
        /// Только цифры, братан
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VehicleNumberTB_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (InputValidation.DigitalFilter.IsMatch(e.Text))
                e.Handled = false;
            else 
                e.Handled = true;
        }
    }
}
