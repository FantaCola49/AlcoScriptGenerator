using AlcoScriptGenerator.BusinessLogic.ScriptGeneration;
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
        IComplexScriptsGenerator _gen;

        public DeleteGpsNavigation()
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
            return ScriptGeneration(GpsPointNameTB.Text);
        }

        private string ScriptGeneration(string gpsName)
        {
            if (string.IsNullOrEmpty(gpsName))
                return InputValidation.NotEnoughArgs;
            
            // Скрипт исключительно для агроспотов, так что минуем проверки...
            return _gen.GenerateComplexScriptForAgrospot(gpsName);
        }
    }
}
