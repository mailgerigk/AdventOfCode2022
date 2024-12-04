namespace Launcher.Problems._2024;
internal class Day02
{
    static bool Check(List<int> ints)
    {
        var isSafe = true;
        var isIncreasing = ints[0] < ints[1];
        for (var i = 1; isSafe && i < ints.Count; i++)
        {
            var direction = ints[i-1] < ints[i];
            isSafe &= direction == isIncreasing;
            var distance = Math.Abs(ints[i-1] - ints[i]);
            isSafe &= distance >= 1 && distance <= 3;
        }
        return isSafe;
    }

    const string exampleInput = "7 6 4 2 1\r\n1 2 7 8 9\r\n9 7 6 2 1\r\n1 3 2 4 5\r\n8 6 4 4 1\r\n1 3 6 7 9";
    [Example(exampleInput, "2")]
    public static string Part1([RemoveEmpty] List<List<int>> lines)
    {
        var safeLevels = 0;
        foreach (var level in lines)
        {
            var isSafe = Check(level);
            if (isSafe)
            {
                safeLevels++;
            }
        }

        return safeLevels.ToString();
    }

    const string exampleInput2 = "7 6 4 2 1\r\n1 2 7 8 9\r\n9 7 6 2 1\r\n1 3 2 4 5\r\n8 6 4 4 1\r\n1 3 6 7 9";
    [Example(exampleInput2, "4")]
    public static string Part2([RemoveEmpty] List<List<int>> lines)
    {
        var safeLevels = 0;
        foreach (var level in lines)
        {
            var isSafe = Check(level);
            var i = 0;
            while (!isSafe && i < level.Count)
            {
                var n = level.ToList();
                n.RemoveAt(i++);
                isSafe = Check(n);
            }
            if (isSafe)
            {
                safeLevels++;
            }
        }

        return safeLevels.ToString();
    }
}
