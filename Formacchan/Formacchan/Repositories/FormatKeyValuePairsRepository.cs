using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Formacchan.Models;

namespace Formacchan.Repositories
{
    public class FormatKeyValuePairsRepository : IFormatKeyValuePairsRepository
    {
        string formatKeyValuePairsFilePath;
        public FormatKeyValuePairsRepository(string formatKeyValuePairsFilePath)
        {
            this.formatKeyValuePairsFilePath = formatKeyValuePairsFilePath;
        }
        public IEnumerable<FormatKeyValuePair> GetFormatKeyValuePairs()
        {
            var reader = new StreamReader(formatKeyValuePairsFilePath);
            var result = new List<FormatKeyValuePair>();
            while(reader.EndOfStream == false)
            {
                var line = reader.ReadLine();
                var split = line.Split("<=>");
                result.Add(new FormatKeyValuePair(split[0], split[1]));
            }
            reader.Close();
            return result;
        }
    }
}
