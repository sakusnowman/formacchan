using System;
using System.Collections.Generic;
using System.Text;
using Formacchan.Extensions;
using FormacchanLibrary.Models;
using Formacchan.Repositories;
using Formacchan.Configuration;

namespace Formacchan.Services
{
    public class FormatKeyValuePairsService : IFormatKeyValuePairsService
    {
        IFormatKeyValuePairsRepository repository;
        private readonly IConfigurationSettings configurationSettings;

        public FormatKeyValuePairsService(IFormatKeyValuePairsRepository repository,
            IConfigurationSettings configurationSettings)
        {
            this.repository = repository;
            this.configurationSettings = configurationSettings;
        }

        public string ReplaceKeyToValue(string sentence, IEnumerable<IFormatKeyValuePair> formatKeyValuePairs)
        {
            var values = new List<string>();
            var result = sentence;
            foreach (var keyValuePair in formatKeyValuePairs)
            {
                result = result.Replace(keyValuePair.Key, keyValuePair.Value);
            }
            return result;
        }

        public IEnumerable<IFormatKeyValuePair> GetKeyValuePairs()
        {
            return repository.GetFormatKeyValuePairs();
        }

        public string FormatSenetence(string sentence)
        {
            return sentence.FormatInnerSenetences(
                configurationSettings.FormatStartMark,
                configurationSettings.FormatEndMark,
                configurationSettings.FormatSplitMark);
        }

        public string CalculateSentence(string sentence)
        {
            return sentence.CalculateInnerSentences(
                configurationSettings.CalculationStartMark,
                configurationSettings.CalculationEndMark);
        }
    }
}
