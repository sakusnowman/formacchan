using FormacchanLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacchan.Services
{
    public interface IFormatKeyValuePairsService
    {
        IEnumerable<IFormatKeyValuePair> GetKeyValuePairs();
        string ReplaceKeyToValue(string sentence, IEnumerable<IFormatKeyValuePair> formatKeyValuePairs);
        string FormatSenetence(string sentence);
        string CalculateSentence(string sentence);
    }
}
