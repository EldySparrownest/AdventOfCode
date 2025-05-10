using System.Data;

namespace AdventOfCode.Y2024;

public class Day07 : Day
{
    public Day07()
    {
        Year = 2024;
        DayNumber = 7;
    }
    public UInt128 ProcessAsPart1(List<string> input)
    {
        UInt128 sum = 0;
        for (var r = 0; r < input.Count(); r++)
        {
            var firstSplit = input[r].Split(':');
            UInt128 targetValue = UInt128.Parse(firstSplit[0]);
            var secondSplit = firstSplit[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            UInt128[] elements = secondSplit.Select(x => UInt128.Parse(x)).ToArray();

            if (elements.CanResultInPart1(targetValue))
                sum += targetValue; 
        }

        return sum;
    }

    public UInt128 ProcessAsPart2(List<string> input)
    {
        UInt128 sum = 0;
        for (var r = 0; r < input.Count(); r++)
        {
            var firstSplit = input[r].Split(':');
            UInt128 targetValue = UInt128.Parse(firstSplit[0]);
            var secondSplit = firstSplit[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            UInt128[] elements = secondSplit.Select(x => UInt128.Parse(x)).ToArray();

            if (elements.CanResultInPart2(targetValue))
                sum += targetValue;
        }

        return sum;
    }

    public void ProcessBothParts(IEnumerable<string> input, out int part1Res, out int part2Res)
    {
        var inputArray = input.ToArray();
        var rulesInput = inputArray.GetRulesPartOfInput(out int emptyLineIndex);

        var rulebook = Day05Helpers.WriteRules(rulesInput);

        rulebook.PrintRulebook();

        var layeredFirstRuleIsAfter = new List<int>();
        rulebook.First().Value.LayeredWhatComesAfter(layeredFirstRuleIsAfter);
        //Console.Write("layered is after:");
        //Console.Write(string.Join(", ", layeredFirstRuleIsAfter.Distinct()));
        //Console.WriteLine();

        //var sorted = rulebook.OrderBy(entry => entry.Value.ThisIsAfter.Count()).ToDictionary();

        //Console.WriteLine("AND NOW SORTED:");
        //sorted.PrintRulebook();

        //Console.WriteLine(rulebook.Count());

        var updatesInput = inputArray.Skip(emptyLineIndex + 1);

        part1Res = 0;
        part2Res = 0;
        foreach (var update in updatesInput)
        {
            var intUpdate = update.Split(',').Select(v => Convert.ToInt32(v)).ToArray();
            if (intUpdate.DoesFollowRules(rulebook))
            {
                Console.Write("valid: ");
                Console.Write(string.Join(", ", intUpdate));
                //Console.Write(" " + (intUpdate.Length / 2).ToString() + " " + intUpdate[intUpdate.Length / 2].ToString());
                Console.WriteLine();
                part1Res += intUpdate[intUpdate.Length / 2];
            }
            else
            {
                Console.Write("NOT valid: ");
                Console.Write(string.Join(", ", intUpdate));
                Console.WriteLine();
                intUpdate.ReorderToFollowRules(rulebook);
                //Console.Write(" " + (intUpdate.Length / 2).ToString() + " " + intUpdate[intUpdate.Length / 2].ToString());
                Console.Write("should be valid : ");
                Console.Write(string.Join(", ", intUpdate));
                Console.WriteLine();
                part2Res += intUpdate[intUpdate.Length / 2];
            }
        }
    }


    public override void SolveBothParts(bool useExample = false, bool useBrian = false, bool printIntros = false)
    {
        if (printIntros)
        {
            PartDescription(1);
        }

        var input = LoadInput(useExample, useBrian);

        //Part 1
        UInt128 sum1 = ProcessAsPart1(input.ToList());
        Universal.WriteSolutionSection(1, "Part 1 result is: ", sum1);

        //Part 2
        //Stopwatch sw = Stopwatch.StartNew();
        UInt128 sum2 = ProcessAsPart2(input.ToList());
        //sw.Stop();
        //Console.WriteLine("THIS MANY ticks ELAPSED: " + sw.ElapsedTicks.ToString());
        Universal.WriteSolutionSection(2, "Part 2 result is: ", sum2);
    }
}

public static class Day07Helpers
{
    public enum Operation 
    {
        Add,
        Concatenate,
        Multiply,
    }

    public static bool CanResultInPart1(this UInt128[] input, UInt128 targetValue)
    {
        bool[] ops = new bool[input.Count() - 1];

        var iterratedOver = 0;
        var maxIterrations = Math.Pow(2, ops.Length);

        while (iterratedOver < maxIterrations)
        {
            ops.Iterrate();
            if (input.ResultsIn(ops, targetValue))
            {
                return true;
            }
            iterratedOver++;
        }

        return false;
    }
    public static bool CanResultInPart2(this UInt128[] input, UInt128 targetValue)
    {
        Operation[] ops = new Operation[input.Count() - 1].Populate(Operation.Add);

        var iterratedOver = 0;
        var maxIterrations = Math.Pow(3, ops.Length);

        while (iterratedOver < maxIterrations)
        {
            ops.Iterrate();
            if (input.ResultsInPart2(ops, targetValue))
            {
                return true;
            }
            iterratedOver++;
        }

        return false;
    }

    public static void Iterrate(this bool[] old)
    {
        for (int i = 0; i < old.Length; i++)
        {
            old[i] = !old[i];

            if (!old[i])
                break;
        }
    }
    public static void Iterrate(this Operation[] input)
    {
        bool carryOver = false;
        for (int i = 0; i < input.Length; i++)
        {
            carryOver = input[i] == Operation.Multiply;
            input[i] = input[i] switch
            {
                Operation.Add => Operation.Concatenate,
                Operation.Concatenate => Operation.Multiply,
                Operation.Multiply => Operation.Add
            };

            if (!carryOver)
                break;
        }
    }

    public static bool ResultsIn(this UInt128[] inputElements, bool[] ops, UInt128 targetValue)
    {
        UInt128 res = inputElements[0];

        for (int i = 0; i < ops.Length; i++)
        {
            if (ops[i])
            {
                res += inputElements[i + 1];
            }
            else 
            { 
                res *= inputElements[i + 1];
            }
        }

        return res == targetValue;
    }
    public static bool ResultsInPart2(this UInt128[] inputElements, Operation[] ops, UInt128 targetValue)
    {
        UInt128 res = inputElements[0];

        for (int i = 0; i < ops.Length; i++)
        {
            switch (ops[i])
            {
                case Operation.Add:
                    res += inputElements[i + 1];
                    break;
                case Operation.Concatenate:
                    res = UInt128.Parse($"{res}{inputElements[i + 1]}");
                    break;
                case Operation.Multiply:
                    res *= inputElements[i + 1];
                    break;
                default:
                    break;
            }
        }

        return res == targetValue;
    }
}