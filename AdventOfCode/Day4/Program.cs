// Part 1 - 9m 13s
// Part 2 - 21m 21s

var input = File.ReadAllLines(@"F:\Advent-Of-Code-2023\AdventOfCode\Day4\input.txt");

List<(int, int)> cardList = new();
for (int i = 0; i < input.Length; i++)
{
    var sets = input[i].Split(':')[1].Split('|').Select(x => x.Trim()).ToArray();

    var winningNumbers = sets[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x));
    var numbers = sets[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x));

    cardList.Add((i, numbers.Intersect(winningNumbers).Count()));
}

var total1 = 0;
foreach (var (index, wins) in cardList)
{
    total1 += (int)Math.Pow(2, wins - 1);
}
Console.WriteLine($"Part 1: {total1}");

var total2 = 0;
var queue = new Queue<(int, int)>(cardList);
while (queue.Count > 0)
{
    var (index, wins) = queue.Dequeue();

    cardList.GetRange(index + 1, wins).ForEach(x => queue.Enqueue(x));

    total2++;
}
Console.WriteLine($"Part 2: {total2}");