// Part 1 - 3m 13s
// Part 2 - 25m 42s

using System.Collections.Specialized;

var input = File.ReadAllText(@"F:\Advent-Of-Code-2023\AdventOfCode\Day15\input.txt");

var sequence = input.Split(',');

Console.WriteLine($"Part 1: {sequence.Sum(x => Hash(x))}");

var boxes = new OrderedDictionary[256];
for (int i = 0; i < 256; i++) boxes[i] = new OrderedDictionary();
foreach (var step in sequence)
{
    var split = step.Split(new char[] { '-', '=' }, StringSplitOptions.RemoveEmptyEntries);
    var label = split[0];
    var box = Hash(label);
    if (split.Length == 1)
    {
        boxes[box].Remove(label);
    }
    else
    {
        boxes[box][label] = int.Parse(split[1]);
    }
}

var total = 0;
for (int i = 0; i < boxes.Length; i++)
{
    for (int j = 0; j < boxes[i].Count; j++)
    {
        total += (i + 1) * (j + 1) * (int)boxes[i][j]!;
    }
}
Console.WriteLine($"Part 2: {total}");

static int Hash(string str) => str.Aggregate(0, (i, c) => (i + c) * 17 % 256);