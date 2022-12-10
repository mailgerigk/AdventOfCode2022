using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.Problems._2022;
internal class Day09
{
    static string Solve(string[] commands, List<List<int>> arguments, int knotCount)
    {
        var positions = new HashSet<(int x, int y)>();
        var knots = new List<(int x, int y)>();

        for (int i = 0; i < knotCount; i++)
        {
            knots.Add((0, 0));
        }

        var directions = new Dictionary<char, (int x, int y)>()
        {
            { 'U', (0, +1) },
            { 'D', (0, -1) },
            { 'L', (-1, 0) },
            { 'R', (+1, 0) },
        };

        positions.Add(knots.Last());
        for (int i = 0; i < commands.Length; i++)
        {
            var dir = directions[commands[i].First()];
            var count = arguments[i].First();

            for (int c = 0; c < count; c++)
            {
                knots[0] = (knots[0].x + dir.x, knots[0].y + dir.y);

                for (int j = 1; j < knots.Count; j++)
                {
                    var head = knots[j-1];
                    var tail = knots[j];

                    var d = (x:head.x - tail.x, y:head.y - tail.y);
                    var distance = Math.Abs( d.x) + Math.Abs( d.y);
                    if ((distance >= 2 && (d.x == 0 || d.y == 0)) || (distance >= 3))
                    {
                        knots[j] = (tail.x + Math.Sign(d.x), tail.y + Math.Sign(d.y));
                        if(j == knotCount - 1)
                        {
                            positions.Add(knots.Last());
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        return positions.Count.ToString();
    }

    public static string Part1([RemoveEmpty] string[] commands, List<List<int>> arguments) => Solve(commands, arguments, 2);
    public static string Part2([RemoveEmpty] string[] commands, List<List<int>> arguments) => Solve(commands, arguments, 10);
}
