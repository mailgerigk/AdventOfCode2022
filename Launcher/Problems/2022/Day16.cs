using System.Collections;
using System.Text.RegularExpressions;

namespace Launcher.Problems._2022;
internal class Day16
{
    const string exampleInput = "Valve AA has flow rate=0; tunnels lead to valves DD, II, BB\r\nValve BB has flow rate=13; tunnels lead to valves CC, AA\r\nValve CC has flow rate=2; tunnels lead to valves DD, BB\r\nValve DD has flow rate=20; tunnels lead to valves CC, AA, EE\r\nValve EE has flow rate=3; tunnels lead to valves FF, DD\r\nValve FF has flow rate=0; tunnels lead to valves EE, GG\r\nValve GG has flow rate=0; tunnels lead to valves FF, HH\r\nValve HH has flow rate=22; tunnel leads to valve GG\r\nValve II has flow rate=0; tunnels lead to valves AA, JJ\r\nValve JJ has flow rate=21; tunnel leads to valve II";

    [Example(exampleInput, "1651")]
    public static string Part1([RemoveEmpty] string[] lines, List<List<int>> data)
    {
        var connections = lines.Select(line => Regex.Matches(line, "[A-Z][A-Z]").Select(match => match.Value)).ToArray();
        var nodes = connections.Select(array => array.First()).ToArray();
        var flow = data.Select(list => list.First()).ToArray();

        var distances = new int[nodes.Length, nodes.Length];
        for (int startNodeIndex = 0; startNodeIndex < nodes.Length; startNodeIndex++)
        {
            for (int endNodeIndex = 0; endNodeIndex < nodes.Length; endNodeIndex++)
            {
                var distance = startNodeIndex == endNodeIndex ? 0 : (connections[startNodeIndex].Contains(nodes[endNodeIndex]) ? 1 : short.MaxValue);
                distances[startNodeIndex, endNodeIndex] = distance;
            }
        }
        var changes = true;
        do
        {
            changes = false;
            for (int startNodeIndex = 0; startNodeIndex < nodes.Length; startNodeIndex++)
            {
                for (int midNodeIndex = 0; midNodeIndex < nodes.Length; midNodeIndex++)
                {
                    for (int endNodeIndex = 0; endNodeIndex < nodes.Length; endNodeIndex++)
                    {
                        if (distances[startNodeIndex, midNodeIndex] + distances[midNodeIndex, endNodeIndex] < distances[startNodeIndex, endNodeIndex])
                        {
                            distances[startNodeIndex, endNodeIndex] = distances[startNodeIndex, midNodeIndex] + distances[midNodeIndex, endNodeIndex];
                            changes = true;
                        }
                    }
                }
            }
        }
        while (changes);

        var score = Solve(30, nodes.ToList().IndexOf("AA"));
        return score.ToString();

        int Solve(int timeLeft, int nodeIndex, BitArray? opened = null)
        {
            opened ??= new(nodes.Length);
            var possibleNodes = Enumerable.Range(0, nodes.Length).Where(i => flow[i] > 0 && !opened[i]).ToArray();
            var bestScore = int.MinValue;
            foreach (var node in possibleNodes)
            {
                var distance = distances[nodeIndex, node];
                var newTime = timeLeft - 1 - distance;
                if (newTime >= 0)
                {
                    opened[node] = true;
                    var score = newTime * flow[node] + Solve(newTime, node, opened);
                    bestScore = Math.Max(bestScore, score);
                    opened[node] = false;
                }
            }
            return Math.Max(bestScore, 0);
        }
    }

    [Example(exampleInput, "1707")]
    public static string Part2([RemoveEmpty] string[] lines, List<List<int>> data)
    {
        var connections = lines.Select(line => Regex.Matches(line, "[A-Z][A-Z]").Select(match => match.Value)).ToArray();
        var nodes = connections.Select(array => array.First()).ToArray();
        var flow = data.Select(list => list.First()).ToArray();

        var distances = new int[nodes.Length, nodes.Length];
        for (int startNodeIndex = 0; startNodeIndex < nodes.Length; startNodeIndex++)
        {
            for (int endNodeIndex = 0; endNodeIndex < nodes.Length; endNodeIndex++)
            {
                var distance = startNodeIndex == endNodeIndex ? 0 : (connections[startNodeIndex].Contains(nodes[endNodeIndex]) ? 1 : short.MaxValue);
                distances[startNodeIndex, endNodeIndex] = distance;
            }
        }
        var changes = true;
        do
        {
            changes = false;
            for (int startNodeIndex = 0; startNodeIndex < nodes.Length; startNodeIndex++)
            {
                for (int midNodeIndex = 0; midNodeIndex < nodes.Length; midNodeIndex++)
                {
                    if (startNodeIndex == midNodeIndex)
                        continue;

                    for (int endNodeIndex = 0; endNodeIndex < nodes.Length; endNodeIndex++)
                    {
                        if (startNodeIndex == endNodeIndex)
                            continue;

                        if (distances[startNodeIndex, midNodeIndex] + distances[midNodeIndex, endNodeIndex] < distances[startNodeIndex, endNodeIndex])
                        {
                            distances[startNodeIndex, endNodeIndex] = distances[startNodeIndex, midNodeIndex] + distances[midNodeIndex, endNodeIndex];
                            distances[endNodeIndex, startNodeIndex] = distances[startNodeIndex, endNodeIndex];
                            changes = true;
                        }
                    }
                }
            }
        }
        while (changes);

        var first = nodes.ToList().IndexOf("AA");
        var flowPositive = flow.Select((f, i) => i).Where(i => flow[i] > 0).Union(new []{ first}).ToArray();
        var bitmaskMax = 1 << flowPositive.Length;
        var scores = new int[26, flowPositive.Length, bitmaskMax];
        for (int time = 1; time < 26; time++)
        {
            Parallel.For(0, flowPositive.Length, (node) =>
            {
                var nodemask = 1 << node;
                for (int bitmask = 0; bitmask < bitmaskMax; bitmask++)
                {
                    var score = scores[time, node, bitmask];
                    if ((bitmask & nodemask) > 0)
                    {
                        score = Math.Max(score, scores[time - 1, node, bitmask - nodemask] + flow[flowPositive[node]] * time);
                    }
                    for (int adjacent = 0; adjacent < flowPositive.Length; adjacent++)
                    {
                        var distance = distances[flowPositive[node], flowPositive[adjacent]];
                        if (distance < time)
                        {
                            score = Math.Max(score, scores[time - distance, adjacent, bitmask]);
                        }
                    }
                    scores[time, node, bitmask] = score;
                }
            });
        }

        var aa = flowPositive.Length - 1;
        var bestScore = int.MinValue; ;
        for (int bitmaskHuman = 0; bitmaskHuman < bitmaskMax; bitmaskHuman++)
        {
            var bitmaskElephant = (bitmaskMax - 1) & ~bitmaskHuman;
            bestScore = Math.Max(bestScore, scores[25, aa, bitmaskHuman] + scores[25, aa, bitmaskElephant]);
        }
        return bestScore.ToString();
    }
}
