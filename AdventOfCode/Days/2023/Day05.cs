namespace AdventOfCode.Y2023;

public class Day05 : Day
{
    public Day05()
    {
        Year = 2023;
        DayNumber = 5;
    }

    //public static void SolveBothParts(bool useExample = false, bool useBrian = false)
    //{
    //    Console.WriteLine($"Advent of Code 2023 - Day {dayNumber}");
    //    Console.WriteLine("Part 1");
    //    //Console.Write("For example:\r\n\r\nCard 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53\r\nCard 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19\r\nCard 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1\r\nCard 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83\r\nCard 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36\r\nCard 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11\r\nIn the above example, card 1 has five winning numbers (41, 48, 83, 86, and 17) and eight numbers you have (83, 86, 6, 31, 17, 9, 48, and 53). Of the numbers you have, four of them (48, 83, 17, and 86) are winning numbers! That means card 1 is worth 8 points (1 for the first match, then doubled three times for each of the three matches after the first).\r\n\r\nCard 2 has two winning numbers (32 and 61), so it is worth 2 points.\r\nCard 3 has two winning numbers (1 and 21), so it is worth 2 points.\r\nCard 4 has one winning number (84), so it is worth 1 point.\r\nCard 5 has no winning numbers, so it is worth no points.\r\nCard 6 has no winning numbers, so it is worth no points.\r\nSo, in this example, the Elf's pile of scratchcards is worth 13 points.\r\n\r\nTake a seat in the large pile of colorful cards. How many points are they worth in total?");
    //    //Console.WriteLine();

    //    //Console.WriteLine("Part 2");
    //    //Console.Write("This time, the above example goes differently:\r\n\r\nCard 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53\r\nCard 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19\r\nCard 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1\r\nCard 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83\r\nCard 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36\r\nCard 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11\r\nCard 1 has four matching numbers, so you win one copy each of the next four cards: cards 2, 3, 4, and 5.\r\nYour original card 2 has two matching numbers, so you win one copy each of cards 3 and 4.\r\nYour copy of card 2 also wins one copy each of cards 3 and 4.\r\nYour four instances of card 3 (one original and three copies) have two matching numbers, so you win four copies each of cards 4 and 5.\r\nYour eight instances of card 4 (one original and seven copies) have one matching number, so you win eight copies of card 5.\r\nYour fourteen instances of card 5 (one original and thirteen copies) have no matching numbers and win no more cards.\r\nYour one instance of card 6 (one original) has no matching numbers and wins no more cards.\r\nOnce all of the originals and copies have been processed, you end up with 1 instance of card 1, 2 instances of card 2, 4 instances of card 3, 8 instances of card 4, 14 instances of card 5, and 1 instance of card 6. In total, this example pile of scratchcards causes you to ultimately have 30 scratchcards!\r\n\r\nProcess all of the original and copied scratchcards until no more scratchcards are won. Including the original set of scratchcards, how many total scratchcards do you end up with?");
    //    //Console.WriteLine();

    //    var input = Universal.LoadInput(dayNumber, useExample, useBrian);

    //    var seeds = new List<long>();
    //    var seedGuides = new List<GardeningEntry>();
    //    var gardeningDic = new Dictionary<long, GardeningEntry>();
    //    var seedToSoil = new List<ConversionEntry>();
    //    var soilToFertiliser = new List<ConversionEntry>();
    //    var fertiliserToWater = new List<ConversionEntry>();
    //    var waterToLight = new List<ConversionEntry>();
    //    var lightToTemperature = new List<ConversionEntry>();
    //    var temperatureToHumidity = new List<ConversionEntry>();
    //    var humidityToLocation = new List<ConversionEntry>();

    //    var mapNameDictionary = new Dictionary<string, List<ConversionEntry>>
    //    {
    //        { "seed-to-soil", seedToSoil },
    //        { "soil-to-fertilizer", soilToFertiliser },
    //        { "fertilizer-to-water", fertiliserToWater },
    //        { "water-to-light", waterToLight },
    //        { "light-to-temperature", lightToTemperature },
    //        { "temperature-to-humidity", temperatureToHumidity },
    //        { "humidity-to-location", humidityToLocation },
    //    };

    //    //Part 1
    //    /*
    //    //for (int i = 0; i < input.Length; i++)
    //    //{
    //    //    var line = input[i];
    //    //    if (string.IsNullOrEmpty(line)) 
    //    //        continue;
    //    //    var lineSplit = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    //    //    if (lineSplit[0] == "seeds:")
    //    //    {
    //    //        for (int s = 1; s < lineSplit.Length; s++)
    //    //        {
    //    //            seeds.Add(Convert.ToInt64(lineSplit[s]));
    //    //        }
    //    //        i++;
    //    //        continue;
    //    //    }
    //    //    var mapList = mapNameDictionary[lineSplit[0]];
    //    //    i++;
    //    //    while (i < input.Length && !string.IsNullOrWhiteSpace(input[i])) {
    //    //        lineSplit = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    //    //        var conversion = new ConversionEntry
    //    //        {
    //    //            DestinationStart = Convert.ToInt64(lineSplit[0]),
    //    //            SourceStart = Convert.ToInt64(lineSplit[1]),
    //    //            RangeLength = Convert.ToInt64(lineSplit[2]),
    //    //        };
    //    //        mapList.Add(conversion);
    //    //        i++;
    //    //    }
    //    //}

    //    //foreach (var seed in seeds)
    //    //{
    //    //    var soil = seedToSoil.GetDestination(seed);
    //    //    var fertiliser = soilToFertiliser.GetDestination(soil);
    //    //    var water = fertiliserToWater.GetDestination(fertiliser);
    //    //    var light = waterToLight.GetDestination(water);
    //    //    var temperature = lightToTemperature.GetDestination(light);
    //    //    var humidity = temperatureToHumidity.GetDestination(temperature);
    //    //    var location = humidityToLocation.GetDestination(humidity);
    //    //    var seedGuide = new GardeningEntry
    //    //    {
    //    //        Seed = seed,
    //    //        Soil = soil,
    //    //        Fertiliser = fertiliser,
    //    //        Water = water,
    //    //        Light = light,
    //    //        Temperature = temperature,
    //    //        Humidity = humidity,
    //    //        Location = location,
    //    //    };
    //    //    seedGuides.Add(seedGuide);
    //    //    gardeningDic.Add(seed, seedGuide);
    //    //}
    //    */


    //    //Part 2
    //    var seedStarts = new List<long>();
    //    var seedEnds = new List<long>();
    //    for (int i = 0; i < input.Length; i++)
    //    {
    //        var line = input[i];
    //        if (string.IsNullOrEmpty(line))
    //            continue;
    //        var lineSplit = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    //        if (lineSplit[0] == "seeds:")
    //        {
    //            for (int p = 1; p + 1 < lineSplit.Length; p += 2)
    //            {
    //                seedStarts.Add(Convert.ToInt64(lineSplit[p]));
    //                seedEnds.Add(Convert.ToInt64(lineSplit[p + 1]));
    //            }
    //            i++;
    //            continue;
    //        }
    //        var mapList = mapNameDictionary[lineSplit[0]];
    //        i++;
    //        while (i < input.Length && !string.IsNullOrWhiteSpace(input[i]))
    //        {
    //            lineSplit = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    //            var conversion = new ConversionEntry(Convert.ToInt64(lineSplit[0]),
    //                Convert.ToInt64(lineSplit[1]),
    //                Convert.ToInt64(lineSplit[2]));
    //            mapList.Add(conversion);
    //            i++;
    //        }
    //    }

    //    var minLoc = humidityToLocation.MinBy(htl => htl.DestinationStart);
    //    var locBorders = minLoc.GetSourceBorders();
    //    var minHum = GetSuitableRanges(temperatureToHumidity, locBorders);

    //    //Shared for both parts
    //    //foreach (var seed in seeds)
    //    //{
    //    //    var soil = seedToSoil.GetDestination(seed);
    //    //    var fertiliser = soilToFertiliser.GetDestination(soil);
    //    //    var water = fertiliserToWater.GetDestination(fertiliser);
    //    //    var light = waterToLight.GetDestination(water);
    //    //    var temperature = lightToTemperature.GetDestination(light);
    //    //    var humidity = temperatureToHumidity.GetDestination(temperature);
    //    //    var location = humidityToLocation.GetDestination(humidity);
    //    //    var seedGuide = new GardeningEntry {
    //    //        Seed = seed,
    //    //        Soil = soil,
    //    //        Fertiliser = fertiliser,
    //    //        Water = water,
    //    //        Light = light,
    //    //        Temperature = temperature,
    //    //        Humidity = humidity,
    //    //        Location = location,
    //    //    };
    //    //    seedGuides.Add(seedGuide);
    //    //    gardeningDic.Add(seed, seedGuide);
    //    //}

    //    //foreach (var entry in gardeningDic)
    //    //{
    //    //    entry.Value.Print();
    //    //}

    //    //var lowestLocation = seedGuides.MinBy(seed => seed.Location);
    //    //Console.WriteLine("The lowest location number corresponding to an initial seed number is:");
    //    //Console.WriteLine(lowestLocation.Location);

    //    ////Part 2
    //    //var sum = SolvePart2(input);

    //    //Console.WriteLine("The total amount of scratchcards is:");
    //    //Console.WriteLine(sum);

    //    Console.ReadLine();
    //}

    public long SolvePart1(string[] input)
    {
        var seeds = new List<long>();
        var seedGuides = new List<GardeningEntry>();
        var gardeningDic = new Dictionary<long, GardeningEntry>();
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
                    seeds.Add(Convert.ToInt64(lineSplit[s]));
                }
                i++;
                continue;
            }
            var mapList = mapNameDictionary[lineSplit[0]];
            i++;
            while (i < input.Length && !string.IsNullOrWhiteSpace(input[i]))
            {
                lineSplit = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var conversion = new ConversionEntry(Convert.ToInt64(lineSplit[0]),
                    Convert.ToInt64(lineSplit[1]),
                    Convert.ToInt64(lineSplit[2]));
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
        return seedGuides.MinBy(seed => seed.Location).Location;
    }

    public static int SolvePart2(string[] input)
    {
        var sum = 0;
        return sum;
    }

    private static List<ConversionEntry> GetSuitableRanges(List<ConversionEntry> mapList, (long, long) borders)
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
    public static long GetDestination(this List<ConversionEntry> mapList, long source)
    {
        return mapList.Where(m => m.HasInRange(source)).FirstOrDefault()?.ConvertedDestination(source) ?? source;
    }
    public static (long, long) GetSourceBorders(this ConversionEntry entry)
    {
        return (entry.SourceStart, entry.SourceStart + entry.RangeLength);
    }
}

public class GardeningEntry
{
    public long Seed { get; set; }
    public long Soil { get; set; }
    public long Fertiliser { get; set; }
    public long Water { get; set; }
    public long Light { get; set; }
    public long Temperature { get; set; }
    public long Humidity { get; set; }
    public long Location { get; set; }

    public void Print() => Console.WriteLine($"Seed {Seed}, soil {Soil}, fertilizer {Fertiliser}, water {Water}, light {Light}, temperature {Temperature}, humidity {Humidity}, location {Location}");
}

public class ConversionEntry
{
    public long DestinationStart { get; set; }
    public long SourceStart { get; set; }
    public long RangeLength { get; set; }
    public long SourceEnd() => SourceStart + RangeLength;
    public long DestinationEnd() => DestinationStart + RangeLength;
    public bool HasInRange(long source) => source >= SourceStart && source <= SourceEnd();
    public bool LeadsToRange((long, long) borders)
    {
        return (borders.Item1 <= DestinationStart && borders.Item2 >= DestinationStart)
            || (borders.Item1 >= DestinationStart && borders.Item1 <= DestinationEnd());
    }
    public ConversionEntry(long destinationStart, long sourceStart, long rangeLenght)
    {
        DestinationStart = destinationStart;
        SourceStart = sourceStart;
        RangeLength = rangeLenght;
    }
    public ConversionEntry CopyWithinBorders((long, long) borders) 
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
    public long ConvertedDestination(long source) => DestinationStart + (source - SourceStart);
}
