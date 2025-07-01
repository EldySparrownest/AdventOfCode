using System.Diagnostics;

namespace AdventOfCode.Y2023;

public class Day05 : Day
{
    public Day05()
    {
        Year = 2023;
        DayNumber = 5;
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
        //var sum1 = SolvePart1(input);
        //sw1.Stop();
        //Console.WriteLine($"THIS MANY ticks ELAPSED: {sw1.ElapsedTicks} which is {sw1.ElapsedMilliseconds} ms");
        //Universal.WriteSolutionSection(1, "Part 1 result is: ", sum1);

        //Part 2
        Stopwatch sw2 = Stopwatch.StartNew();
        var sum2 = SolvePart2(input);
        sw2.Stop();
        Console.WriteLine($"THIS MANY ticks ELAPSED: {sw2.ElapsedTicks} which is {sw2.ElapsedMilliseconds} ms");
        Universal.WriteSolutionSection(2, "Part 2 result is: ", sum2);

        ////BothPartsTogether
        //Stopwatch sw12 = Stopwatch.StartNew();
        //ProcessBothParts(input, out int part1Res, out int part2Res);
        //sw12.Stop();
        //Console.WriteLine($"THIS MANY ticks ELAPSED: {sw12.ElapsedTicks} which is {sw12.ElapsedMilliseconds} ms");
        //Universal.WriteSolutionSection(1, "Part1 result is: ", part1Res);
        //Universal.WriteSolutionSection(2, "Part2 result is: ", part2Res);
    }

    public ulong SolvePart1(string[] input)
    {
        var seeds = new List<ulong>();
        var seedGuides = new List<GardeningEntry>();
        var gardeningDic = new Dictionary<ulong, GardeningEntry>();
        var seedToSoil = new List<ConversionEntry>();
        var soilToFertiliser = new List<ConversionEntry>();
        var fertiliserToWater = new List<ConversionEntry>();
        var waterToLight = new List<ConversionEntry>();
        var lightToTemperature = new List<ConversionEntry>();
        var temperatureToHumidity = new List<ConversionEntry>();
        var humidityToLocation = new List<ConversionEntry>();

        var mapNameDictionary = new Dictionary<string, List<ConversionEntry>>
        {
            { "seed-to-soil", seedToSoil },
            { "soil-to-fertilizer", soilToFertiliser },
            { "fertilizer-to-water", fertiliserToWater },
            { "water-to-light", waterToLight },
            { "light-to-temperature", lightToTemperature },
            { "temperature-to-humidity", temperatureToHumidity },
            { "humidity-to-location", humidityToLocation },
        };

        //Part 1
        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i];
            if (string.IsNullOrEmpty(line))
                continue;
            var lineSplit = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (lineSplit[0] == "seeds:")
            {
                for (int s = 1; s < lineSplit.Length; s++)
                {
                    seeds.Add(Convert.ToUInt64(lineSplit[s]));
                }
                i++;
                continue;
            }
            var mapList = mapNameDictionary[lineSplit[0]];
            i++;
            while (i < input.Length && !string.IsNullOrWhiteSpace(input[i]))
            {
                lineSplit = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var conversion = new ConversionEntry(Convert.ToUInt64(lineSplit[0]),
                    Convert.ToUInt64(lineSplit[1]),
                    Convert.ToUInt64(lineSplit[2]));
                mapList.Add(conversion);
                i++;
            }
        }

        foreach (var seed in seeds)
        {
            var soil = seedToSoil.GetDestination(seed);
            var fertiliser = soilToFertiliser.GetDestination(soil);
            var water = fertiliserToWater.GetDestination(fertiliser);
            var light = waterToLight.GetDestination(water);
            var temperature = lightToTemperature.GetDestination(light);
            var humidity = temperatureToHumidity.GetDestination(temperature);
            var location = humidityToLocation.GetDestination(humidity);
            var seedGuide = new GardeningEntry
            {
                Seed = seed,
                Soil = soil,
                Fertiliser = fertiliser,
                Water = water,
                Light = light,
                Temperature = temperature,
                Humidity = humidity,
                Location = location,
            };
            seedGuides.Add(seedGuide);
            gardeningDic.Add(seed, seedGuide);
        }
        return seedGuides.MinBy(seed => seed.Location)!.Location;
    }

    public static ulong SolvePart2(string[] input)
    {
        var seedRanges = new List<SeedRange>();
        var seedGuides = new List<GardeningEntry>();
        var gardeningDic = new Dictionary<ulong, GardeningEntry>();
        var seedToSoil = new List<ConversionEntry>();
        var soilToFertiliser = new List<ConversionEntry>();
        var fertiliserToWater = new List<ConversionEntry>();
        var waterToLight = new List<ConversionEntry>();
        var lightToTemperature = new List<ConversionEntry>();
        var temperatureToHumidity = new List<ConversionEntry>();
        var humidityToLocation = new List<ConversionEntry>();

        var mapNameDictionary = new Dictionary<string, List<ConversionEntry>>
        {
            { "seed-to-soil", seedToSoil },
            { "soil-to-fertilizer", soilToFertiliser },
            { "fertilizer-to-water", fertiliserToWater },
            { "water-to-light", waterToLight },
            { "light-to-temperature", lightToTemperature },
            { "temperature-to-humidity", temperatureToHumidity },
            { "humidity-to-location", humidityToLocation },
        };

        //Prepare data
        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i];
            if (string.IsNullOrEmpty(line))
                continue;
            var lineSplit = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (lineSplit[0] == "seeds:")
            {
                for (int s = 1; s < lineSplit.Length; s+=2)
                {
                    seedRanges.Add(new SeedRange { 
                        Start = Convert.ToUInt64(lineSplit[s]), 
                        Length = Convert.ToUInt64(lineSplit[s + 1]) 
                    });
                }
                i++;
                continue;
            }
            var mapList = mapNameDictionary[lineSplit[0]];
            i++;
            while (i < input.Length && !string.IsNullOrWhiteSpace(input[i]))
            {
                lineSplit = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var conversion = new ConversionEntry(Convert.ToUInt64(lineSplit[0]),
                    Convert.ToUInt64(lineSplit[1]),
                    Convert.ToUInt64(lineSplit[2]));
                mapList.Add(conversion);
                i++;
            }
        }

        ulong? minSeedLocation = null;
        ulong testedLocation = 1;

        while (minSeedLocation == null)
        {
            var humidity = humidityToLocation.GetSource(testedLocation);
            var temperature = temperatureToHumidity.GetSource(humidity);
            var light = lightToTemperature.GetSource(temperature);
            var water = waterToLight.GetSource(light);
            var fertiliser = fertiliserToWater.GetSource(water);
            var soil = soilToFertiliser.GetSource(fertiliser);
            var seed = seedToSoil.GetSource(soil);

            if (seedRanges.Any(sr => sr.ContainsSeed(seed)))
            {
                minSeedLocation = testedLocation;
            }

            testedLocation++;
        }
        return minSeedLocation.Value;
    }

    private static List<ConversionEntry> GetSuitableRanges(List<ConversionEntry> mapList, (ulong, ulong) borders)
    {
        var potentialOnes = mapList.Where(e => e.LeadsToRange(borders));
        var fittingCopies = new List<ConversionEntry>();
        foreach (var potential in potentialOnes)
        {
            fittingCopies.Add(potential.CopyWithinBorders(borders));
        }
        return fittingCopies;
    }
}

public static class Day05Helpers
{
    public static ulong GetDestination(this List<ConversionEntry> mapList, ulong source)
    {
        return mapList.Where(m => m.HasInRange(source)).FirstOrDefault()?.ConvertedDestination(source) ?? source;
    }

    public static ulong GetSource(this List<ConversionEntry> mapList, ulong destination)
    {
        return mapList.Where(m => m.HasDestinationInRange(destination)).FirstOrDefault()?.ConvertedSource(destination) ?? destination;
    }

    public static (ulong, ulong) GetSourceBorders(this ConversionEntry entry)
    {
        return (entry.SourceStart, entry.SourceStart + entry.RangeLength);
    }
}

public class GardeningEntry
{
    public ulong Seed { get; set; }
    public ulong Soil { get; set; }
    public ulong Fertiliser { get; set; }
    public ulong Water { get; set; }
    public ulong Light { get; set; }
    public ulong Temperature { get; set; }
    public ulong Humidity { get; set; }
    public ulong Location { get; set; }

    public void Print() => Console.WriteLine($"Seed {Seed}, soil {Soil}, fertilizer {Fertiliser}, water {Water}, light {Light}, temperature {Temperature}, humidity {Humidity}, location {Location}");
}

public class ConversionEntry
{
    public ulong DestinationStart { get; set; }
    public ulong SourceStart { get; set; }
    public ulong RangeLength { get; set; }
    public ulong SourceEnd() => SourceStart + RangeLength;
    public ulong DestinationEnd() => DestinationStart + RangeLength;
    public bool HasInRange(ulong source) => source >= SourceStart && source <= SourceEnd();
    public bool HasDestinationInRange(ulong destination) => destination >= DestinationStart && destination <= DestinationEnd();
    public bool LeadsToRange((ulong, ulong) borders)
    {
        return (borders.Item1 <= DestinationStart && borders.Item2 >= DestinationStart)
            || (borders.Item1 >= DestinationStart && borders.Item1 <= DestinationEnd());
    }
    public ConversionEntry(ulong destinationStart, ulong sourceStart, ulong rangeLenght)
    {
        DestinationStart = destinationStart;
        SourceStart = sourceStart;
        RangeLength = rangeLenght;
    }
    public ConversionEntry CopyWithinBorders((ulong, ulong) borders) 
    { 
        var newDestStart = DestinationStart > borders.Item1 ?
            DestinationStart : borders.Item1;
        var newDestEnd = DestinationEnd() < borders.Item2 ?
            DestinationEnd() : borders.Item2;
        var newRangeLength = newDestEnd - newDestStart;
        var moveSourceStartBy = newDestStart - DestinationStart;
        return new ConversionEntry(newDestStart, SourceStart += moveSourceStartBy, 
            newRangeLength);
    }
    public ulong ConvertedDestination(ulong source) => DestinationStart + (source - SourceStart);
    public ulong ConvertedSource(ulong destination) => SourceStart + (destination - DestinationStart);
}

public class SeedRange
{
    public ulong Start {  get; set; }
    public ulong Length { get; set; }

    public bool ContainsSeed(ulong seed) => Start <= seed && seed <= Start + Length;
}
