// Part 1 - 34m 05s

var input = File.ReadAllLines(@"F:\Advent-Of-Code-2023\AdventOfCode\Day12\input.txt");

var total1 = 0;
foreach (var line in input)
{
    var split = line.Split(' ');
    var brokenGroups = split[1]
        .Split(',')
        .Select(x => int.Parse(x))
        .ToArray();
    var combinations = new List<string>();
    GenerateCombinations(split[0].ToCharArray(), combinations);
    total1 += combinations
        .Count(x => IsValidArrangement(x, brokenGroups));
}
Console.WriteLine($"Part 1: {total1}");

static bool IsValidArrangement(string arrangement, int[] brokenGroups)
{
    var split = arrangement.Split('.', StringSplitOptions.RemoveEmptyEntries);
    return split.Length == brokenGroups.Length &&
        !split.Zip(brokenGroups)
        .Any(x => x.First.Length != x.Second);
}

static void GenerateCombinations(char[] arrangement, List<string> combinations, int index = 0)
{
    if (index == arrangement.Length)
    {
        combinations.Add(new string(arrangement));
        return;
    }

    if (arrangement[index] == '?')
    {
        arrangement[index] = '.';
        GenerateCombinations(arrangement, combinations, index + 1);
        arrangement[index] = '#';
        GenerateCombinations(arrangement, combinations, index + 1);
        arrangement[index] = '?';
    }
    else
    {
        GenerateCombinations(arrangement, combinations, index + 1);
    }
}