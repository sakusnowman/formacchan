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

        string Key { get; }
        string Value { get; }
    }
}
