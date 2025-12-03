using System.Diagnostics;

namespace AdventOfCode.Y2025;

public class Day01 : Day
{
    public Day01()
    {
        Year = 2025;
        DayNumber = 1;
    }

    private const int startingPosition = 50;

    public int ProcessAsPart1(IEnumerable<string> input)
    {
        List<bool> adding = new List<bool>();
        List<int> rotations = new List<int>();
        foreach (var line in input)
        {
            var direction = line[0] == 'L';
            adding.Add(direction);
            rotations.Add(Int32.Parse(line.Substring(1)));
        }

        int position = startingPosition;
        int password = 0;

        for (int i = 0; i < rotations.Count; i++)
        {
            position = adding[i] ? position + rotations[i] : position - rotations[i];
            if (position % 100 == 0)
                password++;
        }

        return password;
    }

    public int ProcessAsPart2(IEnumerable<string> input)
    {

        List<bool> adding = new List<bool>();
        List<int> rotations = new List<int>();
        foreach (var line in input)
        {
            var direction = line[0] == 'L';
            adding.Add(direction);
            rotations.Add(Int32.Parse(line.Substring(1)));
        }

        int position = startingPosition;
        int password = 0;

        for (int i = 0; i < rotations.Count; i++)
        {
            var rotationMoves = rotations[i];
            while (rotationMoves > 100)
            {
                rotationMoves -= 100;
                password++;
            }

            var newPosition = adding[i] ? position + rotationMoves : position - rotationMoves;
            if (newPosition >= 100 || (newPosition <= 0 && position != 0))
                password++;

            Console.WriteLine($"index: {i}, position: {position}, rotations: {(adding[i] ? "+" : "-")}{rotations[i]}, new position: {newPosition % 100}");
            position = newPosition < 0 ? 100 + newPosition : newPosition % 100;
        }

        return password;
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
        Universal.WriteSolutionSection(1, "The password using part 1 rules is:", sum1);

        //Part 2
        //Stopwatch sw = Stopwatch.StartNew();
        int sum2 = ProcessAsPart2(input);
        //sw.Stop();
        //Console.WriteLine("THIS MANY ticks ELAPSED: " + sw.ElapsedTicks.ToString());
        Universal.WriteSolutionSection(2, "The password using part 2 rules is:", sum2);
    }
}
