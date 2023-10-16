using AlcoScriptGenerator.BusinessLogic.Entities;
using AlcoScriptGenerator.BusinessLogic.Interfaces;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Windows;

namespace AlcoScriptGenerator.BusinessLogic.WindowElements
{
    public class ButtonsLogic : IButtonsLogic
    {
        #region Interface Implementation

        /// <summary>
        /// Пояснение для скрипта
        /// </summary>
        /// <param name="scriptNum"></param>
        public void QScript(string title, string desc)
        {
            var item = CheckInput(title, desc);
            title = item.Item1 as string;
            desc = item.Item2 as string;
            // объяснение для каждого скрипта
            MessageBox.Show($"{title} - {desc}",
                "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Пояснение для типа скрипта
        /// </summary>
        /// <param name="scriptNum"></param>
        public void QScriptType()
        {
            // объяснение для каждого скрипта
            MessageBox.Show($"{ScriptRelationDto.SelectedScriptRelation.Title} - {ScriptRelationDto.SelectedScriptRelation.Description}",
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

        /// <summary>
        /// Экспортировать скрипт в выбранную папку
        /// </summary>
        /// <param name="filePath"></param>
        public bool ExportScript(string filePath)
        {
            if (ScriptDto.SelectedScript == null ||
                string.IsNullOrEmpty(ScriptDto.SelectedScript.Title) ||
                string.IsNullOrEmpty(ScriptDto.ScriptBody))
                return false;

            string fileName = @$"{ScriptDto.SelectedScript.Title}.sql";
            string path = Path.Combine(filePath, fileName);

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine($@"{ScriptDto.ScriptBody}");
            }
            return true;
        }

        #endregion

        /// <summary>
        /// Кортеж для проверки значений
        /// </summary>
        /// <param name="title"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        private (string, string) CheckInput(string title, string desc)
        {
            if (string.IsNullOrEmpty(title) ||
                string.IsNullOrEmpty(desc))
            {
                title = "Выберите скрипт";
                desc = string.Empty;
                return (title, desc);
            }
            return (title, desc);
        }

        /// <summary>
        /// Проверка уже существующего файла
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private bool IsAlreadyExistingFile(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
