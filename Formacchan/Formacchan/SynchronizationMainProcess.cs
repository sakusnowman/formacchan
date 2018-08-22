using Formacchan.Models;
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

        public IEnumerable<FormatKeyValuePair> LoadFormatKeyValuePairs()
        {
            return service.GetKeyValuePairs();
        }

        public bool SaveFormat(string baseSentence, IEnumerable<FormatKeyValuePair> formatKeyValuePairs, string destFilePath)
        {
            var sentence = service.FormatSentence(baseSentence, formatKeyValuePairs);
            var writer = new StreamWriter(destFilePath);
            writer.Write(sentence);
            writer.Close();
            return true;
        }
    }
}
