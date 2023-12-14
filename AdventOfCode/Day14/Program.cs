// Part 1 - 17m 07s

var input = File.ReadAllLines(@"F:\Advent-Of-Code-2023\AdventOfCode\Day14\input.txt");

var total1 = 0;
for (int i = -1; i < input.Length; i++)
{
    for (int j = 0; j < input.First().Length; j++)
    {
        if (i == -1 || input[i][j] == '#')
        {
            var rockCount = 0;
            for (int k = i + 1; k < input.Length && input[k][j] != '#'; k++)
            {
                if (input[k][j] == 'O') rockCount++;
            }
            for (int c = 0; c < rockCount; c++)
            {
                total1 += input.Length - i - c - 1;
            }
        }
    }
}
Console.WriteLine($"Part 1: {total1}");