using AlcoScriptGenerator.BusinessLogic.Interfaces;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;

namespace AlcoScriptGenerator.BusinessLogic.WindowElements
{
    public class ButtonsLogic : IButtonsLogic
    {
        #region Interface Implementation
        /// <summary>
        /// Пояснение к Типу Скрипта
        /// </summary>
        public void QScriptType(string title, string desc)
        {
            var item = CheckInput(title, desc);
            title = item.Item1 as string;
            desc = item.Item2 as string;
            MessageBox.Show($"{title} - {desc}",
                "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

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

        //TODO:Генерировать скрипт

        //TODO:Экспорт скрипта btn
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
    }
}
