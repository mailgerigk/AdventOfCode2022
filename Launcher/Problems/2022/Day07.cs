using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.Problems._2022;
internal class Day07
{
    static (HashSet<string> dirs, Dictionary<string, int> files) ParseFilesystem(string[] lines)
    {
        var dirs = new HashSet<string>();
        var files = new Dictionary<string, int>();
        var cwd = "/";
        foreach (var line in lines)
        {
            dirs.Add(cwd);

            if (line.StartsWith("$ cd "))
            {
                string arg = line.Substring("$ cd ".Length);
                if (arg == "/")
                    cwd = arg;
                else if (arg == "..")
                {
                    cwd = cwd[..^1];
                    cwd = cwd.Substring(0, cwd.LastIndexOf('/') + 1);
                }
                else
                    cwd += $"{arg}/";
            }
            else if (!line.StartsWith("dir") && !line.StartsWith("$"))
            {
                var size = int.Parse(line.Substring(0, line.IndexOf(' ')));
                var name = line.Substring(line.IndexOf(' ') + 1);
                files.Add($"{cwd}{name}", size);
            }
        }
        return (dirs, files);
    }

    public static string Part1([RemoveEmpty] string[] lines)
    {
        var (dirs, files) = ParseFilesystem(lines);
        var atmostDirSize = 100000;
        return dirs.Select(d => files.Sum(kv => kv.Key.StartsWith(d) ? kv.Value : 0)).Where(n => n < atmostDirSize).Sum().ToString();
    }

    public static string Part2([RemoveEmpty] string[] lines)
    {
        var (dirs, files) = ParseFilesystem(lines);
        var totalSize = 70000000;
        var freeSizeWanted = 30000000;
        var sizeInUse = files.Sum(kv => kv.Value);
        var sizeToFree = Math.Max(0, freeSizeWanted - (totalSize - sizeInUse));
        return dirs.Select(d => files.Sum(kv => kv.Key.StartsWith(d) ? kv.Value : 0)).Where(n => n >= sizeToFree).Min().ToString();
    }
}
