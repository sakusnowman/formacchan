﻿using System;
using System.Collections.Generic;
using System.Text;
using Formacchan.Extensions;
using FormacchanLibrary.Models;
using Formacchan.Repositories;

namespace Formacchan.Services
{
    public class FormatKeyValuePairsService : IFormatKeyValuePairsService
    {
        IFormatKeyValuePairsRepository repository;

        public FormatKeyValuePairsService(IFormatKeyValuePairsRepository repository)
        {
            this.repository = repository;
        }

        public string FormatSentence(string sentence, IEnumerable<IFormatKeyValuePair> formatKeyValuePairs)
        {
            var values = new List<string>();
            var result = sentence;
            foreach(var keyValuePair in formatKeyValuePairs)
            {
                result = result.Replace(keyValuePair.Key, keyValuePair.Value);
            }
            return result;
        }

        public IEnumerable<IFormatKeyValuePair> GetKeyValuePairs()
        {
            return repository.GetFormatKeyValuePairs();
        }
    }
}
