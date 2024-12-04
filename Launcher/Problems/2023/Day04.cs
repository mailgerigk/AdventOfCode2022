namespace Launcher.Problems._2023;
internal class Day04
{
    const string exampleInput = "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53\r\nCard 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19\r\nCard 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1\r\nCard 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83\r\nCard 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36\r\nCard 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11";
    [Example(exampleInput, "13")]
    public static string Part1([RemoveEmpty] string[] lines)
    {
        var sum = 0;
        var winning = new int[100];
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var j = 0;
            while (line[j] != ':')
            {
                j++;
            }
            j++;

            while (line[j] != '|')
            {
                if (char.IsDigit(line[j]))
                {
                    var index = 0;
                    if (char.IsDigit(line[j + 1]))
                    {
                        index = (line[j] - '0') * 10 + line[j + 1] - '0';
                        j += 2;
                    }
                    else
                    {
                        index = line[j] - '0';
                        j++;
                    }
                    winning[index] = i + 1;
                }
                else
                {
                    j++;
                }
            }
            j++;

            var k = 0;
            while (j < line.Length)
            {
                if (char.IsDigit(line[j]))
                {
                    var index = 0;
                    if (j + 1 < line.Length && char.IsDigit(line[j + 1]))
                    {
                        index = (line[j] - '0') * 10 + line[j + 1] - '0';
                        j += 2;
                    }
                    else
                    {
                        index = line[j] - '0';
                        j++;
                    }
                    if (winning[index] == i + 1)
                    {
                        if (k == 0) k = 1;
                        else k *= 2;
                    }
                }
                else
                {
                    j++;
                }
            }
            sum += k;
        }

        return sum.ToString();
    }

    [Example(exampleInput, "30")]
    public static string Part2([RemoveEmpty] string[] lines)
    {
        var winning = new int[100];
        var cardCounts = new int[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            cardCounts[i]++;
            var line = lines[i];
            var j = 0;
            while (line[j] != ':')
            {
                j++;
            }
            j++;

            while (line[j] != '|')
            {
                if (char.IsDigit(line[j]))
                {
                    var index = 0;
                    if (char.IsDigit(line[j + 1]))
                    {
                        index = (line[j] - '0') * 10 + line[j + 1] - '0';
                        j += 2;
                    }
                    else
                    {
                        index = line[j] - '0';
                        j++;
                    }
                    winning[index] = i + 1;
                }
                else
                {
                    j++;
                }
            }
            j++;

            var k = 0;
            while (j < line.Length)
            {
                if (char.IsDigit(line[j]))
                {
                    var index = 0;
                    if (j + 1 < line.Length && char.IsDigit(line[j + 1]))
                    {
                        index = (line[j] - '0') * 10 + line[j + 1] - '0';
                        j += 2;
                    }
                    else
                    {
                        index = line[j] - '0';
                        j++;
                    }
                    if (winning[index] == i + 1)
                    {
                        k++;
                    }
                }
                else
                {
                    j++;
                }
            }

            for (int l = 0; l < k; l++)
            {
                cardCounts[i + 1 + l] += cardCounts[i];
            }
        }

        var sum = cardCounts.Sum();
        return sum.ToString();
    }
}
