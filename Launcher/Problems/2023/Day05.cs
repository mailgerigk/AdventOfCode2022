using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.Problems._2023;
internal class Day05
{
    const string exampleInput = "seeds: 79 14 55 13\r\n\r\nseed-to-soil map:\r\n50 98 2\r\n52 50 48\r\n\r\nsoil-to-fertilizer map:\r\n0 15 37\r\n37 52 2\r\n39 0 15\r\n\r\nfertilizer-to-water map:\r\n49 53 8\r\n0 11 42\r\n42 0 7\r\n57 7 4\r\n\r\nwater-to-light map:\r\n88 18 7\r\n18 25 70\r\n\r\nlight-to-temperature map:\r\n45 77 23\r\n81 45 19\r\n68 64 13\r\n\r\ntemperature-to-humidity map:\r\n0 69 1\r\n1 0 69\r\n\r\nhumidity-to-location map:\r\n60 56 37\r\n56 93 4";
    [Example(exampleInput, "35")]
    public static string Part1([RemoveEmpty] string[] lines)
    {
        var seeds = lines.First().ParseInts();
        var maps = new List<(List<long> dst, List<long> src, List<long> lengths)>();
        {
            var map = (dst: new List<long>(), src: new List<long>(), lengths: new List<long>());
            foreach (var line in lines.Skip(1))
            {
                var ints = line.ParseInts(3);
                if (!ints.Any())
                {
                    maps.Add(map);
                    map = (dst: new List<long>(), src: new List<long>(), lengths: new List<long>());
                }
                else
                {
                    map.dst.Add(ints[0]);
                    map.src.Add(ints[1]);
                    map.lengths.Add(ints[2]);
                }
            }
            maps.Add(map);
        }

        long minValue = long.MaxValue;
        foreach (var seed in seeds)
        {
            long value = seed;
            foreach (var map in maps)
            {
                for (int i = 0; i < map.dst.Count; i++)
                {
                    var dst = map.dst[i];
                    var src = map.src[i];
                    var length = map.lengths[i];

                    if (src <= value && value < src + length)
                    {
                        value = dst + value - src;
                        break;
                    }
                }
            }
            minValue = Math.Min(minValue, value);
        }

        return minValue.ToString();
    }

    /*
    [Example(exampleInput, "46")]
    public static string Part2([RemoveEmpty] string[] lines)
    {
        return "46";
    }
    */
}
