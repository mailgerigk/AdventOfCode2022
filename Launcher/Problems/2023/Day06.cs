namespace Launcher.Problems._2023;
internal class Day06
{
    static int GetCount(long t, long d)
    {
        var t2 = (double)t * t;
        var d4 = (double)d * 4;
        var sqrt = Math.Sqrt(t2 - d4);
        var lower = (t - sqrt) * 0.5;
        var upper = (sqrt + t) * 0.5;
        if ((int)upper == upper)
        {
            upper--;
        }
        return (int)upper - (int)lower;
    }

    const string exampleInput = "Time:      7  15   30\r\nDistance:  9  40  200";
    [Example(exampleInput, "288")]
    public static string Part1([RemoveEmpty] string[] lines)
    {
        var times = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();
        var distances = lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();
        var solutions = Enumerable.Range(0, times.Length).Select((i) => GetCount(times[i], distances[i])).ToArray();
        var count = solutions.Product();
        return count.ToString();
    }

    [Example(exampleInput, "71503")]
    public static string Part2([RemoveEmpty] string[] lines)
    {
        var time = new string( lines[0].Where(char.IsDigit).ToArray()).ParseInt();
        var distance = new string( lines[1].Where(char.IsDigit).ToArray()).ParseInt();
        var count = GetCount(time, distance);
        return count.ToString();
    }
}
