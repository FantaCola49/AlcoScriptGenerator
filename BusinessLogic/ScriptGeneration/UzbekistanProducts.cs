using AlcoScriptGenerator.BusinessLogic.UriHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlcoScriptGenerator.BusinessLogic.ScriptGeneration
{
    public class UzbekistanProducts : InputDataParser
    {
        #region Fields

        string Headers = "Title\tProductCode\tProductCodeType\tVolumeBoxing\tSpecificationProof" +
            //"\tAgencyId" +
            "\tMinTemperature\tMaxTemperature\tMinProof\tMaxProof\tStartGravityInPlato\tCustomDensity20\tFileUrl\tIsDeleted\tEnableProofAsInSpecification\tEnableFitToSpecificProof\tFitToSpecificProofFluctuationPercent\tEnableCustomDensity20\tPackageType\tDensityMultiplayer\tRawVolumeMultiplayer\tMassMultiplayer" +
            "\tSixDigitCode" +
            "\tThreeDigitCode";
        bool continueRead = false;
        int linesRead = 0;
        string RESULT = string.Empty;
        #endregion
        
        private string GetMinMaxTemperatureString(double t)
        {
            string start = "\t-50.00\t60.00";
            string mid = $"\t{(t-0.1).ChangeSymbolInDouble()}\t{(t+0.1).ChangeSymbolInDouble()}";
            string rest = "\tNULL\t0.806238\t" + @"'C:\Products\10_253856789_15.txt'" + "\t0\t0\t0\t0.00100000\t0\t0\t1.00000000\t1.00000000\t1.00000000\t200\t200";
            return start+mid+rest;
        }
        public string AddUzbekistanProduct(string inputData)
        {
            var primary = ParsePrimaryDataz(inputData);
            if (primary == null)
            {
                return RESULT;
            }
            var dict = (GetFilteredDictionary(primary));
            var res1 = GenerateFinalScript(dict);

            if (continueRead) { RESULT += $"{res1};\n\n {AddUzbekistanProduct(inputData)}"; }
            return RESULT;
        }

        private protected  PrimaryData ParsePrimaryDataz(string inputData)
        {
            using (var reader = new StringReader(inputData))
            {
                string valuesString = string.Empty;
                if (linesRead.Equals(0))
                    valuesString = reader.ReadLine();
                else
                {
                    for (var i = 0; i < linesRead; i++)
                    {
                        reader.ReadLine();
                    }

                    // Read the rest
                    valuesString = reader.ReadLine();
                }
                if (String.IsNullOrEmpty(valuesString))
                { continueRead = false; return null; }
                else 
                    //(!valuesString.Equals(null))
                        { continueRead = true; linesRead++; }
                /*
                if (valuesString.Contains("\t40"))  valuesString += AdditionalValues40;
                else if (valuesString.Contains("\t35")) valuesString += AdditionalValues35;
                */
                var list = (valuesString.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList());
                valuesString += GetMinMaxTemperatureString(list[list.Count - 2].DoubleParseAdvanced());

                List<string> headersList = Headers.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                List<string> valuesList = valuesString.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                HeadersValuesDTO dto = new HeadersValuesDTO
                {
                    Headers = headersList,
                    Values = valuesList,
                };

                return new PrimaryData
                {
                    HeadersValuesDTO = dto,
                    PprimaryResults = GetPrimaryResults(dto)
                };
            }
        }

        /// <summary>
        /// Метод для выборки нужных значений из предварительных
        /// </summary>
        /// <param name="headers">Заголовки из первой строки</param>
        /// <param name="primaryResults">Предварительные результаты</param>
        /// <returns></returns>
        private IEnumerable<KeyValuePair<string, string>> GetFilteredDictionary(PrimaryData results)
        {
            List<string> headers = results.HeadersValuesDTO.Headers;
            IEnumerable<KeyValuePair<string, string>> primaryResults = results.PprimaryResults;
            Dictionary<string, string> filteredResults = new();

            foreach (var item in results.HeadersValuesDTO.Headers)
            {
                // Ищем совпадения ключевых слов в созданном словаре
                var key = headers.Find(s => s.Contains(item));
                // Ищем по признаку, ежжи
                var searchedItem = primaryResults.Where(z => z.Key.Equals(key)).First();

                // Если что-то не нашли (защита от нулл значений)
                if (searchedItem.Value.Equals(null)) continue;
                filteredResults.Add(key, searchedItem.Value);
            }
            return filteredResults;
        }

        /// <summary>
        /// Создаст годный SQL-скрипт из представленных аргументов
        /// </summary>
        /// <param name="filteredResults"></param>
        /// <returns></returns>
        private string GenerateFinalScript(IEnumerable<KeyValuePair<string, string>> filteredResults)
        {
            string result =
                    "INSERT INTO [AlcoSpotMainServiceDB].[dbo].[Product] \r\n  (";
            foreach (var item in filteredResults)
            {
                result += $"{item.Key},\n";
            }
            // Удаляем запятые на последнего элемента
            result = result.Remove(result.Length - 2, 2);
            result += ")\n" +
                "values\n(";

            // Заполняем скрипт дополнениями
            foreach (var item in filteredResults)
            {
                if (item.Equals(filteredResults.First()))
                {
                    result += @$"'{item.Value}', --#{item.Key}#--" + "\n";
                }
                //else if (item.Equals(filteredResults.ElementAt(5))
                //    //||
                //    //item.Equals(filteredResults.ElementAt(12))
                //)
                //{
                //    result += @$"'{item.Value}', --#{item.Key}#--" + "\n";
                //}
                else if (item.Equals(filteredResults.Last()))
                {

                    result += @$"{item.Value} --#{item.Key}#--" + "\n";
                }
                else
                {
                    result += @$"{item.Value}, --#{item.Key}#--" + "\n";
                }

            }
            result.Remove(result.Length-1, 1);
            result += ")";
            return result;
        }
    }
}
