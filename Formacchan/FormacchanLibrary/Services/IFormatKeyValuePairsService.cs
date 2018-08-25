using FormacchanLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormacchanLibrary.Services
{
    public interface IFormatKeyValuePairsService
    {
        IEnumerable<IFormatKeyValuePair> GetFormatKeyValuePairs(string pairsSentence, string splitMark = "<=>");
        IEnumerable<IFormatKeyValuePair> GetFormatKeyValuePairFromProperties(object obj, string prefix = "", bool getChildlenProperties = true, string splitMark = "<=>");
    }
}
