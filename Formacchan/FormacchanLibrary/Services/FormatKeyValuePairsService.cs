using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using FormacchanLibrary.Models;

namespace FormacchanLibrary.Services
{
    public class FormatKeyValuePairsService : IFormatKeyValuePairsService
    {
        public IEnumerable<IFormatKeyValuePair> GetFormatKeyValuePairFromProperties(object obj, string prefix = "", bool getPropertiesInNoValueTypeProperty = true)
        {
            CheckNull(obj, prefix);

            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var result = new List<IFormatKeyValuePair>();

            foreach (var property in properties)
            {
                if (getPropertiesInNoValueTypeProperty == false || IsValueTypeOrString(property))
                {
                    var pairs = new FormatKeyValuePair(GetKey(property, prefix), GetValue(property, obj));
                    result.Add(pairs);
                }
                else
                {
                    var classPrefix = prefix + property.Name + "::";
                    result.AddRange(GetFormatKeyValuePairFromProperties(property.GetValue(obj), classPrefix));
                }
            }
            return result;
        }

        public IEnumerable<IFormatKeyValuePair> GetFormatKeyValuePairs(string pairsSentence)
        {
            var reader = new StringReader(pairsSentence);
            var result = new List<FormatKeyValuePair>();

            var line = reader.ReadLine();
            while (line != null)
            {
                if (line.StartsWith('#') == false)
                {
                    var split = line.Split("<=>");
                    result.Add(new FormatKeyValuePair(split[0], split[1]));
                }
                line = reader.ReadLine();
            }
            reader.Close();
            return result;
        }

        void CheckNull(object obj, string prefix)
        {
            if (obj == null) throw new NullReferenceException("Failed to GetFormatKeyValuePairFromProperties, cause obj is null.");
            if (prefix == null) throw new NullReferenceException("Failed to GetFormatKeyValuePairFromProperties, cause prefix is null.");
        }

        bool IsValueTypeOrString(PropertyInfo property)
        {
            return property.PropertyType.Equals(typeof(string)) || property.PropertyType.IsValueType;
        }

        string GetKey(PropertyInfo property, string prefix)
        {
            return string.Format("{{{0}{1}}}", prefix, property.Name);
        }

        string GetValue(PropertyInfo property, object obj)
        {
            var valueTemp = property.GetValue(obj);
            return valueTemp == null ? string.Empty : valueTemp.ToString();
        }


    }
}
