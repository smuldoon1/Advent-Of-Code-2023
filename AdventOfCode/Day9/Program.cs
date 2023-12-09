// Part 1 - 15m 51s
// Part 2 - 19m 52s

var input = File.ReadAllLines(@"F:\Advent-Of-Code-2023\AdventOfCode\Day9\input.txt");

var total1 = 0;
foreach (var sequence in input.Select(x => x.Split(' ').Select(y => int.Parse(y)).ToList()))
{
    total1 += PredictNext(sequence).Last();
}
Console.WriteLine($"Part 1: {total1}");

var total2 = 0;
foreach (var sequence in input.Select(x => x.Split(' ').Select(y => int.Parse(y)).ToList()))
{
    total2 += PredictPrevious(sequence).First();
}
Console.WriteLine($"Part 1: {total2}");

static List<int> PredictNext(List<int> sequence)
{
    var newSequence = new List<int>();
    for (var i = 1; i < sequence.Count; i++)
    {
        newSequence.Add(sequence[i] - sequence[i - 1]);
    }
    if (newSequence.Any(x => x != 0))
    {
        newSequence = PredictNext(newSequence);
    }
    sequence.Add(newSequence.LastOrDefault() + sequence.Last());
    return sequence;
}

static List<int> PredictPrevious(List<int> sequence)
{
    var newSequence = new List<int>();
    for (var i = sequence.Count - 2; i >= 0; i--)
    {
        newSequence.Insert(0, sequence[i] - sequence[i + 1]);
    }
    if (newSequence.Any(x => x != 0))
    {
        newSequence = PredictPrevious(newSequence);
    }
    sequence.Insert(0, newSequence.FirstOrDefault() + sequence.First());
    return sequence;
}