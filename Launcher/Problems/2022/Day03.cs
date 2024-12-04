using System.Numerics;

namespace Launcher.Problems._2022;
internal class Day03
{
    static int CharToPriority(char c) => c < 'a' ? 27 + c - 'A' : 1 + c - 'a';

    static ulong CompartmentToBitSet(IEnumerable<char> chars)
        => chars
        .Select(CharToPriority)
        .Select(i => 1ul << i)
        .Aggregate(0ul, (acc, mask) => acc | mask);

    public static string Part1([RemoveEmpty] string[] lines)
    {
        static int ScoreLine(string line)
            => BitOperations.TrailingZeroCount(CompartmentToBitSet(line[..(line.Length / 2)]) & CompartmentToBitSet(line[(line.Length / 2)..]));

        return lines.Select(ScoreLine).Sum().ToString();
    }

    public static string Part2([RemoveEmpty] string[] lines)
    {
        static int ScoreGroup(IEnumerable<string> lines)
            => BitOperations.TrailingZeroCount(lines.Select(CompartmentToBitSet).Aggregate(ulong.MaxValue, (acc, mask) => acc & mask));

        return lines
            .Select((line, i) => (line, i / 3))
            .GroupBy(t => t.Item2, t => t.line)
            .Select(ScoreGroup)
            .Sum()
            .ToString();
    }
}
