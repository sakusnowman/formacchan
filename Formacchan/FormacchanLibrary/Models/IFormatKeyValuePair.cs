using System;
using System.Collections.Generic;
using System.Text;

namespace FormacchanLibrary.Models
{
    public interface IFormatKeyValuePair
    {
        string Key { get; }
        string Value { get; }
        string GetKeyValueForFormat();

    }
}
