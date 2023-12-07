using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.Problems._2023;
internal class Day02
{
    const string exampleInput = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green\r\nGame 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue\r\nGame 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red\r\nGame 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red\r\nGame 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";
    [Example(exampleInput, "8")]
    public static string Part1([RemoveEmpty] string[] lines)
    {
        var bag = new Dictionary<string, int>
        {
            {"red", 12 },
            {"green", 13 },
            {"blue", 14 },
        };
        var sum = 0l;
        foreach (var line in lines)
        {
            var splits = line.Split(':');
            var id = splits[0].Replace("Game", "").Trim().ParseInt();
            var sets = splits[1].Split(";");
            if (IsPossible())
            {
                sum += id;
            }

            bool IsPossible()
            {
                foreach (var set in sets)
                {
                    var colors = set.Split(',');
                    foreach (var color in colors)
                    {
                        var splits = color.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        var num = splits[0].ParseInt();
                        var colorName = splits[1];
                        var bagValue = bag.GetValueOrDefault(colorName);
                        if (num > bagValue)
                            return false;
                    }
                }
                return true;
            }
        }
        return sum.ToString();
    }

    [Example(exampleInput, "2286")]
    public static string Part2([RemoveEmpty] string[] lines)
    {
        var sum = 0l;
        foreach (var line in lines)
        {
            var bag = new Dictionary<string, long>();

            var splits = line.Split(':');
            var id = splits[0].Replace("Game", "").Trim().ParseInt();
            var sets = splits[1].Split(";");
            foreach (var set in sets)
            {
                var colors = set.Split(',');
                foreach (var color in colors)
                {
                    splits = color.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var num = splits[0].ParseInt();
                    var colorName = splits[1];
                    var bagValue = bag.GetValueOrDefault(colorName, int.MinValue);
                    if(bagValue == int.MinValue)
                    {
                        bag.Add(colorName, num);
                    }
                    else if(bagValue < num)
                    {
                        bag[colorName] = num;
                    }
                }
            }

            sum += bag.Aggregate(1l, (a, k) => a * k.Value);
        }
        return sum.ToString();
    }
}
