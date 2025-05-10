using System;
using System.Data;

namespace AdventOfCode.Y2024;

public class Day06 : Day
{
    public Day06()
    {
        Year = 2024;
        DayNumber = 6;
    }

    public int ProcessAsPart1(IEnumerable<string> input)
    {
        var map = input.ToList();
        Coords guardPosition;

        for (var r = 0; r < map.Count; r++)
        {
            if (map[r].IndexOfAny(Day06Helpers.GuardChars) > -1)
            {
                guardPosition = new Coords(r, map[r].IndexOfAny(Day06Helpers.GuardChars));

                while (!map.LeavesAfterStep(guardPosition, [Day06Helpers.Obstacle], out Coords newGuardPosition))
                {
                    //map.Print();
                    guardPosition = newGuardPosition;
                }
            }
        }

        int xCount = 0;
        for (var r = 0; r < map.Count; r++)
        {
            xCount += map[r].Split('X').Length - 1;
        }
        return xCount;
    }

    public int ProcessAsPart2(IEnumerable<string> input)
    {
        var map = input.ToList();
        Coords guardStartPositon = Day06Helpers.GetGuardStartState(map, out char guardStartDirection);
        Coords guardPosition;

        for (var r = 0; r < map.Count; r++)
        {
            if (map[r].IndexOfAny(Day06Helpers.GuardChars) > -1)
            {
                guardPosition = new Coords(r, map[r].IndexOfAny(Day06Helpers.GuardChars));

                while (!map.LeavesAfterStep(guardPosition, [Day06Helpers.Obstacle], out Coords newGuardPosition))
                {
                    //map.Print();
                    guardPosition = newGuardPosition;
                }
            }
        }

        map.OverwriteCharOnIndexOnIndex(guardStartPositon.R, guardStartPositon.C, guardStartDirection);

        var sabotageLocationOptions = 0;
        for (var r = 0; r < map.Count; r++)
        {
            if (!map[r].Contains(Day06Helpers.PassedThroughTile))
            {
                continue;
            }

            var xIndex = map[r].IndexOf(Day06Helpers.PassedThroughTile);
            while (xIndex > -1 && xIndex < map[r].Length)
            {
                if (guardStartPositon.R != r || guardStartPositon.C != xIndex)
                {
                    var mapWithNewObstacle = map.ToList();
                    mapWithNewObstacle.OverwriteCharOnIndexOnIndex(r, xIndex, Day06Helpers.AddedObstacle);
                    //mapWithNewObstacle[r] = mapWithNewObstacle[r].OverwriteCharOnIndex(xIndex, Day06Helpers.AddedObstacle);

                    for (int i = 0; i < mapWithNewObstacle.Count; i++)
                    {
                        mapWithNewObstacle[i] = mapWithNewObstacle[i].Replace(Day06Helpers.PassedThroughTile, '.');
                    }

                    if (mapWithNewObstacle.WillGetStuckInLoop(guardStartPositon, guardStartDirection))
                    {
                        sabotageLocationOptions++;
                    }
                }
                xIndex = map[r].Substring(xIndex + 1).Contains(Day06Helpers.PassedThroughTile) ? xIndex + map[r].Substring(xIndex + 1).IndexOf(Day06Helpers.PassedThroughTile) + 1 : -1;
            }
        }

        return sabotageLocationOptions;
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
        int sum1 = ProcessAsPart1(input);
        Universal.WriteSolutionSection(1, "The guard travels across this many tiles before leaving: ", sum1);

        //Part 2
        //Stopwatch sw = Stopwatch.StartNew();
        int sum2 = ProcessAsPart2(input);
        //sw.Stop();
        //Console.WriteLine("THIS MANY ticks ELAPSED: " + sw.ElapsedTicks.ToString());
        Universal.WriteSolutionSection(2, "There are this many possible locations to get the guard stuck in loops: ", sum2);

        ////BothPartsTogether
        //ProcessBothParts(input, out int part1Res, out int part2Res);
        //Universal.WriteSolutionSection(1, "Part1 result is: ", part1Res);
        //Universal.WriteSolutionSection(2, "Part2 result is: ", part2Res);
    }
}

public static class Day06Helpers
{
    public static readonly char Obstacle = '#';
    public static readonly char AddedObstacle = '0';
    public static readonly char PassedThroughTile = 'X';
    public static readonly char[] AllObstacles = { Obstacle, AddedObstacle };
    public static readonly char[] GuardChars = { '^',  '>', 'v', '<'};
    public static readonly Dictionary<char, GridMovement> Movements = new Dictionary<char, GridMovement>
    {
        ['^'] = new GridMovement(null, false),
        ['>'] = new GridMovement(true, null),
        ['v'] = new GridMovement(null, true),
        ['<'] = new GridMovement(false, null)
    };

    public static Coords GetGuardStartState(this List<string> map, out char guardOrientation)
    {
        for (var r = 0; r < map.Count; r++)
        {
            if (map[r].IndexOfAny(Day06Helpers.GuardChars) > -1)
            {
                var guardPosition = new Coords(r, map[r].IndexOfAny(Day06Helpers.GuardChars));
                guardOrientation = map[r][guardPosition.C];
                return guardPosition;
            }
        }
        guardOrientation = GuardChars.FirstOrDefault();
        return new Coords(-1, -1);
    }

    public static bool LeavesAfterStep(this List<string> map, Coords guardPosition, IEnumerable<char> obstacles, out Coords stepDestination)
    {
        var newGuardDirection = map[guardPosition.R][guardPosition.C];
        stepDestination = guardPosition.DoMovement(Movements[newGuardDirection]);

        while (stepDestination.IsInside(map) && map.HasObstacleAt(stepDestination, obstacles))
        {
            newGuardDirection = GuardChars[(Array.IndexOf(GuardChars, newGuardDirection) + 1) % GuardChars.Length];
            stepDestination = guardPosition.DoMovement(Movements[newGuardDirection]);
        }

        map.OverwriteCharOnIndexOnIndex(guardPosition.R, guardPosition.C, Day06Helpers.PassedThroughTile);

        if (stepDestination.IsInside(map))
        {
            map.OverwriteCharOnIndexOnIndex(stepDestination.R, stepDestination.C, newGuardDirection);
            //map[stepDestination.R] = map[stepDestination.R].OverwriteCharOnIndex(stepDestination.C, newGuardDirection);
            return false;
        }
        return true;
    }

    public static bool WillGetStuckInLoop(this List<string> map, Coords startingPosition, char startingOrientation)
    {
        var guardPosition = startingPosition;
        var currentOrientation = startingOrientation;
        var turningPoints = new Dictionary<Coords, char> { };
        while (!map.LeavesAfterStep(guardPosition, Day06Helpers.AllObstacles, out Coords newGuardPosition))
        {
            if (currentOrientation != map[newGuardPosition.R][newGuardPosition.C])
            {
                currentOrientation = map[newGuardPosition.R][newGuardPosition.C];
                if (!turningPoints.TryAdd(newGuardPosition, currentOrientation))
                {
                    if (turningPoints[newGuardPosition] == currentOrientation)
                    {
                        //map.Print();
                        return true;
                    }
                }
            }
            guardPosition = newGuardPosition;
        }
        return false;
    }

    public static string OverwriteCharOnIndex(this string original, int index, char newChar)
    {
        char[] charArray = original.ToCharArray();
        charArray[index] = newChar;
        return new string(charArray);
    }
    public static void OverwriteCharOnIndexOnIndex(this List<string> collection, int rowIndex, int charIndex, char newChar)
    {
        collection[rowIndex] = collection[rowIndex].OverwriteCharOnIndex(charIndex, newChar);
    }
    public static bool HasObstacleAt(this List<string> map, Coords location, IEnumerable<char> obstacles)
    {
        return obstacles.Any(o => map[location.R][location.C] == o);
    }
}