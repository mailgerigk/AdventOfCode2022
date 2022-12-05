using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.Problems._2022;
internal class Day04
{
    public static string Part1(List<List<int>> ints)
    {
        static bool Contains(int a, int b, int x, int y) => (a <= x && y <= b) || (x <= a && b <= y);
        static bool Flatten(List<int> ints) => Contains(ints[0], -ints[1], ints[2], -ints[3]);

        return ints.Count(Flatten).ToString();
    }
    public static string Part2(List<List<int>> ints)
    {
        static bool Between(int a, int b, int x) => a <= x && x <= b;
        static bool Overlaps(int a, int b, int x, int y) => Between(a, b, x) || Between(a, b, y) || Between(x, y, a) || Between(x, y, b);
        static bool Flatten(List<int> ints) => Overlaps(ints[0], -ints[1], ints[2], -ints[3]);

        return ints.Count(Flatten).ToString();
    }
}
