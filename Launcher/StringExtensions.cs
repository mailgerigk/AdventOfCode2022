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
}
