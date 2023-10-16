using AlcoScriptGenerator.BusinessLogic.ScriptGeneration;
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
            FillScriptsComboBox(ScriptType.Agrospot);
        }

        /// <summary>
        /// Пояснение типа скрипта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QScriptType_Click(object sender, RoutedEventArgs e)
        {
            _btn.QScriptType();
        }

        /// <summary>
        /// Пояснение скрипта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QScriptName_Click(object sender, RoutedEventArgs e)
        {
            if (ScriptDto.SelectedScript is null)
                _btn.QScript(null, null);
            else
                _btn.QScript(ScriptDto.SelectedScript.Title, ScriptDto.SelectedScript.Description);
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
            ScriptRelation item = (ScriptRelation)ScriptTypeCB.SelectedItem;
            ScriptRelationDto.SelectedScriptRelation = item;
            FillScriptsComboBox(item.ScriptField);
        }

        /// <summary>
        /// Подготовит выпадающий список скриптов для агроспотов
        /// </summary>
        private void FillScriptsComboBox(ScriptType type)
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
            ScriptDto.SelectedScript = (Script)ScriptNameCB.SelectedItem;
            if (ScriptDto.SelectedScript is null)
                return;
            GenerateScript();
        }

        /// <summary>
        /// Отобразит скрипт в текстовом окне приложения
        /// </summary>
        private void DisplayScript()
        {
            GeneratedScriptTB.Text = ScriptDto.ScriptBody;
        }

        /// <summary>
        /// Генерируем скрипт в зависимости от наличия аргументов
        /// </summary>
        private void GenerateScript()
        {
            if (ScriptDto.SelectedScript.СontainsArguments.Equals(false) ||
                ScriptDto.SelectedScript is null)
            {
                _gen.GetSimpleScript(ScriptDto.SelectedScript);
                // Для простого скрипта Frame не отображаем
                var uri = _nav.UriToScriptPage(new Script() { TypeOfScript = ScriptType.Other });
                ArgumentsFrame.Navigate(uri);
            }

            else
            {
                // Чтобы старый скрипт не отображался в окне
                ScriptDto.SetNewScript(string.Empty);
                var uri = _nav.UriToScriptPage(ScriptDto.SelectedScript);
                ArgumentsFrame.Navigate(uri);
            }

            DisplayScript();
        }

        /// <summary>
        /// Изменили путь до экспорта файла SQL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilePathTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(FilePathTB.Text))
                ExportBtn.IsEnabled = false;
            else
                ExportBtn.IsEnabled = true;
        }

        /// <summary>
        /// Кнопка экспорта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(FilePathTB.Text))
            {
                if (_btn.ExportScript(FilePathTB.Text))
                    MessageBox.Show("Скрипт успешно скопирован в выбранную папку!\n" +
                                    "Ваш скрипт добавлен к старому, если файл уже существовал",
                                    "УСПЕХ", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Не удалось сохранить скрипт!\n" +
                                    "Выберите и сгенерируйте тело скрипта, прежде чем сохранять!", "ВНИМАНИЕ!",
                                    MessageBoxButton.OK, MessageBoxImage.Hand);

            }
        }
    }
}
