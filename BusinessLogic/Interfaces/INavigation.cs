using AlcoScriptGenerator.BusinessLogic.Entities;
using System;

namespace AlcoScriptGenerator.BusinessLogic.Interfaces
{
    public interface INavigation
    {
        /// <summary>
        /// Вернёт путь до страницы в зависимости от выбранного скрипта
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Uri UriToScriptPage(Script item);
    }
}
