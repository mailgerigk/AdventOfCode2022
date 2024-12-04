namespace Launcher.Problems._2023;
internal class Day03
{
    static (List<(int number, int x, int y, int end)> parts, List<(int x, int y)> gears) Collect(string[] lines)
    {
        var parts = new List<(int number, int x, int y, int end)>();
        var gears = new List<(int x, int y)>();
        for (int y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (int x = 0; x < line.Length;)
            {
                if (char.IsDigit(line[x]))
                {
                    int start = x;
                    int end = start;
                    int number = 0;
                    do
                    {
                        number = number * 10 + line[x] - '0';
                        x++;
                        end++;
                    } while (x < line.Length && char.IsDigit(line[x]));
                    parts.Add((number, start, y, end - 1));
                }
                else if (line[x] != '.')
                {
                    gears.Add((x, y));
                    x++;
                }
                else
                {
                    x++;
                }
            }
        }
        return (parts, gears);
    }

    const string exampleInput = "467..114..\r\n...*......\r\n..35..633.\r\n......#...\r\n617*......\r\n.....+.58.\r\n..592.....\r\n......755.\r\n...$.*....\r\n.664.598..";
    [Example(exampleInput, "4361")]
    public static string Part1([RemoveEmpty] string[] lines)
    {
        var (parts, symbols) = Collect(lines);
        var sum = 0;
        foreach (var part in parts)
        {
            for (int i = 0; i < symbols.Count; i++)
            {
                var symbol = symbols[i];

                if (symbol.y + 1 < part.y)
                    continue;

                if (symbol.y - 1 > part.y)
                    break;

                if (part.x <= symbol.x + 1 && symbol.x - 1 <= part.end)
                {
                    sum += part.number;
                    break;
                }
            }
        }
        return sum.ToString();
    }

    [Example(exampleInput, "467835")]
    public static string Part2([RemoveEmpty] string[] lines)
    {
        var (parts, gears) = Collect(lines);
        var sum = 0;
        foreach (var gear in gears)
        {
            int part1 = 0;
            int part2 = 0;
            for (int i = 0; i < parts.Count; i++)
            {
                var part = parts[i];
                if (gear.y - 1 > part.y)
                    continue;

                if (gear.y + 1 < part.y)
                    break;

                if (part.x <= gear.x + 1 && gear.x - 1 <= part.end)
                {
                    if (part1 == 0) part1 = part.number;
                    else if (part2 == 0) part2 = part.number;
                    else
                    {
                        part1 = 0;
                        part2 = 0;
                        break;
                    }
                }
            }
            sum += part1 * part2;
        }
        return sum.ToString();
    }
}
