using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Formacchan.Configuration
{
    class ConfigurationSettingFromConfigFile : IConfigurationSettings
    {
        IEnumerable<XElement> settings;
        public ConfigurationSettingFromConfigFile()
        {
            var filePath = Path.GetFileName(this.GetType().Assembly.Location);
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

        string GetValueFromKey(string key)
        {
            var element = settings.First(e => e.Attribute("key").Value.Equals(key));
            return element.Attribute("value").Value;
        }
    }
}
