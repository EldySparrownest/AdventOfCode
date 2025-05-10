using System.Diagnostics;

namespace AdventOfCode.Y2024;

public class Day01 : Day
{
    public Day01()
    {
        Year = 2024;
        DayNumber = 1;
    }

    public int ProcessAsPart1(IEnumerable<string> input)
    {
        int sum = 0;
        List<int> leftValues = new List<int>();
        List<int> rightValues = new List<int>();
        foreach (var line in input)
        {
            var values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            leftValues.Add(Int32.Parse(values[0]));
            rightValues.Add(Int32.Parse(values[1]));
        }

        leftValues.Sort();
        rightValues.Sort();

        for (int i = 0; i < leftValues.Count; i++)
        {
            sum += Math.Abs(leftValues[i] - rightValues[i]);
        }

        return sum;
    }

    public int ProcessAsPart2(IEnumerable<string> input)
    {
        int sum = 0;
        List<int> leftValues = new List<int>();
        List<int> rightValues = new List<int>();
        foreach (var line in input)
        {
            var values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            leftValues.Add(Int32.Parse(values[0]));
            rightValues.Add(Int32.Parse(values[1]));
        }

        leftValues.Sort();
        rightValues.Sort();

        var sum1 = 0;
        for (int i = 0; i < leftValues.Count; i++)
        {
            sum1 += Math.Abs(leftValues[i] - rightValues[i]);
        }

        var addingNumber = Int32.MinValue;
        while (leftValues.Count > 0 && rightValues.Count > 0)
        {
            var rightCount = 0;
            addingNumber = leftValues[0];

            while (rightValues.Count > 0 && addingNumber >= rightValues[0])
            {
                if (rightValues[0] == addingNumber)
                {
                    rightCount++;
                }
                rightValues.RemoveAt(0);
            }

            sum += (addingNumber * rightCount);

            while (leftValues.Count > 0 && leftValues[0] <= addingNumber)
            {
                leftValues.RemoveAt(0);
            }
        }

        return sum;
    }

    public int ProcessAsPart2Brian(IEnumerable<string> input)
    {
        List<int> leftValues = new List<int>();
        List<int> rightValues = new List<int>();
        foreach (var line in input)
        {
            var values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            leftValues.Add(Int32.Parse(values[0]));
            rightValues.Add(Int32.Parse(values[1]));
        }

        leftValues.Sort();
        rightValues.Sort();

        var sum1 = 0;
        for (int i = 0; i < leftValues.Count; i++)
        {
            sum1 += Math.Abs(leftValues[i] - rightValues[i]);
        }

        int similarity = 0;
        int last_r = 0;

        foreach (int l in leftValues)
        {
            while (last_r < rightValues.Count)
            {
                int r = rightValues[last_r];
                if (r == l)
                {
                    similarity += l;
                }
                else if (r > l)
                {
                    break;
                }
                last_r++;
            }
        }

        return similarity;
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
        int sum2 = ProcessAsPart2Brian(input);
        sw.Stop();
        Console.WriteLine("THIS MANY ticks ELAPSED: " + sw.ElapsedTicks.ToString());
        Universal.WriteSolutionSection(2, "The sum of all calibration values using part 2 rules is:", sum2);
    }
}

public static class Day01Helpers
{

}
