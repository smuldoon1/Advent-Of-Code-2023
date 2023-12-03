// Part 1 - 27m 22s

var input = File.ReadAllLines(@"F:\Advent-Of-Code-2023\AdventOfCode\Day3\input.txt");

input = AddBorder();

var total1 = 0;
for (int i = 0; i < input.Length; i++)
{
    for (int j = 0; j < input[i].Length; j++)
    {
        if (int.TryParse(input[i][j].ToString(), out _))
        {
            var number = new string(input[i][j..].TakeWhile(x => int.TryParse(x.ToString(), out _)).ToArray());

            total1 += AddPartNumber(number, i, j);

            j += number.Length - 1;
        }
    }
}
Console.WriteLine($"Part 1: {total1}");

int AddPartNumber(string str, int row, int column)
{
    var number = int.Parse(str);

    foreach (var c in input[row - 1].Substring(column - 1, str.Length + 2))
    {
        if (IsSymbol(c))
        {
            return number;
        }
    }

    foreach (var c in input[row + 1].Substring(column - 1, str.Length + 2))
    {
        if (IsSymbol(c))
        {
            return number;
        }
    }

    if (IsSymbol(input[row][column - 1]))
    {
        return number;
    }

    if (IsSymbol(input[row][column + str.Length]))
    {
        return number;
    }

    return 0;
}

bool IsSymbol(char c)
{
    return c != '.' && !char.IsLetterOrDigit(c);
}

string[] AddBorder()
{
    var newInput = new List<string>();
    foreach (var line in input)
    {
        newInput.Add($".{line}.");
    }
    var topBottom = new string(Enumerable.Repeat('.', input[0].Length + 2).ToArray());
    newInput.Insert(0, topBottom);
    newInput.Add(topBottom);

    return newInput.ToArray();
}