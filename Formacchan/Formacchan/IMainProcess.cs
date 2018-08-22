using Formacchan.Models;
using Formacchan.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Formacchan
{
    public interface IMainProcess
    {
        string LoadBaseFile(string baseSentenceFilePath);
        IEnumerable<FormatKeyValuePair> LoadFormatKeyValuePairs();
        bool SaveFormat(string baseSentence, IEnumerable<FormatKeyValuePair> formatKeyValuePairs, string destFilePath);
    }
}
