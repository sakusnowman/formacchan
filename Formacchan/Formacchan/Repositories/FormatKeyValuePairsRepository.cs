using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Formacchan.Configuration;
using FormacchanLibrary.Models;


namespace Formacchan.Repositories
{
    public class FormatKeyValuePairsRepository : IFormatKeyValuePairsRepository
    {
        string formatKeyValuePairsFilePath;
        private readonly FormacchanLibrary.Services.IFormatKeyValuePairsService service;
        private readonly IConfigurationSettings configurationSettings;

        public FormatKeyValuePairsRepository(string formatKeyValuePairsFilePath, 
            FormacchanLibrary.Services.IFormatKeyValuePairsService service, 
            IConfigurationSettings configurationSettings)
        {
            this.formatKeyValuePairsFilePath = formatKeyValuePairsFilePath;
            this.service = service;
            this.configurationSettings = configurationSettings;
        }
        public IEnumerable<IFormatKeyValuePair> GetFormatKeyValuePairs()
        {
            var reader = new StreamReader(formatKeyValuePairsFilePath);
            return service.GetFormatKeyValuePairs(reader.ReadToEnd(), configurationSettings.KeyValueSplitMark);
        }
    }
}
