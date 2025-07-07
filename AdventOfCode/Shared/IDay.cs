namespace AdventOfCode;

public interface IDay
{
    public int GetDayNumber();
    public int GetYear();
    public static string part1Desc;
    private static string part2Desc;
    public static void SolveBothParts(bool useExample = false, bool useBrian = false) { }
    public static void SolvePart1(bool useExample = false, bool useBrian = false) { }
    public static void SolvePart2(bool useExample = false, bool useBrian = false) { }
}
