// Part 1 - 6m 55s
// Part 2 - 17m 46s

using System.Text.RegularExpressions;

var input = File.ReadAllLines(@"F:\Advent-Of-Code-2023\AdventOfCode\Day6\input.txt");

var races = GetInts(input[0]).Zip(GetInts(input[1]));

var total1 = 1;
foreach (var (t, d) in races)
{
    var wins = 0;
    for (int i = 0; i < t; i++)
    {
        if (i * (t - i) > d) wins++;
    }
    total1 *= wins;
}
Console.WriteLine($"Part 1: {total1}");

var time = long.Parse(input[0].Where(char.IsDigit).ToArray());
var distance = long.Parse(input[1].Where(char.IsDigit).ToArray());

for (int i = 0; i < time; i++)
{
    if (i * (time - i) > distance)
    {
        Console.WriteLine($"Part 2: {time - (i * 2) + 1}");
        break;
    }
}

static int[] GetInts(string str)
{
    return Regex.Matches(str, @"\d+").Select(x => int.Parse(x.Value)).ToArray();
}