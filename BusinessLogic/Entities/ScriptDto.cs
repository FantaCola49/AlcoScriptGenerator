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
        public static string Script { get; private set; }

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
            Script = newScript;
            ScriptChanged?.Invoke();
            return;
        }
    }
}
