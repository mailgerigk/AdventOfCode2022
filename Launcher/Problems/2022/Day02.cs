namespace Launcher.Problems._2022;
internal class Day02
{
    public static string Part1(string input)
    {
        static bool IsWin(char a, char b) => (a - 'A') == ((b - 'A' + 1) % 3);

        static int ShapeScore(char a) => a - 'A' + 1;

        static int ScoreLine(string line)
            => ShapeScore(line.Last()) +
            (IsWin(line.Last(), line.First())
            ? 6
            :
                IsWin(line.First(), line.Last())
                ? 0
                : 3);

        return input
            .Replace("X", "A")
            .Replace("Y", "B")
            .Replace("Z", "C")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(ScoreLine)
            .Sum()
            .ToString();
    }
    public static string Part2([RemoveEmpty] string[] lines)
    {
        static char Replacement(char a, char b) => b switch
        {
            'X' => (char)(((a - 'A' + 2) % 3) + 'X'),
            'Y' => (char)(a + 'X' - 'A'),
            'Z' => (char)(((a - 'A' + 1) % 3) + 'X'),
            _ => throw new ArgumentOutOfRangeException(),
        };

        static string ReplaceLine(string line) => $"{line.First()} {Replacement(line.First(), line.Last())}";

        return Part1(string.Join('\n', lines.Select(ReplaceLine)));
    }
}
