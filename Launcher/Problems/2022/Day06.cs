using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.Problems._2022;
internal class Day06
{
    static string Solve(string input, int count)
        => (Enumerable.Range(0, input.Length).First(i => input.Skip(i).Take(count).Distinct().Count() == count) + count).ToString();

    public static string Part1(string input) => Solve(input, 4);
    public static string Part2(string input) => Solve(input, 14);
}
