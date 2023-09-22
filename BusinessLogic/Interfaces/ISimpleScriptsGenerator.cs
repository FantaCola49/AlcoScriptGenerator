using AlcoScriptGenerator.BusinessLogic.Entities;

namespace AlcoScriptGenerator.BusinessLogic.Interfaces
{
    /// <summary>
    /// Генератор скриптов без аргументов
    /// </summary>
    interface ISimpleScriptsGenerator
    {
        /// <summary>
        /// Сгенерировать простой скрипт (без аргументов)
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public void GetSimpleScript(Script script);
    }
}
