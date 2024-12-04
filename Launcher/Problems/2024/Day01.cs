namespace Launcher.Problems._2024;
internal class Day01
{
    const string exampleInput = "3   4\r\n4   3\r\n2   5\r\n1   3\r\n3   9\r\n3   3";
    [Example(exampleInput, "11")]
    public static string Part1([RemoveEmpty] List<List<int>> lines)
    {
        var left = lines.Select(pair => pair.First()).ToList();
        var right = lines.Select(pair => pair.Last()).ToList();

        left.Sort();
        right.Sort();

        int sum = 0;
        for (int i = 0; i < left.Count; i++)
        {
            sum += Math.Abs(left[i] - right[i]);
        }

        return sum.ToString();
    }

    const string exampleInput2 = "3   4\r\n4   3\r\n2   5\r\n1   3\r\n3   9\r\n3   3";
    [Example(exampleInput2, "31")]
    public static string Part2([RemoveEmpty] List<List<int>> lines)
    {
        var left = lines.Select(pair => pair.First()).ToList();
        var right = lines.Select(pair => pair.Last()).ToList();

        left.Sort();
        right.Sort();

        var scoreCache = new Dictionary<int, int>();
        var score = 0;

        foreach (var item in left)
        {
            if (!scoreCache.TryGetValue(item, out var cachedScore))
            {
                cachedScore = 0;
                foreach (var item2 in right)
                {
                    if (item == item2)
                    {
                        cachedScore++;
                    }
                }
                cachedScore = item * cachedScore;
                scoreCache.Add(item, cachedScore);
            }
            score += cachedScore;
        }

        return score.ToString();
    }
}

