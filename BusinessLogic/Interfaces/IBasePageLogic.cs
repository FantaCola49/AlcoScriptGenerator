using System.Windows;

namespace AlcoScriptGenerator.BusinessLogic.Interfaces
{
    public interface IBaseFrameLogic
    {
        /// <summary>
        /// Сгенерирует скрипт
        /// </summary>
        /// <returns></returns>
        public string GenerateScript();


        // Не уверен, что понадобиться
        //public string GetUriPath();
    }
}
