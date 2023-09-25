using AlcoScriptGenerator.BusinessLogic;
using AlcoScriptGenerator.BusinessLogic.Entities;
using AlcoScriptGenerator.BusinessLogic.Interfaces;
using AlcoScriptGenerator.BusinessLogic.WindowElements;
using System.Windows;
using System.Windows.Controls;

namespace AlcoScriptGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        /// <summary>
        /// Логика Кнопок
        /// </summary>
        private IButtonsLogic _btn;
        /// <summary>
        /// Данные для выпадающих списков
        /// </summary>
        private IComboBoxData _data;
        /// <summary>
        /// Навигация поля аргументов
        /// </summary>
        private INavigation _nav;

        /// <summary>
        /// Генератор скриптов без аргументов
        /// </summary>
        private ISimpleScriptsGenerator _gen;

        /// <summary>
        /// Выбранный скрипт в ниспадающем окне
        /// </summary>
        private Script _selectedScript { get; set; }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            _btn = new ButtonsLogic();
            _data = new ComboBoxData();
            _nav = new Navigation();
            _gen = new SimpleScriptsGenerator();
            ScriptDto.ScriptChanged += DisplayScript;
            // Заполняем ниспадающий список типов скриптов
            FillTypeScriptsComboBox();
            // Заполняем ниспадающий список скриптов
            FillScriptsComboBox(ScriptField.Agrospot_Related);
        }

        /// <summary>
        /// Пояснение типа скрипта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QScriptType_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedScript is null)
                _btn.QScriptType(null, null);
            else
                _btn.QScriptType(_selectedScript.Title, _selectedScript.Description);
        }

        /// <summary>
        /// Пояснение скрипта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QScriptName_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedScript is null)
                _btn.QScriptType(null, null);
            else
                _btn.QScriptType(_selectedScript.Title, _selectedScript.Description);
        }

        /// <summary>
        /// Обзор...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseFolderPath_Click(object sender, RoutedEventArgs e)
        {
            FilePathTB.Text = _btn.BrowseFolderPath();
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
        /// Меняем список скриптов при выборе другого типа скриптов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScriptTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ScriptRelation)ScriptTypeCB.SelectedItem;
            FillScriptsComboBox(item.ScriptField);
        }

        /// <summary>
        /// Подготовит выпадающий список скриптов для агроспотов
        /// </summary>
        private void FillScriptsComboBox(ScriptField type)
        {
            var scripts = _data.GetScriptsByType(type);
            ScriptNameCB.ItemsSource = scripts;
        }

        /// <summary>
        /// Отображаем в зависимости от выбранного элемента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScriptNameCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedScript = (Script)ScriptNameCB.SelectedItem;
            if (_selectedScript is null)
                return;
            GenerateScript();
        }

        /// <summary>
        /// Отобразит скрипт в текстовом окне приложения
        /// </summary>
        private void DisplayScript()
        {
            GeneratedScriptTB.Text = ScriptDto.Script;
        }

        /// <summary>
        /// Генерируем скрипт в зависимости от наличия аргументов
        /// </summary>
        private void GenerateScript()
        {
            if (_selectedScript.СontainsArguments.Equals(false) ||
                _selectedScript is null)
            {
                _gen.GetSimpleScript(_selectedScript);
                //для простого скрипта Frame не нужен
                var uri = _nav.UriToScriptPage(new Script() { TypeOfScript = ScriptType.Other });
                ArgumentsFrame.Navigate(uri);
            }

            else
            {
                // Чтобы старый скрипт не отображался в окне
                ScriptDto.SetNewScript(string.Empty);
                var uri = _nav.UriToScriptPage(_selectedScript);
                ArgumentsFrame.Navigate(uri);
            }

            DisplayScript();
        }
    }
}
