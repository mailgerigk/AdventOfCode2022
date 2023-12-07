using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher;
public static class StringExtensions
{
    public static string Before(this string str, string value)
    {
        var index = str.IndexOf(value);
        if (index < 0)
            return string.Empty;
        return str.Substring(0, index);
    }
    public static string After(this string str, string value)
    {
        var index = str.IndexOf(value);
        if (index < 0)
            return string.Empty;
        return str.Substring(index);
    }
    public static int ParseInt(this string str)
    {
        return int.Parse(str);
    }
    public static List<long> ParseInts(this string str, int capacity = 8)
    {
        var list = new List<long>(capacity);
        for (int i = 0; i < str.Length; i++)
        {
            if (char.IsDigit(str[i]))
            {
                long num = str[i] - '0';
                i++;
                while (i < str.Length && char.IsDigit(str[i]))
                {
                    num = num * 10 + str[i] - '0';
                    i++;
                }
                i--;
                list.Add(num);
            }
        }
        return list;
    }
}
