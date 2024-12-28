/*  Problem Description:

--- Day 20: Race Condition ---
The Historians are quite pixelated again. This time, a massive, black building looms over you - you're right outside the CPU!

While The Historians get to work, a nearby program sees that you're idle and challenges you to a race. 
Apparently, you've arrived just in time for the frequently-held race condition festival!

The race takes place on a particularly long and twisting code path; programs compete to see who can finish in the fewest picoseconds. 
The winner even gets their very own mutex!

They hand you a map of the racetrack (your puzzle input). For example:

###############
#...#...#.....#
#.#.#.#.#.###.#
#S#...#.#.#...#
#######.#.#.###
#######.#.#...#
#######.#.###.#
###..E#...#...#
###.#######.###
#...###...#...#
#.#####.#.###.#
#.#...#.#.#...#
#.#.#.#.#.#.###
#...#...#...###
###############
The map consists of track (.) - including the start (S) and end (E) positions (both of which also count as track) - and walls (#).

When a program runs through the racetrack, it starts at the start position. 
Then, it is allowed to move up, down, left, or right; each such move takes 1 picosecond. 
The goal is to reach the end position as quickly as possible. 
In this example racetrack, the fastest time is 84 picoseconds.

Because there is only a single path from the start to the end and the programs all go the same speed, the races used to be pretty boring. 
To make things more interesting, they introduced a new rule to the races: programs are allowed to cheat.

The rules for cheating are very strict. Exactly once during a race, a program may disable collision for up to 2 picoseconds. 
This allows the program to pass through walls as if they were regular track. 
At the end of the cheat, the program must be back on normal track again; otherwise, it will receive a segmentation fault and get disqualified.

So, a program could complete the course in 72 picoseconds (saving 12 picoseconds) by cheating for the two moves marked 1 and 2:

###############
#...#...12....#
#.#.#.#.#.###.#
#S#...#.#.#...#
#######.#.#.###
#######.#.#...#
#######.#.###.#
###..E#...#...#
###.#######.###
#...###...#...#
#.#####.#.###.#
#.#...#.#.#...#
#.#.#.#.#.#.###
#...#...#...###
###############
Or, a program could complete the course in 64 picoseconds (saving 20 picoseconds) by cheating for the two moves marked 1 and 2:

###############
#...#...#.....#
#.#.#.#.#.###.#
#S#...#.#.#...#
#######.#.#.###
#######.#.#...#
#######.#.###.#
###..E#...12..#
###.#######.###
#...###...#...#
#.#####.#.###.#
#.#...#.#.#...#
#.#.#.#.#.#.###
#...#...#...###
###############
This cheat saves 38 picoseconds:

###############
#...#...#.....#
#.#.#.#.#.###.#
#S#...#.#.#...#
#######.#.#.###
#######.#.#...#
#######.#.###.#
###..E#...#...#
###.####1##.###
#...###.2.#...#
#.#####.#.###.#
#.#...#.#.#...#
#.#.#.#.#.#.###
#...#...#...###
###############
This cheat saves 64 picoseconds and takes the program directly to the end:

###############
#...#...#.....#
#.#.#.#.#.###.#
#S#...#.#.#...#
#######.#.#.###
#######.#.#...#
#######.#.###.#
###..21...#...#
###.#######.###
#...###...#...#
#.#####.#.###.#
#.#...#.#.#...#
#.#.#.#.#.#.###
#...#...#...###
###############
Each cheat has a distinct start position (the position where the cheat is activated, 
just before the first move that is allowed to go through walls) and end position; 
cheats are uniquely identified by their start position and end position.

In this example, the total number of cheats (grouped by the amount of time they save) are as follows:

There are 14 cheats that save 2 picoseconds.
There are 14 cheats that save 4 picoseconds.
There are 2 cheats that save 6 picoseconds.
There are 4 cheats that save 8 picoseconds.
There are 2 cheats that save 10 picoseconds.
There are 3 cheats that save 12 picoseconds.
There is one cheat that saves 20 picoseconds.
There is one cheat that saves 36 picoseconds.
There is one cheat that saves 38 picoseconds.
There is one cheat that saves 40 picoseconds.
There is one cheat that saves 64 picoseconds.

You aren't sure what the conditions of the racetrack will be like, 
so to give yourself as many options as possible, 
you'll need a list of the best cheats. 

How many cheats would save you at least 100 picoseconds?

Resume:
The map consists of track (.) - including the start (S) and end (E) positions (both of which also count as track) - and walls (#).
When a program runs through the racetrack, it starts at the start position. 
Then, it is allowed to move up, down, left, or right; each such move takes 1 picosecond.
The goal is to reach the end position as quickly as possible.
Is allowed to cheat for up to 2 picoseconds, to pass through walls as if they were regular track.
The program must be back on normal track again; otherwise, it will receive a segmentation fault and get disqualified.
How many cheats would save you at least 100 picoseconds?

Resolution steps:
1. Read the input file and parse the map.
2. Find the path from start to end, and store the time to reach each position.
3. Find the cheats: positions in the path where the program can cheat.
4. Check the time saved by each cheat. Count the cheats that save at least 100 picoseconds.


--- Part Two ---
The programs seem perplexed by your list of cheats. 
Apparently, the two-picosecond cheating rule was deprecated several milliseconds ago! 
The latest version of the cheating rule permits a single cheat that instead lasts at most 20 picoseconds.

Now, in addition to all the cheats that were possible in just two picoseconds, many more cheats are possible. 
This six-picosecond cheat saves 76 picoseconds:

###############
#...#...#.....#
#.#.#.#.#.###.#
#S#...#.#.#...#
#1#####.#.#.###
#2#####.#.#...#
#3#####.#.###.#
#456.E#...#...#
###.#######.###
#...###...#...#
#.#####.#.###.#
#.#...#.#.#...#
#.#.#.#.#.#.###
#...#...#...###
###############
Because this cheat has the same start and end positions as the one above, it's the same cheat, even though the path taken during the cheat is different:

###############
#...#...#.....#
#.#.#.#.#.###.#
#S12..#.#.#...#
###3###.#.#.###
###4###.#.#...#
###5###.#.###.#
###6.E#...#...#
###.#######.###
#...###...#...#
#.#####.#.###.#
#.#...#.#.#...#
#.#.#.#.#.#.###
#...#...#...###
###############
Cheats don't need to use all 20 picoseconds; cheats can last any amount of time up to and including 20 picoseconds 
(but can still only end when the program is on normal track). Any cheat time not used is lost; it can't be saved for another cheat later.

You'll still need a list of the best cheats, but now there are even more to choose between. 
Here are the quantities of cheats in this example that save 50 picoseconds or more:

There are 32 cheats that save 50 picoseconds.
There are 31 cheats that save 52 picoseconds.
There are 29 cheats that save 54 picoseconds.
There are 39 cheats that save 56 picoseconds.
There are 25 cheats that save 58 picoseconds.
There are 23 cheats that save 60 picoseconds.
There are 20 cheats that save 62 picoseconds.
There are 19 cheats that save 64 picoseconds.
There are 12 cheats that save 66 picoseconds.
There are 14 cheats that save 68 picoseconds.
There are 12 cheats that save 70 picoseconds.
There are 22 cheats that save 72 picoseconds.
There are 4 cheats that save 74 picoseconds.
There are 3 cheats that save 76 picoseconds.
Find the best cheats using the updated cheating rules. How many cheats would save you at least 100 picoseconds?

Resume:
The latest version of the cheating rule permits a single cheat that instead lasts at most 20 picoseconds.
Find the best cheats using the updated cheating rules. How many cheats would save you at least 100 picoseconds?

Reasoning:
Refactor the code to extract the logic to find the cheats into a separate method.
Adjust the method to allow for the new cheating rules: instead of 2 moves in one single direction, allow for any moves in any direction.
To do this, we iterate over one diamond shape around the current position, all positions that are at most m picoseconds away. (m = 20 in this case)
For each position, if it is a valid position to cheat to, and if so,
we calculate the actual moves needed to reach it, and the time saved by cheating to that position.
If the time saved is at least the minimum time saved, we count it as a valid cheat.
*/

class Program
{
    static void Main()
    {
        // input example
        string[] input = new string[] {
            "###############",
            "#...#...#.....#",
            "#.#.#.#.#.###.#",
            "#S#...#.#.#...#",
            "#######.#.#.###",
            "#######.#.#...#",
            "#######.#.###.#",
            "###..E#...#...#",
            "###.#######.###",
            "#...###...#...#",
            "#.#####.#.###.#",
            "#.#...#.#.#...#",
            "#.#.#.#.#.#.###",
            "#...#...#...###",
            "###############"
        };
        // minimal time saved to count a cheat, in part 1 and part 2, for the example input
        int minSave1 = 2;
        int minSave2 = 50;

        // input from file. Minimal time saved to count a cheat, in both parts, is 100
        input = File.ReadAllLines("input.txt");
        minSave1 = 100;
        minSave2 = 100;

        // Parse the input, fill the map and find the start of race
        // Get the dimensions of the map
        int yy = input.Length, xx = input[0].Length;
        // Stores for map and start position
        char[,] map = new char[yy, xx];
        int startX = 0, startY = 0;
        // Iterate over the input
        int x, y;
        for (y = 0; y < yy; y++)
            for (x = 0; x < xx; x++)
            {
                // Fill each position in the map
                map[y, x] = input[y][x];
                // If it is the start position mark
                if (map[y, x] == 'S')
                {
                    // Store the start position
                    (startY, startX) = (y, x);
                    //Console.WriteLine("Found the start: " + (y, x));
                }
                //if (map[y, x] == 'E') Console.WriteLine("Found the end: " + (y, x));
            }

        // Find in the map the path from start to end, and the time to reach each position
        // Structures to store the time to reach each position and the path (positions at each time)
        int[,] step = new int[yy, xx];
        List<(int, int)> path = new List<(int, int)>();
        // Directions to move: up, down, right, left
        int[] dx = { 0, 0, 1, -1 };
        int[] dy = { 1, -1, 0, 0 };
        // Start at the start position, with time 1 (to not confuse with 0, which is not visited)
        int now = 1;
        x = startX; y = startY;
        //Console.WriteLine("Finding the path from start to end:");
        // Iterate over the map, starting from the start position
        while (true)
        {
            // new position reached: store the time to reach it and add it to the path
            step[y, x] = now;
            path.Add((y, x));
            //Console.WriteLine($"\t{path.Last()}: {step[y, x] - 1} picoseconds.");
            // If reached the end position, job done
            if (map[y, x] == 'E')
                break;
            // Try to move in any direction
            for (int di = 0; di < 4; di++)
            {
                // new position to try
                int nx = x + dx[di], ny = y + dy[di];
                // if it is a valid position to move to, and it was not visited yet
                if (nx >= 0 && nx < xx && ny >= 0 && ny < yy && map[ny, nx] != '#' && step[ny, nx] == 0)
                {
                    // move to the new position, and do not try to move in other directions
                    x = nx;
                    y = ny;
                    break;
                }
            }
            // increment the time at each new position found
            now++;
        }
        Console.WriteLine($"Time to complete the race with no cheat: {path.Count - 1}");

        // Find the cheats - Part 1
        int cheats = CountCheats(map, step, path, minSave1, 2, 0);
        Console.WriteLine($"Number of cheats that would save you at least {minSave1} picoseconds: {cheats}");
        // Find the cheats - Part 2
        cheats = CountCheats(map, step, path, minSave2, 20, 0);
        Console.WriteLine($"Number of cheats that would save you at least {minSave2} picoseconds: {cheats}");
    }

    /// <summary>
    /// Find the cheats in the path, that save at least minSave picoseconds.
    /// </summary>
    /// <param name="map">The map.</param>
    /// <param name="step">The time to reach each position.</param>
    /// <param name="path">The path from start to end.</param>
    /// <param name="minSave">The minimal time saved to count a cheat.</param>
    /// <param name="moves">The number of moves allowed to cheat.</param>
    /// <param name="debugLevel">The debug level. 0 - no debug, 1 - print the cheats, 2 - print the cheats and the moves.</param>
    /// <returns>The number of cheats that save at least minSave picoseconds.</returns>
    private static int CountCheats(char[,] map, int[,] step, List<(int, int)> path, int minSave, int moves, int debugLevel)
    {
        if (debugLevel > 0) Console.WriteLine("Finding the cheats:");
        // Count the cheats
        int cheats = 0;
        // Get the dimensions of the map
        (int yy, int xx) = (map.GetLength(0), map.GetLength(1));
        // Iterate over the path
        for (int currentStep = 0; currentStep < path.Count; currentStep++)
        {
            // Get the current position in the path
            (int y, int x) = path[currentStep];
            if (debugLevel > 1) Console.WriteLine($"\tAt {(y, x)}, {currentStep} picoseconds.");
            // Iterate over the diamond shape around the current position, all positions that are at most m picoseconds away
            // In axis y, from y - moves to y + moves.
            for (int ny = Math.Max(y - moves, 1); ny <= Math.Min(y + moves, yy - 2); ny++)
            {
                //Depending on the y distance, calculate the x range to form a diamond shape (m moves away at most)
                int deltaY = Math.Abs(ny - y);
                // In axis x, iterate from x - (m - deltaY) to x + (m - deltaY).
                for (int nx = Math.Max(x - (moves - deltaY), 1); nx <= Math.Min(x + (moves - deltaY), xx - 2); nx++)
                {
                    // If the position is a valid position to cheat to
                    if ((nx != x || ny != y) && map[ny, nx] != '#')
                    {
                        // Calculate the actual moves needed to reach the position
                        int actualMoves = Math.Abs(nx - x) + deltaY;
                        // Calculate the time to reach the position
                        int otherStep = step[ny, nx] - 1;
                        // Calculate the time saved by cheating to that position
                        int save = otherStep - currentStep - actualMoves;
                        // If the time saved is at least the minimum time saved, count it as a valid cheat
                        if (save >= minSave)
                        {
                            if (debugLevel >= 2) Console.WriteLine($"\t\tCheat to {(ny, nx)}, {otherStep} picoseconds. Save: {save} picoseconds.");
                            if (debugLevel == 1) Console.WriteLine($"\tAt {(y, x)}, {currentStep} picoseconds, cheat to {(ny, nx)}, {otherStep} picoseconds. Moves: {actualMoves}, save: {save} picoseconds.");
                            cheats++;
                        }
                    }
                }
            }
        }
        // Return the total number of valid cheats
        return cheats;
    }
}
