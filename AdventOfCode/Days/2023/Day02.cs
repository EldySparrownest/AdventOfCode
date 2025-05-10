namespace AdventOfCode.Y2023;

public class Day02 : Day
{
    public Day02()
    {   
        Year = 2023;
        DayNumber = 2;
    }
    public override void SolveBothParts(bool useExample = false, bool useBrian = false, bool printIntros = false)
    {
        if (printIntros)
        {
            PartDescription(1);
        }


        var input = LoadInput(useExample, useBrian);

        //Part 1
        var limit = new Game(12, 13, 14);
        var colorDictionary = new Dictionary<string, int>() {
            { "red", 0 },
            { "green", 0 },
            { "blue", 0 },
        };

        var idSum = 0;
        for (int i = 0; i < input.Length; i++)
        {
            foreach (var colorKey in colorDictionary.Keys) { colorDictionary[colorKey] = 0; }
            var reveals = input[i].Split(";,:".ToCharArray());
            for (int j = 1; j < reveals.Length; j++)
            {
                var reveal = reveals[j];
                var revealedColor = reveal.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var cubes = Convert.ToInt32(revealedColor[0]);
                if (cubes > colorDictionary[revealedColor[1]])
                {
                    colorDictionary[revealedColor[1]] = cubes;
                }
            }

            if (limit.IsRevealPossible(colorDictionary["red"], colorDictionary["green"], colorDictionary["blue"]))
            {
                idSum += i + 1;
            }
        }

        Universal.WriteSolutionSection(1, "The sum of ids of all possible games for is:", idSum);

        //Part 2

        if (printIntros)
        {
            PartDescription(2);
        }

        var powerSum = 0;
        for (int i = 0; i < input.Length; i++)
        {
            foreach (var colorKey in colorDictionary.Keys) { colorDictionary[colorKey] = 0; }
            var reveals = input[i].Split(";,:".ToCharArray());
            for (int j = 1; j < reveals.Length; j++)
            {
                var reveal = reveals[j];
                var revealedColor = reveal.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var cubes = Convert.ToInt32(revealedColor[0]);
                if (cubes > colorDictionary[revealedColor[1]])
                {
                    colorDictionary[revealedColor[1]] = cubes;
                }
            }

            powerSum += colorDictionary["red"] * colorDictionary["green"] * colorDictionary["blue"];
        }

        Universal.WriteSolutionSection(2, "The sum of the powers of all sets is:", powerSum);
    }

    public class Game
    {
        public int Red { get; set; }    
        public int Green { get; set; }    
        public int Blue { get; set; }    
        public Game(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public bool IsRevealPossible(int red, int green, int blue)
        {
            return Red >= red && Green >= green && Blue >= blue;
        }
    }
}
