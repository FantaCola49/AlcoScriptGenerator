﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlcoScriptGenerator.BusinessLogic.ScriptGeneration
{
    /*
     * Сверхмассивный кусок кода запрещённый на Земле и Луне
     * Обернул в отдельный класс чтобы изолировать логику
     */
    /// <summary>
    /// Генерирует SQL скрипт для добавления сесси АСКП на основе сессии Агроспота
    /// </summary>
    public class AddAgrospotSessionToAskpScript : InputDataParser
    {
        #region Fields
        /// <summary>
        /// Номер ТС
        /// </summary>
        private string _vehicleNumber { get; set; }

        /// <summary>
        /// Список ключевых слов-названий столбцов, необходимых для скрипта
        /// </summary>
        private readonly List<string> _keyWords = new List<string>()
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
        private readonly List<string> _wordsToChange = new()
        {
            "LineId",
            "MassTotalizerOnStart",
            "MassTotalizerOnFinish",
        };
        /// <summary>
        /// Список слов-столбцов на замену
        /// </summary>
        private readonly List<string> _rightWords = new List<string>()
        {
            "LineType",
            "TotalizerOnStart",
            "TotalizerOnFinish",
        };

        /// <summary>
        /// Список "дополнительных" аргументов скрипта
        /// </summary>
        private readonly List<string> _additionalWords = new List<string>()
        {
            "ProofCalculationType",
            "ProofAverageOverVolumesRatio",
            "ServerAcceptingUtcTime",
            "ExternalId",
            "ControllerId",
        };

        /// <summary>
        /// Формат столбцов, нуждающихся в специальном формате записи даты
        /// </summary>
        private readonly List<string> _specialFormat = new List<string>()
        {
            "StartTimestamp",
            "FinishTimestamp",
        };

        #endregion

        /// <summary>
        /// Генерирует SQL скрипт для добавления сесси АСКП на основе сессии 
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public string AddAgrospotSession(string inputData, string vehicleNum)
        {
            this._vehicleNumber = vehicleNum;
            return GenerateFinalScript(GetFilteredDictionary(ParsePrimaryData(inputData)));
        }

        #region Private methods

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

            foreach (var item in _keyWords)
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
            for (int i = 0; i < _wordsToChange.Count; i++)
            {
                if (searchedItemKey.Contains(_wordsToChange[i]))
                {
                    var trueKey = _rightWords[i];
                    //IEnumerable<KeyValuePair<string, string>> dictionaryItem = new List<KeyValuePair<string, string>>();
                    return new ScriptCoreElement()
                    {
                        Key = trueKey,
                        Value = searchedItem.First().Value,
                    };
                }

            }
            // SQL любит читать даты в кавычках
            // Поэтому меняем значения для элементов с ключём-названием даты
            for (int i = 0; i < _specialFormat.Count; i++)
            {
                if (searchedItemKey.Contains(_specialFormat[i]))
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
            foreach (var item in _additionalWords)
            {
                // Чтобы не было пустого времени принятия сервером
                if (item.Equals(_additionalWords[2]))
                {
                    redundantWords.Add(_additionalWords[2],
                        $"\'{DateTime.UtcNow}\'");
                    continue;
                }
                // Чтобы не было нуллевых ReplyId!
                if (item.Equals(_additionalWords[3]))
                {
                    redundantWords.Add(_additionalWords[3],
                        "NEWID()");
                    continue;
                }
                // Возвращаем подзапрос для ControllerId
                if (item.Equals(_additionalWords.Last()))
                {
                    redundantWords.Add(_additionalWords.Last(),
                        $"(SELECT Id FROM AskpDB.dbo.Controller WHERE VehicleIdentificationNumber LIKE '%{_vehicleNumber}%' AND IsDeleted = 0)");
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
        #endregion
    }
}
