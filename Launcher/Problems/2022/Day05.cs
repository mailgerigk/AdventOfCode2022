namespace Launcher.Problems._2022;
internal class Day05
{
    static string Solve(string[] lines, List<List<int>> ints, bool pickupMultiple)
    {
        int slotsLine = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            if (!lines[i].Any())
            {
                slotsLine = i - 1;
                break;
            }
        }

        var cargoSlotCount = ints.Max(ints => ints.Count);
        var cargoSlots = new List<Stack<char>>();
        for (int i = 0; i < cargoSlotCount; i++)
            cargoSlots.Add(new());

        for (int i = slotsLine - 1; i >= 0; i--)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (char.IsLetter(lines[i][j]))
                {
                    cargoSlots[j / 4].Push(lines[i][j]);
                }
            }
        }

        foreach (var list in ints)
        {
            if (list is [var count, var from, var to])
            {
                if (pickupMultiple)
                {
                    var temp = new List<char>();
                    for (int i = 0; i < count; i++)
                    {
                        temp.Add(cargoSlots[from - 1].Pop());
                    }
                    temp.Reverse();
                    foreach (var item in temp)
                    {
                        cargoSlots[to - 1].Push(item);
                    }
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        cargoSlots[to - 1].Push(cargoSlots[from - 1].Pop());
                    }
                }
            }
        }

        return cargoSlots.Aggregate("", (acc, s) => acc + s.Peek());
    }

    public static string Part1(string[] lines, List<List<int>> ints) => Solve(lines, ints, false);
    public static string Part2(string[] lines, List<List<int>> ints) => Solve(lines, ints, true);
}
