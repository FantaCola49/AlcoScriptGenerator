using AlcoScriptGenerator.BusinessLogic.Entities;
using System;

namespace AlcoScriptGenerator.BusinessLogic.UriHelper
{
    /// <summary>
    /// Пути до страниц
    /// </summary>
    public class UriToScriptPages
    {
        private Uri PagePathFactory(string pageName, ScriptType type)
        {
            string folder = type switch
            {
                ScriptType.Agrospot => "Agrospot",
                ScriptType.Zavod    => "Zavod",
                ScriptType.ASKP     => "ASKP",
            };

            return new Uri($"Pages/{folder}/{pageName}", UriKind.Relative);
        }

        private Uri PagePathFactory(string pageName)
        {
            return new Uri($"Pages/{pageName}", UriKind.Relative);
        }

        #region Agrospot
        private protected Uri ToVehicleNumberMinMaxDatePage => PagePathFactory("VehicleNumberMinMaxDate.xaml", ScriptType.Agrospot);
        private protected Uri ToDeleteGpsNavigationPage => PagePathFactory("DeleteGpsNavigation.xaml", ScriptType.Agrospot);

        #endregion


        #region Zavod
        private protected Uri ToMinMaxDatePage => PagePathFactory("MinMaxDatePage.xaml", ScriptType.Zavod);

        #endregion


        #region ASKP
        private protected Uri ToAgrospotSessionAdditionPage => PagePathFactory("AgrospotSessionAddition.xaml", ScriptType.ASKP);

        private protected Uri ToAskpVehicleNumberPage => PagePathFactory("VehicleNumberPage.xaml", ScriptType.ASKP);

        private protected Uri ToAskpOrganizationNamePage => PagePathFactory("OrganizationNamePage.xaml", ScriptType.ASKP);
        #endregion

        #region Other
        private protected Uri ToBlancPage => PagePathFactory("BlancPage.xaml");
        #endregion
    }
}
