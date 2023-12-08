// Part 1 - 10m 42s
// Part 2 - 28m 42s

using System.Text.RegularExpressions;

var input = File.ReadAllLines(@"F:\Advent-Of-Code-2023\AdventOfCode\Day8\input.txt");

// Parse instructions and node dictionary
var instructions = input[0];
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
    if (instructions[total1 % instructions.Length] == 'L') current = nodes[current][0];
    else if (instructions[total1 % instructions.Length] == 'R') current = nodes[current][1];
    total1++;
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
        if (instructions[c % instructions.Length] == 'L') node = nodes[node][0];
        else if (instructions[c % instructions.Length] == 'R') node = nodes[node][1];
        c++;
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