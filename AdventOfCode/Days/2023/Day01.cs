namespace AdventOfCode.Y2023;

public class Day01 : Day
{
    public Day01()
    {
        Year = 2023;
        DayNumber = 1;
    }
    
    public int ProcessAsPart1(IEnumerable<string> input, char[] digits)
    {
        int sum = 0;
        foreach (var line in input)
        {
            string numbersInLine = line.PreserveOnly(digits);
            if (numbersInLine.Length > 0)
            {
                var addend = Convert.ToInt32($"{numbersInLine[0]}{numbersInLine[numbersInLine.Length - 1]}");
                sum += addend;
            }
        }
        return sum;
    }

    public override void SolveBothParts(bool useExample = false, bool useBrian = false, bool printIntros = false)
    {
        if (printIntros)
        {
            PartDescription(1);
        }

        var input = LoadInput(useExample, useBrian);

        var digits = "0123456789".ToCharArray();
        var digitDictionary = new Dictionary<string, string>() {
            { "zero", "0" },
            { "one", "1" },
            { "two", "2" },
            { "three", "3" },
            { "four", "4" },
            { "five", "5" },
            { "six", "6" },
            { "seven", "7" },
            { "eight", "8" },
            { "nine", "9" },
        };
        var keys = digitDictionary.Keys.ToList();

        //Part 1
        int sum1 = ProcessAsPart1(input, digits);
        Universal.WriteSolutionSection(1, "The sum of all calibration values using part 1 rules is:", sum1);

        //Part 2
        var alteredLines = new List<string>();
        for (int lineIndex = 0; lineIndex < input.Length; lineIndex++)
        {
            var line = input[lineIndex];
            for (int i = line.Length - 1; i >= 0; i--)
            {
                int substringLength = 1;
                string examinedSubstring = line.Substring(i, substringLength);
                while (keys.HasElementEndingWith(examinedSubstring))
                {
                    if (keys.Contains(examinedSubstring))
                    {
                        var newLine = line.Substring(0, i - 1) + digitDictionary[examinedSubstring] + line.Substring(i + 1);
                        line = newLine;
                        break;
                    }
                    if ((i - substringLength) >= 0)
                    {
                        examinedSubstring = line.Substring(i - substringLength, ++substringLength);
                    }
                    else break;
                }
            }
            alteredLines.Add(line);
        }
        int sum2 = ProcessAsPart1(alteredLines, digits);
        Universal.WriteSolutionSection(2, "The sum of all calibration values using part 2 rules is:", sum2);
    }
}

public static class Day01Helpers
{
    public static string PreserveOnly(this string input, Char[] charsToPreserve)
    {
        string reducedInput = "";
        for (int i = 0; i < input.Length; i++)
        {
            if (charsToPreserve.Contains(input[i]))
            {
                reducedInput += input[i];
            }
        }
        return reducedInput;
    }

    public static bool HasElementStartingWith(this IEnumerable<string> list, string begining)
    {
        foreach (string element in list)
        {
            if (element.StartsWith(begining))
            {
                return true;
            }
        }
        return false;
    }

    public static bool HasElementEndingWith(this IEnumerable<string> list, string ending)
    {
        foreach (string element in list)
        {
            if (element.EndsWith(ending))
            {
                return true;
            }
        }
        return false;
    }
}
