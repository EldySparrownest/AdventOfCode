namespace AdventOfCode;

public class Day
{
    public int Year { get; set; }
    public int DayNumber { get; set; }

    public string[] LoadInput(bool useExample = false, bool useBrian = false)
        => Universal.LoadInput(Year, DayNumber, useExample, useBrian);

    public string[] GetDayPuzzlePart(int part) 
        => Universal.GetPuzzle(Year, DayNumber, part);

    public void PartDescription(int part)
    {
        var partDesc = string.Join("\n", GetDayPuzzlePart(part));
        Console.Write(partDesc);
        Console.WriteLine();
        Console.WriteLine();
    }

    public virtual void SolveBothParts(bool useExample = false, bool useBrian = false, bool printIntros = false) { }
    public virtual void SolvePart1(bool useExample = false, bool useBrian = false) { }
    public virtual void SolvePart2(bool useExample = false, bool useBrian = false) { }
}
