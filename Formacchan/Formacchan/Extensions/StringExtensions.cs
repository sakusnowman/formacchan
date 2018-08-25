using System;
using System.Collections.Generic;
using System.Text;

namespace Formacchan.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<string> GetInnerSentences(this string me, string startMark, string endMark)
        {
            var result = new List<string>();
            int currentIndex = me.IndexOf(startMark);
            while (currentIndex != -1)
            {
                int startIndex = currentIndex + startMark.Length;
                int endIndex = me.IndexOf(endMark, currentIndex);
                if (endIndex < -1) break;
                var middleSentence = me.Substring(startIndex, endIndex - startIndex);
                result.Add(middleSentence);
                currentIndex = me.IndexOf(startMark, endIndex + endMark.Length);
            }
            return result;
        }

        public static string FormatInnerSenetences(this string me, string startMark, string endMark, string splitMark)
        {
            var result = me;
            var innerSentences = me.GetInnerSentences(startMark, endMark);
            foreach (var innerSentence in innerSentences)
            {
                var split = innerSentence.Split(splitMark);
                var formatted = "";
                if (double.TryParse(split[0], out double value))
                {
                    formatted = String.Format(split[1], value);
                    result = result.Replace(startMark + innerSentence + endMark, formatted);
                }
            }
            return result;
        }

        public static string CalculateInnerSentences(this string me, string startMark, string endMark)
        {
            var result = me;
            var innerSentences = me.GetInnerSentences(startMark, endMark);
            foreach (var innerSentence in innerSentences)
            {
                if(innerSentence.TryCalculate(out string calculated))
                {
                    result = result.Replace(startMark + innerSentence + endMark, calculated);
                }
            }
            return result;
        }

        public static bool TryCalculate(this string me, out string result)
        {
            try
            {
                var dt = new System.Data.DataTable();
                result = dt.Compute(me, "").ToString();
                return true;
            }
            catch
            {
                result = string.Empty;
                return false;
            }
            
        }
    }
}
