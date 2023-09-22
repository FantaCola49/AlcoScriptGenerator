using System;

namespace AlcoScriptGenerator.BusinessLogic.UriHelper
{
    /// <summary>
    /// Пути до страниц
    /// </summary>
    public class UriToScriptPages
    {
        private Uri PagePathFabric(string pageName)
        {
            return new Uri($"Pages/{pageName}", UriKind.Relative);
        }

        #region Agrospot

        private protected Uri ToGpsNavigationSearchPage() => PagePathFabric("GpsNavigationSearch.xaml");



        #endregion


        #region Zavod


        #endregion


        #region ASKP

        #endregion

        #region Other
        private protected Uri ToBlancPage() => PagePathFabric("BlancPage.xaml");
        #endregion
    }
}
