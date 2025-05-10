using System.Diagnostics;

namespace AdventOfCode.Y2024;

public class Day04 : Day
{

    public Day04()
    {
        Year = 2024;
        DayNumber = 4;
    }

    public int ProcessAsPart1(IEnumerable<string> input)
    {
        int totalFinds = 0;
        var wordSearch = input.ToList();
        for (int r = 0; r < wordSearch.Count; r++)
        {
            for (int c = 0; c < wordSearch[r].Length; c++)
            {
                if (wordSearch[r][c] == 'X')
                    totalFinds += wordSearch.SearchInEveryDirection(new Coords(r, c));
            }
        }

        return totalFinds;
    }

    public int ProcessAsPart2(IEnumerable<string> input)
    {
        int totalFinds = 0;
        var wordSearch = input.ToList();
        for (int r = 1; r < wordSearch.Count; r++)
        {
            for (int c = 1; c < wordSearch[r].Length; c++)
            {
                if (wordSearch[r][c] == 'A' && wordSearch.IsXMAS(new Coords(r, c)))
                    totalFinds++;
            }
        }

        return totalFinds;
    }


    public override void SolveBothParts(bool useExample = false, bool useBrian = false, bool printIntros = false)
    {
        if (printIntros)
        {
            PartDescription(1);
        }

        var input = LoadInput(useExample, useBrian);

        //Part 1
        int sum1 = ProcessAsPart1(input);
        Universal.WriteSolutionSection(1, "The word \"XMAS\" was found total this many times total:", sum1);

        //Part 2
        Stopwatch sw = Stopwatch.StartNew();
        int sum2 = ProcessAsPart2(input);
        sw.Stop();
        Console.WriteLine("THIS MANY ticks ELAPSED: " + sw.ElapsedTicks.ToString());
        Universal.WriteSolutionSection(2, "\"XMAS\" formation was found a total of this many times:", sum2);
    }
}

public static class Day04Helpers
{
    public static readonly char[] XMAS = "XMAS".ToCharArray();
    public static readonly char[] MS = "MS".ToCharArray();
    public static readonly bool?[] directionParts8 = { null, true, false };
    public static int SearchInEveryDirection(this List<string> wordSearch, Coords start)
    {
        int finds = 0;
        foreach (var horizontal in directionParts8)
        {
            foreach (var vertical in directionParts8)
            {
                if (horizontal == null && vertical == null)
                {
                    continue;
                }
                else if (wordSearch.SearchInDirection(start, horizontal, vertical))
                {
                    finds++;
                }
            }
        }
        return finds;
    }
    public static bool SearchInDirection(this List<string> wordSearch, Coords start, bool? horizontal, bool? vertical)
    {
        var nextPosition = GetNextPosition(start, horizontal, vertical);
        for (int i = 1; i < XMAS.Length; i++)
        {
            if (!wordSearch.HasInside(nextPosition)
                || wordSearch[nextPosition.R][nextPosition.C] != XMAS[i])
            {
                return false;
            }

            nextPosition = GetNextPosition(nextPosition, horizontal, vertical);
        }
        Console.WriteLine($"X at [{start.R}, {start.C}], {(horizontal == true ? "right" : (horizontal == false ? "left" : ""))}, {(vertical == true ? "down" : (vertical == false ? "up" : ""))}");
        return true;
    }

    public static bool TryGetAnyMatchingLetter(this List<string> wordSearch, char[] matches, Coords start, bool horizontal, bool vertical, out char letter)
    {
        letter = default;
        var position = GetNextPosition(start, horizontal, vertical);
        if (!wordSearch.HasInside(position))
        {
            return false;
        }
        letter = wordSearch[position.R][position.C];
        return matches.Contains(letter);
    }
    public static bool OtherLetterMatches(this List<string> wordSearch, char match, Coords start, bool horizontal, bool vertical)
    {
        var position = GetNextPosition(start, horizontal, vertical);
        return wordSearch.HasInside(position)
            && match == wordSearch[position.R][position.C];
    }
    public static bool IsXMAS(this List<string> wordSearch, Coords start)
    {
        var downhill = TryGetAnyMatchingLetter(wordSearch, MS, start, false, false, out var firstLetterDownhill)
            && OtherLetterMatches(wordSearch, firstLetterDownhill == 'M' ? 'S' : 'M', start, true, true);
        if (downhill)
        {
            return TryGetAnyMatchingLetter(wordSearch, MS, start, false, true, out var firstLetterUphill)
                && OtherLetterMatches(wordSearch, firstLetterUphill == 'M' ? 'S' : 'M', start, true, false);
        }

        return false;
    }

    public static bool HasInside(this List<string> wordSearch, Coords position)
    {
        if (position.R < 0 || position.R >= wordSearch.Count)
        {
            return false;
        }
        if (position.C < 0 || position.C >= wordSearch[position.R].Length)
        {
            return false;
        }
        return true;
    }

    private static Coords GetNextPosition(this Coords start, bool? horizontal, bool? vertical)
    { 
        var newX = vertical switch
        {
            true => start.R + 1,
            false => start.R - 1,
            _ => start.R
        };

        var newY = horizontal switch
        {
            true => start.C + 1,
            false => start.C - 1,
            _ => start.C
        };

        return new Coords(newX, newY);
    }
}
