using System;

namespace Formacchan
{
    class Program
    {
        static void Main(string[] args)
        {
            StartMainProcess(args[0], args[1], args[2]);
            Console.WriteLine("Hello World!");
        }

        static bool StartMainProcess(string baseSentenceFilePath, string keyValuePairFilePath, string destFilePath)
        {
            IMainProcess mainProcess = new SynchronizationMainProcess(new FormatKeyValuePairsService());
            var baseSentence = mainProcess.LoadBaseFile(baseSentenceFilePath);
            var formatKeyValuePairs = mainProcess.LoadFormatKeyValuePairs(keyValuePairFilePath);
            return mainProcess.SaveFormat(baseSentence, formatKeyValuePairs, destFilePath);
        }
    }
}
