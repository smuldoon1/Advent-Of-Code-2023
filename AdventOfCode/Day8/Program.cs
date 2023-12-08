// Part 1 - 10m 42s
// Part 2 - 28m 42s

using System.Text.RegularExpressions;

var input = File.ReadAllLines(@"F:\Advent-Of-Code-2023\AdventOfCode\Day8\input.txt");

// Parse instructions and node dictionary
var instructions = input[0].Select(x => x == 'L' ? 0 : 1).ToArray();
var nodes = new Dictionary<string, string[]>();
for (int i = 2; i < input.Length; i++)
{
    var matches = Regex.Matches(input[i], @"([A-Z0-9]+)");
    nodes.Add(matches[0].Value, new string[] { matches[1].Value, matches[2].Value });
}

// Part 1
var current = "AAA";
var total1 = 0;
do
{
    current = nodes[current][instructions[total1++ % instructions.Length]];
}
while (current != "ZZZ");
Console.WriteLine($"Part 1: {total1}");

// Part 2
var startNodes = nodes.Keys.Where(x => x[2] == 'A').ToArray();
var pathLengths = new int[startNodes.Length];
for (int s = 0; s < startNodes.Length; s++)
{
    var c = 0;
    var node = startNodes[s];
    do
    {
        node = nodes[node][instructions[c++ % instructions.Length]];
    }
    while (node[2] != 'Z');
    pathLengths[s] = c;
}

long lcm = pathLengths[0];
for (int n = 1; n < pathLengths.Length; n++)
{
    lcm = CalculateLCM(lcm, pathLengths[n]);
}
Console.WriteLine($"Part 2: {lcm}");

static long CalculateLCM(long a, long b)
{
    return a * b / CalculateGCD(a, b);
}

static long CalculateGCD(long a, long b)
{
    while (b != 0)
    {
        long temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}