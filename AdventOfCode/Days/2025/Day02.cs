using System.Diagnostics;

namespace AdventOfCode.Y2025;

public class Day02 : Day
{
    public Day02()
    {
        Year = 2025;
        DayNumber = 2;
    }

    // Lengths to check: even only!
    // Numbers to check for:
    // 11, 22, 33, 44, 55, 66, 77, 88, 99
    // 1010, 1111, 1212, 1313, 1414, 1515, 1616, 1717, 1818, 1919
    // ...

    public UInt128 ProcessAsPart1(IEnumerable<string> input)
    {
        List<UInt128> rangeStarts = new List<UInt128>();
        List<UInt128> rangeEnds = new List<UInt128>();
        
        var inputValues = input.First().Split("-,".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        UInt128 sumOfInvalidIDs = 0;

        for (int i = 0; i < inputValues.Length; i+=2)
        {
            var rangeStart = inputValues[i];
            var rangeEnd = inputValues[i+1];

            if (!(rangeStart.Length % 2 == 1 && rangeEnd.Length == rangeStart.Length))
            {
                var halfOfStart = rangeStart.Length == 1 ? "1" : rangeStart.Substring(0, rangeStart.Length / 2);
                var halfOfEndValue = UInt32.Parse(rangeEnd.Substring(0, rangeEnd.Length % 2 == 0 ? rangeEnd.Length / 2 : (rangeEnd.Length + 1) / 2));

                UInt128 rangeStartValue = UInt128.Parse(inputValues[i]);
                UInt128 rangeEndValue = UInt128.Parse(inputValues[i + 1]);

                for (UInt32 n = UInt32.Parse(halfOfStart); n < halfOfEndValue + 1; n++)
                {
                    UInt128 checkingID = UInt128.Parse($"{n}{n}");
                    if (checkingID >= rangeStartValue)
                    {
                        if (checkingID <= rangeEndValue)
                        {
                            Console.WriteLine(checkingID);
                            sumOfInvalidIDs += checkingID;
                        }
                        else
                            break;
                    }
                }
            }
        }

        return sumOfInvalidIDs;
    }

    public int ProcessAsPart2(IEnumerable<string> input)
    {
        return 0;
    }

    public override void SolveBothParts(bool useExample = false, bool useBrian = false, bool printIntros = false)
    {
        if (printIntros)
        {
            PartDescription(1);
        }

        var input = LoadInput(useExample, useBrian);

        //Part 1
        var sum1 = ProcessAsPart1(input);
        Universal.WriteSolutionSection(1, "The sum of invalid IDs is:", sum1);

        //Part 2
        ////Stopwatch sw = Stopwatch.StartNew();
        //var sum2 = ProcessAsPart2(input);
        ////sw.Stop();
        ////Console.WriteLine("THIS MANY ticks ELAPSED: " + sw.ElapsedTicks.ToString());
        //Universal.WriteSolutionSection(2, "The password using part 2 rules is:", sum2);
    }
}
