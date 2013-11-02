using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.Editor.Extensions
{
    public static class StringExtensions
    {
        public static string Truncate(this string str, int maxLength)
        {
            if (str == null) return null;
            return str.Substring(0, Math.Min(maxLength, str.Length));
        }
    }
}
