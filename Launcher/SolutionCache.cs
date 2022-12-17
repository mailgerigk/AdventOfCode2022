using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Launcher;
internal class SolutionCache
{
    public enum PostResult
    {
        Right,
        Wrong,
        RateLimited,
    }

    static string SolutionBasePath => Path.Combine(Paths.BasePath, "solutions");
    static string BadSolutionsPath => Path.Combine(SolutionBasePath, "bad");

    static string GetLocalPath(int year, int day, int level) => Path.Combine(SolutionBasePath, year.ToString(), $"{day}_{level}.txt");
    static string GetBadLocalPath(int year, int day, int level) => Path.Combine(BadSolutionsPath, year.ToString(), $"{day}_{level}.txt");

    public static PostResult SubmitSolution(int year, int day, int level, string answer)
    {
        var badLocalPath = GetBadLocalPath(year, day, level);
        string[] badAnswers= new string[0];
        if (File.Exists(badLocalPath))
        {
            badAnswers = File.ReadAllLines(badLocalPath);
            if (badAnswers.Contains(answer))
                return PostResult.Wrong;
        }

        var localPath = GetLocalPath(year, day, level);
        if (File.Exists(localPath))
        {
            var correctAnswer = File.ReadAllText(localPath);
            return correctAnswer == answer ? PostResult.Right : PostResult.Wrong;
        }

        var input = Network.Post($"https://adventofcode.com/{year}/day/{day}/answer", level, answer);
        if (input.Contains("You don't seem to be solving the right level"))
        {
            var source = Network.Get($"https://adventofcode.com/{year}/day/{day}");
            var answers = Regex.Matches(source, "Your puzzle answer was <code>(.*?)</code>").Select(match => match.Groups[1].Value).ToArray();
            for (int i = 0; i < answers.Length; i++)
            {
                localPath = GetLocalPath(year, day, i + 1);
                new FileInfo(localPath).Directory!.Create();
                File.WriteAllText(localPath, answers[i]);
            }
            return SubmitSolution(year, day, level, answer);
        }
        if (input.Contains("That's the right answer"))
        {
            new FileInfo(localPath).Directory!.Create();
            File.WriteAllText(localPath, answer);
            return PostResult.Right;
        }
        else if (input.Contains("You gave an answer too recently;"))
        {
            return PostResult.RateLimited;
        }

        badAnswers = badAnswers.Union(new[] { answer }).ToArray();
        new FileInfo(badLocalPath).Directory!.Create();
        File.WriteAllLines(badLocalPath, badAnswers);
        return PostResult.Wrong;
    }
}
