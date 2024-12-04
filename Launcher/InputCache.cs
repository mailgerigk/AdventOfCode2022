namespace Launcher;
internal class InputCache
{
    static string InputBasePath => Path.Combine(Paths.BasePath, "inputs");

    static string GetLocalPath(int year, int day) => Path.Combine(InputBasePath, year.ToString(), $"{day}.txt");

    static string DownloadInput(int year, int day) => Network.Get($"https://adventofcode.com/{year}/day/{day}/input");

    public static string GetInput(int year, int day)
    {
        var localPath = GetLocalPath(year, day);
        if (File.Exists(localPath))
            return File.ReadAllText(localPath);

        var input = DownloadInput(year, day);
        new FileInfo(localPath).Directory!.Create();
        File.WriteAllText(localPath, input);
        return input;
    }
}
