namespace AdventOfCode.Y2023;

public class Day04 : Day
{
    public Day04()
    {
        Year = 2023;
        DayNumber = 4;
    }

    public override void SolveBothParts(bool useExample = false, bool useBrian = false, bool printIntros = false)
    {
        if (printIntros)
        {
            PartDescription(1);
        }

        var input = LoadInput(useExample, useBrian);

        //Part 1
        var pointSum = SolvePart1(input);

        Universal.WriteSolutionSection(1,
            "The sum of all points is:",
            pointSum);

        //Part 2
        if (printIntros)
        {
            PartDescription(2);
        }

        var sum = SolvePart2(input);

        Universal.WriteSolutionSection(2,
            "The total amount of scratchcards is:",
            sum);
    }

    public int SolvePart1(string[] input)
    {
        var pointSum = 0;
        foreach (var line in input)
        {
            var points = 0;
            var winCount = GetWinCountFromLine(line);

            if (winCount > 0)
            {
                winCount--;
                points = 1;

                while (winCount > 0)
                {
                    winCount--;
                    points *= 2;
                }
            }
            pointSum += points;
        }
        return pointSum;
    }

    public int SolvePart2(string[] input)
    {
        var scratchCardCounts = new int[input.Count()];
        for (int i = 0; i < scratchCardCounts.Length; i++)
        {
            scratchCardCounts[i] = 1;
        }
        for (int i = 0; i < input.Count(); i++)
        {
            var winCount = GetWinCountFromLine(input[i]);

            for (int j = i + 1; j < scratchCardCounts.Length && winCount > 0; j++)
            {
                scratchCardCounts[j] += scratchCardCounts[i];
                winCount--;
            }
        }

        var sum = 0;
        for (int i = 0; i < scratchCardCounts.Length; i++)
        {
            sum += scratchCardCounts[i];
        }
        return sum;
    }

    private static int GetWinCount(IEnumerable<int> winning, IEnumerable<int> onCard)
    {
        var winCount = 0;
        foreach (var nbr in winning)
        {
            if (onCard.Contains(nbr))
            {
                winCount++;
            }
        }
        return winCount;
    }

    private static int GetWinCountFromLine(string line)
    {
        var winning = line.Split(":|".ToCharArray())[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(nbr => Convert.ToInt32(nbr)).ToList();
        var onCard = line.Split(":|".ToCharArray())[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(nbr => Convert.ToInt32(nbr)).ToList();
        return GetWinCount(winning, onCard);
    }
}
