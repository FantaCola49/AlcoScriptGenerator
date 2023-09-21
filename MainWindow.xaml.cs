using AlcoScriptGenerator.BusinessLogic.Entities;
using AlcoScriptGenerator.BusinessLogic.Interfaces;
using AlcoScriptGenerator.BusinessLogic.WindowElements;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ScriptType = AlcoScriptGenerator.BusinessLogic.Entities.ScriptType;

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
            if (item is null)
                btn.QScriptType(null, null);
            else
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
            if (item is null)
                btn.QScriptType(null, null);
            else
                btn.QScriptType(item.Title, item.Description);
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
            ScriptTypeCB.SelectedItem = scriptTypes[0];
        }

        /// <summary>
        /// Подготовит выпадающий список скриптов для агроспотов
        /// </summary>
        private void FillScriptsComboBox(ScriptType type)
        {
            IComboBoxData data = new ComboBoxData();
            List<Script> scripts = new();

            if (type is ScriptType.Agrospot)
                scripts = data.GetAgrospotScripts();

            else if (type is ScriptType.ASKP)
                scripts = data.GetAskpScripts();

            else if (type is ScriptType.Zavod)
                scripts = data.GetZavodScripts();

            else return;

            ScriptNameCB.ItemsSource = scripts;
        }

        /// <summary>
        /// Меняем список скриптов при выборе другого типа скриптов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScriptTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (TypeScriptListMember)ScriptTypeCB.SelectedItem;
            FillScriptsComboBox(item.TypeOfScript);
        }
    }
}
