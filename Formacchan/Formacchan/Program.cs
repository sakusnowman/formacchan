using IocLabo;
using System;

namespace Formacchan
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(StartMainProcess(args[0], args[1], args[2]));
        }

        static bool StartMainProcess(string baseSentenceFilePath, string keyValuePairFilePath, string destFilePath)
        {
            try
            {
                new Configuration.Setup(keyValuePairFilePath);

                IMainProcess mainProcess = Labo.Resolve<IMainProcess>();
                var baseSentence = mainProcess.LoadBaseFile(baseSentenceFilePath);
                var formatKeyValuePairs = mainProcess.LoadFormatKeyValuePairs();
                return mainProcess.SaveFormat(baseSentence, formatKeyValuePairs, destFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

        }
    }
}
