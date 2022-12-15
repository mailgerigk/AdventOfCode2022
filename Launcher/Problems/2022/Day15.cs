using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.Problems._2022;
internal class Day15
{
    const string exampleInput = "Sensor at x=2, y=18: closest beacon is at x=-2, y=15\r\nSensor at x=9, y=16: closest beacon is at x=10, y=16\r\nSensor at x=13, y=2: closest beacon is at x=15, y=3\r\nSensor at x=12, y=14: closest beacon is at x=10, y=16\r\nSensor at x=10, y=20: closest beacon is at x=10, y=16\r\nSensor at x=14, y=17: closest beacon is at x=10, y=16\r\nSensor at x=8, y=7: closest beacon is at x=2, y=10\r\nSensor at x=2, y=0: closest beacon is at x=2, y=10\r\nSensor at x=0, y=11: closest beacon is at x=2, y=10\r\nSensor at x=20, y=14: closest beacon is at x=25, y=17\r\nSensor at x=17, y=20: closest beacon is at x=21, y=22\r\nSensor at x=16, y=7: closest beacon is at x=15, y=3\r\nSensor at x=14, y=3: closest beacon is at x=15, y=3\r\nSensor at x=20, y=1: closest beacon is at x=15, y=3";

    [Example(exampleInput, "26")]
    public static string Part1(bool isExample, List<List<int>> data)
    {
        var sensors = data.Select(list => (x:list[0], y:list[1])).ToArray();
        var beacons = data.Select(list => (x:list[2], y:list[3])).ToArray();
        var distances = sensors.Select((s, i) => Distance(s, beacons[i])).ToArray();

        var minX = sensors.Select((s, i) => s.x - distances[i]).Min();
        var maxX = sensors.Select((s, i) => s.x + distances[i]).Max();

        var position = (x:0, y:2000000);
        if (isExample)
        {
            position = (x: 0, y: 10);
        }
        var sum = -beacons.Distinct().Count(b => b.y == position.y);

        for (int i = minX; i <= maxX;)
        {
            position = (i, position.y);
            var stepSize = int.MaxValue;
            var isLess = false;
            for (int j = 0; j < sensors.Length; j++)
            {
                var distance = Distance(sensors[j], position);
                if (distance <= distances[j])
                {
                    isLess = true;
                }
                stepSize = Math.Min(stepSize, Math.Abs(distance - distances[j]));
            }
            stepSize = Math.Max(stepSize, 1);
            if (isLess)
            {
                sum += stepSize;
            }
            i += stepSize;
        }
        return sum.ToString();

        static int Distance((int x, int y) a, (int x, int y) b) => Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
    }

    [Example(exampleInput, "56000011")]
    public static string Part2(bool isExample, List<List<int>> data)
    {
        var sensors = data.Select(list => (x:list[0], y:list[1])).ToArray();
        var beacons = data.Select(list => (x:list[2], y:list[3])).ToArray();
        var distances = sensors.Select((s, i) => Distance(s, beacons[i])).ToArray();

        var min = 0;
        var max = 4000000;

        if (isExample)
        {
            max = 20;
        }

        var ranges = new (int a, int b)[sensors.Length];
        for (int y = min; y < max; ++y)
        {
            for (int i = 0; i < sensors.Length; i++)
            {
                ranges[i] = (int.MaxValue, int.MaxValue);
                var deltaX = distances[i] - Math.Abs(sensors[i].y - y);
                if (deltaX <= 0)
                    continue;

                ranges[i] = (sensors[i].x - deltaX, sensors[i].x + deltaX);
            }

            for (int i = 0; i < ranges.Length; i++)
            {
                for (int j = 0; j < ranges.Length; j++)
                {
                    if (ranges[i].a == int.MaxValue && ranges[j].a != int.MaxValue)
                        ranges[i] = ranges[j];
                    else if (ranges[j].a == int.MaxValue && ranges[i].a != int.MaxValue)
                        ranges[j] = ranges[i];

                    var r = Merge(ranges[i], ranges[j]);
                    if (r.a == int.MaxValue)
                        continue;
                    ranges[i] = r;
                    ranges[j] = r;
                    if (r.a <= min && max <= r.b)
                        goto next;
                }
            }
            if (ranges[0].a > min || ranges[0].b < max)
            {
                var x= ranges[0].a <= min ? ranges[0].b + 1 : ranges[0].a - 1;
                return ((ulong)x * 4000000ul + (ulong)y).ToString();
            }
        next:
            continue;
        }
        throw new InvalidDataException();

        static int Distance((int x, int y) a, (int x, int y) b) => Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        static (int a, int b) Merge((int a, int b) a, (int a, int b) b) => (a.a <= b.a && b.a <= a.b) ? (a.a, Math.Max(a.b, b.b)) : (b.a <= a.a && a.a <= b.b) ? (b.a, Math.Max(b.b, a.b)) : (int.MaxValue, int.MaxValue);
    }
}
