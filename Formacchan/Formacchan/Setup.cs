﻿using Formacchan.Repositories;
using Formacchan.Services;
using IocLabo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacchan
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
        }

        private void RegisterSingleton(string keyValuePairsFilePath)
        {
            var repository = new FormatKeyValuePairsRepository(keyValuePairsFilePath);
            Labo.RegisterSingleton<IFormatKeyValuePairsRepository>(repository);
        }

        
    }
}