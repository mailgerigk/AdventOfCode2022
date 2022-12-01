using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.Problems._2022;
internal class Day01
{
    [DayLevel(2022, 1, 1)]
    public static string Part1(List<int[]> input)
    {
        return input.Select(i => i.Sum()).Max().ToString();
    }

    [DayLevel(2022, 1, 2)]
    public static string Part2(List<int[]> input)
    {
        return input.Select(i => i.Sum()).OrderDescending().Take(3).Sum().ToString();
    }
}
