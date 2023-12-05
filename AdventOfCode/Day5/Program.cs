// Part 1 - 27m 18s

using System.Text.RegularExpressions;

var input = File.ReadAllText(@"F:\Advent-Of-Code-2023\AdventOfCode\Day5\input.txt");
var lines = input.Split("\n\r\n");

var seeds = GetLongs(lines.First());

// Create maps
var maps = new List<(long start, long end, long value)[]>();
foreach (var map in lines.Skip(1))
{
    maps.Add(CreateMap(map));
}

// Part 1
var minLocation = long.MaxValue;
foreach (var seed in seeds)
{
    var value = GetLocationFromSeed(seed);
    if (value < minLocation) minLocation = value;
}
Console.WriteLine($"Part 1: {minLocation}");

// Part 2
minLocation = long.MaxValue;
Console.WriteLine($"Part 2: {minLocation}");

long GetLocationFromSeed(long value)
{
    foreach (var map in maps!)
    {
        foreach (var range in map)
        {
            if (value >= range.start && value <= range.end)
            {
                value += range.value;
                break;
            }
        }
    }
    return value;
}

(long, long, long)[] CreateMap(string text)
{
    var map = new List<(long, long, long)>();
    foreach (var line in text.Split('\n').Skip(1))
    {
        var numbers = GetLongs(line);
        map.Add((numbers[1], numbers[1] + numbers[2], numbers[0] - numbers[1]));
    }
    return map.ToArray();
}

long[] GetLongs(string str)
{
    return Regex.Matches(str, @"\d+").Select(x => long.Parse(x.Value)).ToArray();
}