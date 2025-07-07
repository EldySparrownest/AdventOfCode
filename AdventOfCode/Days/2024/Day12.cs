using System.Data;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2024;

public class Day12 : Day
{
    public Day12()
    {
        Year = 2024;
        DayNumber = 12;
    }

    public long ProcessAsPart1(IEnumerable<string> input)
    {
        var map = input.ToList();
        var regionDic = new Dictionary<char, List<Region>>();

        for (var r = 0; r < map.Count; r++)
        {
            for (int c = 0; c < map[r].Length; c++)
            {
                var currentCoords = new Coords(r, c);
                var neededFencing = map.GetFencingCountOfSquare(currentCoords);

                if (regionDic.TryGetValue(map[r][c], out var preexistingRegions)
                    && preexistingRegions.Any(r => r.NeighboursWith(currentCoords)))
                {
                    var neighbouring = preexistingRegions.Where(r => r.NeighboursWith(currentCoords)).ToList();
                    preexistingRegions.RemoveAll(r => r.NeighboursWith(currentCoords));

                    neighbouring[0].AddSquare(currentCoords, neededFencing);

                    for (int i = neighbouring.Count - 1; i > 0; i--)
                    {
                        neighbouring[0].MergeWithRegion(neighbouring[i]);
                    }

                    preexistingRegions.Add(neighbouring[0]);
                    regionDic[map[r][c]] = preexistingRegions;
                }
                else
                {
                    regionDic.TryAdd(map[r][c], new List<Region>());

                    var region = new Region();
                    region.PlantType = map[r][c];
                    region.AddSquare(currentCoords, neededFencing);
                    regionDic[map[r][c]].Add(region);
                }
            }
        }

        long totalFencingCost = 0;
        foreach (var regionList in regionDic.Values)
        {
            totalFencingCost += regionList.Sum(r => r.FencingCost);
        }

        return totalFencingCost;
    }

    public long ProcessAsPart2(IEnumerable<string> input)
    {
        var map = input.ToList();
        var regionDic = new Dictionary<char, List<Region>>();

        for (var r = 0; r < map.Count; r++)
        {
            for (int c = 0; c < map[r].Length; c++)
            {
                var currentCoords = new Coords(r, c);
                var neededFencing = map.GetFencingCountOfSquare(currentCoords);

                if (regionDic.TryGetValue(map[r][c], out var preexistingRegions)
                    && preexistingRegions.Any(r => r.NeighboursWith(currentCoords)))
                {
                    var neighbouring = preexistingRegions.Where(r => r.NeighboursWith(currentCoords)).ToList();
                    preexistingRegions.RemoveAll(r => r.NeighboursWith(currentCoords));

                    neighbouring[0].AddSquare(currentCoords, neededFencing);

                    for (int i = neighbouring.Count - 1; i > 0; i--)
                    {
                        neighbouring[0].MergeWithRegion(neighbouring[i]);
                    }

                    preexistingRegions.Add(neighbouring[0]);
                    regionDic[map[r][c]] = preexistingRegions;
                }
                else
                {
                    regionDic.TryAdd(map[r][c], new List<Region>());

                    var region = new Region();
                    region.PlantType = map[r][c];
                    region.AddSquare(currentCoords, neededFencing);
                    regionDic[map[r][c]].Add(region);
                }
            }
        }

        long totalFencingCost = 0;
        foreach (var regionList in regionDic.Values)
        {
            totalFencingCost += regionList.Sum(r => r.BulkFencingCost);
        }

        return totalFencingCost;
    }

    public static void ProcessBothParts(IEnumerable<string> input, out long part1Res, out long part2Res)
    {
        var map = input.ToList();
        var regionDic = new Dictionary<char, List<Region>>();

        for (var r = 0; r < map.Count; r++)
        {
            for (int c = 0; c < map[r].Length; c++)
            {
                var currentCoords = new Coords(r, c);
                var neededFencing = map.GetFencingCountOfSquare(currentCoords);

                if (regionDic.TryGetValue(map[r][c], out var preexistingRegions)
                    && preexistingRegions.Any(r => r.NeighboursWith(currentCoords)))
                {
                    var neighbouring = preexistingRegions.Where(r => r.NeighboursWith(currentCoords)).ToList();
                    preexistingRegions.RemoveAll(r => r.NeighboursWith(currentCoords));

                    neighbouring[0].AddSquare(currentCoords, neededFencing);

                    for (int i = neighbouring.Count - 1; i > 0; i--)
                    {
                        neighbouring[0].MergeWithRegion(neighbouring[i]);
                    }

                    preexistingRegions.Add(neighbouring[0]);
                    regionDic[map[r][c]] = preexistingRegions;
                }
                else
                {
                    regionDic.TryAdd(map[r][c], new List<Region>());

                    var region = new Region();
                    region.PlantType = map[r][c];
                    region.AddSquare(currentCoords, neededFencing);
                    regionDic[map[r][c]].Add(region);
                }
            }
        }

        part1Res = 0;
        part2Res = 0;
        foreach (var regionList in regionDic.Values)
        {
            part1Res += regionList.Sum(r => r.FencingCost);
            part2Res += regionList.Sum(r => r.BulkFencingCost);
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
        ProcessBothParts(input, out var part1Res, out var part2Res);
        sw12.Stop();
        Console.WriteLine($"THIS MANY ticks ELAPSED: {sw12.ElapsedTicks} which is {sw12.ElapsedMilliseconds} ms");
        Universal.WriteSolutionSection(1, "Part1 result is: ", part1Res);
        Universal.WriteSolutionSection(2, "Part2 result is: ", part2Res);
    }
}

public static class Day12Helpers
{
    public static int GetFencingCountOfSquare(this List<string> map, Coords square)
    {
        var plantType = map[square.R][square.C];
        var fencingNeeded = GridMovement.Movements.Select(m => square.DoMovement(m))
            .Count(neighbour => !neighbour.IsInside(map)
                || map[neighbour.R][neighbour.C] != plantType);
        
        return fencingNeeded;
    }
}

public class Region
{
    public char PlantType { get; set; }
    public HashSet<Coords> Squares { get; set; } = new HashSet<Coords>();
    public long Area => Squares.Count;
    public long Perimeter { get; set; } = 0;
    public long FencingCost => Area * Perimeter;
    public long BulkFencingCost => Area * GetSideCount();

    public void AddSquare(Coords square, int fencing)
    {
        Squares.Add(square);
        Perimeter += fencing;
    }

    public int GetSideCount()
    {
        var minRow = Squares.MinBy(s => s.R).R;
        var maxRow = Squares.MaxBy(s => s.R).R;
        var minCol = Squares.MinBy(s => s.C).C;
        var maxCol = Squares.MaxBy(s => s.C).C;

        var topSides = 0;
        var bottomSides = 0;
        var leftSides = 0;
        var rightSides = 0;

        for (int r = minRow; r <= maxRow; r++)
        {
            var ignoreUpTil = minCol;
            var ignoreDownTil = minCol;
            for (int c = minCol; c <= maxCol; c++)
            {
                var position = new Coords(r, c);
                if (!Squares.Contains(position))
                {
                    continue;
                }

                if (c >= ignoreUpTil)
                {
                    var upNeighbour = position.DoMovement(GridMovement.Up);
                    if (!Squares.Contains(upNeighbour))
                    {
                        topSides++;
                        var stepRight = position.DoMovement(GridMovement.Right);
                        while (Squares.Contains(stepRight) && !Squares.Contains(stepRight.DoMovement(GridMovement.Up)))
                        {
                            stepRight = stepRight.DoMovement(GridMovement.Right);
                        }
                        ignoreUpTil = stepRight.C + 1;
                    }
                }

                if (c >= ignoreDownTil)
                {
                    var downNeighbour = position.DoMovement(GridMovement.Down);
                    if (!Squares.Contains(downNeighbour))
                    {
                        bottomSides++;
                        var stepRight = position.DoMovement(GridMovement.Right);
                        while (Squares.Contains(stepRight) && !Squares.Contains(stepRight.DoMovement(GridMovement.Down)))
                        {
                            stepRight = stepRight.DoMovement(GridMovement.Right);
                        }
                        ignoreDownTil = stepRight.C + 1;
                    }
                }
            }
        }


        for (int c = minCol; c <= maxCol; c++)
        {
            var ignoreLeftTil = minRow;
            var ignoreRightTil = minRow;
            for (int r = minRow; r <= maxRow; r++)
            {
                var position = new Coords(r, c);
                if (!Squares.Contains(position))
                {
                    continue;
                }

                if (r >= ignoreLeftTil)
                {
                    var leftNeighbour = position.DoMovement(GridMovement.Left);
                    if (!Squares.Contains(leftNeighbour))
                    {
                        leftSides++;
                        var stepDown = position.DoMovement(GridMovement.Down);
                        while (Squares.Contains(stepDown) && !Squares.Contains(stepDown.DoMovement(GridMovement.Left)))
                        {
                            stepDown = stepDown.DoMovement(GridMovement.Down);
                        }
                        ignoreLeftTil = stepDown.R + 1;
                    }
                }

                if (r >= ignoreRightTil)
                {
                    var rightNeighbour = position.DoMovement(GridMovement.Right);
                    if (!Squares.Contains(rightNeighbour))
                    {
                        rightSides++;
                        var stepDown = position.DoMovement(GridMovement.Down);
                        while (Squares.Contains(stepDown) && !Squares.Contains(stepDown.DoMovement(GridMovement.Right)))
                        {
                            stepDown = stepDown.DoMovement(GridMovement.Down);
                        }
                        ignoreRightTil = stepDown.R + 1;    
                    }
                }
            }
        }

        return topSides + bottomSides + leftSides + rightSides;
    }

    public void MergeWithRegion(Region otherRegion)
    {
        Squares.UnionWith(otherRegion.Squares);
        Perimeter += otherRegion.Perimeter;
    }

    public bool NeighboursWith(Coords potentialNeighbour)
    {
        return Squares.Any(s => s.IsOrthogonallyAdjacentTo(potentialNeighbour));
    }
}