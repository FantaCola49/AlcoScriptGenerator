using AlcoScriptGenerator.BusinessLogic.Entities;
using AlcoScriptGenerator.BusinessLogic.Interfaces;
using AlcoScriptGenerator.BusinessLogic.WindowElements;
using ScriptType = AlcoScriptGenerator.BusinessLogic.Entities.ScriptType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AlcoScriptGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Логика Кнопок
        /// </summary>
        private IButtonsLogic btn;

        public MainWindow()
        {
            InitializeComponent();
            btn = new ButtonsLogic();
            // Заполняем ниспадающий список типов скриптов
            FillTypeScriptsComboBox();
            // Заполняем ниспадающий список скриптов
            FillScriptsComboBox(ScriptType.Agrospot);
        }

        /// <summary>
        /// Пояснение типа скрипта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QScriptType_Click(object sender, RoutedEventArgs e)
        {
            var item = (TypeScriptListMember)ScriptTypeCB.SelectedItem;
            btn.QScriptType(item.Title, item.Description);
        }
        
        /// <summary>
        /// Пояснение скрипта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QScriptName_Click(object sender, RoutedEventArgs e)
        {
            var item = (Script)ScriptNameCB.SelectedItem;
            btn.QScript(item.Title, item.Description);
        }

        /// <summary>
        /// Обзор...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseFolderPath_Click(object sender, RoutedEventArgs e)
        {
            FilePathTB.Text = btn.BrowseFolderPath();
        }

        /// <summary>
        /// Подготовит выпадающий список типов скриптов
        /// </summary>
        private void FillTypeScriptsComboBox()
        {
            IComboBoxData data = new ComboBoxData();
            var scriptTypes = data.GetScriptTypes();
            ScriptTypeCB.ItemsSource = scriptTypes;
            ScriptTypeCB.DisplayMemberPath = "Title";
            // Агроспоты
            ScriptTypeCB.SelectedItem = scriptTypes[0];
        }

        /// <summary>
        /// Подготовит выпадающий список скриптов для агроспотов
        /// </summary>
        private void FillScriptsComboBox(ScriptType type)
        {
            IComboBoxData data = new ComboBoxData();
            List<Script> scripts = new List<Script>();

            if (type is ScriptType.Agrospot)
                scripts = data.GetAgrospotScripts();

            else if (type is ScriptType.ASKP)
                scripts = data.GetAskpScripts();

            else if (type is ScriptType.Zavod)
                scripts = data.GetZavodScripts();

            else return;

            ScriptTypeCB.ItemsSource = scripts;
            ScriptTypeCB.DisplayMemberPath = "Title";
            ScriptTypeCB.SelectedItem = scripts[0];
        }

        private void ScriptType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (TypeScriptListMember)ScriptNameCB.SelectedItem;
            FillScriptsComboBox(item.TypeOfScript);
        }
    }
}
