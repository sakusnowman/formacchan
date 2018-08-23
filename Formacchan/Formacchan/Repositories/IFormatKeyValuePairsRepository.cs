using System;
using System.Collections.Generic;
using System.Text;
using FormacchanLibrary.Models;

namespace Formacchan.Repositories
{
    public interface IFormatKeyValuePairsRepository
    {
        IEnumerable<IFormatKeyValuePair> GetFormatKeyValuePairs();
    }
}
