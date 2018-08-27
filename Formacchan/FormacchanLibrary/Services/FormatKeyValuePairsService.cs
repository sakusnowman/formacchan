using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using FormacchanLibrary.Models;
using Extensions;
using System.Linq;

namespace FormacchanLibrary.Services
{
    public class FormatKeyValuePairsService : IFormatKeyValuePairsService
    {
        public IEnumerable<IFormatKeyValuePair> GetFormatKeyValuePairFromProperties(object obj, string prefix = "", bool getChildlenProperties = true, string splitMark = "<=>")
        {
            CheckNull(obj, prefix);
            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return properties.SelectMany(property => GetKeyValuePairsFromProperty(property, obj, prefix, getChildlenProperties, splitMark));
        }

        public IEnumerable<XElement> GetXmlFormatKeyValuePairFromProperties(object obj, bool getChildlenProperties = true)
        {
            var keyValuePairs = GetFormatKeyValuePairFromProperties(obj, getChildlenProperties: getChildlenProperties);

            var result = new List<XElement>();
            foreach (var pair in keyValuePairs)
            {
                result.Add(GetKeyValuePairXElement(pair));
            }
            return result;
        }

        public IEnumerable<IFormatKeyValuePair> GetFormatKeyValuePairs(string pairsSentence, string splitMark)
        {
            var reader = new StringReader(pairsSentence);
            var result = new List<FormatKeyValuePair>();

            var line = reader.ReadLine();
            while (line != null)
            {
                if (line.StartsWith('#') == false)
                {
                    var split = line.Split(splitMark);
                    result.Add(new FormatKeyValuePair(split[0], split[1], splitMark));
                }
                line = reader.ReadLine();
            }
            reader.Close();
            return result;
        }

        IEnumerable<IFormatKeyValuePair> GetKeyValuePairsFromProperty(PropertyInfo property, object obj, string prefix, bool getChildlenProperties, string splitMark)
        {
            if (property.PropertyType.IsCollectionType())
            {
                return GetFormatKeyValuePairsFromCollectionProperty(obj, property, prefix, getChildlenProperties, splitMark);
            }
            else if (getChildlenProperties == false || property.PropertyType.IsValueTypeOrString())
            {
                var pairs = new FormatKeyValuePair(GetKey(property, prefix), GetValue(property, obj), splitMark);
                return new IFormatKeyValuePair[] { pairs };
            }
            else
            {
                var classPrefix = prefix + property.Name + "::";
                return GetFormatKeyValuePairFromProperties(property.GetValue(obj), classPrefix, getChildlenProperties, splitMark);
            }
        }

        void CheckNull(object obj, string prefix)
        {
            if (obj == null) throw new NullReferenceException("Failed to GetFormatKeyValuePairFromProperties, cause obj is null.");
            if (prefix == null) throw new NullReferenceException("Failed to GetFormatKeyValuePairFromProperties, cause prefix is null.");
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

        IEnumerable<IFormatKeyValuePair> GetFormatKeyValuePairsFromCollectionProperty(object obj, PropertyInfo arrayProperty, string prefix, bool getChildlenProperties, string splitMark)
        {
            int count = 0;
            var result = new List<IFormatKeyValuePair>();
            ICollection values = (ICollection)arrayProperty.GetValue(obj);
            foreach (var value in values)
            {
                if (getChildlenProperties == false || value.GetType().IsValueTypeOrString())
                {
                    var pairs = new FormatKeyValuePair(string.Format("{{{0}{1}[{2}]}}", prefix, arrayProperty.Name, count++), value.ToString(), splitMark);
                    result.Add(pairs);
                }
                else
                {
                    var classPrefix = prefix + arrayProperty.Name + string.Format("[{0}]", count++) + "::";
                    result.AddRange(GetFormatKeyValuePairFromProperties(value, classPrefix));
                }
            }
            return result;
        }

        XElement GetKeyValuePairXElement(IFormatKeyValuePair pair)
        {
            return new XElement("pair",
                    new XAttribute("key", pair.Key),
                    new XAttribute("value", pair.Value));
        }
    }
}
