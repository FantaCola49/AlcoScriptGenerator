using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AlcoScriptGenerator.BusinessLogic.UriHelper
{
    public static class StringExtension
    {
        /// <summary>
        /// Запарсит дробное число из любых строк
        /// </summary>
        /// <remarks>95.21 теперь парсится!</remarks>
        /// <param name="strToParse">Строка</param>
        /// <param name="decimalSymbol"></param>
        /// <returns></returns>
        public static float DoubleParseAdvanced(this string strToParse, char decimalSymbol = ',')
        {
            string tmp;
            try
            {
                tmp = Regex.Match(strToParse, @"([-]?[0-9]+)([\s])?([0-9]+)?[." + decimalSymbol + "]?([0-9 ]+)?([0-9]+)?").Value;


                if (tmp.Length > 0 && strToParse.Contains(tmp))
                {
                    var currDecSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

                    tmp = tmp.Replace(".", currDecSeparator).Replace(decimalSymbol.ToString(), currDecSeparator);

                    return float.Parse(tmp);
                }
            }
            catch (Exception ex)
            {
                tmp = "0";
                return float.Parse(tmp);
            }
            return 0;
        }

        public static string ChangeSymbolInDouble(this double Double)
        {
            var reminder = (Math.Round(Double - Math.Floor(Double),2) * 100);

            int Floor = (int)Math.Floor(Double);
            string res = $"{Floor}.{reminder}";
            return res ;
        }
    }
}
