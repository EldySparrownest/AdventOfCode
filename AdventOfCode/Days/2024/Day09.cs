using System.Diagnostics;

namespace AdventOfCode.Y2024;

public class Day09 : Day
{
    public Day09()
    {
        Year = 2024;
        DayNumber = 9;
    }

    public UInt128 ProcessAsPart1(IEnumerable<string> input)
    {
        UInt128 updatedCheckSum = 0;

        foreach (var line in input)
        {
            var adjustedLine = line;
            var compressedList = new List<int>();
            for (int i = 0; i < adjustedLine.Length; i++)
            {
                //even index => file length
                if (i % 2 == 0)
                {
                    compressedList.AppendFileIDToList(adjustedLine, i);
                }
                //odd index => empty space length
                else 
                {
                    int gapLength = Int32.Parse(adjustedLine[i].ToString());
                    while (gapLength > 0) 
                    {
                        compressedList.Append1EndingFileIDToList(adjustedLine, i, out adjustedLine);
                        gapLength--;
                    }
                }
            }

            for (int i = 0; i < compressedList.Count; i++)
            {
                updatedCheckSum += UInt128.Parse($"{compressedList[i]}") * (UInt128)i;
            }
        }

        return updatedCheckSum;
    }

    public UInt128 ProcessAsPart2(IEnumerable<string> input)
    {
        UInt128 updatedCheckSum = 0;

        foreach (var line in input)
        {
            var fsDic = new Dictionary<int, int>();
            var fileSizes = new List<int>();
            var gaps = new List<int>();

            var adjustedLine = line;
            var compressedList = new List<int>();

            for (int i = 0; i < adjustedLine.Length; i++)
            {
                if ((i % 2) == 0)
                {
                    fileSizes.Add(Int32.Parse(line[i].ToString()));
                    fsDic.Add(i / 2, Int32.Parse(line[i].ToString()));
                }
                else
                {
                    gaps.Add(Int32.Parse(line[i].ToString()));
                }
            }
            gaps.Add(0);

            var fileIds = Enumerable.Range(0, fileSizes.Count).ToList();
            var fileIdsNewOrder = fileIds.ToList();
            for (int i = fileSizes.Count - 1; i >= 0; i--)
            {
                var currentFs = fsDic[fileIds[i]];
                var biggerGap = gaps.FirstOrDefault(g => g >= currentFs);
                if (biggerGap != default)
                {
                    var index = gaps.IndexOf(biggerGap);
                    if (index < fileIdsNewOrder.IndexOf(i))
                    {
                        gaps.Insert(index, 0);
                        gaps[index + 1] -= currentFs;
                        var increaseGapAt = fileIdsNewOrder.IndexOf(fileIds[i]) + 1;
                        gaps[increaseGapAt] += currentFs + gaps[increaseGapAt - 1];
                        gaps.RemoveAt(increaseGapAt - 1);
                        fileIdsNewOrder.Remove(fileIds[i]);
                        fileIdsNewOrder.Insert(index + 1, fileIds[i]);
                    }
                    //else
                    //{
                    //    Console.WriteLine($"index: {index}, i: {i}");
                    //    Day09Helpers.PrintVisualisation(fsDic, fileIdsNewOrder, gaps);
                    //}
                }
                //else
                //{
                //    Day09Helpers.PrintVisualisation(fsDic, fileIdsNewOrder, gaps);
                //}

                //Console.WriteLine();

                //Day09Helpers.PrintVisualisation(fsDic, fileIdsNewOrder, gaps);
            }

            updatedCheckSum = Day09Helpers.GetCheckSum(fsDic, fileIdsNewOrder, gaps);

            //for (int j = 0; j < fileSizes[i]; j++)
            //{
            //    updatedCheckSum += (UInt128)((2 * i) + j) * (UInt128)i;
            //}
        }

        return updatedCheckSum;
    }

    public override void SolveBothParts(bool useExample = false, bool useBrian = false, bool printIntros = false)
    {
        if (printIntros)
        {
            PartDescription(1);
        }

        var input = LoadInput(useExample, useBrian);

        //Part 1
        Stopwatch sw = Stopwatch.StartNew();
        var sum1 = ProcessAsPart1(input);
        sw.Stop();
        Console.WriteLine($"THIS MANY ticks ELAPSED: {sw.ElapsedTicks.ToString()}, which is {sw.ElapsedMilliseconds} ms.");
        Universal.WriteSolutionSection(1, "Part 1 result is: ", sum1);

        //Part 2
        Stopwatch sw2 = Stopwatch.StartNew();
        var sum2 = ProcessAsPart2(input.ToList());
        sw2.Stop();
        Console.WriteLine($"THIS MANY ticks ELAPSED: {sw2.ElapsedTicks.ToString()}, which is {sw2.ElapsedMilliseconds} ms.");
        Universal.WriteSolutionSection(2, "Part 2 result is: ", sum2);

        ////BothPartsTogether
        //ProcessBothParts(input, out int part1Res, out int part2Res);
        //Universal.WriteSolutionSection(1, "Part1 result is: ", part1Res);
        //Universal.WriteSolutionSection(2, "Part2 result is: ", part2Res);
    }
}

public static class Day09Helpers
{
    public static void AppendFileIDToList(this List<int> list, string inputLine, int i)
    {
        int fileLength = Int32.Parse($"{inputLine[i]}");
        int id = Int32.Parse($"{i / 2}");
        for (int j = 0; j < fileLength; j++)
        {
            list.Add(id);    
        }
    }
    public static void Append1EndingFileIDToList(this List<int> list, string inputLine, int index, out string adjustedInputLine)
    {
        adjustedInputLine = inputLine;
        var i = inputLine.Length - 1;

        int id = Int32.Parse($"{i / 2}");
        if (id > index / 2)
        {
            list.Add(id);

            var remainingFileSize = Int32.Parse($"{inputLine[i]}") - 1;
            if (remainingFileSize > 0)
            {
                adjustedInputLine = inputLine.OverwriteCharOnIndex(i, $"{remainingFileSize}"[0]);
                return;
            }
            adjustedInputLine = inputLine.Remove(i - 1);
        }
    }
    public static void PrintVisualisation(Dictionary<int, int> fsDic, List<int> fileOrder, List<int> gaps)
    {
        Console.WriteLine(GetVisualisation(fsDic, fileOrder, gaps));
    }
    public static string GetVisualisation(Dictionary<int, int> fsDic, List<int> fileOrder, List<int> gaps)
    {
        var visualisation = string.Empty;
        for (int i = 0; i < fileOrder.Count; i++)
        {
            for (int fs = 0; fs < fsDic[fileOrder[i]]; fs++)
            {
                visualisation += fileOrder[i];
            }
            for (int g = 0; g < gaps[i]; g++)
            {
                visualisation += '.';
            }
        }
        for (int i = fileOrder.Count; i < gaps.Count; i++)
        {
            for (int g = 0; g < gaps[i]; g++)
            {
                visualisation += '.';
            }
        }
        return visualisation;
    }
    public static List<int> GetCompressedDriveAsList(Dictionary<int, int> fsDic, List<int> fileOrder, List<int> gaps)
    {
        var compressedList = new List<int>();
        for (int i = 0; i < fileOrder.Count; i++)
        {
            for (int fs = 0; fs < fsDic[fileOrder[i]]; fs++)
            {
                compressedList.Add(fileOrder[i]);
            }
            for (int g = 0; g < gaps[i]; g++)
            {
                compressedList.Add(0);
            }
        }
        return compressedList;
    }
    public static UInt128 GetCheckSum(Dictionary<int, int> fsDic, List<int> fileOrder, List<int> gaps)
    {
        UInt128 checkSum = 0;
        var compressed = GetCompressedDriveAsList(fsDic, fileOrder, gaps);

        for (int i = 0; i < compressed.Count; i++)
        {
            checkSum += UInt128.Parse($"{compressed[i]}") * (UInt128)i;
        }

        return checkSum;
    }
}