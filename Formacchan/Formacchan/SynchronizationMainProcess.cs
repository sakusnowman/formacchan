using FormacchanLibrary.Models;
using Formacchan.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Formacchan
{
    class SynchronizationMainProcess : IMainProcess
    {
        IFormatKeyValuePairsService service;

        public SynchronizationMainProcess(IFormatKeyValuePairsService service)
        {
            this.service = service;
        }

        public string LoadBaseFile(string baseFilePath)
        {
            var reader = new StreamReader(baseFilePath);
            var result = reader.ReadToEnd();
            reader.Close();
            return result;
        }

        public IEnumerable<IFormatKeyValuePair> LoadFormatKeyValuePairs()
        {
            return service.GetKeyValuePairs();
        }

        public bool SaveFormat(string baseSentence, IEnumerable<IFormatKeyValuePair> formatKeyValuePairs, string destFilePath)
        {
            var replacedSentence = service.ReplaceKeyToValue(baseSentence, formatKeyValuePairs);
            var calculatedSentence = service.CalculateSentence(replacedSentence);
            var formattedSentence = service.FormatSenetence(calculatedSentence);
            var writer = new StreamWriter(destFilePath);
            writer.Write(formattedSentence);
            writer.Close();
            return true;
        }
    }
}
