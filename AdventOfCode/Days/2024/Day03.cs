using System.Diagnostics;

namespace AdventOfCode.Y2024;

public class Day03 : Day
{
    private readonly string disable = "don't()";
    private readonly string enable = "do()";

    public Day03()
    {
        Year = 2024;
        DayNumber = 3;
    }

    public int ProcessAsPart1(IEnumerable<string> input)
    {
        int sum = 0;
        List<int> lineSums = new List<int>();
        foreach (var line in input)
        {
            sum += line.ProcessOneLine();
        }

        return sum;
    }

    public int ProcessAsPart2(IEnumerable<string> input)
    {
        int sum = 0;
        bool enabled = true;
        foreach (var line in input)
        {
            var beginsDont = line.StartsWith(disable);
            var endsDont = line.EndsWith(disable);
            var disabledSegments = line.Split(disable);

            if (beginsDont)
                enabled = false;
            else
                sum += disabledSegments[0].ProcessOneLine();

            for (int i = 1; i < disabledSegments.Length; i++)
            {
                var beginsDo = disabledSegments[i].StartsWith(enable);
                var endsDo = disabledSegments[i].EndsWith(enable);
                var enabledSegments = disabledSegments[i].Split(enable);

                if (beginsDo)
                {
                    enabled = true;
                    sum += enabledSegments[0].ProcessOneLine();
                }

                if (enabled)
                {
                    for (int j = 1; j < enabledSegments.Length; j++)
                    {
                        sum += enabledSegments[j].ProcessOneLine();
                    }
                }

                if (endsDo)
                {
                    enabled = true;
                }
            }

            if (endsDont)
                enabled = false;
        }

        return sum;
    }

    public int ProcessAsPart2Radical(IEnumerable<string> input)
    {
        int sum = 0;
        var allInOne = string.Join("x", input);

        var disabledSegments = allInOne.Split(disable);

        if (!allInOne.StartsWith(disable))
            sum += disabledSegments[0].ProcessOneLine();

        for (int i = 1; i < disabledSegments.Length; i++)
        {
            var beginsDo = disabledSegments[i].StartsWith(enable);
            var enabledSegments = disabledSegments[i].Split(enable);

            if (beginsDo)
            {
                sum += enabledSegments[0].ProcessOneLine();
            }

            for (int j = 1; j < enabledSegments.Length; j++)
            {
                sum += enabledSegments[j].ProcessOneLine();
            }
        }

        return sum;
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
        Universal.WriteSolutionSection(1, "The sum of all calibration values using part 1 rules is:", sum1);

        //Part 2
        Stopwatch sw = Stopwatch.StartNew();
        int sum2 = ProcessAsPart2Radical(input);
        sw.Stop();
        Console.WriteLine("THIS MANY ticks ELAPSED: " + sw.ElapsedTicks.ToString());
        Universal.WriteSolutionSection(2, "The sum of all calibration values using part 2 rules is:", sum2);
    }
}

public static class Day03Helpers
{
    public static bool TryProcessSegment(this string segment, out int multiplied)
    {
        multiplied = 0;
        if (!segment.Contains(')'))
            return false;

        var insideOfBrackets = segment.Split(')')[0];
        var paramsSplit = insideOfBrackets.Split(",");

        if (paramsSplit.Length == 2)
        {
            var aOk = Int32.TryParse(paramsSplit[0], out int a);
            var bOk = Int32.TryParse(paramsSplit[1], out int b);

            if (aOk && bOk)
            {
                multiplied = a * b;
                return true;
            }
        }

        return false;
    }

    public static int ProcessOneLine(this string line)
    {
        var sum = 0;
        var segments = line.Split("mul(");
        if (line.StartsWith("mul("))
        {
            if (segments[0].TryProcessSegment(out int segmentValue))
                sum += segmentValue;
        }
        for (int i = 1; i < segments.Length; i++)
        {
            if (segments[i].TryProcessSegment(out int segmentValue))
                sum += segmentValue;
        }
        return sum;
    }
}
