using Formacchan.Repositories;
using Formacchan.Services;
using IocLabo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacchan.Configuration
{
    class Setup
    {
        public Setup(string keyValuePairsFilePath)
        {
            RegisterImplement();
            RegisterSingleton(keyValuePairsFilePath);
        }

        private void RegisterImplement()
        {
            Labo.Register<IMainProcess, SynchronizationMainProcess>();
            Labo.Register<IFormatKeyValuePairsService, FormatKeyValuePairsService>();
            Labo.Register<FormacchanLibrary.Services.IFormatKeyValuePairsService, FormacchanLibrary.Services.FormatKeyValuePairsService>();
        }

        private void RegisterSingleton(string keyValuePairsFilePath)
        {
            Labo.RegisterSingleton<IConfigurationSettings>(new ConfigurationSettingFromConfigFile());
            var repository = new FormatKeyValuePairsRepository(keyValuePairsFilePath, 
                Labo.Resolve<FormacchanLibrary.Services.IFormatKeyValuePairsService>(), 
                Labo.Resolve<IConfigurationSettings>());
            Labo.RegisterSingleton<IFormatKeyValuePairsRepository>(repository);
        }

        
    }
}
