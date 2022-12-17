using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Launcher;
internal class InputTransformation
{
    public static object? TryTransform(ParameterInfo? parameter, Type targetType, string input)
    {
        StringSplitOptions splitOptions= StringSplitOptions.None;
        if (parameter is not null && parameter.GetCustomAttribute<RemoveEmptyAttribute>() is not null)
            splitOptions |= StringSplitOptions.RemoveEmptyEntries;

        var trim = false;
        if(parameter is not null && parameter.GetCustomAttribute<TrimAttribute>() is not null)
            trim = true;

        if (targetType == typeof(string))
        {
            return trim ? input.Trim() : input;
        }
        if (targetType == typeof(string[]))
        {
            return input.Split('\n', StringSplitOptions.TrimEntries | splitOptions);
        }
        if (targetType == typeof(int[]))
        {
            return input.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        }
        if(targetType == typeof(int[][]))
        {
            return input.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(line => line.Select(c => c - '0').ToArray()).ToArray();
        }
        if (targetType == typeof(List<int[]>))
        {
            var result = new List<int[]>();
            var entry = new List<int>();
            foreach (var item in input.Split('\n', StringSplitOptions.TrimEntries))
            {
                if (string.IsNullOrEmpty(item))
                {
                    result.Add(entry.ToArray());
                    entry.Clear();
                }
                else
                {
                    entry.Add(int.Parse(item));
                }
            }
            result.Add(entry.ToArray());
            return result;
        }
        if (targetType == typeof(List<List<int>>))
        {
            var result = new List<List<int>>();
            foreach (var item in input.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
            {
                var entry = new List<int>();
                foreach (Match match in Regex.Matches(item, "[+-]?\\d+"))
                {
                    entry.Add(int.Parse(match.Value));
                }
                result.Add(entry);
            }
            return result;
        }
        if(targetType == typeof(float[]))
        {
            return input.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(float.Parse).ToArray();
        }
        if (targetType == typeof(List<float[]>))
        {
            var result = new List<float[]>();
            var entry = new List<float>();
            foreach (var item in input.Split('\n', StringSplitOptions.TrimEntries))
            {
                if (string.IsNullOrEmpty(item))
                {
                    result.Add(entry.ToArray());
                    entry.Clear();
                }
                else
                {
                    entry.Add(float.Parse(item));
                }
            }
            result.Add(entry.ToArray());
            return result;
        }
        if (targetType == typeof(List<List<float>>))
        {
            var result = new List<List<float>>();
            foreach (var item in input.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
            {
                var entry = new List<float>();
                foreach (Match match in Regex.Matches(item, "[+-]?%d+(\\.%d+)?"))
                {
                    entry.Add(float.Parse(match.Value));
                }
                result.Add(entry);
            }
            return result;
        }
        return null;
    }
}
