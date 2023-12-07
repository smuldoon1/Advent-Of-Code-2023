// Part 1 - 18m 57s
// Part 2 - 27m 59s

var input = File.ReadAllLines(@"F:\Advent-Of-Code-2023\AdventOfCode\Day7\input.txt");

var part1Comparer = new CardComparer("AKQJT98765432");
var winnings1 = input
    .OrderByDescending(x =>
    {
        var cards = x[..5];

        var occurences = x[..5].GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());

        if (occurences.Count == 1) return 0; // If only one card exists, must be 5 of a kind
        else if (occurences.Any(y => y.Value == 4)) return 1; // If a car occurs 4 times, must be 4 of a kind
        else if (occurences.Any(y => y.Value == 3) && occurences.Count == 2) return 2; // If a card occurs 3 times and only 2 cards exist, must be a full house
        else if (occurences.Any(y => y.Value == 3)) return 3; // Otherwise, if a card occurs 3 times, must be 3 of a kind
        else if (occurences.Count == 3) return 4; // Otherwise, if 3 cards exist, must be two pairs
        else if (occurences.Count == 4) return 5; // Otherwise, if 4 cards exist, must be a single pair
        return 6; // Else, its a high card
    })
    .ThenByDescending(x => x, part1Comparer)
    .Select((x, index) => int.Parse(x.Split(' ')[1]) * (index + 1))
    .Sum();

Console.WriteLine($"Part 1: {winnings1}");

var part2Comparer = new CardComparer("AKQT98765432J");
var winnings2 = input
    .OrderByDescending(x =>
    {
        var cards = x[..5];

        var occurences = x[..5].GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());

        if (occurences.ContainsKey('J') && occurences.Count > 1)
        {
            occurences[occurences.Where(c => c.Key != 'J').OrderByDescending(x => x.Value).First().Key] += occurences['J'];
            occurences.Remove('J');
        }

        if (occurences.Count == 1) return 0; // If only one card exists, must be 5 of a kind
        else if (occurences.Any(y => y.Value == 4)) return 1; // If a car occurs 4 times, must be 4 of a kind
        else if (occurences.Any(y => y.Value == 3) && occurences.Count == 2) return 2; // If a card occurs 3 times and only 2 cards exist, must be a full house
        else if (occurences.Any(y => y.Value == 3)) return 3; // Otherwise, if a card occurs 3 times, must be 3 of a kind
        else if (occurences.Count == 3) return 4; // Otherwise, if 3 cards exist, must be two pairs
        else if (occurences.Count == 4) return 5; // Otherwise, if 4 cards exist, must be a single pair
        return 6; // Else, its a high card
    })
    .ThenByDescending(x => x, part2Comparer)
    .Select((x, index) => int.Parse(x.Split(' ')[1]) * (index + 1))
    .Sum();

Console.WriteLine($"Part 1: {winnings2}");

class CardComparer : IComparer<string>
{
    private readonly string order;

    public CardComparer(string order)
    {
        this.order = order;
    }

    public int Compare(string? x, string? y)
    {
        for (int i = 0; i < 5; i++)
        {
            int compareResult = order.IndexOf(x![i]).CompareTo(order.IndexOf(y![i]));

            if (compareResult != 0)
                return compareResult;
        }

        return x!.Length.CompareTo(y!.Length);
    }
}