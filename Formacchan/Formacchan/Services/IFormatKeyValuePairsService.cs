using Formacchan.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacchan.Services
{
    public interface IFormatKeyValuePairsService
    {
        IEnumerable<FormatKeyValuePair> GetKeyValuePairs();
        string FormatSentence(string sentence, IEnumerable<FormatKeyValuePair> formatKeyValuePairs);
    }
}
