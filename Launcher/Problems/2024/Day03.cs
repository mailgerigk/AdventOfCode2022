namespace Launcher.Problems._2024;
internal class Day03
{
    static (int Left, int Right, int Length) ParseOperands(string s, int i, string op)
    {
        for (; i < s.Length; i++)
        {
            var j = 0;
            var found = true;
            for (; j < op.Length; j++)
            {
                if (i + j >= s.Length || s[i + j] != op[j])
                {
                    found = false;
                    break;
                }
            }
            if (!found || i + op.Length >= s.Length || s[i + op.Length] != '(')
            {
                break;
            }
            int a = 0;
            for (j = i + op.Length + 1; j < s.Length; j++)
            {
                if (char.IsDigit(s[j]))
                {
                    a = a * 10 + (s[j] - '0');
                }
                else
                {
                    break;
                }
            }
            if (j >= s.Length || s[j] != ',')
            {
                break;
            }
            int b = 0;
            for (j = j + 1; j < s.Length; j++)
            {
                if (char.IsDigit(s[j]))
                {
                    b = b * 10 + (s[j] - '0');
                }
                else
                {
                    break;
                }
            }
            if (j >= s.Length || s[j] != ')')
            {
                break;
            }
            j += 1;
            return (a, b, j - i);
        }
        return (int.MinValue, int.MinValue, int.MinValue);
    }

    const string exampleInput = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
    [Example(exampleInput, "161")]
    public static string Part1(string lines)
    {
        var sum = 0;
        for (int i = 0; i < lines.Length;)
        {
            var (a, b, s) = ParseOperands(lines, i, "mul");
            if (s > 0)
            {
                sum += a * b;
                i += s;
            }
            else
            {
                i++;
            }
        }
        return sum.ToString();
    }

    const string exampleInput2 = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
    [Example(exampleInput2, "48")]
    public static string Part2(string lines)
    {
        const string enable = "do()";
        const string disable = "don't()";

        var sum = 0;
        var nextDisable = lines.IndexOf(disable);
        for (int i = 0; i < lines.Length;)
        {
            var (a, b, s) = ParseOperands(lines, i, "mul");
            if (s > 0)
            {
                sum += a * b;
                i += s;
            }
            else
            {
                i++;
            }
            if (i >= nextDisable)
            {
                var n = lines.IndexOf(enable, i);
                if (n < 0)
                {
                    break;
                }
                i = n;
                nextDisable = lines.IndexOf(disable, i);
                if (nextDisable < 0)
                {
                    nextDisable = int.MaxValue;
                }
            }
        }
        return sum.ToString();
    }
}
