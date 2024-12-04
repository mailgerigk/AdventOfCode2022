namespace Launcher.Problems._2023;
internal class Day01
{
    const string exampleInput = "1abc2\r\npqr3stu8vwx\r\na1b2c3d4e5f\r\ntreb7uchet";
    [Example(exampleInput, "142")]
    public static string Part1([RemoveEmpty] string[] lines)
    {
        return lines.Select(line => (line.First(char.IsDigit) - '0') * 10 + (line.Last(char.IsDigit) - '0')).Sum().ToString();
    }

    const string exampleInput2 = "two1nine\r\neightwothree\r\nabcone2threexyz\r\nxtwone3four\r\n4nineeightseven2\r\nzoneight234\r\n7pqrstsixteen";
    [Example(exampleInput2, "281")]
    public static string Part2([RemoveEmpty] string[] lines)
    {
        var digits = new List<string>{ "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        Func<string, string> replace = seed => digits.Aggregate(seed, (a, s) => a.Replace(s, $"{s}{digits.IndexOf(s) + 1}{s}"));
        return Part1(lines.Select(replace).ToArray());
    }
}
