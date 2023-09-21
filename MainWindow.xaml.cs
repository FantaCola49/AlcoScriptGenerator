using AlcoScriptGenerator.BusinessLogic.Entities;
using AlcoScriptGenerator.BusinessLogic.Interfaces;
using AlcoScriptGenerator.BusinessLogic.WindowElements;
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
            FillCountriesComboBox();
        }

        /// <summary>
        /// ? Тип скрипта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QScriptType_Click(object sender, RoutedEventArgs e)
        {
            var item = (TypeScriptListMember)ScriptType.SelectedItem;
            btn.QScriptType(item.Title, item.Description);
        }

        private void QScriptName_Click(object sender, RoutedEventArgs e)
        {

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
        /// <remarks>По дефолту только Росиия</remarks>
        private void FillCountriesComboBox()
        {
            IComboBoxData data = new ComboBoxData();
            var scriptTypes = data.GetScriptTypes();
            ScriptType.ItemsSource = scriptTypes;
            ScriptType.DisplayMemberPath = "Title";
            ScriptType.SelectedItem = scriptTypes[0];
        }
    }
}
