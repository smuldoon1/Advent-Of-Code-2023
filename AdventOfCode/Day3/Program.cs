// Part 1 - 27m 22s
// Part 2 - 29m 45s

var input = File.ReadAllLines(@"F:\Advent-Of-Code-2023\AdventOfCode\Day3\input.txt");

var numbers = new List<NumberSpan>();
for (int i = 0; i < input.Length; i++)
{
    for (int j = 0; j < input.Length; j++)
    {
        if (char.IsDigit(input[i][j]))
        {
            var str = new string(input[i][j..].TakeWhile(x => int.TryParse(x.ToString(), out _)).ToArray());
            numbers.Add(new NumberSpan
            {
                Row = i,
                Column = j,
                Value = int.Parse(str),
                Length = str.Length
            });
            j += str.Length;
        }
    }
}

var partNumbers = new List<NumberSpan>();
for (int i = 0; i < input.Length; i++)
{
    for (int j = 0; j < input[i].Length; j++)
    {
        var c = input[i][j];
        if (IsSymbol(c))
        {
            partNumbers.AddRange(numbers.Where(x =>
                x.Row <= i + 1 &&
                x.Row >= i - 1 &&
                x.Column <= j + 1 &&
                x.Column + x.Length >= j
            ));
        }
    }
}
Console.WriteLine($"Part 1: {partNumbers.Distinct().Sum(x => x.Value)}");

var total = 0;
for (int i = 0; i < input.Length; i++)
{
    for (int j = 0; j < input[i].Length; j++)
    {
        var c = input[i][j];
        if (c == '*')
        {
            var gearNumbers = numbers.Where(x =>
                x.Row <= i + 1 &&
                x.Row >= i - 1 &&
                x.Column <= j + 1 &&
                x.Column + x.Length >= j
            ).ToArray();

            if (gearNumbers.Count() == 2)
            {
                total += gearNumbers[0].Value * gearNumbers[1].Value;
            }
        }
    }
}
Console.WriteLine($"Part 2: {total}");

static bool IsSymbol(char c)
{
    return c != '.' && !char.IsLetterOrDigit(c);
}

struct NumberSpan
{
    public int Value { get; set; }

    public int Row { get; set; }

    public int Column { get; set; }

    public int Length { get; set; }
}