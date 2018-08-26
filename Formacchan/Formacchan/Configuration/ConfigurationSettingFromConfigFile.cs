using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace Formacchan.Configuration
{
    class ConfigurationSettingFromConfigFile : IConfigurationSettings
    {
        IEnumerable<XElement> settings;
        bool useDefaultSetting;
        public ConfigurationSettingFromConfigFile()
        {
            var filePath = Path.GetFileName(Assembly.GetCallingAssembly().Location);
            if (File.Exists(filePath) == false)
            {
                Console.WriteLine("{0} is not found.", filePath);
                Console.WriteLine("Used default setting.");
                useDefaultSetting = true;
                return;
            }
            var document = XDocument.Load(filePath + ".config");
            var root = document.Element("app_setting");
            var table = root.Element(root.Attribute("table").Value);
            settings = table.Elements("add");
        }
        public string KeyValueSplitMark => GetValueFromKey("KeyValueSplitMark");
        public string FormatStartMark => GetValueFromKey("FormatStartMark");
        public string FormatEndMark => GetValueFromKey("FormatEndMark");
        public string FormatSplitMark => GetValueFromKey("FormatSplitMark");
        public string CalculationStartMark => GetValueFromKey("CalculationStartMark");
        public string CalculationEndMark => GetValueFromKey("CalculationEndMark");

        public bool IsXmlMode => GetValueFromKey("isXmlMode").ToLower() == "true";

        string GetValueFromKey(string key)
        {
            if (useDefaultSetting)
                return GetDefaultSetting(key);
            var element = settings.First(e => e.Attribute("key").Value.Equals(key));
            return element.Attribute("value").Value;
        }

        private string GetDefaultSetting(string key)
        {
            switch (key)
            {
                case "KeyValueSplitMark":
                    return "<=>";
                case "FormatStartMark":
                    return "<f=|";
                case "FormatEndMark":
                    return "|=f>";
                case "FormatSplitMark":
                    return "<=>";
                case "CalculationStartMark":
                    return "<c=|";
                case "CalculationEndMark":
                    return "|=c>";
                case "isXmlMode":
                    return "true";
            }
            return null;
        }
    }
}
