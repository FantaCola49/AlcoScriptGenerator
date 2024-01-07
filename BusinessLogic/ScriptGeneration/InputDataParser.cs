using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoScriptGenerator.BusinessLogic.ScriptGeneration
{
    public class InputDataParser
    {
        /// <summary>
        /// ДТО для распарсенных заголовков и значений
        /// </summary>
        private protected class HeadersValuesDTO
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
        private protected class ScriptCoreElement
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
        private protected class PrimaryData
        {
            public HeadersValuesDTO HeadersValuesDTO { get; set; }
            public IEnumerable<KeyValuePair<string, string>> PprimaryResults { get; set; }
        }

        /// <summary>
        /// Распарсит строку заголовков и строку значений
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private protected virtual PrimaryData ParsePrimaryData(string inputData)
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
        private protected IEnumerable<KeyValuePair<string, string>> GetPrimaryResults(HeadersValuesDTO headersAndValues)
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
