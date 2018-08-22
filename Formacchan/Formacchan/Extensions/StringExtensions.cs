using System;
using System.Collections.Generic;
using System.Text;

namespace Formacchan.Extensions
{
    public class StringExtensions
    {
        public static int CountOf(string sentence, string key)
        {
            int result = 0;

            int index = sentence.IndexOf(key);
            while(index != -1)
            {
                index = sentence.IndexOf(key, index + key.Length);
                ++result;
            }
            return result;
        }
    }
}
