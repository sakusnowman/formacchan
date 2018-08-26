using System;
using System.Collections.Generic;
using System.Text;
using Formacchan.Configuration;
using FormacchanLibrary.Models;
using System.Xml.Linq;
namespace Formacchan.Repositories
{
    public class FormatKeyValuePairsXmlRepository : IFormatKeyValuePairsRepository
    {
        private List<IFormatKeyValuePair> keyValuePairs;
        private readonly string formatKeyValuePairsFilePath;
        public FormatKeyValuePairsXmlRepository(string formatKeyValuePairsFilePath)
        {
            this.formatKeyValuePairsFilePath = formatKeyValuePairsFilePath;
        }

        public IEnumerable<IFormatKeyValuePair> GetFormatKeyValuePairs()
        {
            if (keyValuePairs == null) LoadKeyValuePairs();
            return keyValuePairs;
        }

        void LoadKeyValuePairs()
        {
            keyValuePairs = new List<IFormatKeyValuePair>();
            var document = XDocument.Load(formatKeyValuePairsFilePath);
            var root = document.Root;
            var pairs = root.Elements("pair");
            foreach(var pair in pairs)
            {
                var key = pair.Attribute("key").Value;
                var value = pair.Attribute("value").Value;
                keyValuePairs.Add(new FormatKeyValuePair(key, value));
            }
        }
    }
}
