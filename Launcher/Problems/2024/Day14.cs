namespace Launcher.Problems._2024;
internal class Day14
{
    const string exampleInput = "p=0,4 v=3,-3\r\np=6,3 v=-1,-3\r\np=10,3 v=-1,2\r\np=2,0 v=2,-1\r\np=0,0 v=1,3\r\np=3,0 v=-2,-2\r\np=7,6 v=-1,-3\r\np=3,0 v=-1,-2\r\np=9,3 v=2,3\r\np=7,3 v=-1,2\r\np=2,4 v=2,-3\r\np=9,5 v=-3,-3";
    [Example(exampleInput, "12")]
    public static string Part1(List<List<int>> lines, bool isExample)
    {
        var time = 100;

        var width = isExample ? 11 : 101;
        var height = isExample ? 7 : 103;

        var midX = width / 2;
        var midY = height / 2;

        var quadrants = new int[4];

        foreach (var line in lines)
        {
            var px = line[0];
            var py = line[1];
            var vx = line[2];
            var vy = line[3];

            var x = px + vx * time;
            x = x >= 0 ? x % width : (width + x % width) % width;

            var y = py + vy * time;
            y = y >= 0 ? y % height : (height + y % height) % height;

            if (x == midX || y == midY)
                continue;

            var index = (x < midX ? 0 : 1) | (y < midY ? 0 : 2);
            quadrants[index]++;
        }

        var result = quadrants.Product();
        return result.ToString();
    }

    public static string Part2(List<List<int>> lines)
    {
        var width = 101;
        var height = 103;

        var set = new HashSet<(int x, int y)>();

        for (int i = 0; i < int.MaxValue; i++)
        {
            set.Clear();

            foreach (var line in lines)
            {
                var px = line[0];
                var py = line[1];
                var vx = line[2];
                var vy = line[3];

                var x = px + vx * i;
                x = x >= 0 ? x % width : (width + x % width) % width;

                var y = py + vy * i;
                y = y >= 0 ? y % height : (height + y % height) % height;

                if (!set.Add((x, y)))
                {
                    goto continueLoop;
                }
            }

            var maxCount = 0;
            var countCount = 0;

            for (int y = 0; y < height; y++)
            {
                var currentCount = 0;
                var bestCount = 0;
                for (int x = 0; x < width; x++)
                {
                    if (set.Contains((x, y)))
                    {
                        currentCount++;
                    }
                    else
                    {
                        bestCount = Math.Max(bestCount, currentCount);
                        currentCount = 0;
                    }
                }
                if (maxCount < bestCount)
                {
                    maxCount = bestCount;
                    countCount = 1;
                }
                else if (bestCount == maxCount)
                {
                    countCount++;
                }
            }

            if (maxCount == 31)
            {
                return i.ToString();
            }

        continueLoop:
            continue;
        }
        return string.Empty;
    }
}
