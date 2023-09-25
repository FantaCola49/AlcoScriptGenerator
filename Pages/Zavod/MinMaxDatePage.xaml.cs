using AlcoScriptGenerator.BusinessLogic;
using AlcoScriptGenerator.BusinessLogic.Entities;
using AlcoScriptGenerator.BusinessLogic.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace AlcoScriptGenerator.Pages.Zavod
{
    /// <summary>
    /// Interaction logic for MinMaxDatePage.xaml
    /// </summary>
    public partial class MinMaxDatePage : Page, IBaseFrameLogic
    {
        /// <summary>
        /// Генератор скриптов
        /// </summary>
        IComplexScriptsGenerator _gen;
        public MinMaxDatePage()
        {
            InitializeComponent();
            _gen = new ComplexScriptsGenerator();
        }

        public string GenerateScript()
        {
            string result = ScriptDto.SelectedScript.ScriptId switch
            {
                ScriptId.ZavodDailies => 
            };
            return string.Empty;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GenerateScript();
        }
    }
}
