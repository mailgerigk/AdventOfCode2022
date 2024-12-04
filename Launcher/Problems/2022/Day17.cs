namespace Launcher.Problems._2022;
internal class Day17
{
    const string exampleInput = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>";

    static readonly (int x, int y)[][] shapes = new[]
    {
        // ####
        new[]
        {
            (0, 0),
            (1, 0),
            (2, 0),
            (3, 0),
        },
        // .#.
        // ###
        // .#.
        new[]
        {
            (1, 0),
            (0, 1),
            (1, 1),
            (2, 1),
            (1, 2),
        },
        // ..#
        // ..#
        // ###
        new[]
        {
            (0, 0),
            (1, 0),
            (2, 0),
            (2, 1),
            (2, 2),
        },
        // #
        // #
        // #
        // #
        new[]
        {
            (0, 0),
            (0, 1),
            (0, 2),
            (0, 3),
        },
        // ##
        // ##
        new[]
        {
            (0, 0),
            (1, 0),
            (0, 1),
            (1, 1),
        },
    };

    static IEnumerable<int> Simulate(string input)
    {
        var map = new List<byte>();

        var inputIndex = 0;
        var maxHeight = 0;
        for (int i = 0; i < int.MaxValue; i++)
        {
            var rockShape = shapes[i % shapes.Length];
            var position = (x:2, y:maxHeight + 3);

            var rockGrounded = false;
            do
            {
                var jet = input[inputIndex] == '<' ? -1:1;
                inputIndex = (inputIndex + 1) % input.Length;
                if (TryMove((jet, 0)))
                {
                    position = (position.x + jet, position.y);
                }

                // fall
                if (TryMove((0, -1)))
                {
                    position = (position.x, position.y - 1);
                }
                else
                {
                    foreach (var (x, y) in rockShape)
                    {
                        var px = x + position.x;
                        var py = y + position.y;
                        while (map.Count < py + 1)
                        {
                            map.Add(0);
                        }
                        map[py] = (byte)(map[py] | (1 << px));
                        maxHeight = Math.Max(maxHeight, py + 1);
                    }
                    rockGrounded = true;
                    yield return maxHeight;
                }
            } while (!rockGrounded);

            bool TryMove((int x, int y) offset)
            {
                foreach (var (x, y) in rockShape)
                {
                    var px = x + offset.x + position.x;
                    var py = y + offset.y + position.y;
                    if (px < 0 || px >= 7 || py < 0 || (py < map.Count && (map[py] & (1 << px)) > 0))
                        return false;
                }
                return true;
            }
        }
    }

    [Example(exampleInput, "3068")]
    public static string Part1([Trim] string input) => Simulate(input).Skip(2021).First().ToString();


    [Example(exampleInput, "1514285714288")]
    public static string Part2([Trim] string input)
    {
        const long amount = 1000000000000L;

        var list = new List<int>();
        var height = 0;
        var simulate = Simulate(input);
        var enumerator = simulate.GetEnumerator();
        while (true)
        {
            for (int i = 0; i < 100; i++)
            {
                enumerator.MoveNext();
                var nextHeight = enumerator.Current;
                list.Add(nextHeight - height);
                height = nextHeight;
            }

            for (int baseIndex = 0; baseIndex < list.Count / 4 * 2; baseIndex++)
            {
                var remaining = list.Count - baseIndex;
                if ((remaining % 3) != 0)
                    continue;
                var third = remaining / 3;
                if (baseIndex >= third)
                    continue;
                var found = true;
                for (int i = 0; i < third; i++)
                {
                    if (list[baseIndex + i] != list[baseIndex + third + i] || list[baseIndex + i] != list[baseIndex + third + third + i])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    var rocksToGo = amount - baseIndex;
                    var baseAmount = Simulate(input).Skip(baseIndex - 1).First();
                    var cycleCount =  rocksToGo / third;
                    var cycleRest = rocksToGo % third;
                    var cycleSum = list.Skip(baseIndex).Take(third).Sum();
                    var restHeight = list.Skip(baseIndex).Take((int)cycleRest).Sum();
                    var totalHeight = (long)baseAmount + (long)cycleCount * (long)cycleSum + (long)restHeight;
                    return totalHeight.ToString();
                }
            }
        }
    }
}
