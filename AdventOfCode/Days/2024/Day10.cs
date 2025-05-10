using System.Data;
using System.Diagnostics;

namespace AdventOfCode.Y2024;

public class Day10 : Day
{
    public Day10()
    {
        Year = 2024;
        DayNumber = 10;
    }

    public int ProcessAsPart1(IEnumerable<string> input)
    {
        var map = input.ToList();
        HashSet<Coords> trailheads = new HashSet<Coords>();

        for (var r = 0; r < map.Count; r++)
        {
            for (int c = 0; c < map[r].Length; c++)
            {
                if (map[r][c] == '0')
                {
                    trailheads.Add(new Coords(r, c));
                }
            }
        }

        var scoreSum = 0;
        foreach (var trailhead in trailheads)
        {
            scoreSum += map.GetTrailheadScorePeakCount(trailhead);
        }

        return scoreSum;
    }

    public int ProcessAsPart2(IEnumerable<string> input)
    {
        var map = input.ToList();
        HashSet<Coords> trailheads = new HashSet<Coords>();

        for (var r = 0; r < map.Count; r++)
        {
            for (int c = 0; c < map[r].Length; c++)
            {
                if (map[r][c] == '0')
                {
                    trailheads.Add(new Coords(r, c));
                }
            }
        }

        var scoreSum = 0;
        foreach (var trailhead in trailheads)
        {
            scoreSum += map.GetTrailheadScoreTrailCount(trailhead);
        }

        return scoreSum;
    }

    public static void ProcessBothParts(IEnumerable<string> input, out int part1Res, out int part2Res)
    {
        var map = input.ToList();
        HashSet<Coords> trailheads = new HashSet<Coords>();

        for (var r = 0; r < map.Count; r++)
        {
            for (int c = 0; c < map[r].Length; c++)
            {
                if (map[r][c] == '0')
                {
                    trailheads.Add(new Coords(r, c));
                }
            }
        }

        part1Res = 0;
        part2Res = 0;
        foreach (var trailhead in trailheads)
        {
            var peaks = map.ReachablePeakCount(map.GetNextSteps(trailhead));
            part1Res += peaks.Distinct().Count();
            part2Res += peaks.Count();
        }
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

        ////Part 2
        //Stopwatch sw2 = Stopwatch.StartNew();
        //var sum2 = ProcessAsPart2(input.ToList());
        //sw2.Stop();
        //Console.WriteLine($"THIS MANY ticks ELAPSED: {sw2.ElapsedTicks} which is {sw2.ElapsedMilliseconds} ms");
        //Universal.WriteSolutionSection(2, "Part 2 result is: ", sum2);

        //BothPartsTogether
        Stopwatch sw12 = Stopwatch.StartNew();
        ProcessBothParts(input, out int part1Res, out int part2Res);
        sw12.Stop();
        Console.WriteLine($"THIS MANY ticks ELAPSED: {sw12.ElapsedTicks} which is {sw12.ElapsedMilliseconds} ms");
        Universal.WriteSolutionSection(1, "Part1 result is: ", part1Res);
        Universal.WriteSolutionSection(2, "Part2 result is: ", part2Res);
    }
}

public static class Day10Helpers
{
    public static readonly GridMovement[] Movements =
    {
        new GridMovement(null, false),  // up
        new GridMovement(true, null),   // right 
        new GridMovement(null, true),   // down
        new GridMovement(false, null)   // left
    };

    public static int GetTrailheadScorePeakCount(this List<string> map, Coords trailhead)
    {
        var peaks = map.ReachablePeakCount(map.GetNextSteps(trailhead));
        var score = peaks.Distinct().Count();
        //Console.WriteLine($"Trailhed {trailhead.ForPrint()} has a score of {score}");
        return score;
    }
    public static int GetTrailheadScoreTrailCount(this List<string> map, Coords trailhead)
    {
        var peaks = map.ReachablePeakCount(map.GetNextSteps(trailhead));
        var score = peaks.Count();
        //Console.WriteLine($"Trailhed {trailhead.ForPrint()} has a score of {score}");
        return score;
    }

    public static IEnumerable<Coords> ReachablePeakCount(this List<string> map, Coords[] steps)
    {
        if (steps.Length > 0)
        {
            if (map[steps[0].R][steps[0].C] == '9')
            {
                return steps;
            }
            var destinations = new List<Coords>();
            foreach (var step in steps)
            {
                var nextSteps = map.GetNextSteps(step);
                 destinations.AddRange(map.ReachablePeakCount(nextSteps));
            }
            return destinations;
        }
        return Enumerable.Empty<Coords>();
    }

    public static Coords[] GetNextSteps(this List<string> map, Coords currentPosition) 
    {
        var currentElevation = map[currentPosition.R][currentPosition.C];
        //Console.WriteLine($"from {currentPosition.ForPrint()} with elevation {currentElevation} to:");
        var destinations = Movements.Select(m => currentPosition.DoMovement(m))
            .Where(np => np.IsInside(map)
                && map[np.R][np.C] == currentElevation + 1);
        //foreach (var destination in destinations)
        //{
        //    Console.WriteLine(destination.ForPrint() + (destination.IsInside(map) ? $" with elevation {map[destination.R][destination.C]}" : ""));
        //}
        return destinations.ToArray();
    }
}