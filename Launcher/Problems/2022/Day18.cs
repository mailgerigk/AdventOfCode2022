using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Launcher.Problems._2022;
internal class Day18
{
    const string exampleInput = "2,2,2\r\n1,2,2\r\n3,2,2\r\n2,1,2\r\n2,3,2\r\n2,2,1\r\n2,2,3\r\n2,2,4\r\n2,2,6\r\n1,2,5\r\n3,2,5\r\n2,1,5\r\n2,3,5";

    [Example(exampleInput, "64")]
    public static string Part1(List<List<int>> data)
    {
        var set = data.Select(list => (x:list[0], y:list[1], z:list[2])).ToHashSet();
        var range = Enumerable.Range(-1, 3);
        var offsets =
            from x in range
            from y in range
            from z in range
            where (Math.Abs(x) + Math.Abs(y) + Math.Abs(z)) == 1
            select (x, y, z);


        var sides = 0;
        foreach (var (cubeX, cubeY, cubeZ) in set)
        {
            sides += 6;
            foreach (var offset in offsets)
            {
                if (set.Contains((cubeX + offset.x, cubeY + offset.y, cubeZ + offset.z)))
                {
                    sides--;
                }
            }
        }
        return sides.ToString();
    }

    [Example(exampleInput, "58")]
    public static string Part2(List<List<int>> data)
    {
        var set = data.Select(list => (x:list[0], y:list[1], z:list[2])).ToHashSet();
        var range = Enumerable.Range(-1, 3);
        var offsets =
            from x in range
            from y in range
            from z in range
            where (Math.Abs(x) + Math.Abs(y) + Math.Abs(z)) == 1
            select (x, y, z);

        var min = (x:set.Min(t => t.x), y:set.Min(t => t.y), z:set.Min(t => t.z));
        var max = (x:set.Max(t => t.x), y:set.Max(t => t.y), z:set.Max(t => t.z));

        var bounds = (min:(x:min.x - 1, y:min.y - 1, z:min.z - 1), max:(x:max.x + 1, y:max.y + 1, z: max.z + 1));
        bool IsInBounds((int x, int y, int z) p)
            => bounds.min.x <= p.x && p.x <= bounds.max.x
            && bounds.min.y <= p.y && p.y <= bounds.max.y
            && bounds.min.z <= p.z && p.z <= bounds.max.z;

        var water = new HashSet<(int x, int y, int z)>();
        var queue = new Queue<(int x, int y, int z)>();
        queue.Enqueue((min.x - 1, min.y - 1, min.z - 1));

        while(queue.Any())
        {
            var (cubeX, cubeY, cubeZ) = queue.Dequeue();
            foreach (var offset in offsets)
            {
                var point = (cubeX + offset.x, cubeY + offset.y, cubeZ + offset.z);
                if (!IsInBounds(point) || set.Contains(point))
                    continue;
                if(water.Add(point))
                    queue.Enqueue(point);
            }
        }

        var sides = 0;
        foreach (var (cubeX, cubeY, cubeZ) in set)
        {
            sides += 6;
            foreach (var offset in offsets)
            {
                var point = (cubeX + offset.x, cubeY + offset.y, cubeZ + offset.z);
                if (set.Contains(point) || !water.Contains(point))
                {
                    sides--;
                }
            }
        }
        return sides.ToString();
    }
}
