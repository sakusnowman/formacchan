using FormacchanLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormacchanLibrary.Services
{
    public interface IFormatKeyValuePairsService
    {
        IEnumerable<IFormatKeyValuePair> GetFormatKeyValuePairs(string pairsSentence);
        IEnumerable<IFormatKeyValuePair> GetFormatKeyValuePairFromProperties(object obj, string prefix = "", bool getPropertiesInNoValueTypeProperty = true);
    }
}
