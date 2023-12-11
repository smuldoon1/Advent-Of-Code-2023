// Part 1 - 35m 30s
// Part 2 - 58m 44s

var input = File.ReadAllLines(@"F:\Advent-Of-Code-2023\AdventOfCode\Day11\input.txt");

var galaxies = new List<(int x, int y)>();
for (int i = 0; i < input.Length; i++)
{
    input[i]
        .Select((x, index) => new { Char = x, Index = index })
        .Where(x => x.Char == '#')
        .Select(x => (x.Index, i))
        .ToList()
        .ForEach(x => galaxies.Add(x));
}
var emptyRows = Enumerable.Range(0, input.Length - 1).Except(galaxies.Select(g => g.y));
var emptyCols = Enumerable.Range(0, input.First().Length - 1).Except(galaxies.Select(g => g.x));

Console.WriteLine($"Part 1: {CalculateDistances(1)}");
Console.WriteLine($"Part 2: {CalculateDistances(999999)}");

long CalculateDistances(long expansionSize)
{
    long distances = 0;
    for (int i = 0; i < galaxies!.Count; i++)
    {
        distances += galaxies
            .Where((o, index) => index > i)
            .Sum(o =>
            {
                var x1 = galaxies[i].x + emptyCols.Count(c => c < galaxies[i].x) * expansionSize;
                var x2 = o.x + emptyCols.Count(c => c < o.x) * expansionSize;
                var y1 = galaxies[i].y + emptyRows.Count(r => r < galaxies[i].y) * expansionSize;
                var y2 = o.y + emptyRows.Count(r => r < o.y) * expansionSize;
                return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
            });
    }
    return distances;
}