using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace AdventOfCode.Y2024;

public class Day13 : Day
{
    public Day13()
    {
        Year = 2024;
        DayNumber = 13;
    }

    public long ProcessAsPart1(IEnumerable<string> input)
    {
        //var testMachine = new ClawMachine(
        //    a: new DoubleCoords(2, 3),
        //    b: new DoubleCoords(3, 2),
        //    prize: new DoubleCoords(20, 15)
        //);

        //testMachine.HasValidEquation(out var x, out var y);

        var clawMachines =  input.ToList().ParseInput();

        var totalCost = 0;
        foreach (var machine in clawMachines)
        {
            if (machine.ValidEquationResolvedSubtractively(out var a, out var b))
            {
                totalCost += Day13Helpers.GetPrizeCost(a, b);
            }
        }

        return totalCost;
    }

    public long ProcessAsPart2(IEnumerable<string> input)
    {
        var clawMachines = input.ToList().ParseInput(10000000000000);

        Int64 totalCost = 0;
        foreach (var machine in clawMachines)
        {
            if (machine.ValidEquationResolvedSubtractively2(out var a, out var b))
            {
                totalCost += Day13Helpers.GetPrizeCost(a, b);
            }
        }

        return totalCost;
    }

    public static void ProcessBothParts(IEnumerable<string> input, out long part1Res, out long part2Res)
    {
        part1Res = 0;
        part2Res = 0;
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
        //var sum1 = ProcessAsPart1(input);
        //sw1.Stop();
        //Console.WriteLine($"THIS MANY ticks ELAPSED: {sw1.ElapsedTicks} which is {sw1.ElapsedMilliseconds} ms");
        //Universal.WriteSolutionSection(1, "Part 1 result is: ", sum1);

        //Part 2
        Stopwatch sw2 = Stopwatch.StartNew();
        var sum2 = ProcessAsPart2(input.ToList());
        sw2.Stop();
        Console.WriteLine($"THIS MANY ticks ELAPSED: {sw2.ElapsedTicks} which is {sw2.ElapsedMilliseconds} ms");
        Universal.WriteSolutionSection(2, "Part 2 result is: ", sum2);

        ////BothPartsTogether
        //Stopwatch sw12 = Stopwatch.StartNew();
        //ProcessBothParts(input, out var part1Res, out var part2Res);
        //sw12.Stop();
        //Console.WriteLine($"THIS MANY ticks ELAPSED: {sw12.ElapsedTicks} which is {sw12.ElapsedMilliseconds} ms");
        //Universal.WriteSolutionSection(1, "Part1 result is: ", part1Res);
        //Universal.WriteSolutionSection(2, "Part2 result is: ", part2Res);
    }
}

public static class Day13Helpers
{
    public static List<ClawMachine> ParseInput(this List<string> input)
    {
        var clawMachines = new List<ClawMachine>();
        for (int i = 0; i < input.Count(); i += 4)
        {
            var buttonA = GetCoordsFromLine(input[i]);
            var buttonB = GetCoordsFromLine(input[i + 1]);
            var prize = GetCoordsFromLine(input[i + 2]);
            clawMachines.Add(new ClawMachine(buttonA, buttonB, prize));
        }
        return clawMachines;
    }

    public static List<ClawMachine> ParseInput(this List<string> input, Int64 increasePrizeBy)
    {
        var clawMachines = new List<ClawMachine>();
        for (int i = 0; i < input.Count(); i += 4)
        {
            var buttonA = GetCoordsFromLine(input[i]);
            var buttonB = GetCoordsFromLine(input[i + 1]);
            var prize = GetPrizeCoordsFromLine(input[i + 2], increasePrizeBy);
            clawMachines.Add(new ClawMachine { 
                AX = buttonA.R,
                AY = buttonA.C,
                BX = buttonB.R,
                BY = buttonB.C,
                PrizeX = prize.Item1,
                PrizeY = prize.Item2,
            });
        }
        return clawMachines;
    }

    public static Coords GetCoordsFromLine(this string line)
    {
        var numberEntries = line.Substring(line.IndexOf(':') + 1).Split(" +,=XY".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        var x = Int32.Parse(numberEntries[0]);
        var y = Int32.Parse(numberEntries[1]);
        return new Coords(x, y);
    }

    public static (Int64, Int64) GetPrizeCoordsFromLine(this string line, Int64 increaseBy = 0)
    {
        var numberEntries = line.Substring(line.IndexOf(':') + 1).Split(" +,=XY".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        var x = Int64.Parse(numberEntries[0]);
        var y = Int64.Parse(numberEntries[1]);
        return new (x + increaseBy, y + increaseBy);
    }

    public static int GetPrizeCost(int a, int b)
        => 3 * a + b;

    public static Int64 GetPrizeCost(Int64 a, Int64 b)
        => 3 * a + b;
}

public class ClawMachine
{
    public Coords A { get; set; }
    public Coords B { get; set; }
    public Coords Prize { get; set; }

    public Int64 AX { get; set; }
    public Int64 AY { get; set; }
    public Int64 BX { get; set; }
    public Int64 BY { get; set; }
    public Int64 PrizeX { get; set; }
    public Int64 PrizeY { get; set; }

    public ClawMachine() { }
    public ClawMachine(Coords a, Coords b, Coords prize)
    {
        A = a;
        B = b;
        Prize = prize;
    }

    public Func<int, int> ExpressedA1
        => (b) => (Prize.R - b * B.R) / A.R;
    public Func<Int64, Int64> ExpressedA2
        => (b) => (PrizeX - b * BX) / AX;

    public bool ValidEquationResolvedSubtractively(out int a, out int b)
    {
        int multipliedB = B.R * (- A.C) + B.C * A.R;
        int multipliedPrize = Prize.R * (-A.C) + Prize.C * A.R;

        b = multipliedPrize / multipliedB;
        a = ExpressedA1(b);

        return A.R * a + B.R * b == Prize.R && A.C * a + B.C * b == Prize.C;
    }
    public bool ValidEquationResolvedSubtractively2(out Int64 a, out Int64 b)
    {
        Int64 multipliedB = BX * (-AY) + BY * AX;
        Int64 multipliedPrize = PrizeX * (-AY) + PrizeY * AX;

        b = multipliedPrize / multipliedB;
        a = ExpressedA2(b);

        return AX * a + BX * b == PrizeX && AY * a + BY * b == PrizeY;
    }
}