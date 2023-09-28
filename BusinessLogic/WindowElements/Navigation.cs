using AlcoScriptGenerator.BusinessLogic.Entities;
using AlcoScriptGenerator.BusinessLogic.Interfaces;
using AlcoScriptGenerator.BusinessLogic.UriHelper;
using System;

namespace AlcoScriptGenerator.BusinessLogic.WindowElements
{
    public sealed class Navigation : UriToScriptPages, INavigation
    {
        /// <summary>
        /// Вернёт путь до страницы в зависимости от выбранного скрипта
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Uri UriToScriptPage(Script item)
        {
            // Далее item ТОЧНО не нулевой
            // Эсли у нас нет аргументов, то отображаем пустоту нахрен
            if (item == null ||
                item.СontainsArguments.Equals(false))
                return ToBlancPage();

            var type = item.TypeOfScript;

            Uri uri = type switch
            {
                ScriptType.Agrospot => ReturnAgrospotRelatedUri(item),
                ScriptType.Zavod    => ReturnZavodRelatedUri(item),
                ScriptType.ASKP     => ReturnAskpRelatedUri(item),
                _ => ToBlancPage(),
            };

            return uri;
        }

        /// <summary>
        /// Будет возвращать путь до страницы скриптов Агроспотов
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private Uri ReturnAgrospotRelatedUri(Script? item)
        {
            Uri uri = item.ScriptId switch
            {
                ScriptId.DeleteGpsNavigation => ToDeleteGpsNavigationPage(),
                ScriptId.GpsNavigationSearch => ToGpsNavigationSearchPage(),
                _ => ToBlancPage(),
            };
            return uri;
        }

        /// <summary>
        /// Будет возвращать путь до страницы скриптов заводов
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private Uri ReturnZavodRelatedUri(Script? item)
        {
            Uri uri = item.ScriptId switch
            {
                ScriptId.ZavodSessionsMinMaxDate     => ToMinMaxDatePage(),
                ScriptId.ZavodDiscreteFullRemastered => ToMinMaxDatePage(),
                ScriptId.ZavodDailies                => ToMinMaxDatePage(),
                _ => ToBlancPage(),
            };
            return uri;
        }

        /// <summary>
        /// Будет возвращать путь до страницы скриптов АСКП
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private Uri ReturnAskpRelatedUri(Script? item)
        {
            Uri uri = item.ScriptId switch
            {
                ScriptId.AskpAddAgrospotSession => ToAgrospotSessionAdditionPage(),
                _ => ToBlancPage(),
            };
            return uri;
        }
    }
}
