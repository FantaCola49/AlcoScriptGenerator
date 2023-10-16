using AlcoScriptGenerator.BusinessLogic.ScriptGeneration;
using AlcoScriptGenerator.BusinessLogic.Entities;
using AlcoScriptGenerator.BusinessLogic.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace AlcoScriptGenerator.Pages.Askp
{
    /// <summary>
    /// Interaction logic for AgrospotSessionAddition.xaml
    /// </summary>
    public partial class AgrospotSessionAddition : Page, IBaseFrameLogic
    {
        /// <summary>
        /// Генератор скриптов
        /// </summary>
        IComplexScriptsGenerator _gen;

        public AgrospotSessionAddition()
        {
            InitializeComponent();
            _gen = new ComplexScriptsGenerator();
        }

        /// <summary>
        /// Кнопочка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ScriptDto.SetNewScript(GenerateScript());
        }

        /// <summary>
        /// Генерируем скрипт
        /// </summary>
        /// <returns></returns>
        public string GenerateScript()
        {
            if (!string.IsNullOrEmpty(RawSessionInputTB.Text) ||
                !string.IsNullOrEmpty(VehicleNumberTB.Text))
                return _gen.GenerateComplexScriptForAskp(RawSessionInputTB.Text, VehicleNumberTB.Text);

            else
                return InputValidation.UnreconizableArgs;
        }

        /// <summary>
        /// Чтобы нельзя было вводить ничего с клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VehicleNumberTB_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (InputValidation.DigitalFilter.IsMatch(e.Text) ||
                InputValidation.WordFilter.IsMatch(e.Text))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
