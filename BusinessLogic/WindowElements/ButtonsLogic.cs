using AlcoScriptGenerator.BusinessLogic.Interfaces;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AlcoScriptGenerator.BusinessLogic.WindowElements
{
    public class ButtonsLogic : IButtonsLogic
    {
        #region Interface Implementation
        /// <summary>
        /// Пояснение к Типу Скрипта
        /// </summary>
        public void QScriptType(string Title, string Desc)
        {
            MessageBox.Show($"{Title} - {Desc}",
                "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Пояснение для скрипта
        /// </summary>
        /// <param name="scriptNum"></param>
        public void QScript(string Title, string Desc)
        {
            // объяснение для каждого скрипта
            MessageBox.Show($"{Title} - {Desc}",
                "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Кнопка Обзор...
        /// </summary>
        /// <returns>Путь к папке сохранения</returns>
        /// <remarks>Позволяет выбрать путь к папке с файлами</remarks>
        public string BrowseFolderPath()
        {
            string pathDirectory;
            using (CommonOpenFileDialog dialogDirectory = new CommonOpenFileDialog { IsFolderPicker = true })
            {
                pathDirectory = dialogDirectory.ShowDialog() == CommonFileDialogResult.Ok ?
                                dialogDirectory.FileName : null;
                return pathDirectory;
            }
        }
        #endregion
}
