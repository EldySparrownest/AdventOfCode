namespace AdventOfCode;

public static class Universal
{
    public static string ProjectRootDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
    public static string InputsDirName = "Inputs";
    public static string PuzzlesDirName = "Puzzles";

    public static string WebsiteLink = "https://adventofcode.com/";
    public static string GetPuzzleLink(int year, int day) => $"{WebsiteLink}/{year}/day/{day}";
    public static string HowToGetMissingFiles(int year, int day)
        => $"""
        Puzzle itself as well as its inputs can be obtained here:
        {GetPuzzleLink(year, day)}
        They are not included in this repo, because the creator of Advent of Code has specifically requested for neither to be distributed outside the website {WebsiteLink} and I intend to be respectful of their wishes.
        """;

    public static string[] LoadInput(int year, int day, bool useExample = false, bool useBrian = false)
    {
        var brian = useBrian ? "Brian" : "";
        var example = useExample ? "Example" : "";
        var dayString = day < 10 ? $"0{day}" : $"{day}";
        var fileName = $"Day{dayString}{brian}{example}.txt";
        string inputFilePath = Path.Combine(ProjectRootDir, InputsDirName, year.ToString(), fileName);
        try
        {
            return File.ReadLines(inputFilePath).ToArray();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine();
            Console.WriteLine(HowToGetMissingFiles(year, day));
            return new string[0];
        }
    }

    public static string[] GetPuzzle(int year, int day, int part)
    {
        var dayString = day < 10 ? $"0{day}" : $"{day}";
        var fileName = $"Day{dayString}Part{part}.txt";
        string puzzleFilePath = Path.Combine(ProjectRootDir, PuzzlesDirName, year.ToString(), fileName);
        try
        {
            return File.ReadLines(puzzleFilePath).ToArray();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine();
            Console.WriteLine(HowToGetMissingFiles(year, day));
            return new string[0];
        }
    }

    public static void Solve<T>(bool useExample = false, bool useBrian = false, bool printIntros = false) where T : Day, new()
    {
        var day = new T();
        Console.WriteLine($"Advent of Code {day.Year} - Day {day.DayNumber}");
        day.SolveBothParts(useExample, useBrian, printIntros);
        Console.ReadLine();
    }

    public static void WriteSolutionSection<T>(int part, string sentence, T result)
    {
        Console.WriteLine($"--- Part {part} solution ---");
        Console.WriteLine(sentence);
        Console.WriteLine($"{result}");
        Console.WriteLine();
    }

    public static void Print(this IEnumerable<string> strings)
    {
        foreach (var line in strings)
        {
            Console.WriteLine(line);
        }
    }

    public static T[] Populate<T>(this T[] arr, T value)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = value;
        }
        return arr;
    }
}
