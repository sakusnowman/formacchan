using System;
using System.Collections.Generic;
using System.Text;

namespace Formacchan.Configuration
{
    public interface IConfigurationSettings
    {
        string KeyValueSplitMark { get; }
        string FormatStartMark { get; }
        string FormatEndMark { get; }
        string FormatSplitMark { get; }
        string CalculationStartMark { get; }
        string CalculationEndMark { get; }
    }
}
