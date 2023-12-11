// Part 1 - 33m 06s

var input = File.ReadAllLines(@"F:\Advent-Of-Code-2023\AdventOfCode\Day10\input.txt");

// Get coords of S
int i = -1, j = -1;
while (j == -1) j = input[++i].IndexOf('S');

char facing;
var pipeLength = CountPipeLength();
Console.WriteLine($"Part 1: {pipeLength / 2}");

int CountPipeLength()
{
    int pipeLength = 1;

    if (input[i][j + 1] == '-' || input[i][j + 1] == '7' || input[i][j + 1] == 'J') GoEast();
    else if (input[i + 1][j] == '|' || input[i + 1][j] == 'L' || input[i + 1][j] == 'J') GoSouth();
    else if (input[i][j - 1] == '-' || input[i][j - 1] == 'L' || input[i][j - 1] == 'F') GoWest();
    else GoNorth();

    var current = input[i][j];
    while (current != 'S')
    {
        if (current == '|') (facing == 'n' ? (Action)GoNorth : GoSouth)();
        else if (current == '-') (facing == 'w' ? (Action)GoWest : GoEast)();
        else if (current == 'L') (facing == 's' ? (Action)GoEast : GoNorth)();
        else if (current == 'J') (facing == 's' ? (Action)GoWest : GoNorth)();
        else if (current == '7') (facing == 'e' ? (Action)GoSouth : GoWest)();
        else if (current == 'F') (facing == 'w' ? (Action)GoSouth : GoEast)();
        current = input[i][j];
        pipeLength++;
    }

    return pipeLength;
}

void GoNorth()
{
    i--;
    facing = 'n';
}

void GoEast()
{
    j++;
    facing = 'e';
}

void GoSouth()
{
    i++;
    facing = 's';
}

void GoWest()
{
    j--;
    facing = 'w';
}

void DepthFirstSearch(int i, int j)
{

}