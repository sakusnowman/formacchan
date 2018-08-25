using System;
using System.Collections.Generic;
using System.Text;

namespace FormacchanLibrary.Models
{
    public class FormatKeyValuePair  : IFormatKeyValuePair
    {
        public FormatKeyValuePair(string key, string value, string splitMark = "<=>")
        {
            this.Key = key;
            this.Value = value;
            this.splitMark = splitMark;
        }

        public string Key { get; }
        public string Value { get; }

        public string GetKeyValueForFormat()
        {
            return string.Format("{{0}}{1}{2}", Key, splitMark , Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is IFormatKeyValuePair == false) return false;
            return this.ToString().Equals(obj.ToString());
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Key:{0}\nValue:{1}", Key, Value);
        }

        private readonly string splitMark;
    }
}
