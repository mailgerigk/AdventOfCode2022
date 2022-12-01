using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Launcher;
internal class SolutionCache
{
    static string SolutionBasePath => Path.Combine(Paths.BasePath, "solutions");
    static string BadSolutionsPath => Path.Combine(SolutionBasePath, "bad");

    static string GetLocalPath(int year, int day, int level) => Path.Combine(SolutionBasePath, year.ToString(), $"{day}_{level}.txt");
    static string GetBadLocalPath(int year, int day, int level) => Path.Combine(BadSolutionsPath, year.ToString(), $"{day}_{level}.txt");

    public static bool SubmitSolution(int year, int day, int level, string answer)
    {
        var badLocalPath = GetBadLocalPath(year, day, level);
        string[] badAnswers= new string[0];
        if (File.Exists(badLocalPath))
        {
            badAnswers = File.ReadAllLines(badLocalPath);
            if (badAnswers.Contains(answer))
                return false;
        }

        var localPath = GetLocalPath(year, day, level);
        if (File.Exists(localPath))
            return true;

        var input = Network.Post($"https://adventofcode.com/{year}/day/{day}/answer", level, answer);
        if (input.Contains("That's the right answer"))
        {
            new FileInfo(localPath).Directory!.Create();
            File.WriteAllText(localPath, answer);
            return true;
        }

        badAnswers = badAnswers.Union(new[] { answer }).ToArray();
        new FileInfo(badLocalPath).Directory!.Create();
        File.WriteAllLines(badLocalPath, badAnswers);
        return false;
    }
}
