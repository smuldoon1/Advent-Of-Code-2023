// Part 1 - 36m 56s
// Part 2 - 38m 24s

using AdventOfCode.Utilities;

var input = File.ReadAllLines(@"F:\Advent-Of-Code-2023\AdventOfCode\Day16\input.txt");

Console.WriteLine($"Part 1: {EnergiseTiles(new LightBeam(0, 0, CardinalDirection.East))}");

var max = 0;
for (int i = 0; i < input[0].Length; i++)
{
    max = Math.Max(max, EnergiseTiles(new LightBeam(i, 0, CardinalDirection.South)));
    max = Math.Max(max, EnergiseTiles(new LightBeam(i, input.Length - 1, CardinalDirection.North)));
}
for (int i = 0; i < input.Length; i++)
{
    max = Math.Max(max, EnergiseTiles(new LightBeam(0, i, CardinalDirection.East)));
    max = Math.Max(max, EnergiseTiles(new LightBeam(0, input[0].Length - 1, CardinalDirection.West)));
}
Console.WriteLine($"Part 2: {max}");

int EnergiseTiles(LightBeam startingBeam)
{
    var beams = new Queue<LightBeam>();
    beams.Enqueue(startingBeam);
    var visited = new HashSet<LightBeam>();
    var energisedTiles = new HashSet<(int x, int y)>();
    while (beams.Count > 0)
    {
        var beam = beams.Dequeue();

        if (beam.X < 0 || beam.Y < 0 || beam.X >= input[0].Length || beam.Y >= input.Length || visited.Contains(beam)) continue;

        visited.Add(beam);
        energisedTiles.Add((beam.X, beam.Y));

        if (input[beam.Y][beam.X] == '|')
        {
            if (beam.Direction == CardinalDirection.East || beam.Direction == CardinalDirection.West)
            {
                beams.Enqueue(new LightBeam(beam.X, beam.Y - 1, CardinalDirection.North));
                beams.Enqueue(new LightBeam(beam.X, beam.Y + 1, CardinalDirection.South));
                continue;
            }
        }
        else if (input[beam.Y][beam.X] == '-')
        {
            if (beam.Direction == CardinalDirection.North || beam.Direction == CardinalDirection.South)
            {
                beams.Enqueue(new LightBeam(beam.X - 1, beam.Y, CardinalDirection.West));
                beams.Enqueue(new LightBeam(beam.X + 1, beam.Y, CardinalDirection.East));
                continue;
            }
        }
        else if (input[beam.Y][beam.X] == '/')
        {
            if (beam.Direction == CardinalDirection.North)
            {
                beams.Enqueue(new LightBeam(beam.X + 1, beam.Y, CardinalDirection.East));
            }
            else if (beam.Direction == CardinalDirection.East)
            {
                beams.Enqueue(new LightBeam(beam.X, beam.Y - 1, CardinalDirection.North));
            }
            else if (beam.Direction == CardinalDirection.South)
            {
                beams.Enqueue(new LightBeam(beam.X - 1, beam.Y, CardinalDirection.West));
            }
            else
            {
                beams.Enqueue(new LightBeam(beam.X, beam.Y + 1, CardinalDirection.South));
            }
            continue;
        }
        else if (input[beam.Y][beam.X] == '\\')
        {
            if (beam.Direction == CardinalDirection.North)
            {
                beams.Enqueue(new LightBeam(beam.X - 1, beam.Y, CardinalDirection.West));
            }
            else if (beam.Direction == CardinalDirection.East)
            {
                beams.Enqueue(new LightBeam(beam.X, beam.Y + 1, CardinalDirection.South));
            }
            else if (beam.Direction == CardinalDirection.South)
            {
                beams.Enqueue(new LightBeam(beam.X + 1, beam.Y, CardinalDirection.East));
            }
            else
            {
                beams.Enqueue(new LightBeam(beam.X, beam.Y - 1, CardinalDirection.North));
            }
            continue;
        }

        if (beam.Direction == CardinalDirection.North)
        {
            beams.Enqueue(new LightBeam(beam.X, beam.Y - 1, CardinalDirection.North));
        }
        else if (beam.Direction == CardinalDirection.East)
        {
            beams.Enqueue(new LightBeam(beam.X + 1, beam.Y, CardinalDirection.East));
        }
        else if (beam.Direction == CardinalDirection.South)
        {
            beams.Enqueue(new LightBeam(beam.X, beam.Y + 1, CardinalDirection.South));
        }
        else
        {
            beams.Enqueue(new LightBeam(beam.X - 1, beam.Y, CardinalDirection.West));
        }
    }
    return energisedTiles.Count;
}

public struct LightBeam
{
    public int X { get; set; }

    public int Y { get; set; }

    public CardinalDirection Direction { get; set; }

    public LightBeam(int x, int y, CardinalDirection direction)
    {
        X = x;
        Y = y;
        Direction = direction;
    }
}