namespace AdventOfCode.Y2023;

public class Day03 : Day
{
    public Day03()
    {
        Year = 2023;
        DayNumber = 3;
    }

    public override void SolveBothParts(bool useExample = false, bool useBrian = false, bool printIntros = false)
    {
        if (printIntros)
        {
            PartDescription(1);
        }

        var input = LoadInput(useExample, useBrian);

        //Part 1
        var lineLength = input[0].Length;

        var digitIndexes = new List<int>();
        var symbolIndexes = new List<int>();
        int partSum = 0;

        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i];
            for (int j = 0; j < line.Length; j++)
            {
                if (char.IsDigit(line[j]))
                    digitIndexes.Add(line.Length * i + j);
                else if (line[j] != '.')
                {
                    symbolIndexes.Add(line.Length * i + j);
                }
            }
        }

        for (int i = 0; i < digitIndexes.Count;)
        {
            var numberIndexStart = digitIndexes[i];
            var numberIndexEnd = digitIndexes[i];
            var numberLength = 1;
            while (i + numberLength < digitIndexes.Count && digitIndexes[i + numberLength] % lineLength != 0)
            {
                if (digitIndexes[i + numberLength] == numberIndexStart + numberLength)
                {
                    numberIndexEnd = digitIndexes[i + numberLength];
                    numberLength++;
                }
                else break;
            }
            i += numberLength;

            var symbolSearchIndexes = new List<(int, int)>() {
                (numberIndexStart - lineLength - 1, numberIndexEnd - lineLength + 1),
                (numberIndexStart - 1, numberIndexEnd + 1),
                (numberIndexStart + lineLength - 1, numberIndexEnd + lineLength + 1),
            };
            bool isPart = false;
            foreach (var pair in symbolSearchIndexes)
            {
                for (int j = pair.Item1; j <= pair.Item2; j++)
                {
                    if (symbolIndexes.Contains(j))
                    {
                        isPart = true;
                        break;
                    }
                }
                if (isPart)
                    break;
            }

            if (isPart)
            {
                int lineIndex = numberIndexStart / lineLength;
                int startAt = numberIndexStart % lineLength;
                int endAt = numberIndexEnd % lineLength;
                var toConvert = "";
                for (int j = startAt; j <= endAt; j++)
                {
                    toConvert += input[lineIndex][j];
                }
                partSum += Convert.ToInt32(toConvert);
            }
        }

        Universal.WriteSolutionSection(1, 
            "The sum of all parts is:", 
            partSum);

        //Part 2
        if (printIntros)
        {
            PartDescription(2);
        }
        var gearRatioSum = 0;
        for (int i = 0; i < symbolIndexes.Count; i++)
        {
            if (input[symbolIndexes[i] / lineLength][symbolIndexes[i] % lineLength] != '*')
                continue;

            var mightBeGearIndex = symbolIndexes[i];
            var digitSearchIndexes = new int[] {
                mightBeGearIndex - lineLength - 1,
                mightBeGearIndex - lineLength,
                mightBeGearIndex - lineLength + 1,
                mightBeGearIndex - 1,
                mightBeGearIndex + 1,
                mightBeGearIndex + lineLength - 1,
                mightBeGearIndex + lineLength,
                mightBeGearIndex + lineLength + 1,
            };

            var neighbouringNumbersCount = 0;
            var searchPoints = new List<int>();
            if (digitIndexes.Contains(digitSearchIndexes[1]))
            {
                neighbouringNumbersCount++;
                searchPoints.Add(digitSearchIndexes[1]);
            }
            else
            {
                if (digitIndexes.Contains(digitSearchIndexes[0]))
                {
                    neighbouringNumbersCount++;
                    searchPoints.Add(digitSearchIndexes[0]);
                }
                if (digitIndexes.Contains(digitSearchIndexes[2]))
                {
                    neighbouringNumbersCount++;
                    searchPoints.Add(digitSearchIndexes[2]);
                }
            }

            if (digitIndexes.Contains(digitSearchIndexes[3]))
            {
                neighbouringNumbersCount++;
                searchPoints.Add(digitSearchIndexes[3]);
            }
            if (digitIndexes.Contains(digitSearchIndexes[4]))
            {
                neighbouringNumbersCount++;
                searchPoints.Add(digitSearchIndexes[4]);
            }

            if (digitIndexes.Contains(digitSearchIndexes[6]))
            {
                neighbouringNumbersCount++;
                searchPoints.Add(digitSearchIndexes[6]);
            }
            else
            {
                if (digitIndexes.Contains(digitSearchIndexes[5]))
                {
                    neighbouringNumbersCount++;
                    searchPoints.Add(digitSearchIndexes[5]);
                }
                if (digitIndexes.Contains(digitSearchIndexes[7]))
                {
                    neighbouringNumbersCount++;
                    searchPoints.Add(digitSearchIndexes[7]);
                }
            }

            if (neighbouringNumbersCount != 2)
                continue;

            var multiplicands = new int[2];
            for (int j = 0; j < multiplicands.Length; j++)
            {
                var lineIndex = searchPoints[j] / lineLength;
                var indexWithinLine = searchPoints[j] % lineLength;
                var asString = "" + input[lineIndex][searchPoints[j] % lineLength];
                var counter = 1;
                while (digitIndexes.Contains(searchPoints[j] - counter) && (searchPoints[j] - counter) / lineLength == lineIndex)
                {
                    asString = input[lineIndex][indexWithinLine - counter] + asString;
                    counter++;
                }
                counter = 1;
                while (digitIndexes.Contains(searchPoints[j] + counter) && (searchPoints[j] + counter) / lineLength == lineIndex)
                {
                    asString += input[lineIndex][indexWithinLine + counter];
                    counter++;
                }
                multiplicands[j] = Convert.ToInt32(asString);
            }
            gearRatioSum += multiplicands[0] * multiplicands[1];
        }

        Universal.WriteSolutionSection(2,
            "The sum of all gear ratios is:", 
            gearRatioSum);
    }
}
