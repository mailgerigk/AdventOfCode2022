using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Launcher.Problems._2022;
internal class Day10
{
    public static string Part1(string[] lines)
    {
        var cycle = 0;
        var x = 1;
        var sum = 0;
        foreach (var line in lines)
        {
            incrementCycle();
            if (line.StartsWith("addx"))
            {
                incrementCycle();
                var num = int.Parse( Regex.Match(line, "[+-]?\\d+").Value);
                x += num;
            }
        }
        return sum.ToString();

        void incrementCycle()
        {
            cycle++;
            var isImportantCycle = cycle == 20 || (cycle > 20 && ((cycle - 20) % 40) == 0);
            if (isImportantCycle)
            {
                sum += cycle * x;
            }
        }
    }
    public static string Part2(string[] lines)
    {
        var cycle = 0;
        var x = 1;
        var crtLines = new List<string>();
        var crt = new char[40];
        clearCrt();
        foreach (var line in lines)
        {
            incrementCycle();
            if (line.StartsWith("addx"))
            {
                incrementCycle();
                var num = int.Parse( Regex.Match(line, "[+-]?\\d+").Value);
                x += num;
            }
        }
        return ASCII.Parse(crtLines);

        void incrementCycle()
        {
            cycle++;

            var crtIndex = (cycle - 1) % 40;
            if (Math.Abs(crtIndex - x) < 2)
                crt[crtIndex] = '#';

            if ((cycle % 40) == 0)
            {
                crtLines.Add(new(crt));
                clearCrt();
            }
        }
        void clearCrt()
        {
            for (int i = 0; i < crt.Length; i++)
            {
                crt[i] = '.';
            }
        }
    }
}
