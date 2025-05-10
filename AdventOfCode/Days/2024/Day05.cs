using System.Data;

namespace AdventOfCode.Y2024;

public class Day05 : Day
{
    public Day05()
    {
        Year = 2024;
        DayNumber = 5;
    }

    public int ProcessAsPart1(IEnumerable<string> input)
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

        int sum = 0;
        foreach (var update in updatesInput)
        {
            var intUpdate = update.Split(',').Select(v => Convert.ToInt32(v)).ToArray();
            if (intUpdate.DoesFollowRules(rulebook))
            {
                Console.Write("valid: ");
                sum += intUpdate[intUpdate.Length / 2];
            }
            else 
            {
                Console.Write("NOT valid: ");
            }
            Console.Write(string.Join(", ", intUpdate));
            //Console.Write(" " + (intUpdate.Length / 2).ToString() + " " + intUpdate[intUpdate.Length / 2].ToString());
            Console.WriteLine();
        }
        return sum;
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

        ////Part 1
        //int sum1 = ProcessAsPart1(input);
        //Universal.WriteSolutionSection(1, "Summary of center pages in correct updates is: ", sum1);

        ////Part 2
        //Stopwatch sw = Stopwatch.StartNew();
        //int sum2 = ProcessAsPart2(input);
        //sw.Stop();
        //Console.WriteLine("THIS MANY ticks ELAPSED: " + sw.ElapsedTicks.ToString());
        //Universal.WriteSolutionSection(2, "\"XMAS\" formation was found a total of this many times:", sum2);

        //BothPartsTogether
        ProcessBothParts(input, out int part1Res, out int part2Res);
        Universal.WriteSolutionSection(1, "Part1 result is: ", part1Res);
        Universal.WriteSolutionSection(2, "Part2 result is: ", part2Res);
    }
}

public class Rule 
{
    public int Number;
    public List<Rule> M_means_before_number = new List<Rule>();
    public List<Rule> O_means_after_number = new List<Rule>();

    public override string ToString()
    {
        var oString = string.Join(", ", O_means_after_number.Select(a => a.Number.ToString()).ToArray());
        //var chaosString = ChaosString(true);
        var mString = string.Join(", ", M_means_before_number.Select(a => a.Number.ToString()).ToArray());
        var problems = string.Join(", ", M_means_before_number.IntersectBy(O_means_after_number.Select(x => x.Number), x => x.Number));
        //return $"these come first:{mString}\nthen this, that is {Number}\nthen come these: {oString}\nsummary: {chaosString}\nproblems: {problems}\n";
        return $"these come first:{mString}\nthen this, that is {Number}\nthen come these: {oString}\nproblems: {problems}\n";
    }

    public string ChaosString(bool isRoot = false)
    {
        var valueAfter = O_means_after_number.FirstOrDefault();
        var afterString = (isRoot ? $"{Number} is after " : valueAfter != null ? " which is after " : "") + (valueAfter != null ? ($"{valueAfter.Number}" + valueAfter.ChaosString()) : "");
        return afterString;
    }
    public List<int> LayeredWhatComesAfter(List<int> whatComesAfter, int depth = 0)
    {
        depth++;
        if (depth < 3)
        {
            foreach (var oElement in O_means_after_number)
            {
                oElement.LayeredWhatComesAfter(whatComesAfter, depth);
            }
        }
        if (!whatComesAfter.Contains(Number))
        {
            whatComesAfter.Add(Number);
        }
        return whatComesAfter.Distinct().ToList();
    }
}

public static class Day05Helpers
{
    private static char delim = '|';
    public static IEnumerable<string> GetRulesPartOfInput(this string[] input, out int emptyLineIndex)
    { 
        emptyLineIndex = 0;
        for (int i = 0; i < input.Count(); i++)
        {
            if (string.IsNullOrEmpty(input[i]))
            {
                emptyLineIndex = i;
                break;
            }

        }
        return input.Take(emptyLineIndex);
    }

    public static Dictionary<int, Rule> WriteRules(IEnumerable<string> ruleLines)
    {
        var rulebook = new Dictionary<int, Rule>();
        foreach (string line in ruleLines)
        {
            rulebook.AddRule(line);
        }
        return rulebook;
    }

    public static void AddRule(this Dictionary<int, Rule> rulebook, string ruleInput)
    {
        var parts = ruleInput.Split('|');
        var a = Int32.Parse(parts[0]);
        var b = Int32.Parse(parts[1]);

        if (rulebook.ContainsKey(a))
        {
            if (rulebook.ContainsKey(b))
            {
                rulebook[a].O_means_after_number.Add(rulebook[b]);
                rulebook[b].M_means_before_number.Add(rulebook[a]);
            }
            else
            {
                var ruleB = new Rule { Number = b };
                ruleB.M_means_before_number.Add(rulebook[a]);

                rulebook[a].O_means_after_number.Add(ruleB);
            }
        }
        else if (rulebook.ContainsKey(b))
        {
            var ruleA = new Rule { Number = a };
            ruleA.O_means_after_number.Add(rulebook[b]);

            rulebook[b].M_means_before_number.Add(ruleA);
        }
        else
        {
            var ruleA = new Rule { Number = a };
            var ruleB = new Rule { Number = b };
            rulebook.Add(a, ruleA);
            rulebook.Add(b, ruleB);

            rulebook[a].O_means_after_number.Add(rulebook[b]);
            rulebook[b].M_means_before_number.Add(rulebook[a]);
        }
    }

    public static void PrintRulebook(this Dictionary<int, Rule> rulebook)
    {
        foreach (var rule in rulebook)
        {
            Console.WriteLine(rule.ToString());
        }
    }
    public static bool DoesFollowRules(this int[] update, Dictionary<int, Rule> rulebook)
    {
        for (int i = 0; i < update.Length; i++)
        {
            var page = update[i];
            if (rulebook.TryGetValue(page, out var rule))
            {
                for (int j = i + 1; j < update.Length; j++)
                {
                    if (rule.M_means_before_number.Any(r => r.Number == update[j]))
                    {
                        return false;
                    }
                }

                for (int b = i - 1; b >= 0; b--)
                {
                    if (rule.O_means_after_number.Any(r => r.Number == update[b]))
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public static void ReorderToFollowRules(this int[] update, Dictionary<int, Rule> rulebook)
    {
        var rulebookSubset = rulebook.Where(kvp => update.Contains(kvp.Key)).ToDictionary();
        while (!DoesFollowRules(update, rulebookSubset))
        {
            for (int i = 0; i < update.Length; i++)
            {
                var page = update[i];
                if (rulebookSubset.TryGetValue(page, out var rule))
                {
                    for (int j = i + 1; j < update.Length; j++)
                    {
                        var willBreak = rule.M_means_before_number.Any(r => r.Number == update[j]);
                        while (rule.M_means_before_number.Any(r => r.Number == update[j]))
                        {
                            var temp = update[i];
                            update[i] = update[j];
                            update[j] = temp;
                        }
                        if (willBreak)
                            break;
                    }

                    for (int b = i - 1; b >= 0; b--)
                    {
                        var willBreak = rule.O_means_after_number.Any(r => r.Number == update[b]);
                        while (rule.O_means_after_number.Any(r => r.Number == update[b]))
                        {
                            var temp = update[i];
                            update[i] = update[b];
                            update[b] = temp;
                        }
                        if (willBreak)
                            break;
                    }
                }
            }
        }
    }
}
