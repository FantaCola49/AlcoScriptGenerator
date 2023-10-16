using AlcoScriptGenerator.BusinessLogic.Entities;
using AlcoScriptGenerator.BusinessLogic.Interfaces;
using AlcoScriptGenerator.BusinessLogic.ScriptGeneration;
using System.Windows.Controls;

namespace AlcoScriptGenerator.Pages.Askp
{
    /// <summary>
    /// Interaction logic for VehicleNumberPage.xaml
    /// </summary>
    public partial class VehicleNumberPage : Page, IBaseFrameLogic
    {
        /// <summary>
        /// Генератор скриптов
        /// </summary>
        IComplexScriptsGenerator _gen;
        public VehicleNumberPage()
        {
            InitializeComponent();
            _gen = new ComplexScriptsGenerator();
        }
        /// <summary>
        /// Генерируем скрипт
        /// </summary>
        /// <returns></returns>
        public string GenerateScript()
        {
            if (string.IsNullOrEmpty(VehicleNumberTB.Text))
                return InputValidation.NotEnoughArgs;

            if (ScriptDto.SelectedScript.TypeOfScript.Equals(ScriptType.Agrospot))
                return _gen.GenerateComplexScriptForAgrospot(VehicleNumberTB.Text);
            else if (ScriptDto.SelectedScript.TypeOfScript.Equals(ScriptType.ASKP))
                return _gen.GenerateComplexScriptForAskp(VehicleNumberTB.Text);

            return string.Empty;
        }

        /// <summary>
        /// Вводим только циферки
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

        /// <summary>
        /// Кнопка ежжи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ScriptDto.SetNewScript(GenerateScript());
        }
    }
}
