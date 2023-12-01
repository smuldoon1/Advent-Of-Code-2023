// Part 1 - 9m 26s
// Part 2 - 25m 02s

var input = File.ReadAllLines(@"F:\Advent-Of-Code-2023\AdventOfCode\Day1\input.txt");

var total1 = 0;
foreach (var line in input)
{
    var left = line.First(x => int.TryParse(x.ToString(), out _));
    var right = line.Last(x => int.TryParse(x.ToString(), out _));

    total1 += int.Parse($"{left}{right}");
}
Console.WriteLine($"Part 1: {total1}");

var dict = new Dictionary<string, string>()
{
    { "one", "1" },
    { "two", "2" },
    { "three", "3" },
    { "four", "4" },
    { "five", "5" },
    { "six", "6" },
    { "seven", "7" },
    { "eight", "8" },
    { "nine", "9" },
    { "1", "1" },
    { "2", "2" },
    { "3", "3" },
    { "4", "4" },
    { "5", "5" },
    { "6", "6" },
    { "7", "7" },
    { "8", "8" },
    { "9", "9" },
};

var total2 = 0;
foreach (var line in input)
{
    var left = dict[dict.Keys
        .Where(x => line.Contains(x))
        .OrderBy(x => line.IndexOf(x))
        .First()
    ];

    var right = dict[dict.Keys
        .Where(x => line.Contains(x))
        .OrderBy(x => line.LastIndexOf(x))
        .Last()
    ];

    total2 += int.Parse($"{left}{right}");
}
Console.WriteLine($"Part 2: {total2}");