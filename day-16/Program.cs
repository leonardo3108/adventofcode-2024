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
2. Create a function to find the path from the start to the end.
3. Create a function to find the path with the lowest score.
4. Find the path with the lowest score.
5. Print the lowest score.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;

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
        //Console.WriteLine("Start: " + (startY, startX) + ", End: " + (endY, endX));
        (int [,] tileNeighbors, int [,] tileDirections) = OptimizeGrid(grid);
        //PrintGrid(null, tileNeighbors, tileDirections);
        
        FindPath((startY, startX), (endY, endX), grid, tileNeighbors, tileDirections);

        //return;

        // Store the best score of each tile, and the direction where it comes from.
        Dictionary<(int, int), int> tileScore = new Dictionary<(int, int), int>();
        Dictionary<(int, int), (int, int)> tileDirection = new Dictionary<(int, int), (int, int)>();
        // Initialize the end position with a score of 0 and direction of (0, 0).
        tileScore[(endY, endX)] = 0;
        tileDirection[(endY, endX)] = (0, 0);

        // Find the lowest score of each tile in the grid, recursively, backtracking from the end to the start.
        FindLowestScores(grid, tileScore, tileDirection, (endY, endX));

        // The reindeer starts at the start position with direction to the left.
        (int startDirectionY, int startDirectionX) = (0, -1);
        // If the direction of the start position is different from left, it is necessary to rotate in the beginning.
        if (tileDirection[(startY, startX)].Item2 * startDirectionY + tileDirection[(startY, startX)].Item1 * startDirectionX != 0) 
            // So, add 1000 points to the start position's score, and update the direction to towards left.
            tileScore[(startY, startX)] = tileScore[(startY, startX)] + 1000;
        else if (tileDirection[(startY, startX)] == (-startDirectionY, -startDirectionX))
            // So, add 1000 points to the start position's score, and update the direction to towards left.
            tileScore[(startY, startX)] = tileScore[(startY, startX)] + 2000;

        tileDirection[(startY, startX)] = (startDirectionY, startDirectionX);
        // Print the lowest score of the start position.
        Console.WriteLine("Lowest score: " + tileScore[(startY, startX)]);
    }
    
    static void PrintGrid(char[,] grid, int[,] tileNeighbors, int[,] tileDirections)
    {
        (int y, int x) size = grid != null? (grid.GetLength(0), grid.GetLength(1)): 
                                tileNeighbors != null? (tileNeighbors.GetLength(0), tileNeighbors.GetLength(1)): 
                                                            (tileDirections.GetLength(0), tileDirections.GetLength(1));
        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                if (grid != null)
                    Console.Write(grid[y, x]);
                else if (tileNeighbors != null)
                    Console.Write(tileNeighbors[y, x]);
                else if (tileDirections != null)
                    if (tileDirections[y, x] > 9) 
                        Console.Write((char)('A' + tileDirections[y, x] - 10));
                    else
                        Console.Write(tileDirections[y, x]);
            }
            Console.WriteLine();
        }
    }
    static (int[,], int[,]) OptimizeGrid(char[,] grid)
    {
        int[,] tileNeighbors = new int[grid.GetLength(0), grid.GetLength(1)];
        int[,] tileDirections = new int[grid.GetLength(0), grid.GetLength(1)];

        // Block dead ends and gather neighborwoords information
        bool found = false;
        (int y, int x) size = (grid.GetLength(0) - 1, grid.GetLength(1) - 1);
        do
        {
            found = false;
            for (int y = 1; y < size.y; y++)
            {
                for (int x = 1; x < size.x; x++)
                {
                    if (grid[y, x] != '#')
                    {
                        tileNeighbors[y, x] = (grid[y - 1, x] != '#'? 1: 0) 
                                            + (grid[y + 1, x] != '#'? 1: 0) 
                                            + (grid[y, x - 1] != '#'? 1: 0) 
                                            + (grid[y, x + 1] != '#'? 1: 0);
                        tileDirections[y, x] = (grid[y - 1, x] != '#'? 2: 0) 
                                             + (grid[y + 1, x] != '#'? 8: 0) 
                                             + (grid[y, x - 1] != '#'? 1: 0) 
                                             + (grid[y, x + 1] != '#'? 4: 0);

                        if (tileNeighbors[y, x] <= 1 && grid[y, x] != 'S' && grid[y, x] != 'E')
                        {
                            grid[y, x] = '#';
                            tileNeighbors[y, x] = 0;
                            tileDirections[y, x] = 0;
                            found = true;
                        }
                    }
                }
            }
        } while (found);
        return (tileNeighbors, tileDirections);
    }

    /// <summary>
    /// Find the score of each tile in the grid, recursively, backtracking from the end to the start.
    /// </summary>
    /// <param name="grid">Maze grid</param>
    /// <param name="tileScore">Score of each tile</param>
    /// <param name="tileDirection">Direction at each tile</param>
    /// <param name="actual">Actual tile</param>
    static void FindLowestScores(char[,] grid, Dictionary<(int, int), int> tileScore, Dictionary<(int, int), (int, int)> tileDirection,
                                 (int y, int x) actual)
    {
        //Console.WriteLine("Actual: " + actual + ", Score: " + tileScore[actual] + ", Direction: " + tileDirection[actual]);
        // Check the four directions
        (int dY, int dX)[] directions = new (int, int)[] { (0, 1), (-1, 0), (0, -1), (1, 0) };
        // Check each direction
        foreach ((int dY, int dX) direction in directions)
        {
            if (direction == (-tileDirection[actual].Item1, -tileDirection[actual].Item2))
                continue;
            // Move from the actual tile, towards the direction specified, to get the new tile.
            (int newY, int newX) = (actual.y + direction.dY, actual.x + direction.dX);
            // If the new tile is out of the grid or is a wall, ignore it. Go to the next direction.
            if (newY < 0 || newY >= grid.GetLength(0) || newX < 0 || newX >= grid.GetLength(1) || grid[newY, newX] == '#')
                continue;
            // Calculate the new score. It is the score of the actual tile plus 1.
            int newScore = 1 + tileScore[actual];
            // If the direction of the new tile is different from the actual direction, add 1000 points.
            if (tileDirection[actual].Item2 * direction.dY + tileDirection[actual].Item1 * direction.dX != 0)
                newScore += 1000;
            // If the new tile has a score and it is less or equal to the new score, ignore it. Go to the next direction.
            if (tileScore.ContainsKey((newY, newX)) && tileScore[(newY, newX)] <= newScore)
                continue;
            // No previous score or the new score is better: update the score and direction of the new tile.
            tileScore[(newY, newX)] = newScore;
            tileDirection[(newY, newX)] = direction;
            // Recursively, find the score of all the tiles around the new tile.
            FindLowestScores(grid, tileScore,  tileDirection, (newY, newX));
        }
    }
    static void FindPath((int y, int x) start, (int y, int x) end, char[,] grid, int [,] tileNeighbors, int [,] tileDirections)
    {
        // Find the path from the start to the end.
        (int y, int x) direction = (0, -1);
        (int y, int x) size = (grid.GetLength(0), grid.GetLength(1));
        List<(int y, int x)> vertices = new List<(int y, int x)>();
        Dictionary <(int, int), List<(int, int)>> vertexNeighbors = new Dictionary<(int, int), List<(int, int)>>();
        Dictionary <(int y, int x), (int y, int x)> vertexDirection = new Dictionary<(int, int), (int, int)>();
        Dictionary <(int, int), int> vertexCost = new Dictionary<(int, int), int>();        
        Dictionary <(int y, int x), (int y, int x)> vertexPrevious = new Dictionary<(int, int), (int, int)>();
        vertices.Add(start);
        vertexCost[start] = 0;
        vertexDirection[start] = (0, -1);
        int actualCost;
        int nextVertex = 0;
        Dictionary<int, (int, int)> decodeDirection = new Dictionary<int, (int, int)> { { 1, (0, -1) }, { 2, (-1, 0) }, { 4, (0, 1) }, { 8, (1, 0) } };
        while (nextVertex < vertices.Count)
        {
            (int y, int x) actualVertex = vertices[nextVertex];
            //Console.WriteLine("Exploring vertex: " + actualVertex + ", cost: " + vertexCost[actualVertex] + ", direction: " + vertexDirection[actualVertex]);
            //Console.WriteLine("       Neighbors: " + tileNeighbors[actualVertex.y, actualVertex.x] + ", Directions: " + tileDirections[actualVertex.y, actualVertex.x] + ", Content: " + grid[actualVertex.y, actualVertex.x]);
            // Check the four directions
            for (int directionIndex = 0; directionIndex < 4; directionIndex++)
            {
                actualCost = vertexCost[actualVertex];
                (int y, int x) actualTile = actualVertex;
                int directionCode = tileDirections[actualVertex.y, actualVertex.x] & (1 << directionIndex);
                if (directionCode == 0)
                    continue;
                (int y, int x) actualDirection = decodeDirection[directionCode];
                if (vertexDirection[actualVertex] != actualDirection)
                    actualCost += 1000;
                if (vertexDirection[actualVertex] == (-actualDirection.y, -actualDirection.x))
                    actualCost += 1000;
                // Move from the actual tile, towards the direction specified, to get the new tile.
                actualTile = (actualTile.y + actualDirection.y, actualTile.x + actualDirection.x);
                actualCost++;
                //Console.WriteLine("Tile: " + actualTile + " Cost: " + actualCost + ", Neighbors: " + tileNeighbors[actualTile.y, actualTile.x] + ", Directions: " + tileDirections[actualTile.y, actualTile.x]);
                (int y, int x) tileBefore = actualVertex;
                (int y, int x) directionBefore = actualDirection;
                while (tileNeighbors[actualTile.y, actualTile.x] == 2 && actualTile != end)
                {
                    for (int directionIndex2 = 0; directionIndex2 < 4; directionIndex2++)
                    {
                        directionCode = tileDirections[actualTile.y, actualTile.x] & (1 << directionIndex2);
                        if (directionCode == 0)
                            continue;
                        actualDirection = decodeDirection[directionCode];
                        // Move from the actual tile, towards the direction specified, to get the new tile.
                        if ((actualTile.y + actualDirection.y, actualTile.x + actualDirection.x) != tileBefore)
                        {
                            actualCost++;
                            if (actualDirection != directionBefore)
                                actualCost += 1000;
                            tileBefore = actualTile;
                            actualTile = (actualTile.y + actualDirection.y, actualTile.x + actualDirection.x);
                            directionBefore = actualDirection;
                            break;
                        }
                    }
                    //Console.WriteLine("Tile: " + actualTile + " Cost: " + actualCost + ", Neighbors: " + tileNeighbors[actualTile.y, actualTile.x] + ", Directions: " + tileDirections[actualTile.y, actualTile.x]);
                }
                if (vertices.Contains(actualTile))
                {
                    if (actualCost < vertexCost[actualTile])
                    {
                        vertexCost[actualTile] = actualCost;
                        vertexDirection[actualTile] = actualDirection;
                        vertexPrevious[actualTile] = actualVertex;
                        //Console.WriteLine("      Old vertex: " + actualTile + ", cost: " + vertexCost[actualTile] + ", direction: " + vertexDirection[actualTile]);
                    }
                    else {
                        //Console.WriteLine("     Same vertex: " + actualTile + ", cost: " + vertexCost[actualTile] + ", direction: " + vertexDirection[actualTile]);
                    }
                }
                else
                {
                    vertices.Add(actualTile);
                    vertexCost[actualTile] = actualCost;
                    vertexDirection[actualTile] = actualDirection;
                    vertexPrevious[actualTile] = actualVertex;
                    //Console.WriteLine("      New vertex: " + actualTile + ", cost: " + vertexCost[actualTile] + ", direction: " + vertexDirection[actualTile]);
                }
                if (!vertexNeighbors.ContainsKey(actualVertex))
                    vertexNeighbors[actualVertex] = new List<(int, int)>();
                if (!vertexNeighbors[actualVertex].Contains(actualTile))
                    vertexNeighbors[actualVertex].Add(actualTile);
                if (!vertexNeighbors.ContainsKey(actualTile))
                    vertexNeighbors[actualTile] = new List<(int, int)>();
                if (!vertexNeighbors[actualTile].Contains(actualVertex))
                    vertexNeighbors[actualTile].Add(actualVertex);                    
            }
            nextVertex++;
        }
        Console.WriteLine("         The end: " + end + ", cost: " + vertexCost[end] + ", direction: " + vertexDirection[end]);
        return;
        Console.WriteLine("Graph:");
        for (int i = 0; i < vertices.Count; i++)
            Console.WriteLine("\t" + vertices[i] + ": " + string.Join(", ", vertexNeighbors[vertices[i]]));

        Dictionary <(int y, int x), (int y, int x)> vertexNext = new Dictionary<(int, int), (int, int)>();
        (int y, int x) vertex = end;
        Console.Write("Path (E) ");
        while (vertex != start)
        {
            Console.Write(vertex + " <- ");
            vertexNext[vertexPrevious[vertex]] = vertex;
            vertex = vertexPrevious[vertex];
        }
        Console.WriteLine(vertex + " (S)");
        vertex = start;
        Console.Write("Path (S) ");
        while (vertex != end)
        {
            Console.Write(vertex + " -> ");
            vertex = vertexNext[vertex];
        }
        Console.WriteLine(vertex + " (E)");
    }
}


