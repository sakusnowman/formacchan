using System;
using System.Collections.Generic;
using System.Text;
using Formacchan.Models;

namespace Formacchan.Repositories
{
    public interface IFormatKeyValuePairsRepository
    {
        IEnumerable<FormatKeyValuePair> GetFormatKeyValuePairs();
    }
}
