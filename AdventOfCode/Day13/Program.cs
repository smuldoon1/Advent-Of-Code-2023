// Part 1 - 27m 26s

var input = File.ReadAllText(@"F:\Advent-Of-Code-2023\AdventOfCode\Day13\input.txt");

var patterns = input.Split(Environment.NewLine + Environment.NewLine);

var verticalMirrors = 0;
var horizontalMirrors = 0;
foreach (var pattern in patterns.Select(x => x.Split(Environment.NewLine)))
{
    var h = HorizontalScan(pattern);
    if (h != -1) verticalMirrors += h;
    else horizontalMirrors += VerticalScan(pattern);
}
Console.WriteLine($"Part 1: {verticalMirrors + horizontalMirrors * 100}");

verticalMirrors = 0;
horizontalMirrors = 0;
foreach (var pattern in patterns.Select(x => x.Split(Environment.NewLine)))
{
    var h = SmudgedHorizontalScan(pattern);
    if (h != -1) verticalMirrors += h;
    else horizontalMirrors += SmudgedVerticalScan(pattern);
}
Console.WriteLine($"Part 2: {verticalMirrors + horizontalMirrors * 100}");

static int VerticalScan(string[] pattern)
{
    for (int i = 1; i < pattern.Length; i++)
    {
        var depth = 0;
        while (true)
        {
            try
            {
                if (!AreRowsMirrored(pattern, i - depth - 1, i + depth)) break;
            }
            catch (IndexOutOfRangeException)
            {
                return i;
            }
            depth++;
        }
    }
    return -1;
}

static int HorizontalScan(string[] pattern)
{
    for (int i = 1; i < pattern[0].Length; i++)
    {
        var depth = 0;
        while (true)
        {
            try
            {
                if (!AreColumnsMirrored(pattern, i - depth - 1, i + depth)) break;
            }
            catch (IndexOutOfRangeException)
            {
                return i;
            }
            depth++;
        }
    }
    return -1;
}

static int SmudgedVerticalScan(string[] pattern)
{
    for (int i = 1; i < pattern.Length; i++)
    {
        var depth = 0;
        var totalErrors = new List<(int, int)>();
        var errors = new List<(int, int)>();
        while (true)
        {
            try
            {
                if (!AreSmudgedRowsMirrored(pattern, i - depth - 1, i + depth, out errors) || errors.Count > 1) break;
            }
            catch (IndexOutOfRangeException)
            {
                if (totalErrors.Count == 1)
                    return i;
                else break;
            }
            totalErrors = errors;
            depth++;
        }
    }
    return -1;
}

static int SmudgedHorizontalScan(string[] pattern)
{
    for (int i = 1; i < pattern[0].Length; i++)
    {
        var depth = 0;
        var totalErrors = new List<(int, int)>();
        var errors = new List<(int, int)>();
        while (true)
        {
            try
            {
                if (!AreSmudgedColumnsMirrored(pattern, i - depth - 1, i + depth, out errors) || errors.Count > 1) break;
            }
            catch (IndexOutOfRangeException)
            {
                if (totalErrors.Count == 1)
                    return i;
                else break;
            }
            totalErrors = errors;
            depth++;
        }
    }
    return -1;
}

static bool AreRowsMirrored(string[] pattern, int topIndex, int bottomIndex)
{
    for (int j = 0; j < pattern[0].Length; j++) // Assume top and bottom have same length
    {
        if (pattern[topIndex][j] != pattern[bottomIndex][j]) return false;
    }
    return true;
}

static bool AreColumnsMirrored(string[] pattern, int leftIndex, int rightIndex)
{
    for (int j = 0; j < pattern.Length; j++) // Assume top and bottom have same length
    {
        if (pattern[j][leftIndex] != pattern[j][rightIndex]) return false;
    }
    return true;
}

static bool AreSmudgedRowsMirrored(string[] pattern, int topIndex, int bottomIndex, out List<(int, int)> errors)
{
    errors = new List<(int, int)>();
    for (int j = 0; j < pattern[0].Length; j++) // Assume top and bottom have same length
    {
        if (pattern[topIndex][j] != pattern[bottomIndex][j])
            errors.Add((topIndex, j));
    }
    return errors.Count <= 1;
}

static bool AreSmudgedColumnsMirrored(string[] pattern, int leftIndex, int rightIndex, out List<(int, int)> errors)
{
    errors = new List<(int, int)>();
    for (int j = 0; j < pattern.Length; j++) // Assume top and bottom have same length
    {
        if (pattern[j][leftIndex] != pattern[j][rightIndex])
            errors.Add((j, leftIndex));
    }
    return errors.Count <= 1;
}