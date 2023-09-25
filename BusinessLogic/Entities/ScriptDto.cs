namespace AlcoScriptGenerator.BusinessLogic.Entities
{
    /// <summary>
    /// DTO Скриптов
    /// </summary>
    public static class ScriptDto
    {
        /// <summary>
        /// Непосредственно скрипт
        /// </summary>
        public static string ScriptBody { get; private set; }

        /// <summary>
        /// Выбранный скрипт
        /// </summary>
        public static Script SelectedScript { get; set; }

        /// <summary>
        /// Делегат обработки событий со скриптом
        /// </summary>
        public delegate void ScriptHandler();

        /// <summary>
        /// Проставлен новый скрипт
        /// </summary>
        public static event ScriptHandler? ScriptChanged;

        /// <summary>
        /// Проставить новый скрипт
        /// </summary>
        /// <param name="newScript"></param>
        public static void SetNewScript(string newScript)
        {
            ScriptBody = newScript;
            ScriptChanged?.Invoke();
            return;
        }
    }
}
