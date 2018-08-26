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

            SetRepository(keyValuePairsFilePath);
        }

        void SetRepository(string keyValuePairsFilePath)
        {
            var configurtion = Labo.Resolve<IConfigurationSettings>();
            IFormatKeyValuePairsRepository repository = null;
            if (configurtion.IsXmlMode)
            {
                repository = new FormatKeyValuePairsXmlRepository(keyValuePairsFilePath);
            }
            else
            {
                repository = new FormatKeyValuePairsRepository(keyValuePairsFilePath,
                Labo.Resolve<FormacchanLibrary.Services.IFormatKeyValuePairsService>(), configurtion);
            }

            Labo.RegisterSingleton<IFormatKeyValuePairsRepository>(repository);
        }
    }
}
