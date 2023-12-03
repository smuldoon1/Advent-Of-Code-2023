// Part 1 - 24m 10s
// Part 2 - 27m 30s

using System.Text.RegularExpressions;

var input = File.ReadAllLines(@"F:\Advent-Of-Code-2023\AdventOfCode\Day2\input.txt");

var maxRed = 12;
var maxGreen = 13;
var maxBlue = 14;

var total1 = 0;
for (int i = 0; i < input.Length; i++)
{
    if (ValidateGame(input[i])) total1 += i + 1;
}
Console.WriteLine($"Part 1: {total1}");

int total2 = 0;
for (int i = 0; i < input.Length; i++)
{
    total2 += CountCubes(input[i]);
}
Console.WriteLine($"Part 2: {total2}");

bool ValidateGame(string games)
{
    foreach (var game in Regex.Match(games, @"(?<=:).*$").Value.Split(';'))
    {
        foreach (var cubes in game.Split(','))
        {
            var round = cubes.Trim().Split(' ');
            var number = int.Parse(round[0]);

            switch (round[1])
            {
                case "red":
                    if (number > maxRed) return false;
                    break;
                case "green":
                    if (number > maxGreen) return false;
                    break;
                case "blue":
                    if (number > maxBlue) return false;
                    break;
            }
        }
    }
    return true;
}

int CountCubes(string games)
{
    int maxRed = 0;
    int maxGreen = 0;
    int maxBlue = 0;
    foreach (var game in Regex.Match(games, @"(?<=:).*$").Value.Split(';'))
    {
        foreach (var cubes in game.Split(','))
        {
            var round = cubes.Trim().Split(' ');
            var number = int.Parse(round[0]);

            switch (round[1])
            {
                case "red":
                    if (number > maxRed) maxRed = number;
                    break;
                case "green":
                    if (number > maxGreen) maxGreen = number;
                    break;
                case "blue":
                    if (number > maxBlue) maxBlue = number;
                    break;
            }
        }
    }
    return maxRed * maxBlue * maxGreen;
}