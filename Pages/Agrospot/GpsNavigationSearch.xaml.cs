using AlcoScriptGenerator.BusinessLogic;
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

        private IComplexScriptsGenerator _gen;
        public GpsNavigationSearch()
        {
            InitializeComponent();
            _val = new InputValidation();
            _gen = new ComplexScriptsGenerator();
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

            return _gen.GpsNavigationSearch(startDate, endDate, vehicleNumber);            
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
