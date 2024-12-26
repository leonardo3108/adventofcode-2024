/*  Problem Description:

--- Day 16: Reindeer Maze ---
It's time again for the Reindeer Olympics! This year, the big event is the Reindeer Maze, where the Reindeer compete for the lowest score.

You and The Historians arrive to search for the Chief right as the event is about to start. It wouldn't hurt to watch a little, right?

The Reindeer start on the Start Tile (marked S) facing East and need to reach the End Tile (marked E). They can move forward one tile at a time (increasing their score by 1 point), but never into a wall (#). They can also rotate clockwise or counterclockwise 90 degrees at a time (increasing their score by 1000 points).

To figure out the best place to sit, you start by grabbing a map (your puzzle input) from a nearby kiosk. For example:

###############
#.......#....E#
#.#.###.#.###.#
#.....#.#...#.#
#.###.#####.#.#
#.#.#.......#.#
#.#.#####.###.#
#...........#.#
###.#.#####.#.#
#...#.....#.#.#
#.#.#.###.#.#.#
#.....#...#.#.#
#.###.#.#.#.#.#
#S..#.....#...#
###############
There are many paths through this maze, but taking any of the best paths would incur a score of only 7036. This can be achieved by taking a total of 36 steps forward and turning 90 degrees a total of 7 times:


###############
#.......#....E#
#.#.###.#.###^#
#.....#.#...#^#
#.###.#####.#^#
#.#.#.......#^#
#.#.#####.###^#
#..>>>>>>>>v#^#
###^#.#####v#^#
#>>^#.....#v#^#
#^#.#.###.#v#^#
#^....#...#v#^#
#^###.#.#.#v#^#
#S..#.....#>>^#
###############
Here's a second example:

#################
#...#...#...#..E#
#.#.#.#.#.#.#.#.#
#.#.#.#...#...#.#
#.#.#.#.###.#.#.#
#...#.#.#.....#.#
#.#.#.#.#.#####.#
#.#...#.#.#.....#
#.#.#####.#.###.#
#.#.#.......#...#
#.#.###.#####.###
#.#.#...#.....#.#
#.#.#.#####.###.#
#.#.#.........#.#
#.#.#.#########.#
#S#.............#
#################
In this maze, the best paths cost 11048 points; following one such path would look like this:

#################
#...#...#...#..E#
#.#.#.#.#.#.#.#^#
#.#.#.#...#...#^#
#.#.#.#.###.#.#^#
#>>v#.#.#.....#^#
#^#v#.#.#.#####^#
#^#v..#.#.#>>>>^#
#^#v#####.#^###.#
#^#v#..>>>>^#...#
#^#v###^#####.###
#^#v#>>^#.....#.#
#^#v#^#####.###.#
#^#v#^........#.#
#^#v#^#########.#
#S#>>^..........#
#################
Note that the path shown above includes one 90 degree turn as the very first move, rotating the Reindeer from facing East to facing North.

Analyze your map carefully. What is the lowest score a Reindeer could possibly get?

Resume and resolution:
The problem is a maze problem where we need to find a path from the start to the end.
The maze is represented as a grid where each cell can be either a wall or a path.
The reindeer can move in four directions, up, down, left, and right, and can rotate 90 degrees clockwise or counterclockwise.
One step forward costs 1 point, and a rotation costs 1000 points. 
Find the path with the lowest score.

Resolution steps:
1. Read the input grid.
2. Create a list of positions to visit, with the score, and the direction of the movement.
3. Create a dictionary of visited positions, with the score, and the direction of the movement.
4. Start from the start position, with a score of 0, and direction to the left.
5. Loop while there are positions to visit.
6. Get the first position to visit, with the lowest score.
7. Find the neighbors of the current position, starting from the same direction, and turning clockwise and counterclockwise.
8. If it finds the end position, print the score and break.

--- Part Two ---
Now that you know what the best paths look like, you can figure out the best spot to sit.

Every non-wall tile (S, ., or E) is equipped with places to sit along the edges of the tile. 
While determining which of these tiles would be the best spot to sit depends on a whole bunch of factors 
(how comfortable the seats are, how far away the bathrooms are, whether there's a pillar blocking your view, etc.), 
the most important factor is whether the tile is on one of the best paths through the maze. 
If you sit somewhere else, you'd miss all the action!

So, you'll need to determine which tiles are part of any best path through the maze, including the S and E tiles.

In the first example, there are 45 tiles (marked O) that are part of at least one of the various best paths through the maze:

###############
#.......#....O#
#.#.###.#.###O#
#.....#.#...#O#
#.###.#####.#O#
#.#.#.......#O#
#.#.#####.###O#
#..OOOOOOOOO#O#
###O#O#####O#O#
#OOO#O....#O#O#
#O#O#O###.#O#O#
#OOOOO#...#O#O#
#O###.#.#.#O#O#
#O..#.....#OOO#
###############
In the second example, there are 64 tiles that are part of at least one of the best paths:

#################
#...#...#...#..O#
#.#.#.#.#.#.#.#O#
#.#.#.#...#...#O#
#.#.#.#.###.#.#O#
#OOO#.#.#.....#O#
#O#O#.#.#.#####O#
#O#O..#.#.#OOOOO#
#O#O#####.#O###O#
#O#O#..OOOOO#OOO#
#O#O###O#####O###
#O#O#OOO#..OOO#.#
#O#O#O#####O###.#
#O#O#OOOOOOO..#.#
#O#O#O#########.#
#O#OOO..........#
#################
Analyze your map further. How many tiles are part of at least one of the best paths through the maze?
*/

using System.ComponentModel;

class Program
{
    static void Main()
    {
        string[] lines = new string[]
        {
            "###############",
            "#.......#....E#",
            "#.#.###.#.###.#",
            "#.....#.#...#.#",
            "#.###.#####.#.#",
            "#.#.#.......#.#",
            "#.#.#####.###.#",
            "#...........#.#",
            "###.#.#####.#.#",
            "#...#.....#.#.#",
            "#.#.#.###.#.#.#",
            "#.....#...#.#.#",
            "#.###.#.#.#.#.#",
            "#S..#.....#...#",
            "###############"
        };

        // Read the second example
        lines = new string[]
        {
            "#################",
            "#...#...#...#..E#",
            "#.#.#.#.#.#.#.#.#",
            "#.#.#.#...#...#.#",
            "#.#.#.#.###.#.#.#",
            "#...#.#.#.....#.#",
            "#.#.#.#.#.#####.#",
            "#.#...#.#.#.....#",
            "#.#.#####.#.###.#",
            "#.#.#.......#...#",
            "#.#.###.#####.###",
            "#.#.#...#.....#.#",
            "#.#.#.#####.###.#",
            "#.#.#.........#.#",
            "#.#.#.#########.#",
            "#S#.............#",
            "#################"
        };

        // Read the input grid
        lines = File.ReadAllLines("input.txt");

        // Create a grid from the input lines, and the start and end positions. Initialize them to 0.
        char[,] grid = new char[lines.Length, lines[0].Length];
        (int startY, int startX, int endY, int endX) = (0, 0, 0, 0);
        // Fill the grid from the input lines.
        int lineIndex = 0;
        foreach (string line in lines)
        {
            int charIndex = 0;
            foreach (var c in line)
            {
                grid[lineIndex, charIndex] = c;
                // If the character is S, it is the start position.
                if (c == 'S')
                    (startY, startX) = (lineIndex, charIndex);
                // If the character is E, it is the end position.
                else if (c == 'E')
                    (endY, endX) = (lineIndex, charIndex);
                charIndex++;
            }
            lineIndex++;
        }

        // List of positions to visit, with the score, and the direction of the movement.
        List<(int score, int y, int x, int dy, int dx)> toVisit = new List<(int, int, int, int, int)>();
        // Dictionary of visited positions, with the score, and the direction of the movement.
        Dictionary<(int, int), (int score, int dy, int dx)> visited = new Dictionary<(int, int), (int, int, int)>();
        // The start position is the first position to visit, with a score of 0, and direction to the left.
        toVisit.Add((0, startY, startX, 0, -1));
        visited[(startY, startX)] = (0, 0, -1);
        int score; int y; int x; int dy; int dx;

        // Loop while there are positions to visit
        while (toVisit.Any())
        {
            // sort the scores by the score value
            toVisit.Sort((a, b) => a.score.CompareTo(b.score));
            // get the first score
            (score, y, x, dy, dx) = toVisit[0];
            //Console.WriteLine("To visit: " + toVisit.Count + ". Next: " + (y, x) + " - score " + score + " - direction " + GetDirectionSymbol(dy, dx));
            // remove the first score
            toVisit.RemoveAt(0);
            // if the current position already has a lower score, nothing to do
            if (visited[(y, x)].score < score)
                continue;
            // if the current position is the end position, print the score and break
            if (y == endY && x == endX)
            {
                Console.WriteLine("Lowest score: " + score);
                break;
            }
            // find the neighbors of the current position, starting from the same direction
            EvaluateNeighbor(grid, toVisit, visited, score + 1, dy, dx, y + dy, x + dx, false);
            // find the neighbor turning clockwise
            EvaluateNeighbor(grid, toVisit, visited, score + 1001, dx, -dy, y + dx, x - dy, false);
            // find the neighbor turning counterclockwise
            EvaluateNeighbor(grid, toVisit, visited, score + 1001, -dx, dy, y - dx, x + dy, false);
            // find the neighbor turning back, only for the start position
            if (x == startX && y == startY)
                EvaluateNeighbor(grid, toVisit, visited, score + 2001, -dy, -dx, y - dy, x - dx, false);
        }

        int debugLevel = 0;

        if (debugLevel > 0) DisplayVisitedCells(grid, visited);

        // Part two
        // Create a list of tiles from actual path
        HashSet<(int, int)> actualPath = new HashSet<(int, int)>();
        // Create a set of tiles that are part of the best path
        HashSet<(int, int)> bestPaths = new HashSet<(int, int)>();

        ExploreGridPosition(startY, startX, 0, -1, 0, grid, startY, startX, endY, endX, visited, actualPath, bestPaths, ref debugLevel);


        if (debugLevel > 0) 
        {
            //sort the best paths
            bestPaths = new HashSet<(int, int)>(bestPaths.OrderBy(p => p.Item1).ThenBy(p => p.Item2));
            Console.WriteLine("Best paths: " + string.Join(", ", bestPaths.Select(p => p.ToString())) + "\n");
        }

        Console.WriteLine("Tiles part of the best path: " + bestPaths.Count);

    }

    private static bool ExploreGridPosition(int y, int x, int dy, int dx, int score, 
                                            char[,] grid, int startY, int startX, int endY, int endX, 
                                            Dictionary<(int, int), (int score, int dy, int dx)> visited, 
                                            HashSet<(int, int)> actualPath, 
                                            HashSet<(int, int)> bestPaths,
                                            ref int debugLevel)
    {
        // add the current position to the actual path
        actualPath.Add((y, x));
        if (debugLevel > 1) 
        {
            Console.Write("Explore: " + (y, x) + " - score " + score + " - direction " + GetDirectionSymbol(dy, dx));
            if (visited.ContainsKey((y, x)))
                Console.WriteLine(" - best score " + visited[(y, x)].score + " - best direction " + GetDirectionSymbol(visited[(y, x)].dy, visited[(y, x)].dx));
            else
                Console.WriteLine(" - not visited before");
        }
        // if the current position is the end position, add it to the best path
        if (y == endY && x == endX)
        {
            // for debugging purposes, print the actual path, and exit
            if (debugLevel > 0) Console.WriteLine("Best path: " + string.Join(" > ", actualPath.Select(p => p.ToString())) + "\n");
            // add the actual path to the best paths
            bestPaths.UnionWith(actualPath);
            return true;
        }
        // we need to check if that is a valid position to visit
        int pathCount = 0;
        // find the neighbors of the current position, starting from the same direction
        pathCount += EvaluateNeighborInDepth(grid, visited, score + 1, dy, dx, y + dy, x + dx, 
                                             startY, startX, endY, endX, actualPath, bestPaths, ref debugLevel) ? 1 : 0;
        // find the neighbor turning clockwise
        pathCount += EvaluateNeighborInDepth(grid, visited, score + 1001, dx, -dy, y + dx, x - dy, 
                                             startY, startX, endY, endX, actualPath, bestPaths, ref debugLevel) ? 1 : 0;
        // find the neighbor turning counterclockwise
        pathCount += EvaluateNeighborInDepth(grid, visited, score + 1001, -dx, dy, y - dx, x + dy, 
                                             startY, startX, endY, endX, actualPath, bestPaths, ref debugLevel) ? 1 : 0;
        // find the neighbor turning back, only for the start position
        if (x == startX && y == startY)
            pathCount += EvaluateNeighborInDepth(grid, visited, score + 2001, -dy, -dx, y - dy, x - dx, 
                                                 startY, startX, endY, endX, actualPath, bestPaths, ref debugLevel) ? 1 : 0;
        actualPath.Remove((y, x));
        return pathCount > 0;
    }

    private static void DisplayVisitedCells(char[,] grid, Dictionary<(int, int), (int s, int dy, int dx)> visited)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (visited.ContainsKey((i, j)))
                    Console.Write(visited[(i, j)].s.ToString().PadLeft(6));
                else
                    Console.Write("      ");
            }
            Console.WriteLine();
        }
    }
    
    /// <summary>
    /// Get the symbol of the direction. For debugging purposes.
    /// </summary>
    /// <param name="dy">Movement in the y-axis</param>
    /// <param name="dx">Movement in the x-axis</param>
    /// <returns>Symbol of the direction: ^ (up), v (down), > (right), < (left)</returns>
    private static char GetDirectionSymbol(int dy, int dx)
    {
        return dy == 0 ? (dx == 1 ? '>' : '<') : (dy == 1 ? 'v' : '^');
    }
    
    /// <summary>
    /// Evaluate a neighbor position. 
    /// If is a valid position to visit or revisit, add it to the list of positions to visit, and update the score and direction.
    /// </summary>
    /// <param name="grid">Grid of the maze</param>
    /// <param name="toVisit">List of positions to visit</param>
    /// <param name="visited">Dictionary of visited positions</param>
    /// <param name="score">Current score</param>
    /// <param name="dy">Current movement in the y-axis</param>
    /// <param name="dx">Current movement in the x-axis</param>
    /// <param name="ny">Neighbor position in the y-axis</param>
    /// <param name="nx">Neighbor position in the x-axis</param>
    private static void EvaluateNeighbor(char[,] grid, 
                                         List<(int s, int y, int x, int dy, int dx)> toVisit, 
                                         Dictionary<(int, int), (int s, int dy, int dx)> visited, 
                                         int score, int dy, int dx, int ny, int nx, bool debug = false)
    {
        if (debug) Console.Write("\tNeighbor: " + (ny, nx) + " - score " + score + " - direction " + GetDirectionSymbol(dy, dx));
        //To visit: " + toVisit.Count + ". Next: " + (y, x) + " - score " + score + " - direction " + d);
        // if the neighbor is a valid position and not a wall
        if (ny >= 0 && ny < grid.GetLength(0) && nx >= 0 && nx < grid.GetLength(1) && grid[ny, nx] != '#')
        {
            // If the neighbor is already visited and has a lower score, nothing to do
            if (visited.ContainsKey((ny, nx)) && visited[(ny, nx)].s <= score)
            {
                if (debug) Console.WriteLine(" - already visited");
                return;
            }
            // set (update ou include) the score and direction for the neighbor
            visited[(ny, nx)] = (score, dy, dx);
            // add the neighbor to the list of positions to visit (first time, or again, with a better score)
            toVisit.Add((score, ny, nx, dy, dx));
            if (debug) Console.WriteLine(" - to visit");
        }
        else
            if (debug) Console.WriteLine(" - wall");
    }

    private static bool EvaluateNeighborInDepth(char[,] grid, 
                                                Dictionary<(int, int), (int score, int dy, int dx)> visited, 
                                                int score, int dy, int dx, int ny, int nx, int startY, int startX, int endY, int endX, 
                                                HashSet<(int, int)> actualPath, 
                                                HashSet<(int, int)> bestPaths, 
                                                ref int debugLevel)
    {
        if (debugLevel > 2) Console.Write("\t" + (ny - dy, nx - dx) + " > " + (ny, nx) + " - score " + score + " - direction " + GetDirectionSymbol(dy, dx));
        //To visit: " + toVisit.Count + ". Next: " + (y, x) + " - score " + score + " - direction " + d);
        // if the neighbor is a valid position and not a wall
        if (ny >= 0 && ny < grid.GetLength(0) && nx >= 0 && nx < grid.GetLength(1) && grid[ny, nx] != '#')
        {
            // If the neighbor is already visited
            if (visited.ContainsKey((ny, nx)))
            {
                //  and has a lower score at same direction, nothing to do
                if (visited[(ny, nx)].score < score && visited[(ny, nx)].dy == dy && visited[(ny, nx)].dx == dx)
                {
                    if (debugLevel > 2) Console.WriteLine(" - neighbor has a lower score (same direction)");
                    return false;
                }
                //  and has a very lower score regardless of the direction, nothing to do either
                else if (visited[(ny, nx)].score < score - 1000)
                {
                    if (debugLevel > 2) Console.WriteLine(" - neighbor has a much lower score");
                    return false;
                }
                // and has a lower score at end position
                else if (visited[(ny, nx)].score < score && ny == endY && nx == endX)
                {
                    if (debugLevel > 2) Console.WriteLine(" - neighbor has a lower score (end tile)");
                    return false;
                }
            }
            if (debugLevel > 2) Console.WriteLine(" - to visit");
            // visit the neighbor
            //toVisit.Push((score, ny, nx, dy, dx));
            return ExploreGridPosition(ny, nx, dy, dx, score, grid, startY, startX, endY, endX, visited, actualPath, bestPaths, ref debugLevel);
        }
        else
        {
            if (debugLevel > 2) Console.WriteLine(" - wall");
            return false;
        }
    }
}


