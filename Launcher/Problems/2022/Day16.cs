using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        var flowPositive = Enumerable.Range(0,flow.Length).Where(i => flow[i] > 0).ToArray();
        var first = nodes.ToList().IndexOf("AA");
        var opened = new BitArray(nodes.Length);
        var maxScore = int.MinValue;
        for (int i = 0; i < 1 << flowPositive.Length; i++)
        {
            for (int j = 0; j < flowPositive.Length; j++)
            {
                opened[flowPositive[j]] = (i & (1 << j)) > 0;
            }
            var a = Solve(26,first, opened);
            for (int j = 0; j < flowPositive.Length; j++)
            {
                opened[flowPositive[j]] = !((i & (1 << j)) > 0);
            }
            var b = Solve(26, first, opened);
            var sum = a+ b;
            maxScore = Math.Max(maxScore, sum);
        }

        return maxScore.ToString();

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
}
