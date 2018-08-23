using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FormacchanLibrary.Models;


namespace Formacchan.Repositories
{
    public class FormatKeyValuePairsRepository : IFormatKeyValuePairsRepository
    {
        string formatKeyValuePairsFilePath;
        private readonly FormacchanLibrary.Services.IFormatKeyValuePairsService service;

        public FormatKeyValuePairsRepository(string formatKeyValuePairsFilePath, FormacchanLibrary.Services.IFormatKeyValuePairsService service)
        {
            this.formatKeyValuePairsFilePath = formatKeyValuePairsFilePath;
            this.service = service;
        }
        public IEnumerable<IFormatKeyValuePair> GetFormatKeyValuePairs()
        {
            var reader = new StreamReader(formatKeyValuePairsFilePath);
            return service.GetFormatKeyValuePairs(reader.ReadToEnd());
        }
    }
}
