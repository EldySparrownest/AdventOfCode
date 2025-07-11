﻿namespace AdventOfCode.Y2023;

public static class Day06
{
    //private static int dayNumber = 6;
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

    //    Console.WriteLine("Part 1");

    //    var resPart1 = SolvePart1(input);
    //    Console.WriteLine("Total possible ways to win:");
    //    Console.WriteLine(resPart1);

    //    Console.WriteLine("Part 2");

    //    var resPart2 = SolvePart2(input);
    //    Console.WriteLine("Total possible ways to win:");
    //    Console.WriteLine(resPart2);

    //    Console.ReadLine();
    //}

    //public static ulong SolvePart1(string[] input)
    //{
    //    var times = new List<ulong>();
    //    var distances = new List<ulong>();
    //    var races = new List<RaceEntry>();
    //    var waysToWin = new List<ulong>();
    //    ulong totalWaysToWin = 1;

    //    for (int i = 0; i < input.Length; i++)
    //    {
    //        var line = input[i];
    //        if (string.IsNullOrEmpty(line))
    //            continue;
    //        var lineSplit = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    //        if (lineSplit[0] == "Time:")
    //        {
    //            for (int s = 1; s < lineSplit.Length; s++)
    //            {
    //                times.Add(Convert.ToUInt64(lineSplit[s]));
    //            }
    //            continue;
    //        }
    //        if (lineSplit[0] == "Distance:")
    //        {
    //            for (int s = 1; s < lineSplit.Length; s++)
    //            {
    //                distances.Add(Convert.ToUInt64(lineSplit[s]));
    //            }
    //            continue;
    //        }
    //    }

    //    for (int i = 0; i < times.Count; i++)
    //    {
    //        races.Add(new RaceEntry { Time = times[i], RecordDistance = distances[i] });
    //        var possibleWins = CountWinningStrategies(races[i]);
    //        waysToWin.Add(possibleWins);
    //        totalWaysToWin *= possibleWins;
    //    }

    //    return totalWaysToWin;
    //}

    //public static ulong SolvePart2(string[] input)
    //{
    //    var timeAsString = "";
    //    var distanceAsString = "";

    //    long totalWaysToWin = 1;

    //    for (int i = 0; i < input.Length; i++)
    //    {
    //        var line = input[i];
    //        if (string.IsNullOrEmpty(line))
    //            continue;
    //        var lineSplit = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    //        if (lineSplit[0] == "Time:")
    //        {
    //            for (int s = 1; s < lineSplit.Length; s++)
    //            {
    //                timeAsString += lineSplit[s];
    //            }
    //            continue;
    //        }
    //        if (lineSplit[0] == "Distance:")
    //        {
    //            for (int s = 1; s < lineSplit.Length; s++)
    //            {
    //                distanceAsString += lineSplit[s];
    //            }
    //            continue;
    //        }
    //    }

    //    ulong time = Convert.ToUInt64(timeAsString);
    //    ulong distance = Convert.ToUInt64(distanceAsString);

    //    var race = new RaceEntry { Time = time, RecordDistance = distance };
    //    return CountWinningStrategies(race);
    //}

    //public static ulong CountWinningStrategies(this RaceEntry race)
    //{
    //    ulong winning = 0;
    //    for (ulong i = 1; i < race.Time; i++)
    //    {
    //        if (race.WillBeatRecord(i))
    //        {
    //            winning++;
    //        }
    //    }
    //    return winning;
    //}
    //public static bool WillBeatRecord(this RaceEntry race, ulong msHolding)
    //{
    //    return (race.Time - msHolding) * msHolding > race.RecordDistance;
    //}
    //public class RaceEntry
    //{
    //    public ulong Time { get; set; }
    //    public ulong RecordDistance { get; set; }
    //}
}
