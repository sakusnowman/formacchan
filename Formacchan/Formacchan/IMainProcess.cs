using FormacchanLibrary.Models;
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
        IEnumerable<IFormatKeyValuePair> LoadFormatKeyValuePairs();
        bool SaveFormat(string baseSentence, IEnumerable<IFormatKeyValuePair> formatKeyValuePairs, string destFilePath);
    }
}
