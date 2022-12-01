using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Launcher;
internal class ProblemRunner
{
    public record struct ProblemResult(int Year, int Day, int Level, bool Result);

    public static ImmutableArray< ProblemResult> Run()
    {
        var methodPairs = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .SelectMany(type => type.GetMethods())
            .Where(method => method.IsStatic)
            .Where(method => method.GetCustomAttribute<DayLevelAttribute>() is not null)
            .Select(method => (method, method.GetCustomAttribute<DayLevelAttribute>()!))
            .OrderBy(pair => pair.Item2.Year)
            .ThenBy(pair => pair.Item2.Day)
            .ThenBy(pair => pair.Item2.Level)
            .ToList();

        var results = ImmutableArray.CreateBuilder<ProblemResult>();
        foreach (var (method, attribute) in methodPairs)
        {
            var input = InputCache.GetInput(attribute.Year, attribute.Day);
            var invokeReturn = method.Invoke(null, new[] { input });
            if(invokeReturn is string answer)
            {
                var result = new ProblemResult(attribute.Year, attribute.Day, attribute.Level, SolutionCache.SubmitSolution(attribute.Year, attribute.Day, attribute.Level, answer));
                results.Add(result);
            }
        }
        return results.ToImmutable();
    }
}
