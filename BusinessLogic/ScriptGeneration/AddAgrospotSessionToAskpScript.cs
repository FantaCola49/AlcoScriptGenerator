using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlcoScriptGenerator.BusinessLogic.ScriptGeneration
{
    /*
     * Сверхмассивный кусок кода запрещённый на Земле и Луне
     * Обернул в отдельный класс чтобы изолировать
     */
    /// <summary>
    /// Генерирует SQL скрипт для добавления сесси АСКП на основе сессии Агроспота
    /// </summary>
    public class AddAgrospotSessionToAskpScript
    {
        /// <summary>
        /// Номер ТС
        /// </summary>
        private string vehicleNumber { get; set; }

        /// <summary>
        /// Список ключевых слов-названий столбцов, необходимых для скрипта
        /// </summary>
        private readonly List<string> keyWords = new List<string>()
        {
            "Mode",
            "LineId",
            "ProductId",
            "StartTimestamp",
            "FinishTimestamp",
            "AqueousAlcoholicSolutionOnStart",
            "AqueousAlcoholicSolutionOnEnd",
            "AnhydrousAlcoholicSolutionOnStart",
            "AnhydrousAlcoholicSolutionOnEnd",
            "ProofAverage",
            "TemperatureAverage",
            "DensityAverage",
            //"TotalizerOnStart",
            "MassTotalizerOnStart",
            //"TotalizerOnFinish",
            "MassTotalizerOnFinish",
            //"ControllerId",
        };

        /// <summary>
        /// Список слов-столбцов, подлежащих замене
        /// </summary>
        private readonly List<string> wordsToChange = new()
        {
            "LineId",
            "MassTotalizerOnStart",
            "MassTotalizerOnFinish",
        };
        /// <summary>
        /// Список слов-столбцов на замену
        /// </summary>
        private readonly List<string> rightWords = new List<string>()
        {
            "LineType",
            "TotalizerOnStart",
            "TotalizerOnFinish",
        };

        /// <summary>
        /// Список "дополнительных" аргументов скрипта
        /// </summary>
        private readonly List<string> additionalWords = new List<string>()
        {
            "ProofCalculationType",
            "ProofAverageOverVolumesRatio",
            "ServerAcceptingUtcTime",
            "ControllerId",
        };

        /// <summary>
        /// Формат столбцов, нуждающихся в специальном формате записи даты
        /// </summary>
        private readonly List<string> specialFormat = new List<string>()
        {
            "StartTimestamp",
            "FinishTimestamp",
        };

        /// <summary>
        /// ДТО для распарсенных заголовков и значений
        /// </summary>
        private class HeadersValuesDTO
        {
            /// <summary>
            /// Заголовки
            /// </summary>
            public List<string> Headers { get; init; }

            /// <summary>
            /// Значения
            /// </summary>
            public List<string> Values { get; init; }
        }

        /// <summary>
        /// ДТО для передачи данных о кор-элементе скрипта
        /// </summary>
        private class ScriptCoreElement
        {
            /// <summary>
            /// Ключ (Название столбца)
            /// </summary>
            public string Key { get; set; }

            /// <summary>
            /// Значение
            /// </summary>
            public string Value { get; set; }
        }

        /// <summary>
        /// ДТО распознанных данных сессии
        /// </summary>
        private class PrimaryData
        {
            public HeadersValuesDTO HeadersValuesDTO { get; set; }
            public IEnumerable<KeyValuePair<string, string>> PprimaryResults { get; set; }
        }

        /// <summary>
        /// Генерирует SQL скрипт для добавления сесси АСКП на основе сессии 
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public string AddAgrospotSessionTest(string inputData, string vehicleNum)
        {
            this.vehicleNumber = vehicleNum;
            return GenerateFinalScript(GetFilteredDictionary(ParsePrimaryData(inputData)));
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

            foreach (var item in keyWords)
            {
                // Ищем совпадения ключевых слов в созданном словаре
                var key = headers.Find(s => s.Contains(item));
                // Ищем по признаку, ежжи
                var searchedItem = primaryResults.Where(z => z.Key.Equals(key));

                // Если что-то не нашли (защита от нулл значений)
                if (!searchedItem.Any()) continue;

                ScriptCoreElement coreElement = GetCoreElementOfScript(searchedItem);
                filteredResults.Add(coreElement.Key, coreElement.Value);
            }

            // Добавляем дополнительные скрипты
            foreach (var addition in GetAdditionalPartOfScript())
            {
                filteredResults.Add(addition.Key, addition.Value);
            }
            return filteredResults;

        }

        /// <summary>
        /// Получить "основной" компонент скрипта
        /// </summary>
        /// <param name="searchedItem"></param>
        /// <returns></returns>
        private ScriptCoreElement GetCoreElementOfScript(IEnumerable<KeyValuePair<string, string>> searchedItem)
        {
            var searchedItemKey = searchedItem.First().Key;
            //Смотрим все слова для замены (одни и теже поля агроспота и АСКП называются по разному)
            //Меняем, если нашли
            for (int i = 0; i < wordsToChange.Count; i++)
            {
                if (searchedItemKey.Contains(wordsToChange[i]))
                {
                    var trueKey = rightWords[i];
                    IEnumerable<KeyValuePair<string, string>> dictionaryItem = new List<KeyValuePair<string, string>>();
                    return new ScriptCoreElement()
                    {
                        Key = trueKey,
                        Value = searchedItem.First().Value,
                    };
                }

            }
            // SQL любит читать даты в кавычках
            // Поэтому меняем значения для элементов с ключём-названием даты
            for (int i = 0; i < specialFormat.Count; i++)
            {
                if (searchedItemKey.Contains(specialFormat[i]))
                {
                    return new ScriptCoreElement()
                    {
                        Key = searchedItemKey,
                        Value = @$"'{searchedItem.First().Value}'"
                    };
                }
            }

            // В противном случае просто возвращаем элемент
            return new ScriptCoreElement()
            {
                Key = searchedItem.First().Key,
                Value = searchedItem.First().Value
            };
        }

        /// <summary>
        /// Значения и столбцы добавления, которые невозможно получить из отчёта Агроспота
        /// </summary>
        /// <returns></returns>
        private IEnumerable<KeyValuePair<string, string>> GetAdditionalPartOfScript()
        {
            Dictionary<string, string> redundantWords = new();
            // Добавляем дополнительные фильтры
            foreach (var item in additionalWords)
            {
                // Возвращаем подзапрос для ControllerId
                if (item.Equals(additionalWords.Last()))
                {
                    redundantWords.Add(additionalWords.Last(),
                        $"(SELECT Id FROM AskpDB.dbo.Controller WHERE VehicleIdentificationNumber LIKE '%{vehicleNumber}%' AND IsDeleted = 0)");
                    break;
                }
                redundantWords.Add(item, "0");
            }

            return redundantWords;
        }

        /// <summary>
        /// Создаст годный SQL-скрипт из представленных аргументов
        /// </summary>
        /// <param name="filteredResults"></param>
        /// <returns></returns>
        private string GenerateFinalScript(IEnumerable<KeyValuePair<string, string>> filteredResults)
        {
            string result =
                    "INSERT INTO [AskpDB].[dbo].[Session] \r\n  (";
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
                if (item.Equals(filteredResults.Last()))
                {
                    result += @$"{item.Value}) --#{item.Key}#--";
                }
                else
                {
                    result += @$"{item.Value}, --#{item.Key}#--" + "\n";
                }

            }
            return result;
        }

        /// <summary>
        /// Распарсит строку заголовков и строку значений
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private PrimaryData ParsePrimaryData(string inputData)
        {
            using (var reader = new StringReader(inputData))
            {
                string headersString = reader.ReadLine();
                string valuesString = reader.ReadLine();

                List<string> headersList = headersString.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList();
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
        /// Получить предварительный (неотфильтрованный) список всех элементов и значений предоставленной сессии
        /// </summary>
        /// <param name="headersAndValues"></param>
        /// <returns></returns>
        private IEnumerable<KeyValuePair<string, string>> GetPrimaryResults(HeadersValuesDTO headersAndValues)
        {
            List<string> headers = headersAndValues.Headers;
            List<string> values = headersAndValues.Values;

            //Сформировали список из значений
            Dictionary<string, string> primaryResults = new Dictionary<string, string>();

            for (int i = 0; i < headers.Count; i++)
            {
                primaryResults.Add(headers[i], values[i]);
            }

            return primaryResults;
        }
    }
}
