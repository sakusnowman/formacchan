using System;
using System.Collections.Generic;
using System.Text;

namespace Formacchan.Models
{
    public class FormatKeyValuePair
    {
        public FormatKeyValuePair(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public string Key { get; }
        public string Value { get; }
    }
}
