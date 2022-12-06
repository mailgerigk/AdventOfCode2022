﻿using Launcher;

using System.Diagnostics;

var stopwatch = Stopwatch.StartNew();
var results = ProblemRunner.Run(benchmark: false);
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
    Console.WriteLine($"] {result.Year} {result.Day} {result.Level} {result.Time} @ {result.Repetitions} Reps");
}
RainbowConsoleText.AnimateLine($"[Total time: {stopwatch.Elapsed}]");
