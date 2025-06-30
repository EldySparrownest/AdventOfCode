using System.Data;
using System.Diagnostics;
using System.Numerics;

namespace AdventOfCode.Y2024;

public class Day11 : Day
{
    public static Dictionary<BigInteger, (BigInteger, BigInteger?)> ResCache = new Dictionary<BigInteger, (BigInteger, BigInteger?)>();
    public static Dictionary<(BigInteger, int), BigInteger> DepthCache = new Dictionary<(BigInteger, int), BigInteger>();

    public Day11()
    {
        Year = 2024;
        DayNumber = 11;
    }
    
    public BigInteger Process(IEnumerable<string> input, int blinkCount)
    {
        var stoneLine = input.First()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(BigInteger.Parse)
            .ToList();

        Console.WriteLine("input:");
        stoneLine.OutputValues();
        Console.WriteLine();

        return GetStoneCountAfterBlinks(stoneLine, blinkCount);
    }

    public override void SolveBothParts(bool useExample = false, bool useBrian = false, bool printIntros = false)
    {
        if (printIntros)
        {
            PartDescription(1);
        }

        var input = LoadInput(useExample, useBrian);

        ////Part 1
        //Stopwatch sw1 = Stopwatch.StartNew();
        //var sum1 = Process(input, 25);
        //sw1.Stop();
        //Console.WriteLine($"THIS MANY ticks ELAPSED: {sw1.ElapsedTicks} which is {sw1.ElapsedMilliseconds} ms");
        //Universal.WriteSolutionSection(1, "Part 1 result is: ", sum1);

        //Part 2
        Stopwatch sw2 = Stopwatch.StartNew();
        var sum2 = Process(input, 75);
        sw2.Stop();
        Console.WriteLine($"THIS MANY ticks ELAPSED: {sw2.ElapsedTicks} which is {sw2.ElapsedMilliseconds} ms");
        Universal.WriteSolutionSection(2, "Part 2 result is: ", sum2);
    }

    private BigInteger GetStoneCountAfterBlinks(List<BigInteger> stoneLine, int blinkCount)
    {
        BigInteger stoneCount = 0;
        var sw = new Stopwatch();
        for (var i = 0; i < stoneLine.Count; i++)
        {
            sw.Restart();
            stoneCount += stoneLine[i].StoneCountFromThisAfterBlinks(blinkCount);
            sw.Stop();
            Console.WriteLine($"Processed original stone at index {i} in {sw.ElapsedMilliseconds} ms, {DepthCache.Count} operations cached, {ResCache.Count} results cached");
        }
        return stoneCount;
    }
}

public static class Day11Helpers
{
    public static int StoneDigitCount(this BigInteger stone) => stone.ToString().Length; 
    public static bool HasEvenDigitCount(this BigInteger stone) => stone.StoneDigitCount() % 2 == 0; 

    public static bool ReactToBlink(this BigInteger originalStone, out BigInteger splitLeft, out BigInteger? splitRight)
    {
        bool splits = false;
        splitLeft = originalStone;
        splitRight = null;

        if (Day11.ResCache.ContainsKey(originalStone))
        {
            splitLeft = Day11.ResCache[originalStone].Item1;
            splitRight = Day11.ResCache[originalStone].Item2;
            splits = splitRight != null;
        }
        else if (originalStone == 0)
        {
            Day11.ResCache.Add(0, new(1, null));
            splitLeft = 1;
        }
        else if (originalStone.HasEvenDigitCount())
        {
            originalStone.SplitStone(out splitLeft, out splitRight);
            Day11.ResCache.Add(originalStone, new(splitLeft, splitRight));
            splits = true;
        }
        else
        {
            Day11.ResCache.Add(originalStone, new(originalStone * 2024, null));
            splitLeft *= 2024;
        }

        return splits;
    }

    public static void SplitStone(this BigInteger originalStone, out BigInteger splitLeft, out BigInteger? splitRight)
    {
        splitLeft = BigInteger.Parse(originalStone.ToString().Substring(0, originalStone.StoneDigitCount() / 2));
        splitRight = BigInteger.Parse(originalStone.ToString().Substring(originalStone.StoneDigitCount() / 2));
    }

    public static void OutputValues(this List<BigInteger> stoneLine) => stoneLine.ForEach(x => Console.Write($"{x} "));

    public static BigInteger StoneCountFromThisAfterBlinks(this BigInteger stone, int blinkCount)
    {
        if (Day11.DepthCache.ContainsKey((stone, blinkCount)))
        {
            return Day11.DepthCache[(stone, blinkCount)];
        }

        var originalStone = stone;
        BigInteger stoneCount = 1;
        //Stopwatch sw2 = Stopwatch.StartNew();
        for (int i = 0; i < blinkCount; i++)
        {
            //Console.WriteLine("starting blink " + (i + 1) + " out of " + blinkCount);
            var stoneSplit = stone.ReactToBlink(out BigInteger leftSplit, out BigInteger? rightSplit);
            if (stoneSplit)
            {
                //Console.WriteLine($"{stone} split into {leftSplit} an {rightSplit}");
                stoneCount += rightSplit!.Value.StoneCountFromThisAfterBlinks(blinkCount - (i + 1));
            }
            stone = leftSplit!;
        }
        //sw2.Stop();
        //Console.WriteLine($"processedd {blinkCount} blinks for stone that split into {stoneCount} in {sw2.ElapsedTicks} ticks which is {sw2.ElapsedMilliseconds} ms");

        Day11.DepthCache.Add((originalStone, blinkCount), stoneCount);
        return stoneCount;
    }
}