using AlcoScriptGenerator.BusinessLogic.Entities;
using System;

namespace AlcoScriptGenerator.BusinessLogic.UriHelper
{
    /// <summary>
    /// Пути до страниц
    /// </summary>
    public class UriToScriptPages
    {
        /// <summary>
        /// ПАПКА ZАВОДОВ
        /// </summary>
        private readonly ScriptType z = ScriptType.Zavod;
        /// <summary>
        /// ПАПКА АСКП
        /// </summary>
        private readonly ScriptType As = ScriptType.ASKP;
        
        private Uri PagePathFactory(string pageName, ScriptType type)
        {
            string folder = type switch
            {
                ScriptType.Agrospot => "Agrospot",
                ScriptType.Zavod => "Zavod",
                ScriptType.ASKP => "ASKP",
            };

            return new Uri($"Pages/{folder}/{pageName}", UriKind.Relative);
        }

        private Uri PagePathFactory(string pageName)
        {
            return new Uri($"Pages/{pageName}", UriKind.Relative);
        }

        #region Agrospot
        private protected Uri ToGpsNavigationSearchPage() => PagePathFactory("GpsNavigationSearch.xaml", ScriptType.Agrospot);
        private protected Uri ToDeleteGpsNavigation() => PagePathFactory("DeleteGpsNavigation.xaml", ScriptType.Agrospot);

        #endregion


        #region Zavod


        #endregion


        #region ASKP

        #endregion

        #region Other
        private protected Uri ToBlancPage() => PagePathFactory("BlancPage.xaml");
        #endregion
    }
}
