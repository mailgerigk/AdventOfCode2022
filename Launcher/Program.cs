using Launcher;

using System.Diagnostics;

var stopwatch = Stopwatch.StartNew();
var results = ProblemRunner.Run(minYear: 2023, runWithExample: true, benchmark: false);
stopwatch.Stop();

foreach (var result in results)
{
    Console.Write("[");
    Console.ForegroundColor = result.Result switch
    {
        SolutionCache.PostResult.Right => ConsoleColor.Green,
        SolutionCache.PostResult.Wrong => ConsoleColor.Red,
        SolutionCache.PostResult.RateLimited => ConsoleColor.Yellow,
        _ => throw new ArgumentOutOfRangeException(),
    };
    string text = result.Result switch
    {
        SolutionCache.PostResult.Right => "O",
        SolutionCache.PostResult.Wrong => "X",
        SolutionCache.PostResult.RateLimited => "?",
        _ => throw new ArgumentOutOfRangeException(),
    };
    Console.Write(text);
    Console.ResetColor();
    var exampleString = result.IsExample ? " Example" : "";
    Console.WriteLine($"] {result.Year} {result.Day:00} {result.Level} {result.Time} @ {result.Repetitions} Reps {exampleString}");
}
RainbowConsoleText.AnimateLine($"[Total time: {stopwatch.Elapsed}]");
