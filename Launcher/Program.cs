using Launcher;

var results = ProblemRunner.Run(benchmark: false);
foreach (var result in results)
{
    Console.Write("[");
    Console.ForegroundColor = result.Result ? ConsoleColor.Green : ConsoleColor.Red;
    Console.Write(result.Result ? "O" : "X");
    Console.ResetColor();
    Console.WriteLine($"] {result.Year} {result.Day} {result.Level} {result.Time} @ {result.Repetitions} Reps");
}
Console.ReadLine();