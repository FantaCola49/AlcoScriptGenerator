using AlcoScriptGenerator.BusinessLogic;
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
        IComplexScriptsGenerator _gen;

        public DeleteGpsNavigation()
        {
            InitializeComponent();
            _val = new InputValidation();
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
                return _val.NotEnoughArgs;

            return _gen.DeleteGpsNavigation(gpsName);
        }
    }
}
