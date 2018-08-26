using FormacchanLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FormacchanLibrary.Services
{
    public interface IFormatKeyValuePairsService
    {
        IEnumerable<IFormatKeyValuePair> GetFormatKeyValuePairs(string pairsSentence, string splitMark = "<=>");
        IEnumerable<XElement> GetXmlFormatKeyValuePairFromProperties(object obj, bool getChildlenProperties = true);
        IEnumerable<IFormatKeyValuePair> GetFormatKeyValuePairFromProperties(object obj, string prefix = "", bool getChildlenProperties = true, string splitMark = "<=>");
    }
}
