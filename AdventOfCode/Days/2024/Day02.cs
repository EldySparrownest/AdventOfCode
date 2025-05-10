using System.Diagnostics;

namespace AdventOfCode.Y2024;

public class Day02 : Day
{
    public Day02()
    {
        Year = 2024;
        DayNumber = 2;
    }

    public int ProcessAsPart1(IEnumerable<string> input)
    {
        var tolerance = 3;
        var safeCount = 0;

        foreach (var line in input)
        {
            var values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(v => Int32.Parse(v)).ToList();
            bool increasing = values[0] < values[1];
            bool safe = true;
            
            for (int i = 0; i < values.Count() - 1; i++)
            {
                if (values[i] == values[i + 1])
                {
                    safe = false;
                    break;
                }

                if (increasing)
                {
                    safe = values[i + 1] > values[i] && values[i + 1] - values[i] <= 3;
                    if (!safe)
                        break;
                }
                else
                {
                    safe = values[i + 1] < values[i] && values[i] - values[i + 1] <= 3;
                    if (!safe)
                        break;
                }
            }

            if (!safe) 
            { 
                continue; 
            }

            safeCount++;
        }

        return safeCount;
    }

    public int ProcessAsPart2(IEnumerable<string> input)
    {
        var tolerance = 3;
        var safeCount = 0;

        foreach (var line in input)
        {
            var values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(v => Int32.Parse(v)).ToList();
            bool increasing = Day02Helpers.IsIncreasing(values);

            var myResult = Day02Helpers.IsSequenceSafe(values, increasing, tolerance);
            if (Day02Helpers.IsSafeSequenceBrian(values, tolerance, false) != myResult)
            {
                Console.Write((increasing ? "increasing" : "decreasing") + " " + string.Join(" ", values.Select(v => v.ToString()).ToArray()));
                Console.Write($" myResult says {(myResult ? "safe" : "UNSAFE")} but that's wrong\n");
            }

            if (!myResult)
            {
                //Console.Write(new string(' ', values.Count() - 4) + "UNSAFE\n");
                continue;
            }

            //Console.WriteLine();
            safeCount++;
        }

        return safeCount;
    }

    public override void SolveBothParts(bool useExample = false, bool useBrian = false, bool printIntros = false)
    {
        if (printIntros)
        {
            PartDescription(1);
        }

        var input = LoadInput(useExample, useBrian);

        ////Part 1
        //int sum1 = ProcessAsPart1(input);
        //Universal.WriteSolutionSection(1, "The sum of all calibration values using part 1 rules is:", sum1);

        //Part 2
        var testInput = "0 0 8 10 11 12\n" +
            "5 5 5 5 5\n" +
            "5 5 6 7 8\n" +
            "5 5 4 3 2\n" +
            "5 6 5 7 8\n" +
            "5 6 5 4 3\n" +
            "5 6 6 7 8\n" +
            "5 4 3 2\n";


        var dumbInput =
            //"23 21 18 19 19\n" +
            //"76 74 70 69 66\n" +
            "13 11 8 10 10\n" + 
            //"1 3 2 4 5\n" + 
            //"8 6 4 4 1\n" + 
            "";

        var split = dumbInput.Split("\n", StringSplitOptions.RemoveEmptyEntries);

        //var testList = new List<int>() { 4,8,10,11,12 };
        //var x = Day02Helpers.IsIncreasing(testList);
        //var y = Day02Helpers.IsSequenceSafe(testList, x, 3);
        int count2 = ProcessAsPart2(input);
        Universal.WriteSolutionSection(2, "The sum of all calibration values using part 2 rules is:", count2);
    }
}

public static class Day02Helpers
{
    public static bool IsIncreasing(List<int> values)
    {
        if (values.Count < 3)
            return true; //doesn't matter

        else if (values[0] == values[1])
        {
            return values[1] < values[2];
        }

        else if (values[0] < values[1])
        {
            if (values[0] < values[2])
                return true;
            else
                return values.Count > 3 && values[3] > values[2];
        }

        else
        { 
            return values.Count > 3 && values[2] >= values[0] && values[3] > values[2];
        }
    }

    public static bool IsSequenceSafe(List<int> values, bool increasing, int tolerance)
    {
        bool stillHasRemoveLeft = true;
        bool safe = true;
        for (int i = 0; i < values.Count() - 1; i++)
        {
            if (values[i] == values[i + 1])
            {
                if (!stillHasRemoveLeft)
                {
                    safe = false;
                    break;
                }
                else
                {
                    stillHasRemoveLeft = false;
                }
            }
            else if (increasing)
            {
                var ok = values[i + 1] > values[i] && values[i + 1] - values[i] <= tolerance;
                if (!ok)
                {
                    if (!stillHasRemoveLeft)
                    {
                        safe = false;
                        break;
                    }
                    else
                    {
                        //Console.WriteLine(string.Join(" ", values.Select(v => v.ToString()).ToArray()));
                        stillHasRemoveLeft = false;
                        var okWithoutLeft = !(i - 1 > 0) 
                            ? (values[i + 1 + 1] > values[i + 1] && values[i + 1 + 1] - values[i] <= tolerance)  
                            : (values[i + 1] > values[i - 1] && values[i + 1] - values[i - 1] <= tolerance);

                        var okWithoutRight = 
                            !(i + 1 < values.Count() - 1)
                            ? true
                            : (values[i + 1 + 1] > values[i] && values[i + 1 + 1] - values[i] <= tolerance);
                        if (okWithoutLeft || okWithoutRight)
                        {
                            i++;
                        }
                        else
                        {
                            safe = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                var ok = values[i + 1] < values[i] && values[i] - values[i + 1] <= tolerance;
                if (!ok)
                {
                    if (!stillHasRemoveLeft)
                    {
                        safe = false;
                        break;
                    }
                    else
                    {
                        //Console.WriteLine(string.Join(" ", values.Select(v => v.ToString()).ToArray()));
                        stillHasRemoveLeft = false;
                        var okWithoutLeft = !(i - 1 > 0) 
                            ? (values[i + 1] > values[i + 1 + 1] && values[i + 1] - values[i + 1 + 1] <= tolerance) 
                            : (!(i + 1 < values.Count() - 1) 
                                ? true 
                                : (values[i + 1] > values[i + 1 + 1] 
                                    && values[i + 1] - values[i + 1 + 1] <= tolerance)) 
                                    && (values[i - 1] > values[i + 1] && values[i - 1] - values[i + 1] <= tolerance);
                        var okWithoutRight = !(i + 1 < values.Count() - 1)
                            ? true
                            : (values[i] > values[i + 1 + 1] && values[i] - values[i + 1 + 1] <= tolerance);
                        if (okWithoutLeft || okWithoutRight)
                        {
                            i++;
                        }
                        else
                        {
                            safe = false;
                            break;
                        }
                    }
                }
            }
        }
        return safe;
    }

    public static bool IsSafeSequenceBrian(List<int> values, int tolerance, bool calledRecursively)
    {
        var onlyIncreases = true;
        var onlyDecreases = true;

        var minGap = Int32.MaxValue;
        var maxGap = 0;

        var isSafe = true;
        var firstErrorIndex = -1;

        for (var i = 0; i < values.Count() - 1; i++)
        {
            if (values[i] > values[i + 1])
            {
                onlyDecreases = false;
            }
            else if (values[i] < values[i + 1])
            {
                onlyIncreases = false;
            }

            var gap = Math.Abs(values[i] - values[i + 1]);
            minGap = Math.Min(minGap, gap);
            maxGap = Math.Max(maxGap, gap);

            if (minGap == 0 || maxGap > tolerance || (!onlyDecreases && !onlyIncreases))
            {
                firstErrorIndex = i;
                break;
            }
        }

        if (firstErrorIndex == -1) {
            return true;
        }

        if (!calledRecursively)
        {
            for (var i = Math.Max(0, firstErrorIndex - 1); i < firstErrorIndex + 2; i++)
            {
                var alteredReport = values.ToList();
                alteredReport.RemoveAt(i);

                var safe = IsSafeSequenceBrian(alteredReport, tolerance, true);
                if (safe)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
