using AlcoScriptGenerator.BusinessLogic.Entities;
using AlcoScriptGenerator.BusinessLogic.Interfaces;
using AlcoScriptGenerator.BusinessLogic.ScriptGeneration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AlcoScriptGenerator.Pages.Askp
{
    /// <summary>
    /// Interaction logic for OrganizationNamePage.xaml
    /// </summary>
    public partial class OrganizationNamePage : Page, IBaseFrameLogic
    {
        /// <summary>
        /// Генератор скриптов
        /// </summary>
        private IComplexScriptsGenerator _gen;

        public OrganizationNamePage()
        {
            InitializeComponent();
            _gen = new ComplexScriptsGenerator();
        }
        /// <summary>
        /// GO!!!!!!!
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
            if (string.IsNullOrEmpty(OrgNameTextBox.Text))
                return InputValidation.NotEnoughArgs;

            if (ScriptDto.SelectedScript.TypeOfScript.Equals(ScriptType.ASKP))
                return _gen.GenerateComplexScriptForAskp(OrgNameTextBox.Text);

            return string.Empty;
        }

        /// <summary>
        /// Пишем только цифры и словечки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrgNameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (InputValidation.WordFilter.IsMatch(e.Text) ||
                InputValidation.DigitalFilter.IsMatch(e.Text))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
