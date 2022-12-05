using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Launcher;
internal class ProblemRunner
{
    public record struct ProblemResult(int Year, int Day, int Level, bool Result, TimeSpan Time, int Repetitions);

    static (int year, int day)? ParseYearDay(string name)
    {
        if (!name.StartsWith("Launcher.Problems"))
            return null;
        var splits = name.Split('.');
        if (splits.Length < 2)
            return null;
        if (!splits.Last().StartsWith("Day") || splits.Last().Contains("+"))
            return null;
        var day = int.Parse(splits.Last().Substring("Day".Length));
        if (!splits[^2].StartsWith("_"))
            return null;
        var year = int.Parse(splits[^2].Substring("_".Length));
        return (year, day);
    }

    public static ImmutableArray<ProblemResult> Run(bool benchmark = true)
    {
        var tuples = new List<(Type type, MethodInfo method, int year, int day, int level)>();
        foreach (var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()))
        {
            if (ParseYearDay(type.FullName!) is (int year, int day))
            {
                for (int i = 1; i < 3; i++)
                {
                    if (type.GetMethod($"Part{i}") is MethodInfo method && method.IsStatic)
                        tuples.Add((type, method, year, day, i));
                }
            }
        }
        tuples = tuples.OrderBy(t => t.year).ThenBy(t => t.day).ThenBy(t => t.level).ToList();

        var results = ImmutableArray.CreateBuilder<ProblemResult>();
        var stopwatch = new Stopwatch();
        foreach (var (type, method, year, day, level) in tuples)
        {
            var input = InputCache.GetInput(year, day);
            var argumentList = new List<object>();
            foreach (var parameter in method.GetParameters())
            {
                argumentList.Add(InputTransformation.TryTransform(parameter, parameter.ParameterType, input)!);
            }
            var arguments = argumentList.ToArray();
            object? invokeReturn = null;
            stopwatch.Restart();
            int Repetitions = benchmark ? 10_000 : 1;
            for (int i = 0; i < Repetitions; i++)
            {
                invokeReturn = method.Invoke(null, arguments);
            }
            stopwatch.Stop();
            if (invokeReturn is string answer)
            {
                var result = new ProblemResult(year, day, level, SolutionCache.SubmitSolution(year, day, level, answer), stopwatch.Elapsed / Repetitions, Repetitions);
                results.Add(result);
            }
        }
        return results.ToImmutable();
    }
}
