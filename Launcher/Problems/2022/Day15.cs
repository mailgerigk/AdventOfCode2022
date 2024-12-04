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

        for (int i = 0; i < sensors.Length; i++)
        {
            var sensor = sensors[i];
            var distance = distances[i];
            var delta = distance + 1;

            var left = (x:sensor.x - delta, y:sensor.y);
            var right = (x:sensor.x + delta, y:sensor.y);
            var top = (x:sensor.x, y:sensor.y + delta);
            var bottom = (x:sensor.x, y:sensor.y - delta);

            var lines = new[]
            {
                (left, top),
                (left, bottom),
                (top, right),
                (bottom, right),
            };

            foreach (var line in lines)
            {
                var (from, to) = line;
                var diff = (x:from.x < to.x ? 1 : - 1, y:from.y < to.y ? 1 : -1);

                var current = from;
                var count = Math.Abs(from.x - to.x);
                while (count > 0)
                {
                    if (min <= current.x && current.x <= max && min <= current.y && current.y <= max)
                    {
                        for (int j = 0; j < sensors.Length; j++)
                        {
                            var sensorDistance = Distance(sensors[j], current);
                            if (sensorDistance <= distances[j])
                            {
                                var gamma = Math.Max( (distances[j] - sensorDistance) / 2 - 1, 0);
                                gamma = Math.Min(gamma, count - 1);
                                count -= gamma;
                                current = (current.x + diff.x * gamma, current.y + diff.y * gamma);
                                goto next;
                            }
                        }
                        return ((ulong)current.x * 4000000ul + (ulong)current.y).ToString();
                    }
                    else
                    {
                        var gamma = Math.Min(Distance((min, min), current), Distance((max, max), current));
                        gamma = Math.Min(gamma, count - 1);
                        count -= gamma;
                        current = (current.x + diff.x * gamma, current.y + diff.y * gamma);
                    }
                next:
                    current = (current.x + diff.x, current.y + diff.y);
                    count -= 1;
                }
            }
        }
        throw new InvalidDataException();

        static int Distance((int x, int y) a, (int x, int y) b) => Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
    }
}
