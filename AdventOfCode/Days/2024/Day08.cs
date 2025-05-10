using System.Data;

namespace AdventOfCode.Y2024;

public class Day08 : Day
{
    public Day08()
    {
        Year = 2024;
        DayNumber = 8;
    }

    public int ProcessAsPart1(IEnumerable<string> input)
    {
        var map = input.ToList();
        HashSet<Coords> antinodePositions = new HashSet<Coords>();
        HashSet<char> antenaCodes = new HashSet<char>();

        for (var r = 0; r < map.Count; r++)
        {
            for (int c = 0; c < map[r].Length; c++)
            {
                if (map[r][c] != '.' && !antenaCodes.Contains(map[r][c]))
                {
                    antenaCodes.Add(map[r][c]);
                    map.GetAntinodeCoords(antinodePositions, map[r][c]);
                }
            }
        }

        return map.CountValidAntinodes(antinodePositions);
    }

    public int ProcessAsPart2(IEnumerable<string> input)
    {
        var map = input.ToList();
        HashSet<Coords> antinodePositions = new HashSet<Coords>();
        HashSet<char> antenaCodes = new HashSet<char>();

        for (var r = 0; r < map.Count; r++)
        {
            for (int c = 0; c < map[r].Length; c++)
            {
                if (map[r][c] != '.' && !antenaCodes.Contains(map[r][c]))
                {
                    antenaCodes.Add(map[r][c]);
                    map.GetHarmonicAntinodeCoords(antinodePositions, map[r][c]);
                }
            }
        }

        return map.CountValidAntinodes(antinodePositions);
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
        Universal.WriteSolutionSection(1, "Part 1 result is: ", sum1);

        //Part 2
        //Stopwatch sw = Stopwatch.StartNew();
        var sum2 = ProcessAsPart2(input.ToList());
        //sw.Stop();
        //Console.WriteLine("THIS MANY ticks ELAPSED: " + sw.ElapsedTicks.ToString());
        Universal.WriteSolutionSection(2, "Part 2 result is: ", sum2);

        ////BothPartsTogether
        //ProcessBothParts(input, out int part1Res, out int part2Res);
        //Universal.WriteSolutionSection(1, "Part1 result is: ", part1Res);
        //Universal.WriteSolutionSection(2, "Part2 result is: ", part2Res);
    }
}

public static class Day08Helpers
{
    public static void GetAntinodeCoords(this List<string> map, HashSet<Coords> antinodes, char antenaCode)
    {
        List<Coords> antenas = new List<Coords>();
        for (int r = 0; r < map.Count; r++)
        {
            if (!map[r].Contains(antenaCode))
                continue;

            var antenaIndex = map[r].IndexOf(antenaCode);
            while (antenaIndex > -1 && antenaIndex < map[r].Length)
            {
                antenas.Add(new Coords(r, antenaIndex));
                antenaIndex = map[r].Substring(antenaIndex + 1).Contains(antenaCode) ? antenaIndex + map[r].Substring(antenaIndex + 1).IndexOf(antenaCode) + 1 : -1;
            }
        }

        for (int i = 0; i < antenas.Count - 1; i++)
        {
            for (int j = i + 1; j < antenas.Count; j++)
            {
                antinodes.Add(antenas[i].MakeThisCenterOfLineWith(antenas[j]));
                antinodes.Add(antenas[j].MakeThisCenterOfLineWith(antenas[i]));
            }
        }
    }
    public static void GetHarmonicAntinodeCoords(this List<string> map, HashSet<Coords> antinodes, char antenaCode)
    {
        List<Coords> antenas = new List<Coords>();
        for (int r = 0; r < map.Count; r++)
        {
            if (!map[r].Contains(antenaCode))
                continue;

            var antenaIndex = map[r].IndexOf(antenaCode);
            while (antenaIndex > -1 && antenaIndex < map[r].Length)
            {
                antenas.Add(new Coords(r, antenaIndex));
                antenaIndex = map[r].Substring(antenaIndex + 1).Contains(antenaCode) ? antenaIndex + map[r].Substring(antenaIndex + 1).IndexOf(antenaCode) + 1 : -1;
            }
        }

        for (int i = 0; i < antenas.Count - 1; i++)
        {
            for (int j = i + 1; j < antenas.Count; j++)
            {
                antinodes.Add(antenas[i]);
                antinodes.Add(antenas[j]);
                var baseAntinode = antenas[i];
                var paramAntinode = antenas[j];
                var discoveredAntinode = baseAntinode.MakeThisCenterOfLineWith(paramAntinode);
                while (discoveredAntinode.IsInside(map) && baseAntinode.IsInside(map))
                {
                    antinodes.Add(discoveredAntinode);
                    paramAntinode = baseAntinode;
                    baseAntinode = discoveredAntinode;
                    discoveredAntinode = baseAntinode.MakeThisCenterOfLineWith(paramAntinode);
                }

                baseAntinode = antenas[j];
                paramAntinode = antenas[i];
                discoveredAntinode = baseAntinode.MakeThisCenterOfLineWith(paramAntinode);
                while (discoveredAntinode.IsInside(map) && baseAntinode.IsInside(map))
                {
                    antinodes.Add(discoveredAntinode);
                    paramAntinode = baseAntinode;
                    baseAntinode = discoveredAntinode;
                    discoveredAntinode = baseAntinode.MakeThisCenterOfLineWith(paramAntinode);
                }
            }
        }
    }

    public static int CountValidAntinodes(this List<string> map, HashSet<Coords> antinodes)
    {
        int count = 0;
        foreach (var antinode in antinodes)
        {
            if (antinode.IsInside(map))
            {
                count++;
            }
        }
        return count;
    }
}