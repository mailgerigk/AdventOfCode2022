using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.Problems._2022;
internal class Day01
{
    [DayLevel(2022, 1, 1)]
    public static string Part1(string input)
    {
        var calories = input.Split('\n').Select(s => string.IsNullOrEmpty(s) ? 0 : int.Parse(s)).ToList();
        var elfs = new List<int>(){ 0 };
        foreach (var c in calories)
        {
            if (c == 0)
                elfs.Add(0);
            elfs[elfs.Count - 1] += c;
        }
        return elfs.Max().ToString();
    }

    [DayLevel(2022, 1, 2)]
    public static string Part2(string input)
    {
        var calories = input.Split('\n').Select(s => string.IsNullOrEmpty(s) ? 0 : int.Parse(s)).ToList();
        var elfs = new List<int>(){ 0 };
        foreach (var c in calories)
        {
            if (c == 0)
                elfs.Add(0);
            elfs[elfs.Count - 1] += c;
        }
        return elfs.OrderDescending().Take(3).Sum().ToString();
    }
}
