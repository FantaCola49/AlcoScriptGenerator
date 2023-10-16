namespace AlcoScriptGenerator.BusinessLogic.Interfaces
{
    /// <summary>
    /// Логика кнопок
    /// </summary>
    public interface IButtonsLogic
    {
        /// <summary>
        /// Пояснение для скрипта
        /// </summary>
        /// <param name="scriptNum"></param>
        void QScript(string Title, string Desc);

        /// <summary>
        /// Пояснение для типа скрипта
        /// </summary>
        /// <param name="scriptNum"></param>
        void QScriptType();

        /// <summary>
        /// Кнопка Обзор...
        /// </summary>
        /// <remarks>Позволяет выбрать путь к папке с файлами</remarks>
        string BrowseFolderPath();

        /// <summary>
        /// Экспортировать скрипт в выбранную папку
        /// </summary>
        /// <param name="FilePath"></param>
        bool ExportScript(string FilePath);
    }
}
