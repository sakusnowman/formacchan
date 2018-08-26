using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using FormacchanLibrary.Models;

namespace FormacchanLibrary.Services
{
    public class FormatKeyValuePairsService : IFormatKeyValuePairsService
    {
        public IEnumerable<IFormatKeyValuePair> GetFormatKeyValuePairFromProperties(object obj, string prefix = "", bool getChildlenProperties = true, string splitMark = "<=>")
        {
            CheckNull(obj, prefix);

            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var result = new List<IFormatKeyValuePair>();

            foreach (var property in properties)
            {
                if(IsEnumerable(property))
                {
                    var aa = property.GetValue(obj);
                    SetIEnumerableProperty(obj, property, result, prefix, getChildlenProperties, splitMark);
                }
                else if (getChildlenProperties == false || IsValueTypeOrString(property))
                {
                    var pairs = new FormatKeyValuePair(GetKey(property, prefix), GetValue(property, obj), splitMark);
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

        public IEnumerable<XElement> GetXmlFormatKeyValuePairFromProperties(object obj, bool getChildlenProperties = true)
        {
            var keyValuePairs = GetFormatKeyValuePairFromProperties(obj, getChildlenProperties: getChildlenProperties);
            var result = new List<XElement>();
            foreach(var pair in keyValuePairs)
            {
                var element = new XElement("pair",
                    new XAttribute("key", pair.Key), 
                    new XAttribute("value", pair.Value));
                result.Add(element);
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
        
        void CheckNull(object obj, string prefix)
        {
            if (obj == null) throw new NullReferenceException("Failed to GetFormatKeyValuePairFromProperties, cause obj is null.");
            if (prefix == null) throw new NullReferenceException("Failed to GetFormatKeyValuePairFromProperties, cause prefix is null.");
        }

        bool IsValueTypeOrString(PropertyInfo property)
        {
            return property.PropertyType.Equals(typeof(string)) || property.PropertyType.IsValueType;
        }

        bool IsValueTypeOrString(object obj)
        {
            return obj.GetType().Equals(typeof(string)) || obj.GetType().IsValueType;
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

        void SetIEnumerableProperty(object obj, PropertyInfo arrayProperty,  List<IFormatKeyValuePair> result, string prefix, bool getChildlenProperties, string splitMark)
        {
            int count = 0;
            var properties = arrayProperty.GetValue(obj) as Array;
            foreach (var property in properties)
            {
                if (getChildlenProperties == false || IsValueTypeOrString(property))
                {
                    var pairs = new FormatKeyValuePair(string.Format("{{{0}{1}[{2}]}}", prefix, arrayProperty.Name, count++), property.ToString(), splitMark);
                    result.Add(pairs);
                }
                else
                {
                    var classPrefix = prefix + arrayProperty.Name + string.Format("[{0}]", count++) + "::";
                    result.AddRange(GetFormatKeyValuePairFromProperties(property, classPrefix));
                }
            }
        }
        

        bool IsEnumerable(PropertyInfo propertyInfo)
        {
            var type = propertyInfo.PropertyType;
            try
            {
                return type.IsArray || type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
            }
            catch
            {
                return false;
            }
        }
    }
}
